using System.Collections.Generic;
using System.IO;
using System.Linq;
using DotExtensions.IO.Directories;
using DotExtensions.IO.Files;

namespace DotExtensions.Benchmarking.Benchmarks.IO;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net10_0)]
[MemoryDiagnoser]
public class SafeFileEnumerationBenchmarks
{
    private DirectoryInfo _directoryInfo;

    public SafeFileEnumerationBenchmarks()
    {
        _directoryInfo = new(Path.GetTempPath());
    }
    
    [GlobalSetup]
    public void Setup()
    {
        string directoryPath = Path.GetNonNullPathRoot(Directory.GetCurrentDirectory());
      
        _directoryInfo = new  DirectoryInfo(directoryPath);
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
}