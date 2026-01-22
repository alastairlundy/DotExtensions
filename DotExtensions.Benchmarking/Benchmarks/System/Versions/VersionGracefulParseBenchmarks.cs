using System.Collections.Generic;
using Bogus;
using DotExtensions.Versions;

namespace DotExtensions.Benchmarking.Benchmarks.System.Versions;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net10_0)]
[MemoryDiagnoser(true)]
[CsvMeasurementsExporter]
public class VersionGracefulParseBenchmarks
{
    private readonly Faker _faker;
    
    private IList<string> _bogusVersionStrings;
    
    public VersionGracefulParseBenchmarks()
    {
        _faker = new Faker();
    }

    private const int N = 1_000_000;

    [GlobalSetup]
    public void Setup()
    {
        _bogusVersionStrings = _faker.Make<string>(N, () =>
        {
            if (int.IsOddInteger(Random.Shared.Next()))
            {
                int randomSuffix = Random.Shared.Next(0, 5);

                string suffix = randomSuffix switch
                {
                    0 => "-preview",
                    1 => "-alpha",
                    2 => "-beta",
                    3 => "-rc",
                    4 => "-final",
                    _ => ""
                };
                
                string versionString = $"{_faker.System.Version()}{suffix}";

                int random =  Random.Shared.Next(0, 3);
                versionString = random switch
                {
                    3 => versionString,
                    2 => $"{versionString}.{_faker.Random.Int(min: 0)}",
                    1 => $"{versionString}{_faker.Random.Int(min:0)}",
                    _ => versionString
                };
                
                return versionString;
            }

            return _faker.System.Version().ToString();
        });
    }

    [Benchmark]
    public IList<Version> GracefulParse_Impl()
    {
        List<Version> output = new(capacity:_bogusVersionStrings.Count);
        
        foreach (string version in _bogusVersionStrings)
        {
            Version result = Version.GracefulParse(version);
            
            output.Add(result);
        }
        
        return output;
    }
}