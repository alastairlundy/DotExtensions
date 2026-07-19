/*
        MIT License

       Copyright (c) 2026 Alastair Lundy

       Permission is hereby granted, free of charge, to any person obtaining a copy
       of this software and associated documentation files (the "Software"), to deal
       in the Software without restriction, including without limitation the rights
       to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
       copies of the Software, and to permit persons to whom the Software is
       furnished to do so, subject to the following conditions:

       The above copyright notice and this permission notice shall be included in all
       copies or substantial portions of the Software.

       THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
       IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
       FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
       AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
       LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
       OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
       SOFTWARE.
   */

using System;
using System.Linq;
using BenchmarkDotNet.Configs;
using DotExtensions.Benchmarking;
using DotExtensions.Strings;

namespace DotExtensions.Benchmarking.Benchmarks.Strings;

/// <summary>
/// Benchmark for <see cref="StringRemoveExtensions"/> methods across three input sizes
/// (short 16, medium 256, long 4096 chars), producing a 3x3 matrix of
/// (method x size) results.
/// </summary>
/// <remarks>
/// Inputs are ASCII-only to avoid culture-specific flakiness. The same generated
/// source string is used for every method at a given <see cref="N"/> so that the
/// only thing varying between the three methods is the algorithm itself.
/// </remarks>
[Config(typeof(FastBenchConfig))]
[MemoryDiagnoser]
[CsvMeasurementsExporter]
public class StringRemoveExtensionsBenchmarks
{
    private const string Needle = "ab";

    private string _source = string.Empty;

    /// <summary>Source string length. Cycle is "abcd" (4 chars) so <see cref="N"/> must be a multiple of 4.</summary>
    [Params(16, 256, 4096)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        const int cycleLength = 4;
        int repeats = N / cycleLength;
        _source = string.Concat(Enumerable.Repeat("abcd", repeats));
    }

    [Benchmark]
    public string RemoveAll_ShortMediumLong()
    {
        // "abcdabcd..." -> "cdcdcd..." (N/2 chars)
        return _source.RemoveAll(Needle, StringComparison.Ordinal);
    }

    [Benchmark]
    public string RemoveFirst_ShortMediumLong()
    {
        // "abcdabcd..." -> "cdabcdabcd..." (N-2 chars)
        return _source.RemoveFirst(Needle, StringComparison.Ordinal);
    }

    [Benchmark]
    public string RemoveLast_ShortMediumLong()
    {
        // "abcdabcd..." -> "abcdabcd...cd" (N-2 chars)
        return _source.RemoveLast(Needle, StringComparison.Ordinal);
    }
}
