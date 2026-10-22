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
using DotExtensions.Strings;

namespace DotExtensions.Tests.Strings;

public class StringRemoveRangeExtensionsTests
{
    [Test]
    public async Task Remove_Start_Inclusive()
    {
        Index start = 0;
        Index end = 4;

        string actual = "Hello World".Remove(start, end);

        await Assert.That(actual).IsEqualTo(" World");
    }

    [Test]
    public async Task Remove_End_Inclusive()
    {
        Index start = 6;
        Index end = 10;

        string actual = "Hello World".Remove(start, end);

        await Assert.That(actual).IsEqualTo("Hello ");
    }

    [Test]
    public async Task Remove_Middle_Inclusive()
    {
        Index start = 1;
        Index end = 3;

        string actual = "Hello".Remove(start, end);

        await Assert.That(actual).IsEqualTo("Ho");
    }

    [Test]
    public async Task Remove_FromEndIndices_ResolvedAgainstLength()
    {
        string actual = "Hello".Remove(^4, ^1);

        await Assert.That(actual).IsEqualTo("H");
    }

    [Test]
    public async Task Remove_ZeroToTwo_RemovesThreeCharsInclusive()
    {
        Index start = 0;
        Index end = 2;

        string actual = "Hello".Remove(start, end);

        await Assert.That(actual).IsEqualTo("lo");
    }

    [Test]
    public async Task Remove_EqualIndices_RemovesSingleChar()
    {
        Index index = 1;

        string actual = "Hello".Remove(index, index);

        await Assert.That(actual).IsEqualTo("Hllo");
    }

    [Test]
    public async Task Remove_EmptySource_ThrowsArgumentOutOfRangeException()
    {
        await Assert.That(() => "".Remove(^0, ^0))
            .Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Remove_FromEndIndexBeyondLength_ThrowsArgumentOutOfRangeException()
    {
        await Assert.That(() => "Hello".Remove(^10, ^1))
            .Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task RemoveRange_MultipleRanges_AppliedAgainstShrinkingString()
    {
        Range[] ranges = [0..4, 0..4];

        string actual = "Hello World".RemoveRange(ranges);

        // First range removes "Hello" (inclusive end), second removes " Worl" against the shrunken string.
        await Assert.That(actual).IsEqualTo("d");
    }

    [Test]
    public async Task RemoveRange_RangeBecomesOutOfBounds_ThrowsArgumentOutOfRangeException()
    {
        Range[] ranges = [0..2, 0..4];

        await Assert.That(() => "Hello".RemoveRange(ranges))
            .Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Remove_Range_Inclusive()
    {
        string actual = "Hello".Remove(1..3);

        await Assert.That(actual).IsEqualTo("Ho");
    }
}
