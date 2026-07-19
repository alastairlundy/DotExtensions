using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace DotExtensions.Benchmarking;

/// <summary>
/// Shared, tuned BDN config used as the default for every benchmark class in
/// <c>DotExtensions.Benchmarking</c>. The defaults
/// (<c>WarmupCount = 3</c>, <c>IterationCount = 5</c>) are intentionally small
/// to keep full-suite run time manageable while still retaining enough
/// statistical power to detect performance differences for methods in the
/// 1 us - 100 ms range.
/// </summary>
/// <remarks>
/// Built on <see cref="Job.ShortRun"/> so we inherit BDN's pre-tuned
/// min/max iteration-time bounds and outlier detection; we then constrain
/// the warmup and iteration counts to keep the wall-clock cost predictable.
/// Per-class overrides remain available by composing this config with a
/// custom job or by adding additional <c>[Config]</c> attributes on the
/// benchmark class.
/// </remarks>
public class FastBenchConfig : ManualConfig
{
    public FastBenchConfig()
    {
        AddJob(Job.ShortRun
            .WithWarmupCount(3)
            .WithIterationCount(5)
            .AsDefault());
    }
}
