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

public class DateTimeAddExtensionsTests
{
    [Test]
    public async Task Add_EqualDateTimes_ReturnsSameDateTime()
    {
        DateTime dt = new(2024, 6, 15, 10, 30, 0);

        DateTime result = dt.Add(dt);

        await Assert.That(result).IsEqualTo(dt);
    }

    [Test]
    [Arguments("2024-01-01", "2024-06-15")]
    [Arguments("2020-01-01", "2024-01-01")]
    [Arguments("2024-12-31", "2025-01-01")]
    [Arguments("2024-01-01 00:00:00", "2024-01-01 12:30:45")]
    public async Task Add_SecondDateTimeLater_ReturnsSecondDateTime(string earlierStr, string laterStr)
    {
        DateTime earlier = DateTime.Parse(earlierStr);
        DateTime later = DateTime.Parse(laterStr);

        DateTime result = earlier.Add(later);

        await Assert.That(result).IsEqualTo(later);
    }

    [Test]
    [Arguments("2024-06-15", "2024-01-01")]
    [Arguments("2024-01-01", "2020-01-01")]
    [Arguments("2025-01-01", "2024-12-31")]
    [Arguments("2024-01-01 12:30:45", "2024-01-01 00:00:00")]
    public async Task Add_SecondDateTimeEarlier_ReturnsCorrectDateTime(string dt1Str, string dt2Str)
    {
        DateTime dt1 = DateTime.Parse(dt1Str);
        DateTime dt2 = DateTime.Parse(dt2Str);

        DateTime result = dt1.Add(dt2);

        long expectedTicks = dt1.Ticks + Math.Abs(dt1.Ticks - dt2.Ticks);
        DateTime expected = new(expectedTicks);

        await Assert.That(result).IsEqualTo(expected);
    }
}
