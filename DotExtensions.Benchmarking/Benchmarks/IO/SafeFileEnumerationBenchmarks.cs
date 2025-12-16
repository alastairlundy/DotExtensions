using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AlastairLundy.DotExtensions.IO.Directories;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace DotExtensions.Benchmarking.Benchmarks.IO;

[SimpleJob(RuntimeMoniker.NativeAot80)]
[SimpleJob(RuntimeMoniker.NativeAot90)]
[SimpleJob(RuntimeMoniker.NativeAot10_0)]
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
    public FileInfo DotExtensions_SafeFileGetting()
    {
        FileInfo[] files = _directoryInfo.SafelyGetFiles();
        
        return files.First();
    }

    [Benchmark]
    public FileInfo Normal_FileEnumeration()
    {
        IEnumerable<FileInfo> files = _directoryInfo.EnumerateFiles();
        
        return files.First();
    }
    
    [Benchmark]
    public FileInfo Normal_FileSaving()
    {
        FileInfo[] files = _directoryInfo.GetFiles();
        
        return files.First();
    }
}