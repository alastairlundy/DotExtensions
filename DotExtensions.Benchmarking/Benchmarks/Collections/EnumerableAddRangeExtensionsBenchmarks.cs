using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AlastairLundy.DotExtensions.Collections.Generic.Enumerables;
using AlastairLundy.DotExtensions.Collections.Generic.ICollections;
using AlastairLundy.DotExtensions.Collections.ILists;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using DotExtensions.Benchmarking.Infra.FakeData;

namespace DotExtensions.Benchmarking.Benchmarks.Collections;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser(true)]
[CsvMeasurementsExporter]
public class EnumerableAddRangeExtensionsBenchmarks
{
    private List<string> fakeData;

    private List<string> fakeData2List;
    private IList<string> fakeData2IList;
    private IEnumerable<string> fakeData2Enumerable;
    
    [GlobalSetup]
    public void Setup()
    {
        FakeEnumerables fakeEnumerables = new FakeEnumerables();
        fakeData = fakeEnumerables.Create(N).ToList();
        
        fakeData2Enumerable = fakeEnumerables.Create(N / 10);
        fakeData2IList = fakeEnumerables.Create(N / 10).ToList();
        fakeData2List = fakeEnumerables.Create(N / 10).ToList();
    }

    [Params(100_000)]
    public int N;
    
    [Benchmark]
    public void DotExtensions_Enumerables_AddRange()
    {
     IEnumerable<string> enumerable = fakeData.AsEnumerable().AddRange(fakeData2Enumerable);

     // Console.WriteLine($"{nameof(enumerable)} exists");
    }
    
    [Benchmark]
    public void DotExtensions_Collection_AddRange()
    {
        Collection<string> fakestData = [..fakeData];
            
            GenericCollectionRangeExtensions.AddRange(fakestData, fakeData2IList);
    }
    
    [Benchmark]
    public void DotExtensions_IList_AddRange()
    {
        IList<string> fakestData = [..fakeData];
        
        // ReSharper disable once InvokeAsExtensionMethod
        IListRangeExtensions.AddRange(fakestData, fakeData2IList);
    }

    [Benchmark(Baseline = true)]
    public void Linq_List_AddRange()
    {
        fakeData.AddRange(fakeData2List);
    }
}