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
using DotExtensions.Memory;

namespace DotExtensions.Tests.Memory;

public class IsEmptyOrWhiteSpaceExtensionsTests
{
    [Test]
    public async Task IsEmptyOrWhiteSpace_SpanOfStrings_AllWhiteSpace_ReturnsTrue()
    {
        string[] values = [" ", "\t"];

        bool actual = Span<string>.IsEmptyOrWhiteSpace((Span<string>)values);

        await Assert.That(actual).IsTrue();
    }

    [Test]
    public async Task IsEmptyOrWhiteSpace_SpanOfStrings_NullElement_ReturnsFalseWithoutThrowing()
    {
        string[] values = [" ", null];

        bool actual = Span<string>.IsEmptyOrWhiteSpace((Span<string>)values);

        await Assert.That(actual).IsFalse();
    }

    [Test]
    public async Task IsEmptyOrWhiteSpace_ReadOnlySpanOfStrings_NullElement_ReturnsFalseWithoutThrowing()
    {
        string[] values = [null];

        bool actual = ReadOnlySpan<string>.IsEmptyOrWhiteSpace(values);

        await Assert.That(actual).IsFalse();
    }

    [Test]
    public async Task IsEmptyOrWhiteSpace_MemoryOfStrings_NullElement_ReturnsFalseWithoutThrowing()
    {
        string[] values = [null];

        bool actual = Memory<string>.IsEmptyOrWhiteSpace(values);

        await Assert.That(actual).IsFalse();
    }

    [Test]
    public async Task IsEmptyOrWhiteSpace_ReadOnlyMemoryOfStrings_AllWhiteSpace_ReturnsTrue()
    {
        string[] values = [" ", ""];

        bool actual = ReadOnlyMemory<string>.IsEmptyOrWhiteSpace(values);

        await Assert.That(actual).IsTrue();
    }
}
