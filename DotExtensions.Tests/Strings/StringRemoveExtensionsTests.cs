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
using DotExtensions.Strings;

namespace DotExtensions.Tests.Strings;

/// <summary>
/// Comprehensive invariant tests for <see cref="StringRemoveExtensions"/>.
/// These tests assert the documented contract per XML doc comments (D006) using
/// TUnit <c>[Arguments]</c> parameterization (T003) with ASCII or stable
/// Unicode inputs to avoid culture-sensitive flakiness.
/// </summary>
public class StringRemoveExtensionsTests
{
    // -----------------------------------------------------------------------
    // RemoveAll: edge cases
    // -----------------------------------------------------------------------

    [Test]
    [Arguments(null, "x")]
    [Arguments("", "x")]
    [Arguments("hello", null)]
    [Arguments("hello", "")]
    public async Task RemoveAll_InvalidInputs_ThrowArgumentException(string? source, string? value)
    {
        await Assert.That(() => source!.RemoveAll(value!, StringComparison.Ordinal))
            .Throws<ArgumentException>();
    }

    [Test]
    [Arguments("abc", "abcdef")]
    [Arguments("ab", "abc")]
    [Arguments("a", "aa")]
    public async Task RemoveAll_ValueLongerThanSource_ReturnsOriginalUnchanged(string source, string value)
    {
        string result = source.RemoveAll(value, StringComparison.Ordinal);

        await Assert.That(result).IsEqualTo(source);
    }

    [Test]
    [Arguments("hello world", "xyz")]
    [Arguments("foo bar baz", "qux")]
    [Arguments("aaaaa", "bbbb")]
    public async Task RemoveAll_ValueNotPresent_ReturnsOriginalUnchanged(string source, string value)
    {
        string result = source.RemoveAll(value, StringComparison.Ordinal);

        await Assert.That(result).IsEqualTo(source);
    }

    [Test]
    [Arguments("aaaaa", "a", "")]
    [Arguments("abcabc", "abc", "")]
    [Arguments("xyzxyzxyz", "xyz", "")]
    public async Task RemoveAll_OnlyOccurrences_ReturnsEmptyString(string source, string value, string expected)
    {
        string result = source.RemoveAll(value, StringComparison.Ordinal);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments("abc", "abc", "")]
    [Arguments("hello", "hello", "")]
    [Arguments("xxxxx", "xxxxx", "")]
    public async Task RemoveAll_ValueEqualsEntireSource_ReturnsEmptyString(string source, string value, string expected)
    {
        string result = source.RemoveAll(value, StringComparison.Ordinal);

        await Assert.That(result).IsEqualTo(expected);
    }

    // -----------------------------------------------------------------------
    // RemoveAll: parameterized output tests (short/medium/long, single/multi,
    // leading/trailing/middle occurrences)
    // -----------------------------------------------------------------------

    [Test]
    [Arguments("hello world", "world", "hello ")]
    [Arguments("hello world", "hello", " world")]
    [Arguments("hello world", "o", "hell wrld")]
    [Arguments("hello world", "l", "heo word")]
    [Arguments("aXbXcXd", "X", "abcd")]
    [Arguments("abcXYZdef", "XYZ", "abcdef")]
    [Arguments("fooXXbarXXbaz", "XX", "foobarbaz")]
    [Arguments("ababab", "ab", "")]
    [Arguments("Mississippi", "ss", "Miiippi")]
    [Arguments("aabbcc", "b", "aacc")]
    public async Task RemoveAll_Ordinal_ReturnsExpected(string source, string value, string expected)
    {
        string result = source.RemoveAll(value, StringComparison.Ordinal);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments("Hello World", "hello", StringComparison.OrdinalIgnoreCase, " World")]
    [Arguments("ABCABC", "abc", StringComparison.OrdinalIgnoreCase, "")]
    [Arguments("FooBar", "FOO", StringComparison.OrdinalIgnoreCase, "Bar")]
    [Arguments("FOOFOOFOO", "foo", StringComparison.OrdinalIgnoreCase, "")]
    public async Task RemoveAll_CaseInsensitive_RemovesAllOccurrences(
        string source, string value, StringComparison comparison, string expected)
    {
        string result = source.RemoveAll(value, comparison);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments("Hello World", "hello", StringComparison.Ordinal, "Hello World")]
    [Arguments("ABCABC", "abc", StringComparison.Ordinal, "ABCABC")]
    [Arguments("FooBar", "FOO", StringComparison.Ordinal, "FooBar")]
    public async Task RemoveAll_CaseSensitive_DoesNotRemoveDifferentCase(
        string source, string value, StringComparison comparison, string expected)
    {
        string result = source.RemoveAll(value, comparison);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments("hello world", "world", StringComparison.CurrentCulture, "hello ")]
    [Arguments("Hello World", "hello", StringComparison.CurrentCultureIgnoreCase, " World")]
    [Arguments("hello world", "world", StringComparison.InvariantCulture, "hello ")]
    [Arguments("Hello World", "hello", StringComparison.InvariantCultureIgnoreCase, " World")]
    public async Task RemoveAll_AllCultureComparisons_BehaveConsistentlyWithOrdinal(
        string source, string value, StringComparison comparison, string expected)
    {
        // For ASCII inputs culture-aware comparisons must produce the same result
        // as ordinal comparisons. This guards against regressions where
        // culture-specific behaviour is unintentionally introduced.
        string result = source.RemoveAll(value, comparison);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments("aabaabaab", "aab", "")]
    [Arguments("abcabcabc", "abcabc", "abc")]
    [Arguments("aaaaa", "aa", "a")]
    [Arguments("xxxxxx", "xxx", "")]
    public async Task RemoveAll_OverlappingValues_StillRemovesAllNonOverlappingOccurrences(
        string source, string value, string expected)
    {
        // "RemoveAll" is defined to find non-overlapping occurrences from left to
        // right. This test pins that semantic so it cannot silently change.
        string result = source.RemoveAll(value, StringComparison.Ordinal);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments("héllo wörld", "héllo", " wörld")]
    [Arguments("élève élève élève", "élève", "  ")]
    [Arguments("naïve naïve", "naïve", " ")]
    public async Task RemoveAll_StableUnicode_RemovesAllOccurrences(string source, string value, string expected)
    {
        // Stable Unicode (NFC normalised) inputs to keep comparisons deterministic
        // regardless of the host culture or default ICU data.
        string result = source.RemoveAll(value, StringComparison.Ordinal);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task RemoveAll_LongString_RepeatedOccurrences()
    {
        string value = "abc";
        string source = string.Concat(Enumerable.Repeat("abc", 1_000));
        string expected = string.Empty;

        string result = source.RemoveAll(value, StringComparison.Ordinal);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task RemoveAll_LongString_NoOccurrences()
    {
        string value = "xyz";
        string source = string.Concat(Enumerable.Repeat("abc", 1_000));

        string result = source.RemoveAll(value, StringComparison.Ordinal);

        await Assert.That(result).IsEqualTo(source);
    }

    [Test]
    public async Task RemoveAll_LongString_SingleOccurrence()
    {
        string value = "needle";
        string source = string.Concat(Enumerable.Repeat("haystack ", 1_000)) + value;

        string result = source.RemoveAll(value, StringComparison.Ordinal);

        string expected = string.Concat(Enumerable.Repeat("haystack ", 1_000));
        await Assert.That(result).IsEqualTo(expected);
    }

    // -----------------------------------------------------------------------
    // RemoveFirst: edge cases
    // -----------------------------------------------------------------------

    [Test]
    [Arguments(null, "x")]
    [Arguments("", "x")]
    [Arguments("hello", null)]
    [Arguments("hello", "")]
    public async Task RemoveFirst_InvalidInputs_ThrowArgumentException(string? source, string? value)
    {
        await Assert.That(() => source!.RemoveFirst(value!, StringComparison.Ordinal))
            .Throws<ArgumentException>();
    }

    [Test]
    [Arguments("abc", "abcdef")]
    [Arguments("ab", "abc")]
    public async Task RemoveFirst_ValueLongerThanSource_ThrowsArgumentException(string source, string value)
    {
        await Assert.That(() => source.RemoveFirst(value, StringComparison.Ordinal))
            .Throws<ArgumentException>();
    }

    [Test]
    [Arguments("hello world", "xyz")]
    [Arguments("foo bar", "qux")]
    public async Task RemoveFirst_ValueNotPresent_ThrowsArgumentException(string source, string value)
    {
        await Assert.That(() => source.RemoveFirst(value, StringComparison.Ordinal))
            .Throws<ArgumentException>();
    }

    [Test]
    [Arguments("hello world", "world", "hello ")]
    [Arguments("hello world", "hello", " world")]
    [Arguments("hello world", "o", "hell world")]
    [Arguments("abcabc", "abc", "abc")]
    [Arguments("Mississippi", "ss", "Miissippi")]
    [Arguments("foo bar foo", "foo", " bar foo")]
    public async Task RemoveFirst_Ordinal_RemovesOnlyFirstOccurrence(
        string source, string value, string expected)
    {
        string result = source.RemoveFirst(value, StringComparison.Ordinal);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments("Hello World", "hello", StringComparison.OrdinalIgnoreCase, " World")]
    [Arguments("FOObar", "foo", StringComparison.OrdinalIgnoreCase, "bar")]
    [Arguments("FOOFOO", "foo", StringComparison.OrdinalIgnoreCase, "FOO")]
    public async Task RemoveFirst_CaseInsensitive_RemovesFirstOccurrence(
        string source, string value, StringComparison comparison, string expected)
    {
        string result = source.RemoveFirst(value, comparison);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task RemoveFirst_CaseSensitive_DoesNotMatchDifferentCase()
    {
        const string source = "Hello World";
        const string value = "hello";

        await Assert.That(() => source.RemoveFirst(value, StringComparison.Ordinal))
            .Throws<ArgumentException>();
    }

    [Test]
    [Arguments("abc", "abc", "")]
    [Arguments("hello", "hello", "")]
    public async Task RemoveFirst_ValueEqualsEntireSource_ReturnsEmptyString(
        string source, string value, string expected)
    {
        string result = source.RemoveFirst(value, StringComparison.Ordinal);

        await Assert.That(result).IsEqualTo(expected);
    }

    // -----------------------------------------------------------------------
    // RemoveLast: edge cases
    // -----------------------------------------------------------------------

    [Test]
    [Arguments(null, "x")]
    [Arguments("", "x")]
    [Arguments("hello", null)]
    [Arguments("hello", "")]
    public async Task RemoveLast_InvalidInputs_ThrowArgumentException(string? source, string? value)
    {
        await Assert.That(() => source!.RemoveLast(value!, StringComparison.Ordinal))
            .Throws<ArgumentException>();
    }

    [Test]
    [Arguments("abc", "abcdef")]
    [Arguments("ab", "abc")]
    public async Task RemoveLast_ValueLongerThanSource_ThrowsArgumentException(string source, string value)
    {
        await Assert.That(() => source.RemoveLast(value, StringComparison.Ordinal))
            .Throws<ArgumentException>();
    }

    [Test]
    [Arguments("hello world", "xyz")]
    [Arguments("foo bar", "qux")]
    public async Task RemoveLast_ValueNotPresent_ThrowsArgumentException(string source, string value)
    {
        await Assert.That(() => source.RemoveLast(value, StringComparison.Ordinal))
            .Throws<ArgumentException>();
    }

    [Test]
    [Arguments("hello world", "world", "hello ")]
    [Arguments("hello world", "hello", " world")]
    [Arguments("hello world", "o", "hello wrld")]
    [Arguments("abcabc", "abc", "abc")]
    [Arguments("Mississippi", "ss", "Missiippi")]
    [Arguments("foo bar foo", "foo", "foo bar ")]
    public async Task RemoveLast_Ordinal_RemovesOnlyLastOccurrence(
        string source, string value, string expected)
    {
        string result = source.RemoveLast(value, StringComparison.Ordinal);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments("Hello World", "hello", StringComparison.OrdinalIgnoreCase, " World")]
    [Arguments("FOObar", "foo", StringComparison.OrdinalIgnoreCase, "bar")]
    [Arguments("FOOFOO", "foo", StringComparison.OrdinalIgnoreCase, "FOO")]
    public async Task RemoveLast_CaseInsensitive_RemovesLastOccurrence(
        string source, string value, StringComparison comparison, string expected)
    {
        string result = source.RemoveLast(value, comparison);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task RemoveLast_CaseSensitive_DoesNotMatchDifferentCase()
    {
        const string source = "Hello World";
        const string value = "hello";

        await Assert.That(() => source.RemoveLast(value, StringComparison.Ordinal))
            .Throws<ArgumentException>();
    }

    [Test]
    [Arguments("abc", "abc", "")]
    [Arguments("hello", "hello", "")]
    public async Task RemoveLast_ValueEqualsEntireSource_ReturnsEmptyString(
        string source, string value, string expected)
    {
        string result = source.RemoveLast(value, StringComparison.Ordinal);

        await Assert.That(result).IsEqualTo(expected);
    }

    // -----------------------------------------------------------------------
    // Cross-method: removing the same value should produce a consistent
    // result when only one occurrence exists, regardless of which method
    // is used. This guards against subtle contract drift between methods.
    // -----------------------------------------------------------------------

    [Test]
    [Arguments("hello world", "world")]
    [Arguments("foo bar baz", "bar")]
    [Arguments("abcabcabc", "abc")]
    public async Task RemoveFirstAndRemoveLast_Agree_WhenOnlyOneOccurrence(string source, string value)
    {
        string first = source.RemoveFirst(value, StringComparison.Ordinal);
        string last = source.RemoveLast(value, StringComparison.Ordinal);

        await Assert.That(first).IsEqualTo(last);
    }
}
