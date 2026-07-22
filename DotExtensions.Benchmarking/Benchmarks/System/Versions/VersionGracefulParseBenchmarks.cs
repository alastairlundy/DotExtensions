using System.Collections.Generic;
using BenchmarkDotNet.Configs;
using Bogus;
using DotExtensions.Benchmarking;
using DotExtensions.Versions;

namespace DotExtensions.Benchmarking.Benchmarks.System.Versions;

[Config(typeof(FastBenchConfig))]
[MemoryDiagnoser()]
[CsvMeasurementsExporter]
[BenchmarkCategory("Short")]
public class VersionGracefulParseBenchmarks
{
    // Bogus-generated appended integers are clamped to this maximum so
    // int.Parse / Version's int-typed constructor never receives an
    // OverflowException. Realistic version components are well within
    // this range; the constant is named explicitly to make the intent
    // (clamping, not unrestricted random) obvious to readers and to
    // future contributors tweaking the bound.
    private const int MaxVersionComponent = 9_999;

    private readonly Faker _faker;

    private IList<string> _bogusVersionStrings;

    public VersionGracefulParseBenchmarks()
    {
        _faker = new Faker();
        _bogusVersionStrings = new List<string>();
        N = 0;
    }

    [Params(100, 1_000, 10_000)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        _bogusVersionStrings = _faker.Make(N, () =>
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
                    2 => $"{versionString}.{_faker.Random.Int(min: 0, max: MaxVersionComponent)}",
                    1 => $"{versionString}{_faker.Random.Int(min: 0, max: MaxVersionComponent)}",
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