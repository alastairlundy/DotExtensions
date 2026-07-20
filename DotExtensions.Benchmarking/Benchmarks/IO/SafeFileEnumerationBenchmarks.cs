using System.Collections.Generic;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Configs;
using DotExtensions.Benchmarking;
using DotExtensions.IO.Directories;
using DotExtensions.IO.Files;

namespace DotExtensions.Benchmarking.Benchmarks.IO;

[Config(typeof(FastBenchConfig))]
[MemoryDiagnoser]
public class SafeFileEnumerationBenchmarks
{
    // Pinned, seeded %TEMP% subtree so the benchmark enumerates a fixed
    // dataset (10 subdirectories, 100 files) instead of the disk root.
    // Subtree and file names are deterministic so successive runs are
    // reproducible.
    private const string SubtreeRootName = "DotExtensions.Benchmarking.SafeFileEnumeration";
    private const int SubdirectoryCount = 10;
    private const int FilesPerSubdirectory = 10;
    private const int TotalFileCount = SubdirectoryCount * FilesPerSubdirectory;

    private DirectoryInfo _directoryInfo;

    public SafeFileEnumerationBenchmarks()
    {
        string subtreePath = Path.Combine(Path.GetTempPath(), SubtreeRootName);
        _directoryInfo = new DirectoryInfo(subtreePath);
    }

    [GlobalSetup]
    public void Setup()
    {
        EnsureSeededSubtree(_directoryInfo);
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