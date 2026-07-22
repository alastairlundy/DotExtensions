# DotExtensions.Benchmarking

Benchmark suite for the DotExtensions library, powered by [BenchmarkDotNet](https://benchmarkdotnet.org/).

## Usage

All commands run from the repository root or the project directory.

### Full suite

```sh
dotnet run -c Release
```

Runs every benchmark with the default `FastBenchConfig` (3 warmup, 5 iterations).

### Quick mode (Short benchmarks only)

```sh
dotnet run -c Release --quick
```

Runs only benchmarks classified as `"Short"` — ideal for fast feedback during development. The suite should complete in under 30 seconds.

### Filter by class or method

```sh
dotnet run -c Release --filter *StringRemove*
dotnet run -c Release --filter *VersionGracefulParseBenchmarks*
```

Uses BenchmarkDotNet's glob syntax. Multiple patterns are supported:

```sh
dotnet run -c Release --filter *StringRemove* *DigitCounting*
```

### Combining flags

`--quick` and `--filter` compose:

```sh
dotnet run -c Release --quick --filter *DigitCounting*
```

### Multi-TFM validation

```sh
dotnet run -c Release --tfm all
```

Runs the suite for `net8.0`, `net9.0`, and `net10.0` sequentially by re-spawning the process for each TFM. Individual TFM runs still accept `--quick` and `--filter`.

To target a single TFM without `--tfm all`:

```sh
dotnet run -c Release -f net8.0
```

## Benchmark classes and input sizes

| Benchmark class | Category | N values |
|---|---|---|
| `StringRemoveExtensionsBenchmarks` | Short | 16, 256, 4096 |
| `VersionGracefulParseBenchmarks` | Short | 100, 1,000, 10,000 |
| `RandomFileRetrievalBenchmark` | Short | 10, 100 |
| `SafeFileEnumerationBenchmarks` | Short | Fixed seeded tree (100 files, 10 subdirs) |
| `DigitCountingExperimentBenchmarks` | Medium | 10,000, 1,000,000 |

## Profiles

- **default**: Uses `FastBenchConfig` — 3 warmup iterations, 5 measured iterations, based on `Job.ShortRun`. Tuned for a balance of speed and statistical reliability for methods in the 1us–100ms range.
- **full**: A full run without `--quick` executes all categories (Short + Medium). Multi-TFM coverage requires `--tfm all`.

All benchmark classes apply `FastBenchConfig` by default via `[Config(typeof(FastBenchConfig))]`.

## Project structure

```
DotExtensions.Benchmarking/
  Program.cs          Entry point with --quick, --tfm all, --filter parsing
  FastBenchConfig.cs  Shared BDN config (3 warmup, 5 iterations)
  Benchmarks/
    IO/               Random file retrieval, safe file enumeration
    Strings/          StringRemove extension methods
    System/
      Numbers/        Digit counting experiments
      Versions/       Version.GracefulParse
  Infra/              Shared helpers (benchmark data seeding)
```
