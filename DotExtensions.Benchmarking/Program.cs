/*
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

﻿// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using DotExtensions.Benchmarking.Benchmarks.System.Numbers;

BenchmarkRunner.Run<DigitCountingExperimentBenchmarks>();
