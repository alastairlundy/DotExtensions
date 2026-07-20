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

    public DigitCountingExperimentBenchmarks()
    {
        _numbers = new int[N];
    }
    
    [GlobalSetup]
    public void Setup()
    {
        _numbers = new int[N];

        for (int i = 0; i < _numbers.Length; i++)
        {
            _numbers[i] = Random.Shared.Next(int.MinValue, int.MaxValue);
        }
    }

    [Params(10_000, 1_000_000)]
    public int N;
    

    [Benchmark]
    public int[] String_Length()
    {
        int[] results = new int[N];

        for (int index = 0; index < _numbers.Length; index++)
        {
            int number = _numbers[index];
            int tempI = 0;

            if (number < 0)
            {
                tempI = number * -1;
            }

            results[index] = tempI.ToString().Length;
        }

        return results;
    }

    [Benchmark]
    public int[] DigitCounting()
    {
        int[] results = new int[N];

        for (int index = 0; index < _numbers.Length; index++)
        {
            results[index] = _numbers[index].CountNumberOfDigits();
        }

        return results;
    }
}
