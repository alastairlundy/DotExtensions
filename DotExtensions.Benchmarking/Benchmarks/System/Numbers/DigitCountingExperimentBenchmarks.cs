using System.Linq;
using DotExtensions.Numbers;

namespace DotExtensions.Benchmarking.Benchmarks.System.Numbers;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser(true)]
[CsvMeasurementsExporter]
public class DigitCountingExperimentBenchmarks
{
    private int[] numbers;

    [GlobalSetup]
    public void Setup()
    {
        numbers = new int[N];

        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = Random.Shared.Next(int.MinValue, int.MaxValue);
        }
    }

    [Params(
        //    10_000_000,
        100_000_000
    )]
    public int N;

    [Benchmark]
    public int[] String_Linq()
    {
        int[] results = new int[N];

        for (int index = 0; index < numbers.Length; index++)
        {
            int number = numbers[index];
            int tempI = 0;

            if (number < 0)
            {
                tempI = number * -1;
            }

            // String Implementation
            results[index] = tempI.ToString().Count(char.IsDigit);
        }

        return results;
    }

    [Benchmark]
    public int[] String_Length()
    {
        int[] results = new int[N];

        for (int index = 0; index < numbers.Length; index++)
        {
            int number = numbers[index];
            int tempI = 0;

            if (number < 0)
            {
                tempI = number * -1;
            }

            // String Implementation
            results[index] = tempI.ToString().Length;
        }

        return results;
    }

    [Benchmark]
    public int[] DigitCounting()
    {
        int[] results = new int[N];

        for (int index = 0; index < numbers.Length; index++)
        {
            results[index] = numbers[index].CountNumberOfDigits();
        }

        return results;
    }
}
