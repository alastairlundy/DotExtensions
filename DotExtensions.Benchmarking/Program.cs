// See https://aka.ms/new-console-template for more information

using System;
using DotExtensions.Benchmarking.Benchmarks.Collections;
using BenchmarkDotNet.Running;
using DotExtensions.Benchmarking.Benchmarks.System.Numbers;


BenchmarkRunner.Run<DigitCountingExperimentBenchmarks>();