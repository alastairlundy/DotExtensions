using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

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
    1_000_000,
    10_000_000)]
    public int N;
    
    [Benchmark]
    public int[] String_Linq()
    {
        int[] results = new int[N];
        
        foreach (int number in numbers)
        {
            int tempI = 0;

            if (number < 0)
            {
                tempI = number * -1;
            }
        
            // String Implementation
            results[number] = tempI.ToString()
                .Count(char.IsDigit);
        }
        
        return results;
    }

    [Benchmark]
    public int[] String_Length()
    {
        int[] results = new int[N];
        
        foreach (int number in numbers)
        {
            int tempI = 0;

            if (number < 0)
            {
                tempI = number * -1;
            }
        
            // String Implementation
            results[number] = tempI.ToString()
                .Length;
        }
                
        return results;
    }
    
}