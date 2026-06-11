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

public class TimeSpanDifferenceExtensionsTests
{
    [Test]
    [Arguments(2024, 6, 15, 12, 30, 0, 2024, 6, 10, 8, 15, 0)]
    [Arguments(2023, 1, 1, 0, 0, 0, 2024, 1, 1, 0, 0, 0)]
    [Arguments(2025, 12, 31, 23, 59, 59, 2025, 12, 31, 0, 0, 0)]
    public async Task Difference_DateTime_ReturnsAbsoluteDifference(int y1, int mo1, int d1, int h1, int mi1, int s1, int y2, int mo2, int d2, int h2, int mi2, int s2)
    {
        DateTime dt1 = new(y1, mo1, d1, h1, mi1, s1);
        DateTime dt2 = new(y2, mo2, d2, h2, mi2, s2);
        TimeSpan expected = dt1 - dt2;
        if (expected < TimeSpan.Zero)
        {
            expected = expected.Negate();
        }

        TimeSpan result = dt1.Difference(dt2);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(2024, 6, 15, 12, 30, 0, 2024, 6, 10, 8, 15, 0)]
    [Arguments(2023, 1, 1, 0, 0, 0, 2024, 1, 1, 0, 0, 0)]
    [Arguments(2025, 12, 31, 23, 59, 59, 2025, 12, 31, 0, 0, 0)]
    public async Task Difference_DateTime_ReturnsAbsoluteDifferenceRegardlessOfOperandOrder(int y1, int mo1, int d1, int h1, int mi1, int s1, int y2, int mo2, int d2, int h2, int mi2, int s2)
    {
        DateTime dt1 = new(y1, mo1, d1, h1, mi1, s1);
        DateTime dt2 = new(y2, mo2, d2, h2, mi2, s2);

        TimeSpan forward = dt1.Difference(dt2);
        TimeSpan reverse = dt2.Difference(dt1);

        await Assert.That(forward).IsEqualTo(reverse);
    }

    [Test]
    public async Task Difference_DateTime_SameValue_ReturnsZero()
    {
        DateTime dt = new(2024, 6, 15, 12, 0, 0);

        TimeSpan result = dt.Difference(dt);

        await Assert.That(result).IsEqualTo(TimeSpan.Zero);
    }

    [Test]
    public async Task Difference_DateTime_LargeDateRange_ReturnsCorrectDifference()
    {
        DateTime dt1 = new(1, 1, 1);
        DateTime dt2 = new(9999, 12, 31);

        TimeSpan result = dt1.Difference(dt2);

        TimeSpan expected = dt2 - dt1;
        await Assert.That(result).IsEqualTo(expected);
    }

#if NET8_0_OR_GREATER
    [Test]
    [Arguments(10, 30, 0, 8, 15, 0)]
    [Arguments(23, 59, 59, 0, 0, 0)]
    [Arguments(0, 0, 0, 23, 59, 59)]
    [Arguments(12, 0, 0, 12, 30, 0)]
    public async Task Difference_TimeOnly_ReturnsAbsoluteDifference(int h1, int m1, int s1, int h2, int m2, int s2)
    {
        TimeOnly t1 = new(h1, m1, s1);
        TimeOnly t2 = new(h2, m2, s2);
        TimeSpan expected = TimeSpan.FromTicks(Math.Abs(t1.Ticks - t2.Ticks));

        TimeSpan result = t1.Difference(t2);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(23, 30, 0, 0, 45, 0)]
    [Arguments(22, 0, 0, 2, 0, 0)]
    [Arguments(23, 59, 59, 0, 0, 1)]
    public async Task Difference_TimeOnly_SpanMidnight_ReturnsAbsoluteDifference(int h1, int m1, int s1, int h2, int m2, int s2)
    {
        TimeOnly t1 = new(h1, m1, s1);
        TimeOnly t2 = new(h2, m2, s2);
        TimeSpan expected = TimeSpan.FromTicks(Math.Abs(t1.Ticks - t2.Ticks));

        TimeSpan result = t1.Difference(t2);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task Difference_TimeOnly_SameValue_ReturnsZero()
    {
        TimeOnly t = new(12, 0, 0);

        TimeSpan result = t.Difference(t);

        await Assert.That(result).IsEqualTo(TimeSpan.Zero);
    }

    [Test]
    [Arguments(2024, 6, 15, 2024, 6, 10)]
    [Arguments(2023, 1, 1, 2024, 1, 1)]
    [Arguments(2025, 12, 31, 2025, 1, 1)]
    [Arguments(1, 1, 1, 9999, 12, 31)]
    public async Task Difference_DateOnly_ReturnsAbsoluteDifference(int y1, int mo1, int d1, int y2, int mo2, int d2)
    {
        DateOnly do1 = new(y1, mo1, d1);
        DateOnly do2 = new(y2, mo2, d2);
        long expectedTicks = Math.Abs(do1.DayNumber - do2.DayNumber) * TimeSpan.TicksPerDay;

        TimeSpan result = do1.Difference(do2);

        await Assert.That(result.Ticks).IsEqualTo(expectedTicks);
    }

    [Test]
    [Arguments(2024, 6, 15, 2024, 6, 10)]
    [Arguments(2023, 1, 1, 2024, 1, 1)]
    public async Task Difference_DateOnly_ReturnsAbsoluteDifferenceRegardlessOfOperandOrder(int y1, int mo1, int d1, int y2, int mo2, int d2)
    {
        DateOnly do1 = new(y1, mo1, d1);
        DateOnly do2 = new(y2, mo2, d2);

        TimeSpan forward = do1.Difference(do2);
        TimeSpan reverse = do2.Difference(do1);

        await Assert.That(forward).IsEqualTo(reverse);
    }

    [Test]
    public async Task Difference_DateOnly_SameValue_ReturnsZero()
    {
        DateOnly d = new(2024, 6, 15);

        TimeSpan result = d.Difference(d);

        await Assert.That(result).IsEqualTo(TimeSpan.Zero);
    }
#endif
}
