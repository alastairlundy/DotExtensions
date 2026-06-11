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

#if NET8_0_OR_GREATER
using System;
using DotExtensions.Dates;

namespace DotExtensions.Tests.Dates;

public class DateOnlyToDateTimeExtensionsTests
{
    [Test]
    [Arguments(2024, 6, 15)]
    [Arguments(2023, 1, 1)]
    [Arguments(2025, 12, 31)]
    [Arguments(2000, 2, 29)]
    public async Task ToDateTime_VariousDates_ReturnsCorrectDate(int year, int month, int day)
    {
        DateOnly date = new(year, month, day);
        DateTime result = DateOnly.ToDateTime(date);
        await Assert.That(result.Year).IsEqualTo(year);
        await Assert.That(result.Month).IsEqualTo(month);
        await Assert.That(result.Day).IsEqualTo(day);
    }

    [Test]
    public async Task ToDateTime_DateOnly_ReturnsDateTimeAtMidnight()
    {
        DateOnly date = new(2024, 6, 15);
        DateTime result = DateOnly.ToDateTime(date);
        await Assert.That(result.TimeOfDay).IsEqualTo(TimeSpan.Zero);
    }

    [Test]
    public async Task ToDateTime_MinValue_ReturnsCorrectDateTime()
    {
        DateOnly date = DateOnly.MinValue;
        DateTime result = DateOnly.ToDateTime(date);
        await Assert.That(result.Year).IsEqualTo(1);
        await Assert.That(result.Month).IsEqualTo(1);
        await Assert.That(result.Day).IsEqualTo(1);
    }

    [Test]
    public async Task ToDateTime_MaxValue_ReturnsCorrectDateTime()
    {
        DateOnly date = DateOnly.MaxValue;
        DateTime result = DateOnly.ToDateTime(date);
        await Assert.That(result.Year).IsEqualTo(9999);
        await Assert.That(result.Month).IsEqualTo(12);
        await Assert.That(result.Day).IsEqualTo(31);
    }
}
#endif
