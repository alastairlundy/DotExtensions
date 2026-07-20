using System.Collections.Generic;
using System.IO;
using System.Linq;

using BenchmarkDotNet.Configs;
using DotExtensions.Benchmarking;
using System.Threading;
using DotExtensions.IO.Directories;
using DotExtensions.IO.Files;

namespace DotExtensions.Benchmarking.Benchmarks.IO;

[Config(typeof(FastBenchConfig))]
[MemoryDiagnoser]
public class SafeFileEnumerationBenchmarks
{
    // The seeded %TEMP% subtree replaces the previous disk-root enumeration,
    // which was both machine-dependent (results varied by disk layout) and
    // extremely slow (millions of system files). 100 files in the root plus
    // 10 subdirectories are enough to detect allocation and iteration
    // regressions while keeping the benchmark runnable in seconds.
    private const int SeededFileCount = 100;
    private const int SeededSubdirectoryCount = 10;
    private const string SeededRootName = "DotExtensionsBenchmarks";
    private const string SeededSubdirName = "SafeEnum";

    // Fixed seed so the seeded structure is identical across runs and
    // machines, making the benchmark results replicable.
    private const int RandomSeed = 0x5EED_C0DE;

    private DirectoryInfo _directoryInfo = null!;
    private DirectoryInfo _seededRoot = null!;

    public SafeFileEnumerationBenchmarks()
    {
        string subtreePath = Path.Combine(Path.GetTempPath(), SubtreeRootName);
        _directoryInfo = new DirectoryInfo(subtreePath);
    }

    [GlobalSetup]
    public void Setup()
    {
        string seededRootPath = Path.Combine(Path.GetTempPath(), SeededRootName);
        _seededRoot = new DirectoryInfo(seededRootPath);
        // Best-effort cleanup of a previous run that may have been interrupted
        // before [GlobalCleanup] could run, so a stale tree does not inflate
        // the file count on subsequent runs.
        if (_seededRoot.Exists)
        {
            _seededRoot.Delete(recursive: true);
        }

        string benchmarkDirPath = Path.Combine(seededRootPath, SeededSubdirName);
        Directory.CreateDirectory(benchmarkDirPath);

        Random random = new(RandomSeed);

        for (int i = 0; i < SeededFileCount; i++)
        {
            string filePath = Path.Combine(benchmarkDirPath, $"file_{i:000}.txt");
            byte[] payload = new byte[16];
            random.NextBytes(payload);
            File.WriteAllBytes(filePath, payload);
        }

        for (int i = 0; i < SeededSubdirectoryCount; i++)
        {
            string subdirPath = Path.Combine(benchmarkDirPath, $"subdir_{i:000}");
            Directory.CreateDirectory(subdirPath);

            for (int j = 0; j < SeededFileCount / SeededSubdirectoryCount; j++)
            {
                string nestedFilePath = Path.Combine(subdirPath, $"nested_{i:000}_{j:000}.txt");
                byte[] payload = new byte[8];
                random.NextBytes(payload);
                File.WriteAllBytes(nestedFilePath, payload);
            }
        }

        _directoryInfo = new DirectoryInfo(benchmarkDirPath);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        if (_seededRoot is null)
        {
            return;
        }

        // Use a fresh DirectoryInfo so we observe the live filesystem state
        // (DirectoryInfo.Exists is cached on the original instance).
        DirectoryInfo seededRoot = new(_seededRoot.FullName);
        if (!seededRoot.Exists)
        {
            return;
        }

        // File system delete is occasionally observed to fail on Windows when
        // the operating system still holds directory handles from a recent
        // enumeration; retry briefly before giving up so [GlobalCleanup]
        // reliably tears the seeded tree down.
        const int maxAttempts = 10;
        Exception? lastException = null;
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                seededRoot.Delete(recursive: true);
                seededRoot.Refresh();
                if (!seededRoot.Exists)
                {
                    return;
                }
            }
            catch (Exception ex) when (attempt < maxAttempts)
            {
                lastException = ex;
                Thread.Sleep(100);
            }
        }

        // If the seeded root still exists after the retries, log the last
        // exception so a flapping teardown is visible in benchmark output.
        if (seededRoot.Exists && lastException is not null)
        {
            Console.Error.WriteLine(
                $"SafeFileEnumerationBenchmarks.Cleanup could not delete '{seededRoot.FullName}' after {maxAttempts} attempts: {lastException.GetType().Name}: {lastException.Message}");
        }
    }

    [Benchmark]
    public FileInfo DotExtensions_SafeFileEnumeration()
    {
        IEnumerable<FileInfo> files = _directoryInfo.SafelyEnumerateFiles();

        return files.First();
    }

    [Benchmark(Baseline = true)]
    public FileInfo Normal_FileEnumeration()
    {
        IEnumerable<FileInfo> files = _directoryInfo.EnumerateFiles();

        return files.First();
    }

    private static void EnsureSeededSubtree(DirectoryInfo root)
    {
        // Cheap sentinel: if a known leaf file is present we assume the
        // seeded subtree is already populated and skip the work.
        string marker = Path.Combine(root.FullName, "sub_000", "file_000_000.txt");
        if (File.Exists(marker))
        {
            return;
        }

        if (!root.Exists)
        {
            root.Create();
        }

        for (int i = 0; i < SubdirectoryCount; i++)
        {
            DirectoryInfo sub = root.CreateSubdirectory($"sub_{i:D3}");
            for (int j = 0; j < FilesPerSubdirectory; j++)
            {
                string filePath = Path.Combine(sub.FullName, $"file_{i:D3}_{j:D3}.txt");
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, $"seed={i:D3}-{j:D3}");
                }
            }
        }
    }
}