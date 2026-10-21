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

using DotExtensions.MsExtensions.Primitives;
using Microsoft.Extensions.Primitives;

namespace DotExtensions.Tests.MsExtensions;

/// <summary>
/// Deterministic tests for <see cref="SegmentContainsExtensions"/>.
/// A needle whose first character occurs near the end of the source must not run past the
/// end of the segment, and an empty needle is always contained (matching
/// <c>string.Contains(string)</c> behaviour).
/// </summary>
public class SegmentContainsExtensionsTests
{
    [Test]
    public async Task Contains_FirstCharCandidateNearEnd_DoesNotThrowAndReturnsFalse()
    {
        // "abcb" has no "ba", but its last 'b' is too close to the end to hold a
        // two-character window; the search must stop instead of slicing past the end.
        StringSegment source = "abcb";

        bool result = source.Contains("ba");

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Contains_CandidateWindowRunsPastEnd_ReturnsFalse()
    {
        StringSegment source = "abb";

        bool result = source.Contains("ba");

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Contains_SubstringAtEnd_ReturnsTrue()
    {
        StringSegment source = "abc";

        bool result = source.Contains("bc");

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_SubstringAtStart_ReturnsTrue()
    {
        StringSegment source = "abc";

        bool result = source.Contains("ab");

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_SubstringEndingAtLastCharacter_ReturnsTrue()
    {
        // The whole match window sits at the very end of the source.
        StringSegment source = "abcb";

        bool result = source.Contains("cb");

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_AbsentSubstring_ReturnsFalse()
    {
        // "abc" does not contain "ba" (agrees with BCL string.Contains(string)).
        StringSegment source = "abc";

        bool result = source.Contains("ba");

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Contains_EmptyNeedleInNonEmptySource_ReturnsTrue()
    {
        StringSegment source = "abc";

        bool result = source.Contains("");

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_EmptyNeedleInEmptySource_ReturnsTrue()
    {
        StringSegment source = "";

        bool result = source.Contains("");

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_PresentCharacter_ReturnsTrue()
    {
        StringSegment source = "abc";

        bool result = source.Contains('b');

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_AbsentCharacter_ReturnsFalse()
    {
        StringSegment source = "abc";

        bool result = source.Contains('z');

        await Assert.That(result).IsFalse();
    }
}
