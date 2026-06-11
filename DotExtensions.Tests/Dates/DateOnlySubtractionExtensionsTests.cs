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

public class DateOnlySubtractionExtensionsTests
{
    [Test]
    public async Task SubtractDays_ZeroDays_ReturnsSameDate()
    {
        DateOnly date = new(2024, 6, 15);
        DateOnly result = date.SubtractDays(0);
        await Assert.That(result).IsEqualTo(date);
    }

    [Test]
    [Arguments(1)]
    [Arguments(5)]
    [Arguments(30)]
    [Arguments(365)]
    public async Task SubtractDays_PositiveDays_ReturnsCorrectDate(int days)
    {
        DateOnly date = new(2024, 6, 15);
        DateOnly result = date.SubtractDays(days);
        DateOnly expected = DateOnly.FromDateTime(new DateTime(2024, 6, 15).AddDays(-days));
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(1)]
    [Arguments(5)]
    [Arguments(30)]
    public async Task SubtractDays_NegativeDays_ReturnsLaterDate(int days)
    {
        DateOnly date = new(2024, 6, 15);
        DateOnly result = date.SubtractDays(-days);
        DateOnly expected = DateOnly.FromDateTime(new DateTime(2024, 6, 15).AddDays(days));
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task SubtractDays_CrossingMonthBoundary_ReturnsCorrectDate()
    {
        DateOnly date = new(2024, 3, 1);
        DateOnly result = date.SubtractDays(1);
        await Assert.That(result).IsEqualTo(new DateOnly(2024, 2, 29));
    }

    [Test]
    public async Task SubtractDays_CrossingYearBoundary_ReturnsCorrectDate()
    {
        DateOnly date = new(2024, 1, 1);
        DateOnly result = date.SubtractDays(1);
        await Assert.That(result).IsEqualTo(new DateOnly(2023, 12, 31));
    }

    [Test]
    public async Task SubtractMonths_ZeroMonths_ReturnsSameDate()
    {
        DateOnly date = new(2024, 6, 15);
        DateOnly result = date.SubtractMonths(0);
        await Assert.That(result).IsEqualTo(date);
    }

    [Test]
    [Arguments(1)]
    [Arguments(3)]
    [Arguments(6)]
    [Arguments(12)]
    public async Task SubtractMonths_WholeMonths_ReturnsCorrectDate(int months)
    {
        DateOnly date = new(2024, 6, 15);
        DateOnly result = date.SubtractMonths(months);
        DateTime dateTime = new(2024, 6, 15);
        DateOnly expected = DateOnly.FromDateTime(dateTime.AddMonths(-months));
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task SubtractMonths_CrossingYearBoundary_ReturnsCorrectDate()
    {
        DateOnly date = new(2024, 1, 15);
        DateOnly result = date.SubtractMonths(1);
        await Assert.That(result).IsEqualTo(new DateOnly(2023, 12, 15));
    }

    [Test]
    public async Task SubtractMonths_NegativeMonths_ReturnsLaterDate()
    {
        DateOnly date = new(2024, 1, 15);
        DateOnly result = date.SubtractMonths(-1);
        await Assert.That(result).IsEqualTo(new DateOnly(2024, 2, 15));
    }

    [Test]
    public async Task SubtractMonths_NegativeMonthsCrossingYearBoundary_ReturnsCorrectDate()
    {
        DateOnly date = new(2023, 12, 15);
        DateOnly result = date.SubtractMonths(-1);
        await Assert.That(result).IsEqualTo(new DateOnly(2024, 1, 15));
    }

    [Test]
    public async Task SubtractMonths_MonthEndClamping_LeapYear_ReturnsCorrectDate()
    {
        DateOnly date = new(2024, 3, 31);
        DateOnly result = date.SubtractMonths(1);
        await Assert.That(result).IsEqualTo(new DateOnly(2024, 2, 29));
    }

    [Test]
    public async Task SubtractMonths_MonthEndClamping_NonLeapYear_ReturnsCorrectDate()
    {
        DateOnly date = new(2023, 3, 31);
        DateOnly result = date.SubtractMonths(1);
        await Assert.That(result).IsEqualTo(new DateOnly(2023, 2, 28));
    }

    [Test]
    public async Task SubtractYears_ZeroYears_ReturnsSameDate()
    {
        DateOnly date = new(2024, 6, 15);
        DateOnly result = date.SubtractYears(0);
        await Assert.That(result).IsEqualTo(date);
    }

    [Test]
    [Arguments(1)]
    [Arguments(5)]
    [Arguments(10)]
    public async Task SubtractYears_WholeYears_ReturnsCorrectDate(int years)
    {
        DateOnly date = new(2024, 6, 15);
        DateOnly result = date.SubtractYears(years);
        DateTime dateTime = new(2024, 6, 15);
        DateOnly expected = DateOnly.FromDateTime(dateTime.AddYears(-years));
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task SubtractYears_NegativeYears_ReturnsLaterDate()
    {
        DateOnly date = new(2024, 6, 15);
        DateOnly result = date.SubtractYears(-1);
        await Assert.That(result).IsEqualTo(new DateOnly(2025, 6, 15));
    }

    [Test]
    public async Task SubtractYears_LeapYearToNonLeapYear_ClampsToFebruary28()
    {
        DateOnly date = new(2024, 2, 29);
        DateOnly result = date.SubtractYears(1);
        await Assert.That(result).IsEqualTo(new DateOnly(2023, 2, 28));
    }

    [Test]
    public async Task SubtractYears_LeapYearToLeapYear_ReturnsFebruary29()
    {
        DateOnly date = new(2024, 2, 29);
        DateOnly result = date.SubtractYears(4);
        await Assert.That(result).IsEqualTo(new DateOnly(2020, 2, 29));
    }

    [Test]
    public async Task SubtractYears_NegativeYearsLeapYear_ClampsToFebruary28()
    {
        DateOnly date = new(2023, 2, 28);
        DateOnly result = date.SubtractYears(-1);
        await Assert.That(result).IsEqualTo(new DateOnly(2024, 2, 28));
    }
}
#endif
