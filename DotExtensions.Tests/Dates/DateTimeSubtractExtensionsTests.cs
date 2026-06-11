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
using DotExtensions.Dates;

namespace DotExtensions.Tests.Dates;

public class DateTimeSubtractExtensionsTests
{
    [Test]
    public async Task Subtract_EqualDateTimes_ReturnsSameDateTime()
    {
        DateTime dt = new(2024, 6, 15, 10, 30, 0);

        DateTime result = DateTimeSubtractExtensions.Subtract(dt, dt);

        await Assert.That(result).IsEqualTo(dt);
    }

    [Test]
    [Arguments("2024-06-15", "2024-01-01")]
    [Arguments("2024-01-01", "2020-01-01")]
    [Arguments("2025-01-01", "2024-12-31")]
    [Arguments("2024-01-01 12:30:45", "2024-01-01 00:00:00")]
    public async Task Subtract_VariousDatePairs_ReturnsCorrectDateTime(string dt1Str, string dt2Str)
    {
        DateTime dt1 = DateTime.Parse(dt1Str);
        DateTime dt2 = DateTime.Parse(dt2Str);

        DateTime result = DateTimeSubtractExtensions.Subtract(dt1, dt2);

        long expectedTicks = 2 * dt1.Ticks - dt2.Ticks;
        DateTime expected = new(expectedTicks);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(1)]
    [Arguments(3)]
    [Arguments(6)]
    [Arguments(12)]
    [Arguments(24)]
    public async Task SubtractMonths_WholeMonths_ReturnsCorrectDateTime(int months)
    {
        DateTime dt = new(2024, 6, 15);

        DateTime result = dt.SubtractMonths(months);

        DateTime expected = dt.AddMonths(-months);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task SubtractMonths_CrossingYearBoundary_ReturnsCorrectDateTime()
    {
        DateTime dt = new(2024, 1, 15);

        DateTime result = dt.SubtractMonths(1);

        await Assert.That(result).IsEqualTo(new DateTime(2023, 12, 15));
    }

    [Test]
    public async Task SubtractMonths_MonthEndClamping_ReturnsCorrectDateTime()
    {
        DateTime dt = new(2024, 3, 31);

        DateTime result = dt.SubtractMonths(1);

        await Assert.That(result).IsEqualTo(new DateTime(2024, 2, 29));
    }

    [Test]
    public async Task SubtractMonths_MonthEndClamping_NonLeapYear_ReturnsCorrectDateTime()
    {
        DateTime dt = new(2023, 3, 31);

        DateTime result = dt.SubtractMonths(1);

        await Assert.That(result).IsEqualTo(new DateTime(2023, 2, 28));
    }

    [Test]
    [Arguments(1)]
    [Arguments(2)]
    [Arguments(5)]
    [Arguments(10)]
    public async Task SubtractYears_WholeYears_ReturnsCorrectDateTime(int years)
    {
        DateTime dt = new(2024, 6, 15);

        DateTime result = dt.SubtractYears(years);

        DateTime expected = dt.AddYears(-years);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task SubtractYears_LeapYearToNonLeapYear_ClampsToFebruary28()
    {
        DateTime dt = new(2024, 2, 29);

        DateTime result = dt.SubtractYears(1);

        await Assert.That(result).IsEqualTo(new DateTime(2023, 2, 28));
    }

    [Test]
    public async Task SubtractYears_LeapYearToLeapYear_ReturnsFebruary29()
    {
        DateTime dt = new(2024, 2, 29);

        DateTime result = dt.SubtractYears(4);

        await Assert.That(result).IsEqualTo(new DateTime(2020, 2, 29));
    }

    [Test]
    public async Task SubtractYears_ToEraBoundary_ReturnsCorrectDateTime()
    {
        DateTime dt = new(2, 1, 1);

        DateTime result = dt.SubtractYears(1);

        await Assert.That(result).IsEqualTo(new DateTime(1, 1, 1));
    }
}
