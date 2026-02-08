using System.Collections.Generic;
using System.IO;
using DotExtensions.IO;

namespace DotExtensions.Benchmarking.Benchmarks.IO;

/*[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]*/
[SimpleJob(RuntimeMoniker.Net10_0)]
[MemoryDiagnoser]
public class RandomFileRetrievalBenchmark
{
    [Params(
        1
        //,10
    )]
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