using System;
using System.Collections.Generic;
using System.Reflection;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Filters;
using BenchmarkDotNet.Running;

// DotExtensions.Benchmarking entry point.
//
// Usage:
//   dotnet run -c Release                          -> run the full benchmark suite
//   dotnet run -c Release --filter *Foo*           -> run only benchmarks whose full
//                                                    name matches the BDN glob
//                                                    pattern *Foo*
//   dotnet run -c Release --filter *Class*Method*  -> multiple glob patterns can
//                                                    follow --filter
//
// GlobFilter uses BenchmarkDotNet's standard glob syntax (e.g. *ClassName* or
// *ClassName*MethodName*). When no --filter is supplied the entire discovered
// benchmark set runs, matching the BDN default behaviour.
string[] filterPatterns = ParseFilterArgs(args);

IConfig config = ManualConfig.CreateMinimumViable();

if (filterPatterns.Length > 0)
{
    config = config.AddFilter(new IFilter[] { new GlobFilter(filterPatterns) });
}

BenchmarkRunner.Run(Assembly.GetExecutingAssembly(), config);

// Extracts BDN glob patterns from the standard --filter CLI token.
// Patterns continue to be consumed until the next '--' flag, mirroring
// how BenchmarkSwitcher.Run(args) parses BDN's own command-line grammar.
static string[] ParseFilterArgs(string[] rawArgs)
{
    List<string> patterns = new();

    for (int i = 0; i < rawArgs.Length; i++)
    {
        if (!string.Equals(rawArgs[i], "--filter", StringComparison.Ordinal))
        {
            continue;
        }

        int j = i + 1;
        while (j < rawArgs.Length && !rawArgs[j].StartsWith("--", StringComparison.Ordinal))
        {
            patterns.Add(rawArgs[j]);
            j++;
        }

        i = j - 1;
    }

    return patterns.ToArray();
}