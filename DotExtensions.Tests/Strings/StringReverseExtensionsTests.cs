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

using DotExtensions.Strings;

namespace DotExtensions.Tests.Strings;

public class StringReverseExtensionsTests
{
    [Test]
    public async Task Reverse_PlainAscii()
    {
        string actual = "Hello".Reverse();

        await Assert.That(actual).IsEqualTo("olleH");
    }

    [Test]
    public async Task Reverse_SurrogatePair_PreservedAsUnit()
    {
        // "a" followed by U+10405 (Deseret Small Letter Long I), encoded as a surrogate pair.
        string input = "a\uD801\uDC05";
        string expected = "\uD801\uDC05" + "a";

        string actual = input.Reverse();

        await Assert.That(actual).IsEqualTo(expected);
        await Assert.That(actual.Length).IsEqualTo(3);
        await Assert.That(char.IsHighSurrogate(actual[0])).IsTrue();
        await Assert.That(char.IsLowSurrogate(actual[1])).IsTrue();
    }

    [Test]
    public async Task Reverse_MultipleSurrogatePairs_PreservedAsUnits()
    {
        // U+1F600 (grinning face) followed by U+10405, then "z".
        string input = "😀𐐅z";
        string expected = "z𐐅😀";

        string actual = input.Reverse();

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task Reverse_SingleChar_ReturnsSameChar()
    {
        string actual = "a".Reverse();

        await Assert.That(actual).IsEqualTo("a");
    }
}
