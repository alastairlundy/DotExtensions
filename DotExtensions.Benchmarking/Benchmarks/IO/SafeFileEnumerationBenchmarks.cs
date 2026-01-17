using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Mathematics;
using DotExtensions.IO.Directories;

namespace DotExtensions.Benchmarking.Benchmarks.IO;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net10_0)]
[RPlotExporter]
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
        string directoryPath = (Path.GetPathRoot(Directory.GetCurrentDirectory()) ?? Path.GetPathRoot(Path.GetRandomFileName())) 
                               ?? throw new ArgumentException($"{nameof(directoryPath)} must be a valid path. Path was null or invalid.");
      
        _directoryInfo = new  DirectoryInfo(directoryPath);
    }

    [Benchmark]
    public FileInfo DotExtensions_SafeFileEnumeration()
    {
        IEnumerable<FileInfo> files = _directoryInfo.SafelyEnumerateFiles();

        return files.First();
    }
    
    [Benchmark]
    public FileInfo DotExtensions_SafeFileEnumeration_NetStandard20_Fallback()
    {
        IEnumerable<FileInfo> files = _directoryInfo
            .SafeFileEnumeration_NetStandard20("*", SearchOption.TopDirectoryOnly, true);

        return files.First();
    }

    [Benchmark(Baseline = true)]
    public FileInfo Normal_FileEnumeration()
    {
        IEnumerable<FileInfo> files = _directoryInfo.EnumerateFiles();
        
        return files.First();
    }
}