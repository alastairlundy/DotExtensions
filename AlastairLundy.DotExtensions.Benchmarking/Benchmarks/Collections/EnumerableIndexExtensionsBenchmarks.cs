using System;
using System.Collections.Generic;
using System.Linq;

using AlastairLundy.DotExtensions.Benchmarking.Infra.FakeData;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AlastairLundy.DotExtensions.Benchmarking.Benchmarks.Collections;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[RPlotExporter]
[MemoryDiagnoser(true)]
public class EnumerableIndexExtensionsBenchmarks
{
    private int[] indexes;

    private List<string> fakeData;
    
    [GlobalSetup]
    public void Setup()
    {
        indexes = new int[N];
        
        FakeEnumerables fakeEnumerables = new FakeEnumerables();
        fakeData = fakeEnumerables.Create(N).ToList();

        for (var i = 0; i < indexes.Length; i++)
        {
            indexes[i] = Random.Shared.Next(0, N);
        }
    }

    [Params(10_000, 100_000)]
    public int N;
 
       
    [Benchmark]
    public void DotExtensions_Enumerables_IndexOf()
    {
        int index = DotExtensions.Collections.Generic.
            Enumerables.EnumerableIndexExtensions.IndexOf(fakeData,
                fakeData[indexes[Random.Shared.Next(0, N)]]);

        //fakeData[index];
    }

    [Benchmark]
    public void Linq_List_IndexOf()
    {
        int index = fakeData.IndexOf(fakeData[indexes[Random.Shared.Next(0, N)]]);
    }
}