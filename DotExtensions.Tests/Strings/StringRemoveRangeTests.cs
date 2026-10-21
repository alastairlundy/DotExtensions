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

/// <summary>
/// Deterministic tests for <see cref="StringRemoveRangeExtensions"/>.
/// The end index of every removal is exclusive, and from-end (<c>^</c>) indexes
/// are resolved against the original string's length.
/// </summary>
public class StringRemoveRangeTests
{
    [Test]
    public async Task Remove_TwoIndices_RemovesUpToExclusiveEnd()
    {
        string result = "abcdef".Remove((Index)1, (Index)3);

        await Assert.That(result).IsEqualTo("adef");
    }

    [Test]
    public async Task Remove_Range_RemovesUpToExclusiveEnd()
    {
        string result = "abcdef".Remove(new Range(1, 3));

        await Assert.That(result).IsEqualTo("adef");
    }

    [Test]
    public async Task Remove_RangeWithFromEndEnd_ResolvesFromEnd()
    {
        string result = "abcdef".Remove(new Range(1, ^1));

        await Assert.That(result).IsEqualTo("af");
    }

    [Test]
    public async Task Remove_RangeWithFromEndStartAndEnd_ResolvesBothFromEnd()
    {
        string result = "abcdef".Remove(new Range(^3, ^0));

        await Assert.That(result).IsEqualTo("abc");
    }

    [Test]
    public async Task Remove_FromZero_RemovesPrefix()
    {
        string result = "abcdef".Remove((Index)0, (Index)2);

        await Assert.That(result).IsEqualTo("cdef");
    }

    [Test]
    public async Task Remove_EqualStartAndEnd_RemovesNothing()
    {
        string result = "abcdef".Remove((Index)2, (Index)2);

        await Assert.That(result).IsEqualTo("abcdef");
    }

    [Test]
    public async Task RemoveRange_MultipleDisjointRanges_RemovesAll()
    {
        string result = "abcdef".RemoveRange([new Range(0, 2), new Range(3, 6)]);

        //Removes [0,2)="ab" and [3,6)="def" from "abcdef", leaving only "c".
        await Assert.That(result).IsEqualTo("c");
    }

    [Test]
    public async Task RemoveRange_EmptyCollection_ReturnsOriginalUnchanged()
    {
        const string source = "abcdef";

        string result = source.RemoveRange([]);

        await Assert.That(result).IsEqualTo(source);
    }

    [Test]
    public async Task RemoveRange_OverlappingRanges_DoesNotThrow()
    {
        // Applying [2..6) before [0..4) yields the union of both ranges.
        string result = "abcdef".RemoveRange([new Range(0, 4), new Range(2, 6)]);

        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task RemoveRange_NullCollection_ThrowsArgumentNullException()
    {
        await Assert.That(() => "abcdef".RemoveRange(null!))
            .Throws<ArgumentNullException>();
    }

    [Test]
    public async Task RemoveRange_EndGreaterThanLength_ThrowsArgumentOutOfRangeException()
    {
        await Assert.That(() => "abcdef".RemoveRange([new Range(3, 7)]))
            .Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Remove_EndGreaterThanLength_ThrowsArgumentOutOfRangeException()
    {
        await Assert.That(() => "abcdef".Remove((Index)1, (Index)7))
            .Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Remove_StartGreaterThanEnd_ThrowsArgumentOutOfRangeException()
    {
        await Assert.That(() => "abcdef".Remove((Index)3, (Index)1))
            .Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Remove_NegativeResolvedStart_ThrowsArgumentOutOfRangeException()
    {
        // ^7 on a six-character string resolves to -1.
        await Assert.That(() => "abcdef".Remove(^7, (Index)0))
            .Throws<ArgumentOutOfRangeException>();
    }
}
