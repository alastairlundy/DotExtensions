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
using DotExtensions.Numbers;

namespace DotExtensions.Tests.Numbers;

/// <summary>
/// Tests for <see cref="NumberRangeExtensions"/> covering the <see cref="int"/>-rooted and
/// <see cref="Index"/>-rooted <c>AsRange</c> overloads, including from-end index semantics.
/// </summary>
public class NumberRangeExtensionsTests
{
    [Test]
    public async Task AsRange_FromStartInt_CoversCountElementsStartingAtStart()
    {
        int[] arr = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];

        Range range = 5.AsRange(3);

        await Assert.That(arr[range].SequenceEqual(new[] { 5, 6, 7 })).IsTrue();
    }

    [Test]
    public async Task AsRange_FromEndIndex_CoversLastCountElements()
    {
        int[] arr = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];

        // ^2 on a 10-element array with count 2 must produce ^2..^0 (the last two elements),
        // not a from-start end index derived from Index.Value.
        Range range = new Index(2, fromEnd: true).AsRange(2);

        await Assert.That(arr[range].SequenceEqual(new[] { 8, 9 })).IsTrue();
    }
}
