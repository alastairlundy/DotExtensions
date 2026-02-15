using System.Reflection;
using BenchmarkDotNet.Running;

//using DotExtensions.Benchmarking.Benchmarks.IO;

//using DotExtensions.Benchmarking.Benchmarks.System.Versions;

BenchmarkRunner.Run(Assembly.GetCallingAssembly());

//BenchmarkRunner.Run<RandomFileRetrievalBenchmark>();
//BenchmarkRunner.Run<VersionGracefulParseBenchmarks>();