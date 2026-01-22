// See https://aka.ms/new-console-template for more information

using System.Reflection;
using BenchmarkDotNet.Running;

using DotExtensions.Benchmarking.Benchmarks.System.Versions;

//BenchmarkRunner.Run(assembly: Assembly.GetCallingAssembly());

BenchmarkRunner.Run<VersionGracefulParseBenchmarks>();