// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using DotExtensions.Benchmarking.Benchmarks.System.Numbers;


BenchmarkRunner.Run<DigitCountingExperimentBenchmarks>();