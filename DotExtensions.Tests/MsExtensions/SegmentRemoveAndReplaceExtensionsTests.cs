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
using DotExtensions.MsExtensions.Primitives;
using Microsoft.Extensions.Primitives;

namespace DotExtensions.Tests.MsExtensions;

/// <summary>
/// Deterministic tests for <see cref="SegmentRemoveAndReplaceExtensions"/>.
/// <c>Remove(int)</c> keeps the characters before the start index (BCL <c>string.Remove(int)</c>
/// semantics), <c>Remove(int, int)</c> removes the half-open window
/// [<c>startIndex</c>, <c>startIndex</c> + <c>count</c>), and the Index/Range overloads treat the
/// end index as exclusive with from-end (<c>^</c>) index support.
/// </summary>
public class SegmentRemoveAndReplaceExtensionsTests
{
    [Test]
    public async Task Remove_StartIndex_KeepsCharactersBeforeStartIndex()
    {
        StringSegment source = "abcdef";

        StringSegment result = source.Remove(3);

        await Assert.That(result.ToString()).IsEqualTo("abc");
    }

    [Test]
    public async Task Remove_ZeroStartIndex_RemovesWholeSegment()
    {
        // string.Remove(0) removes from index 0 to the end, so nothing is kept.
        StringSegment source = "abcdef";

        StringSegment result = source.Remove(0);

        await Assert.That(result.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Remove_StartIndexAtLength_KeepsWholeSegment()
    {
        // string.Remove(Length) is valid and removes nothing.
        StringSegment source = "abcdef";

        StringSegment result = source.Remove(6);

        await Assert.That(result.ToString()).IsEqualTo("abcdef");
    }

    [Test]
    public async Task Remove_Count_RemovesWindowAndKeepsAdjacentCharacters()
    {
        StringSegment source = "abcdef";

        StringSegment result = source.Remove(2, 1);

        await Assert.That(result.ToString()).IsEqualTo("abdef");
    }

    [Test]
    public async Task Remove_ZeroStartIndexWithCount_RemovesPrefix()
    {
        StringSegment source = "abcdef";

        StringSegment result = source.Remove(0, 2);

        await Assert.That(result.ToString()).IsEqualTo("cdef");
    }

    [Test]
    public async Task Remove_CountSpansMiddle_KeepsBothEnds()
    {
        StringSegment source = "abcdef";

        StringSegment result = source.Remove(1, 4);

        await Assert.That(result.ToString()).IsEqualTo("af");
    }

    [Test]
    public async Task Remove_CountToEnd_KeepsPrefixOnly()
    {
        StringSegment source = "abcdef";

        StringSegment result = source.Remove(2, 4);

        await Assert.That(result.ToString()).IsEqualTo("ab");
    }

    [Test]
    public async Task Remove_CountEqualsLength_ReturnsEmpty()
    {
        StringSegment source = "abcdef";

        StringSegment result = source.Remove(0, 6);

        await Assert.That(result.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Remove_ZeroCount_ReturnsSegmentUnchanged()
    {
        StringSegment source = "abcdef";

        StringSegment result = source.Remove(2, 0);

        await Assert.That(result.ToString()).IsEqualTo("abcdef");
    }

    [Test]
    public async Task Remove_TwoIndices_RemovesUpToExclusiveEnd()
    {
        StringSegment source = "abcdef";

        StringSegment result = source.Remove((Index)1, (Index)3);

        await Assert.That(result.ToString()).IsEqualTo("adef");
    }

    [Test]
    public async Task Remove_TwoIndicesFromZero_RemovesPrefix()
    {
        StringSegment source = "abcdef";

        StringSegment result = source.Remove((Index)0, (Index)2);

        await Assert.That(result.ToString()).IsEqualTo("cdef");
    }

    [Test]
    public async Task Remove_Range_RemovesUpToExclusiveEnd()
    {
        StringSegment source = "abcdef";

        StringSegment result = source.Remove(new Range(1, 3));

        await Assert.That(result.ToString()).IsEqualTo("adef");
    }

    [Test]
    public async Task Remove_RangeWithFromEndEnd_ResolvesFromEnd()
    {
        StringSegment source = "abcdef";

        StringSegment result = source.Remove(new Range(1, ^1));

        await Assert.That(result.ToString()).IsEqualTo("af");
    }

    [Test]
    public async Task Remove_RangeWithFromEndEndAtZero_RemovesWholeSegment()
    {
        StringSegment source = "abcdef";

        StringSegment result = source.Remove(new Range(0, ^0));

        await Assert.That(result.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Remove_StartIndexGreaterThanLength_ThrowsArgumentOutOfRangeException()
    {
        await Assert.That(() => ((StringSegment)"abcdef").Remove(7))
            .Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Remove_NegativeStartIndex_ThrowsArgumentOutOfRangeException()
    {
        await Assert.That(() => ((StringSegment)"abcdef").Remove(-1))
            .Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Remove_StartIndexPlusCountBeyondLength_ThrowsArgumentOutOfRangeException()
    {
        await Assert.That(() => ((StringSegment)"abcdef").Remove(2, 5))
            .Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Remove_IndexStartAfterEnd_ThrowsArgumentOutOfRangeException()
    {
        await Assert.That(() => ((StringSegment)"abcdef").Remove((Index)3, (Index)1))
            .Throws<ArgumentOutOfRangeException>();
    }
}
