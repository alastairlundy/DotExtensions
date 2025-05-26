// See https://aka.ms/new-console-template for more information

using System;
using DotExtensions.Benchmarking.Benchmarks.Collections;
using BenchmarkDotNet.Running;


BenchmarkRunner.Run<EnumerableAddRangeExtensionsBenchmarks>();