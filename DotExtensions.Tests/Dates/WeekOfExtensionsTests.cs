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
using System.Globalization;
using DotExtensions.Dates;

namespace DotExtensions.Tests.Dates;

[NotInParallel]
public class WeekOfExtensionsTests
{
    private static bool TryGetCulture(string name, out CultureInfo culture)
    {
        try
        {
            culture = CultureInfo.GetCultureInfo(name);
            return true;
        }
        catch (CultureNotFoundException)
        {
            culture = CultureInfo.InvariantCulture;
            return false;
        }
    }

    [Test]
    [Arguments(CalendarWeekRule.FirstDay, 1)]
    [Arguments(CalendarWeekRule.FirstFourDayWeek, 1)]
    [Arguments(CalendarWeekRule.FirstFullWeek, 53)]
    public async Task WeekOfYear_EnUs_Jan1_2024_MatchesBcl(CalendarWeekRule calendarWeekRule, int expected)
    {
        CultureInfo original = CultureInfo.CurrentCulture;

        try
        {
            if (!TryGetCulture("en-US", out CultureInfo culture))
            {
                return;
            }

            CultureInfo.CurrentCulture = culture;

            DateTime date = new(2024, 1, 1);

            await Assert.That(date.WeekOfYear(calendarWeekRule)).IsEqualTo(expected);
            await Assert.That(date.WeekOfYear(calendarWeekRule))
                .IsEqualTo(culture.Calendar.GetWeekOfYear(date, calendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek));
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }

    [Test]
    [Arguments(CalendarWeekRule.FirstDay, 1)]
    [Arguments(CalendarWeekRule.FirstFourDayWeek, 52)]
    [Arguments(CalendarWeekRule.FirstFullWeek, 52)]
    public async Task WeekOfYear_EnUs_Jan1_2022_MatchesBcl(CalendarWeekRule calendarWeekRule, int expected)
    {
        CultureInfo original = CultureInfo.CurrentCulture;

        try
        {
            if (!TryGetCulture("en-US", out CultureInfo culture))
            {
                return;
            }

            CultureInfo.CurrentCulture = culture;

            DateTime date = new(2022, 1, 1);

            await Assert.That(date.WeekOfYear(calendarWeekRule)).IsEqualTo(expected);
            await Assert.That(date.WeekOfYear(calendarWeekRule))
                .IsEqualTo(culture.Calendar.GetWeekOfYear(date, calendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek));
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }

    [Test]
    [Arguments(CalendarWeekRule.FirstDay)]
    [Arguments(CalendarWeekRule.FirstFourDayWeek)]
    [Arguments(CalendarWeekRule.FirstFullWeek)]
    public async Task WeekOfYear_DeDe_Jan1_2024_ReturnsOne(CalendarWeekRule calendarWeekRule)
    {
        CultureInfo original = CultureInfo.CurrentCulture;

        try
        {
            if (!TryGetCulture("de-DE", out CultureInfo culture))
            {
                return;
            }

            CultureInfo.CurrentCulture = culture;

            DateTime date = new(2024, 1, 1);

            await Assert.That(date.WeekOfYear(calendarWeekRule)).IsEqualTo(1);
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }

    [Test]
    [Arguments(CalendarWeekRule.FirstDay, 36)]
    [Arguments(CalendarWeekRule.FirstFourDayWeek, 36)]
    [Arguments(CalendarWeekRule.FirstFullWeek, 35)]
    public async Task WeekOfYear_ArSaCulture_DoesNotThrow_AndMatchesBcl(CalendarWeekRule calendarWeekRule, int expected)
    {
        CultureInfo original = CultureInfo.CurrentCulture;

        try
        {
            if (!TryGetCulture("ar-SA", out CultureInfo culture))
            {
                return;
            }

            CultureInfo.CurrentCulture = culture;

            DateTime date = new(2024, 3, 20);

            await Assert.That(date.WeekOfYear(calendarWeekRule)).IsEqualTo(expected);
            await Assert.That(date.WeekOfYear(calendarWeekRule))
                .IsEqualTo(culture.Calendar.GetWeekOfYear(date, calendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek));
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }

#if NET8_0_OR_GREATER
    [Test]
    [Arguments(CalendarWeekRule.FirstDay)]
    [Arguments(CalendarWeekRule.FirstFourDayWeek)]
    [Arguments(CalendarWeekRule.FirstFullWeek)]
    public async Task WeekOfYear_ArSaCulture_DateOnlyMinValue_FallsBackWithoutThrowing(CalendarWeekRule calendarWeekRule)
    {
        CultureInfo original = CultureInfo.CurrentCulture;

        try
        {
            if (!TryGetCulture("ar-SA", out CultureInfo culture))
            {
                return;
            }

            CultureInfo.CurrentCulture = culture;

            DateOnly date = DateOnly.MinValue;
            int result = date.WeekOfYear(calendarWeekRule);

            await Assert.That(result).IsGreaterThanOrEqualTo(1);
            await Assert.That(result).IsLessThanOrEqualTo(53);
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }
#endif

#if NET8_0_OR_GREATER
    [Test]
    [Arguments(CalendarWeekRule.FirstDay)]
    [Arguments(CalendarWeekRule.FirstFourDayWeek)]
    [Arguments(CalendarWeekRule.FirstFullWeek)]
    public async Task WeekOfYear_DateOnly_MatchesDateTimeCounterpart(CalendarWeekRule calendarWeekRule)
    {
        CultureInfo original = CultureInfo.CurrentCulture;

        try
        {
            if (!TryGetCulture("en-US", out CultureInfo culture))
            {
                return;
            }

            CultureInfo.CurrentCulture = culture;

            DateOnly date = new(2024, 3, 20);

            await Assert.That(date.WeekOfYear(calendarWeekRule)).IsEqualTo(DateOnly.ToDateTime(date).WeekOfYear(calendarWeekRule));
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }
#endif

    [Test]
    [Arguments(1, 1)]
    [Arguments(2, 1)]
    [Arguments(3, 1)]
    [Arguments(4, 1)]
    [Arguments(5, 1)]
    [Arguments(6, 1)]
    [Arguments(7, 2)]
    [Arguments(8, 2)]
    public async Task WeekOfMonth_EnUs_IsOneBased(int day, int expected)
    {
        CultureInfo original = CultureInfo.CurrentCulture;

        try
        {
            if (!TryGetCulture("en-US", out CultureInfo culture))
            {
                return;
            }

            CultureInfo.CurrentCulture = culture;

            DateTime date = new(2024, 1, day);

            await Assert.That(date.WeekOfMonth).IsEqualTo(expected);
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }

    [Test]
    [Arguments(1, 1)]
    [Arguments(6, 1)]
    [Arguments(7, 1)]
    [Arguments(8, 2)]
    public async Task WeekOfMonth_DeDe_IsOneBased(int day, int expected)
    {
        CultureInfo original = CultureInfo.CurrentCulture;

        try
        {
            if (!TryGetCulture("de-DE", out CultureInfo culture))
            {
                return;
            }

            CultureInfo.CurrentCulture = culture;

            DateTime date = new(2024, 1, day);

            await Assert.That(date.WeekOfMonth).IsEqualTo(expected);
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }

#if NET8_0_OR_GREATER
    [Test]
    public async Task WeekOfMonth_DateOnly_MatchesDateTimeCounterpart()
    {
        DateOnly date = new(2024, 3, 20);

        await Assert.That(date.WeekOfMonth).IsEqualTo(DateOnly.ToDateTime(date).WeekOfMonth);
    }
#endif
}
