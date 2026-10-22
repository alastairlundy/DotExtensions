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
using System.Collections.Generic;
using DotExtensions.Numbers;

namespace DotExtensions.Tests.Numbers;

public class NumberRangeExtensionsTests
{
    // Regression: AsRange(Index, count) used to build end = start.Value + count regardless of
    // from-end-ness, producing end < start for ^n indexes and throwing when slicing.
    [Test]
    public async Task AsRange_FromEndIndex_CoversCountElementsFromTheResolvedOffset()
    {
        const string Sequence = "abcdefghij"; // offsets 0..9; ^3 resolves to offset 7.

        Index start = Index.FromEnd(3);
        Range range = start.AsRange(2);

        await Assert.That(Sequence[range]).IsEqualTo("hi"); // elements 7 and 8
        await Assert.That(range.Start.IsFromEnd).IsTrue();
        await Assert.That(range.End.GetOffset(Sequence.Length)).IsEqualTo(9);
    }

    [Test]
    public async Task AsRange_FromStartIndex_CoversCountElements()
    {
        const string Sequence = "abcdefghij";

        Index start = new(2);
        Range range = start.AsRange(3);

        await Assert.That(Sequence[range]).IsEqualTo("cde");
        await Assert.That(range.End.Value).IsEqualTo(5);
    }

    [Test]
    public async Task AsRange_FromIntStart_CoversCountElements()
    {
        const string Sequence = "abcdefghij";

        Range range = 2.AsRange(3);

        await Assert.That(Sequence[range]).IsEqualTo("cde");
    }
}

public class IncrementedNumberRangeTests
{
    [Test]
    public async Task IsIncrementedNumberRange_ContiguousIncrement_ReturnsTrue()
    {
        List<int> ascending = [1, 2, 3, 4];
        List<int> stepTwo = [0, 2, 4, 6];
        List<int> descending = [-3, -2, -1, 0];

        await Assert.That(ascending.IsIncrementedNumberRange(1)).IsTrue();
        await Assert.That(stepTwo.IsIncrementedNumberRange(2)).IsTrue();
        await Assert.That(descending.IsIncrementedNumberRange(1)).IsTrue();
        await Assert.That(descending.IsIncrementedNumberRange(-1)).IsFalse();
    }

    [Test]
    public async Task IsIncrementedNumberRange_NonContiguous_ReturnsFalse()
    {
        List<int> source = [1, 2, 4, 5];

        await Assert.That(source.IsIncrementedNumberRange(1)).IsFalse();
    }

    // A VALID chain that touches the type bound must stay true.
    [Test]
    public async Task IsIncrementedNumberRange_endingAtIntMaxValue_ReturnsTrue()
    {
        List<int> source = [int.MaxValue - 1, int.MaxValue];

        await Assert.That(source.IsIncrementedNumberRange(1)).IsTrue();
    }

    // Regression: the old accumulating `expectedNumber += increment` wrapped around
    // ([int.MaxValue, int.MinValue] with increment 1 summed to int.MinValue and matched).
    // Crossing the type bound cannot be a valid increment chain - wrapping must be a mismatch.
    [Test]
    public async Task IsIncrementedNumberRange_WrappingAcrossIntBounds_ReturnsFalse()
    {
        List<int> wrapping = [int.MaxValue, int.MinValue];
        List<int> wrappingNegative = [int.MinValue, int.MaxValue];

        await Assert.That(wrapping.IsIncrementedNumberRange(1)).IsFalse();
        await Assert.That(wrappingNegative.IsIncrementedNumberRange(-1)).IsFalse();
    }

    [Test]
    public async Task IsIncrementedNumberRange_EnumerableOverload_MatchesListSemantics()
    {
        // Queue<T> is enumerable-only: it takes the non-IList path.
        Queue<int> queue = new([1, 2, 3, 4]);
        Queue<int> wrapping = new([int.MaxValue, int.MinValue]);

        await Assert.That(queue.IsIncrementedNumberRange(1)).IsTrue();
        await Assert.That(wrapping.IsIncrementedNumberRange(1)).IsFalse();
    }

    // decimal arithmetic always throws on overflow instead of wrapping; the guard treats that
    // overflow as a mismatch rather than letting it escape.
    [Test]
    public async Task IsIncrementedNumberRange_DecimalOverflow_ReturnsFalseWithoutThrowing()
    {
        List<decimal> source = [decimal.MaxValue, decimal.MaxValue];

        await Assert.That(source.IsIncrementedNumberRange(1m)).IsFalse();
    }

    [Test]
    public async Task IsIncrementedNumberRange_EmptySource_ReturnsTrue()
    {
        // Vacuously true, matching the pre-existing enumerable-overload behavior for empty input.
        List<int> empty = [];

        await Assert.That(empty.IsIncrementedNumberRange(1)).IsTrue();
    }
}
