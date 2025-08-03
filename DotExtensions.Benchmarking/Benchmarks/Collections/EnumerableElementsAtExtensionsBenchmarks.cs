/*using System.Collections.Generic;
using System.Linq;

using AlastairLundy.DotExtensions.Collections.Generic.Enumerables;
using AlastairLundy.DotExtensions.Collections.Generic.ILists;
using AlastairLundy.DotExtensions.Linq.Enumerables;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using DotExtensions.Benchmarking.Infra.FakeData;

namespace DotExtensions.Benchmarking.Benchmarks.Collections;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser(true)]
[CsvMeasurementsExporter]
public class EnumerableElementsAtExtensionsBenchmarks
{
    private List<string> fakeData;
    private List<string> fakeData2List;
    private IList<string> fakeData2IList;
    private IEnumerable<string> fakeData2Enumerable;

    private IEnumerable<int> enumerableIndices;
    private IList<int> listIndices;
    
    [GlobalSetup]
    public void Setup()
    {
        FakeEnumerables fakeEnumerables = new FakeEnumerables();
        fakeData = fakeEnumerables.Create(N).ToList();
        
        fakeData2Enumerable = fakeEnumerables.Create(N / 10);
        fakeData2IList = fakeEnumerables.Create(N / 10).ToList();
        fakeData2List = fakeEnumerables.Create(N / 10).ToList();

        enumerableIndices = fakeEnumerables.Indices(N / 10);
        listIndices = fakeEnumerables.Indices(N / 10).ToList();
    }
    
    [Params(
        1_000_000
    )]
    public int N;
    
    [Benchmark]
    public void DotExtensions_Enumerable_ElementsAt()
    {
        // ReSharper disable once InvokeAsExtensionMethod
        IEnumerable<string> results = EnumerableElementsAtExtensions.ElementsAt(fakeData2Enumerable, enumerableIndices);

        Consumer consumer = new Consumer();
        results.Consume(consumer);
    }
    
    [Benchmark]
    public IList<string> DotExtensions_IList_ElementsAt()
    {
        // ReSharper disable once InvokeAsExtensionMethod
        return IListElementsAtExtensions.ElementsAt(fakeData2IList, listIndices);
    }
}*/