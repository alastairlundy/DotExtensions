using System;
using System.Linq;
using System.Reflection;
using AlastairLundy.DotExtensions.Dates;
using AlastairLundy.DotPrimitives.Dates;

namespace DotExtensions.Tests.Dates;

public class DateSpanDifferenceTests
{
    [Fact]
    public void SameDate_ReturnsZeroDays()
    {
        DateTime a = new DateTime(2024, 6, 15);
        DateSpan span = a.DateSpanDifference(a);

        Assert.Equal(0, span.Years);
        Assert.Equal(0, span.Months);
        Assert.Equal(0, span.Days);
    }

    [Theory]
    [InlineData(2023, 1, 10, 2023, 3, 1, 50)] // Non-leap year: Jan 10 -> Mar 1 = 21 (Jan) + 28 (Feb) + 1 (Mar 1 offset?) = 50 by DayOfYear diff
    [InlineData(2024, 2, 28, 2024, 3, 1, 2)]  // Leap year: Feb 28 -> Mar 1 = 2 days (Feb 29 exists)
    [InlineData(2024, 2, 29, 2024, 3, 1, 1)]  // Leap day -> next day
    public void SameYear_DifferentDays_ComputesAbsoluteDayOfYearDifference(int y1, int m1, int d1, int y2, int m2, int d2, int expectedDays)
    {
        DateTime first = new DateTime(y1, m1, d1);
        DateTime second = new DateTime(y2, m2, d2);

        // Sanity: ensure same-year path
        Assert.Equal(first.Year, second.Year);

        DateSpan span = first.DateSpanDifference(second);

        Assert.Equal(0, span.Years);
        Assert.Equal(0, span.Months);
        Assert.Equal(expectedDays, span.Days);
    }

    [Fact]
    public void SameYear_OrderIndependence()
    {
        DateTime earlier = new DateTime(2023, 7, 1);
        DateTime later = new DateTime(2023, 12, 31);

        DateSpan span1 = earlier.DateSpanDifference(later);
        DateSpan span2 = later.DateSpanDifference(earlier);
        
        Assert.Equal(span1.Years, span2.Years);
        Assert.Equal(span1.Months, span2.Months);
        Assert.Equal(span1.Days, span2.Days);
    }

#if NET8_0_OR_GREATER
    [Fact]
    public void DateOnly_SameDate_ReturnsZeroDays()
    {
        DateOnly a = new DateOnly(2024, 6, 15);
        DateSpan span = a.DateSpanDifference(a);

        Assert.Equal(0, span.Years);
        Assert.Equal(0, span.Months);
        Assert.Equal(0, span.Days);
    }

    [Fact]
    public void DateOnly_SameYear_OrderIndependence()
    {
        DateOnly earlier = new DateOnly(2024, 1, 1);
        DateOnly later = new DateOnly(2024, 3, 1);

        DateSpan span1 = earlier.DateSpanDifference(later);
        DateSpan span2 = later.DateSpanDifference(earlier);
        
        Assert.Equal(span1.Years, span2.Years);
        Assert.Equal(span1.Months, span2.Months);
        Assert.Equal(span1.Days, span2.Days);
    }
#endif
}