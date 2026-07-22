using System.Collections.Generic;
using System.IO;
using BenchmarkDotNet.Configs;
using DotExtensions.Benchmarking;
using DotExtensions.IO;

namespace DotExtensions.Benchmarking.Benchmarks.IO;

[Config(typeof(FastBenchConfig))]
[MemoryDiagnoser]
[BenchmarkCategory("Short")]
public class RandomFileRetrievalBenchmark
{
    [Params(10, 100)]
    public int N;
    
    [Benchmark]
    public IList<string> Path_GetRandomFileName()
    {
        List<string> output = new(capacity: N);
        for (int i = 0; i < N; i++)
        {
            output.Add(Path.GetRandomFileName());
        }
        
        return output;
    }
    
    [Benchmark]
    public IList<FileInfo> DotExtensions_GetRandomFile()
    {
        List<FileInfo> output = new(capacity: N);
        for (int i = 0; i < N; i++)
        {
            output.Add(FileInfo.GetRandomFile());
        }
        return output;
    }
}