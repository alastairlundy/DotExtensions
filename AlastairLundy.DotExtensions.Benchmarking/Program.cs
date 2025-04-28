// See https://aka.ms/new-console-template for more information

using System;
using AlastairLundy.DotExtensions.Benchmarking.Benchmarks.Collections.ICollections;
using BenchmarkDotNet.Running;

Console.WriteLine("Hello World!");

BenchmarkRunner.Run<EnumerableIndexExtensionsBenchmarks>();