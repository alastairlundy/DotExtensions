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

namespace DotExtensions.Tests.MsExtensions.Primitives;

public class StringSegmentExtensionsTests
{
    #region Remove(int)

    // Remove(int startIndex) removes the window [startIndex, end): the result keeps everything
    // BEFORE startIndex. This is the family identity Remove(i) == Remove(i, length - i).
    [Test]
    public async Task Remove_SingleIndex_KeepsCharactersBeforeStartIndex()
    {
        StringSegment segment = new("abcde");

        await Assert.That(segment.Remove(0).ToString()).IsEqualTo(string.Empty);
        await Assert.That(segment.Remove(2).ToString()).IsEqualTo("ab");
        await Assert.That(segment.Remove(4).ToString()).IsEqualTo("abcd");
    }

    [Test]
    public async Task Remove_SingleIndex_MatchesRemovingTheRemainingWindow()
    {
        StringSegment segment = new("abcde");

        await Assert.That(segment.Remove(1).ToString()).IsEqualTo(segment.Remove(1, 4).ToString());
        await Assert.That(segment.Remove(1, 4).ToString()).IsEqualTo("a");
    }

    [Test]
    public async Task Remove_SingleIndex_OutOfRange_ThrowsArgumentOutOfRangeException()
    {
        StringSegment segment = new("abcde");

        await Assert.That(() => segment.Remove(5)).ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(() => segment.Remove(-1)).ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Remove_SingleIndex_EmptySegment_ThrowsExactlyArgumentNullException()
    {
        StringSegment empty = new(string.Empty);

        await Assert.That(() => empty.Remove(0)).ThrowsExactly<ArgumentNullException>();
    }

    #endregion

    #region Remove(int, int)

    // The removed window is the half-open range [startIndex, startIndex + count).
    [Test]
    public async Task Remove_StartIndexAndCount_RemovesTheWindow()
    {
        StringSegment segment = new("abcdef");

        await Assert.That(segment.Remove(1, 3).ToString()).IsEqualTo("aef");
        await Assert.That(segment.Remove(0, 2).ToString()).IsEqualTo("cdef");
        await Assert.That(segment.Remove(2, 3).ToString()).IsEqualTo("abf");

        // Removing to the very end leaves only the prefix.
        StringSegment five = new("abcde");
        await Assert.That(five.Remove(2, 3).ToString()).IsEqualTo("ab");
    }

    // The old implementation special-cased startIndex + count == length - 1 by delegating to the
    // (then broken) single-index Remove; the corrected math handles that shape directly.
    [Test]
    public async Task Remove_WindowEndingOneBeforeLength_UsesCorrectedMath()
    {
        StringSegment segment = new("abcdef");

        // Removes only index 4 ('e'): the previous shortcut dropped an extra character.
        await Assert.That(segment.Remove(4, 1).ToString()).IsEqualTo("abcdf");
    }

    [Test]
    public async Task Remove_InvalidCountOrStart_ThrowsArgumentOutOfRangeException()
    {
        StringSegment segment = new("abcdef");

        await Assert.That(() => segment.Remove(0, 0)).ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(() => segment.Remove(0, -1)).ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(() => segment.Remove(0, 6)).ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(() => segment.Remove(-1, 2)).ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(() => segment.Remove(6, 1)).ThrowsExactly<ArgumentOutOfRangeException>();
    }

    #endregion

    #region Remove(Index, Index) / Remove(Range)

    // endIndex is inclusive: Remove(1, 3) on "abcdef" removes indices 1..3 -> "aef".
    [Test]
    public async Task Remove_TwoIndexes_TreatsEndIndexAsInclusive()
    {
        StringSegment segment = new("abcdef");

        await Assert.That(segment.Remove(new Index(1), new Index(3)).ToString()).IsEqualTo("aef");
        await Assert.That(segment.Remove(new Index(0), new Index(0)).ToString()).IsEqualTo("bcdef");
    }

    // From-end Index values are resolved against the segment length, not used as raw values.
    [Test]
    public async Task Remove_TwoIndexes_ResolvesFromEndIndexes()
    {
        StringSegment segment = new("abcdef");

        // ^5 -> 1, ^4 -> 2: removes indices 1..2 inclusive.
        await Assert.That(segment.Remove(^5, ^4).ToString()).IsEqualTo("adef");

        // ^4 -> 1, ^1 -> 4: removes indices 1..4 inclusive.
        StringSegment hello = new("Hello");
        await Assert.That(hello.Remove(^4, ^1).ToString()).IsEqualTo("H");
    }

    [Test]
    public async Task Remove_TwoIndexes_InvalidBounds_ThrowArgumentOutOfRangeException()
    {
        StringSegment segment = new("abcdef");

        // End resolves before start.
        await Assert.That(() => segment.Remove(new Index(3), new Index(1)))
            .ThrowsExactly<ArgumentOutOfRangeException>();

        // From-end value larger than the length resolves to a negative offset.
        await Assert.That(() => segment.Remove(^10, ^1))
            .ThrowsExactly<ArgumentOutOfRangeException>();

        // Resolved end at/after the length.
        await Assert.That(() => segment.Remove(new Index(1), new Index(6)))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    // Remove(Range) routes through Remove(Index, Index), so the Range's End Index is inclusive -
    // the same convention the string-family Remove(Range) follows.
    [Test]
    public async Task Remove_Range_RemovesResolvedInclusiveWindow()
    {
        StringSegment segment = new("abcdef");

        await Assert.That(segment.Remove(1..3).ToString()).IsEqualTo("aef");

        // ^3 -> 3, ^2 -> 4: removes indices 3..4 inclusive.
        await Assert.That(segment.Remove(^3..^2).ToString()).IsEqualTo("abcf");
    }

    #endregion

    #region IEnumerable<StringSegment>.ToString(separator)

    // Regression: an empty sequence used to call StringBuilder.Remove(-n, n) and crash instead of
    // producing the empty string.
    [Test]
    public async Task ToStringSequence_CharSeparator_EmptySequence_ReturnsEmptyString()
    {
        StringSegment[] empty = [];

        await Assert.That(empty.ToString('-')).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task ToStringSequence_StringSegmentSeparator_EmptySequence_ReturnsEmptyString()
    {
        StringSegment[] empty = [];

        await Assert.That(empty.ToString(new StringSegment("-"))).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task ToStringSequence_Separator_JoinsSegmentsWithoutTrailingSeparator()
    {
        StringSegment[] segments = [new StringSegment("a"), new StringSegment("b"), new StringSegment("c")];

        await Assert.That(segments.ToString('-')).IsEqualTo("a-b-c");
        await Assert.That(segments.ToString(new StringSegment("-"))).IsEqualTo("a-b-c");
    }

    #endregion

    #region CapitalizeChars index validation

    [Test]
    public async Task CapitalizeChars_ValidIndices_UppercasesEachPosition()
    {
        StringSegment segment = new("hello");

        await Assert.That(segment.CapitalizeChars([0, 4]).ToString()).IsEqualTo("HellO");
        await Assert.That(segment.CapitalizeChars([1]).ToString()).IsEqualTo("hEllo");
    }

    // Regression: only index -1 was checked, so -2 and lower slipped through to the builder and
    // surfaced as a raw ArgumentOutOfRangeException without the documented paramName; the {0}
    // placeholder of the larger-index message was also thrown unformatted.
    [Test]
    public async Task CapitalizeChars_NegativeIndices_ThrowArgumentExceptionWithFormattedMessage()
    {
        StringSegment segment = new("hello");

        await Assert.That(() => segment.CapitalizeChars([-1]))
            .ThrowsExactly<ArgumentException>()
            .WithMessageContaining("-1");

        await Assert.That(() => segment.CapitalizeChars([-5]))
            .ThrowsExactly<ArgumentException>()
            .WithMessageContaining("-5");
    }

    [Test]
    public async Task CapitalizeChars_IndexLargerThanLength_ThrowsArgumentExceptionWithFormattedMessage()
    {
        StringSegment segment = new("hello");

        await Assert.That(() => segment.CapitalizeChars([999]))
            .ThrowsExactly<ArgumentException>()
            .WithMessageContaining("999");
    }

    #endregion
}
