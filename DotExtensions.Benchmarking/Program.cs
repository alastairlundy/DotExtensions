/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using BenchmarkDotNet.Running;
using DotExtensions.Benchmarking.Benchmarks.System.Numbers;

BenchmarkRunner.Run<DigitCountingExperimentBenchmarks>();
