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
public class DayOfExtensionsTests
{
    //2024-03-17 is a Sunday, so adding the DayOfWeek enum value yields the matching date within one week.
    private static DateTime DateFor(DayOfWeek dayOfWeek) => new(2024, 3, 17 + (int)dayOfWeek);

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
    [Arguments(DayOfWeek.Sunday, 1)]
    [Arguments(DayOfWeek.Monday, 2)]
    [Arguments(DayOfWeek.Tuesday, 3)]
    [Arguments(DayOfWeek.Wednesday, 4)]
    [Arguments(DayOfWeek.Thursday, 5)]
    [Arguments(DayOfWeek.Friday, 6)]
    [Arguments(DayOfWeek.Saturday, 7)]
    public async Task CalculateDayOfWeekAsInteger_EnUsSundayFirst_NumbersSundayAsOne(DayOfWeek dayOfWeek, int expected)
    {
        CultureInfo original = CultureInfo.CurrentCulture;

        try
        {
            if (!TryGetCulture("en-US", out CultureInfo culture))
            {
                return;
            }

            CultureInfo.CurrentCulture = culture;

            await Assert.That(DateFor(dayOfWeek).CalculateDayOfWeekAsInteger()).IsEqualTo(expected);
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }

    [Test]
    [Arguments(DayOfWeek.Monday, 1)]
    [Arguments(DayOfWeek.Tuesday, 2)]
    [Arguments(DayOfWeek.Wednesday, 3)]
    [Arguments(DayOfWeek.Thursday, 4)]
    [Arguments(DayOfWeek.Friday, 5)]
    [Arguments(DayOfWeek.Saturday, 6)]
    [Arguments(DayOfWeek.Sunday, 7)]
    public async Task CalculateDayOfWeekAsInteger_DeDeMondayFirst_NumbersMondayAsOne(DayOfWeek dayOfWeek, int expected)
    {
        CultureInfo original = CultureInfo.CurrentCulture;

        try
        {
            if (!TryGetCulture("de-DE", out CultureInfo culture))
            {
                return;
            }

            CultureInfo.CurrentCulture = culture;

            await Assert.That(DateFor(dayOfWeek).CalculateDayOfWeekAsInteger()).IsEqualTo(expected);
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }

    [Test]
    [Arguments(DayOfWeek.Saturday, 1)]
    [Arguments(DayOfWeek.Sunday, 2)]
    [Arguments(DayOfWeek.Monday, 3)]
    [Arguments(DayOfWeek.Tuesday, 4)]
    [Arguments(DayOfWeek.Wednesday, 5)]
    [Arguments(DayOfWeek.Thursday, 6)]
    [Arguments(DayOfWeek.Friday, 7)]
    public async Task CalculateDayOfWeekAsInteger_FaIrSaturdayFirst_NumbersSaturdayAsOne(DayOfWeek dayOfWeek, int expected)
    {
        CultureInfo original = CultureInfo.CurrentCulture;

        try
        {
            if (!TryGetCulture("fa-IR", out CultureInfo culture))
            {
                return;
            }

            CultureInfo.CurrentCulture = culture;

            await Assert.That(DateFor(dayOfWeek).CalculateDayOfWeekAsInteger()).IsEqualTo(expected);
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }

#if NET8_0_OR_GREATER
    [Test]
    [Arguments(DayOfWeek.Sunday)]
    [Arguments(DayOfWeek.Monday)]
    [Arguments(DayOfWeek.Tuesday)]
    [Arguments(DayOfWeek.Wednesday)]
    [Arguments(DayOfWeek.Thursday)]
    [Arguments(DayOfWeek.Friday)]
    [Arguments(DayOfWeek.Saturday)]
    public async Task CalculateDayOfWeekAsInteger_DateOnly_MatchesDateTimeCounterpart(DayOfWeek dayOfWeek)
    {
        DateTime date = DateFor(dayOfWeek);
        DateOnly dateOnly = DateOnly.FromDateTime(date);

        await Assert.That(dateOnly.CalculateDayOfWeekAsInteger()).IsEqualTo(date.CalculateDayOfWeekAsInteger());
    }
#endif
}
