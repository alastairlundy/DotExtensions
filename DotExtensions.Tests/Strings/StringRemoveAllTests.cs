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

public class StringRemoveAllTests
{
    [Test]
    [Arguments("hello world", "xyz")]
    [Arguments("abc", "abcdef")]
    public async Task RemoveAll_ValueNotFound_ReturnsOriginal(string input, string value)
    {
        string result = input.RemoveAll(value, StringComparison.Ordinal);

        await Assert.That(result).IsEqualTo(input);
    }

    [Test]
    [Arguments("hello world", "world", "hello ")]
    [Arguments("hello world", "hello", " world")]
    [Arguments("abcXYZdef", "XYZ", "abcdef")]
    [Arguments("aaa", "a", "")]
    [Arguments("abcabc", "abc", "")]
    public async Task RemoveAll_SingleOccurrence_RemovesCorrectly(string input, string value, string expected)
    {
        string result = input.RemoveAll(value, StringComparison.Ordinal);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments("ababab", "ab", "")]
    [Arguments("aXbXcXd", "X", "abcd")]
    [Arguments("fooXXbarXXbaz", "XX", "foobarbaz")]
    public async Task RemoveAll_MultipleOccurrences_RemovesAll(string input, string value, string expected)
    {
        string result = input.RemoveAll(value, StringComparison.Ordinal);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments("Hello World", "hello", StringComparison.OrdinalIgnoreCase, " World")]
    [Arguments("ABCABC", "abc", StringComparison.OrdinalIgnoreCase, "")]
    public async Task RemoveAll_CaseInsensitiveComparison_RemovesCorrectly(
        string input, string value, StringComparison comparison, string expected)
    {
        string result = input.RemoveAll(value, comparison);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task RemoveAll_NullSource_ThrowsArgumentException()
    {
        string? source = null;

        await Assert.That(() => source!.RemoveAll("x", StringComparison.Ordinal))
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task RemoveAll_EmptySource_ThrowsArgumentException()
    {
        await Assert.That(() => "".RemoveAll("x", StringComparison.Ordinal))
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task RemoveAll_NullValue_ThrowsArgumentException()
    {
        await Assert.That(() => "hello".RemoveAll(null!, StringComparison.Ordinal))
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task RemoveAll_EmptyValue_ThrowsArgumentException()
    {
        await Assert.That(() => "hello".RemoveAll("", StringComparison.Ordinal))
            .Throws<ArgumentException>();
    }
}
