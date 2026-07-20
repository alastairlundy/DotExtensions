using BenchmarkDotNet.Configs;
using DotExtensions.Benchmarking;
using DotExtensions.Numbers;

namespace DotExtensions.Benchmarking.Benchmarks.System.Numbers;

[Config(typeof(FastBenchConfig))]
[MemoryDiagnoser]
[CsvMeasurementsExporter]
public class DigitCountingExperimentBenchmarks
{
    private int[] _numbers;
    private int[] _results;

    public DigitCountingExperimentBenchmarks()
    {
        _numbers = Array.Empty<int>();
        _results = Array.Empty<int>();
    }

    [GlobalSetup]
    public void Setup()
    {
        _numbers = new int[N];
        // Pre-allocate the results array once per [Params] value so the
        // per-iteration allocation cost is removed from the measured body.
        // A consumer of CountNumberOfDigits() would not typically keep a
        // 100M-element result array around, so this allocation is test
        // scaffolding rather than a realistic workload cost.
        _results = new int[N];

        for (int i = 0; i < _numbers.Length; i++)
        {
            _numbers[i] = Random.Shared.Next(int.MinValue, int.MaxValue);
        }
    }

    [Params(10_000, 1_000_000)]
    public int N;


    [Benchmark]
    [BenchmarkCategory("Slow", "StringBased")]
    public int[] String_Length()
    {
        for (int index = 0; index < _numbers.Length; index++)
        {
            int number = _numbers[index];
            int tempI = 0;

            if (number < 0)
            {
                tempI = number * -1;
            }

            _results[index] = tempI.ToString().Length;
        }

        return _results;
    }

    [Benchmark]
    [BenchmarkCategory("Quick")]
    public int[] DigitCounting()
    {
        for (int index = 0; index < _numbers.Length; index++)
        {
            _results[index] = _numbers[index].CountNumberOfDigits();
        }

        return _results;
    }
}
