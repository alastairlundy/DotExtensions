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

public class StringReplaceLastTests
{
    [Test]
    public async Task ReplaceLast_EmptyNewValue_RemovesLastOccurrence()
    {
        string actual = "foo bar foo".ReplaceLast("foo", "");

        await Assert.That(actual)
            .IsEqualTo("foo bar ");
    }

    [Test]
    public async Task ReplaceFirst_EmptyNewValue_RemovesFirstOccurrence()
    {
        string actual = "foo bar foo".ReplaceFirst("foo", "");

        await Assert.That(actual)
            .IsEqualTo(" bar foo");
    }

    [Test]
    public async Task ReplaceLast_WithNewValue_ReplacesLastOccurrence()
    {
        string actual = "foo bar foo".ReplaceLast("foo", "baz");

        await Assert.That(actual)
            .IsEqualTo("foo bar baz");
    }

    [Test]
    public async Task ReplaceFirst_WithNewValue_ReplacesFirstOccurrence()
    {
        string actual = "foo bar foo".ReplaceFirst("foo", "baz");

        await Assert.That(actual)
            .IsEqualTo("baz bar foo");
    }

    [Test]
    public async Task ReplaceLast_NullSource_ThrowsArgumentNullException()
    {
        string? source = null;

        await Assert.That(() => source!.ReplaceLast("x", "y"))
            .Throws<ArgumentNullException>();
    }

    [Test]
    public async Task ReplaceFirst_NullSource_ThrowsArgumentNullException()
    {
        string? source = null;

        await Assert.That(() => source!.ReplaceFirst("x", "y"))
            .Throws<ArgumentNullException>();
    }

    [Test]
    public async Task ReplaceLast_NullOldValue_ThrowsArgumentException()
    {
        await Assert.That(() => "hello".ReplaceLast(null!, "y"))
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task ReplaceLast_EmptyOldValue_ThrowsArgumentException()
    {
        await Assert.That(() => "hello".ReplaceLast("", "y"))
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task ReplaceFirst_NullOldValue_ThrowsArgumentException()
    {
        await Assert.That(() => "hello".ReplaceFirst(null!, "y"))
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task ReplaceFirst_EmptyOldValue_ThrowsArgumentException()
    {
        await Assert.That(() => "hello".ReplaceFirst("", "y"))
            .Throws<ArgumentException>();
    }
}
