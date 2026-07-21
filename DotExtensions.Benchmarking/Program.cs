using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Filters;
using BenchmarkDotNet.Running;

// DotExtensions.Benchmarking entry point.
//
// Usage:
//   dotnet run -c Release                               -> run the full suite
//   dotnet run -c Release --quick                       -> run only "Short" benchmarks
//   dotnet run -c Release --tfm all                     -> run suite for each TFM
//   dotnet run -c Release --filter *Foo*                -> run benchmarks matching glob
//   dotnet run -c Release --quick --filter *Foo*        -> combine --quick with --filter
//
// GlobFilter uses BenchmarkDotNet's standard glob syntax. When --tfm all is
// specified the process re-spawns itself once per target framework.
string[] filterPatterns = ParseArgs(args, out bool quick, out bool tfmAll);

if (tfmAll)
{
    string[] tfms = ["net8.0", "net9.0", "net10.0"];

    // Forward all args except --tfm all so child processes don't loop.
    string[] childArgs = args
        .Where((a, i) => a != "--tfm"
            && !(i > 0 && args[i - 1] == "--tfm" && a == "all"))
        .ToArray();

    string childArgString = string.Join(" ",
        childArgs.Select(a => a.Contains(' ') ? $"\"{a}\"" : a));

    foreach (string tfm in tfms)
    {
        using Process process = Process.Start("dotnet",
            $"run -c Release -f {tfm} -- {childArgString}")!;

        process.WaitForExit();
    }

    return;
}

IConfig config = ManualConfig.CreateMinimumViable();

if (quick)
{
    config = config.AddFilter(new IFilter[] { new AnyCategoriesFilter(["Short"]) });
}

if (filterPatterns.Length > 0)
{
    config = config.AddFilter(new IFilter[] { new GlobFilter(filterPatterns) });
}

BenchmarkRunner.Run(Assembly.GetExecutingAssembly(), config);

// Parses --quick, --tfm all, and --filter from the CLI arguments.
static string[] ParseArgs(string[] rawArgs, out bool quick, out bool tfmAll)
{
    quick = false;
    tfmAll = false;
    List<string> patterns = new();

    for (int i = 0; i < rawArgs.Length; i++)
    {
        if (string.Equals(rawArgs[i], "--quick", StringComparison.Ordinal))
        {
            quick = true;
        }
        else if (string.Equals(rawArgs[i], "--tfm", StringComparison.Ordinal)
                 && i + 1 < rawArgs.Length
                 && string.Equals(rawArgs[i + 1], "all", StringComparison.Ordinal))
        {
            tfmAll = true;
            i++; // consume "all"
        }
        else if (!string.Equals(rawArgs[i], "--filter", StringComparison.Ordinal))
        {
            continue;
        }
        else
        {
            // --filter consumes non-flag tokens that follow
            int j = i + 1;
            while (j < rawArgs.Length && !rawArgs[j].StartsWith("--", StringComparison.Ordinal))
            {
                patterns.Add(rawArgs[j]);
                j++;
            }

            i = j - 1;
        }
    }

    return patterns.ToArray();
}