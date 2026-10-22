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

namespace DotExtensions.Dates;

/// <summary>
/// Provides culture-explicit extension members for calculating week information from a <see cref="DateOnly"/>.
/// </summary>
/// <remarks>
/// The <c>CultureInfo</c> argument is required on every member: the first day of the week, the week rule backing
/// <c>WeekOfYear</c>, and the calendar all come from the culture the caller passes. No member here consults
/// ambient culture, so results are machine-independent and testable.
/// </remarks>
public static class WeekOfExtensions
{
    /// <summary>
    /// Counts the weeks of the month elapsed up to (and including) <paramref name="date"/>, where a new week
    /// begins on the culture's <see cref="DateTimeFormatInfo.FirstDayOfWeek"/>.
    /// </summary>
    /// <param name="date">The date whose week number within its month is calculated.</param>
    /// <param name="culture">The culture that supplies the first day of the week; never ambient culture.</param>
    /// <returns>The week number within the month; <c>0</c> when no week has started yet.</returns>
    private static int InternalWeekOfMonthCount(DateOnly date, CultureInfo culture)
    {
        DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;
        int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

        int weekCount = 0;

        for (int day = 1; day <= daysInMonth; day++)
        {
            DateOnly currentDate = new(date.Year, date.Month, day);

            if (currentDate.DayOfWeek == firstDayOfWeek)
                weekCount++;

            if (currentDate.Day == date.Day)
                break;
        }

        return weekCount;
    }

    /// <summary>
    /// Returns the proleptic-Gregorian day number of January 1 of the given year (0001-01-01 has
    /// day number 0). Year 0 is supported so that dates before week 1 of year 1 can resolve their
    /// trailing week number against a virtual previous year exactly like
    /// <see cref="Calendar.GetWeekOfYear(DateTime, CalendarWeekRule, DayOfWeek)"/> does.
    /// </summary>
    private static int GetJanuary1DayNumber(int year)
    {
        if (year <= 0)
        {
            // Year 0 is a leap year under the proleptic Gregorian calendar: 366 days before 0001-01-01.
            return -366;
        }

        int yearsSinceEpoch = year - 1;

        return 365 * yearsSinceEpoch + yearsSinceEpoch / 4 - yearsSinceEpoch / 100 + yearsSinceEpoch / 400;
    }

    /// <summary>
    /// Returns the day of week for a day number (0001-01-01 was a Monday).
    /// </summary>
    private static DayOfWeek GetDayOfWeek(int dayNumber) =>
        (DayOfWeek)((((dayNumber + 1) % 7) + 7) % 7);

    /// <summary>
    /// Returns the day number on which week 1 of the given year starts under the supplied rule,
    /// or January 1 itself for <see cref="CalendarWeekRule.FirstDay"/>.
    /// </summary>
    /// <remarks>
    /// For <see cref="CalendarWeekRule.FirstFourDayWeek"/> the leading partial week is week 1 when
    /// it holds four or more days of the new year (the week may start in the previous year); for
    /// <see cref="CalendarWeekRule.FirstFullWeek"/> only a full seven-day leading week qualifies.
    /// The result can be a negative day number (year 1 weeks that start before 0001-01-01), which
    /// is why the arithmetic stays in day numbers rather than <see cref="DateOnly"/> values.
    /// </remarks>
    private static int GetWeek1StartDayNumber(int year, DayOfWeek firstDayOfWeek, CalendarWeekRule calendarWeekRule)
    {
        int january1 = GetJanuary1DayNumber(year);

        if (calendarWeekRule == CalendarWeekRule.FirstDay)
            return january1;

        int offset = ((int)firstDayOfWeek - (int)GetDayOfWeek(january1) + 7) % 7;

        if (calendarWeekRule == CalendarWeekRule.FirstFourDayWeek && offset >= 4)
            return january1 + offset - 7;

        return january1 + offset;
    }

    /// <summary>
    /// Counts the weeks of the year elapsed up to (and including) <paramref name="date"/> following
    /// <see cref="Calendar.GetWeekOfYear(DateTime, CalendarWeekRule, DayOfWeek)"/> semantics: the
    /// first week of the year is determined by the culture's <see cref="DateTimeFormatInfo.CalendarWeekRule"/>,
    /// new weeks begin on the culture's <see cref="DateTimeFormatInfo.FirstDayOfWeek"/>, and dates
    /// before week 1 report the previous year's trailing week number (52 or 53) - never 0.
    /// </summary>
    /// <param name="date">The date whose week number within its year is calculated.</param>
    /// <param name="culture">The culture that supplies the week rule and first day of the week; never ambient culture.</param>
    /// <returns>The week number within the year of the date.</returns>
    private static int InternalWeekOfYearCount(DateOnly date, CultureInfo culture)
    {
        DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;
        CalendarWeekRule calendarWeekRule = culture.DateTimeFormat.CalendarWeekRule;
        int dayNumber = date.DayNumber;
        int anchor;

        if (calendarWeekRule == CalendarWeekRule.FirstDay)
        {
            // Week 1 is the (possibly partial) week containing January 1. Later weeks start on
            // firstDayOfWeek, so the 7-day grid is anchored one week before week 2 starts: that
            // keeps the grid aligned while January 1 itself still counts as week 1.
            int january1 = GetJanuary1DayNumber(date.Year);
            int offset = ((int)firstDayOfWeek - (int)GetDayOfWeek(january1) + 7) % 7;
            anchor = offset == 0 ? january1 : january1 + offset - 7;
        }
        else
        {
            anchor = GetWeek1StartDayNumber(date.Year, firstDayOfWeek, calendarWeekRule);
        }

        if (dayNumber >= anchor)
            return (dayNumber - anchor) / 7 + 1;

        // The date precedes the year's first proper week (impossible under FirstDay): the BCL
        // reports the previous year's trailing week number instead of 0.
        int previousAnchor = GetWeek1StartDayNumber(date.Year - 1, firstDayOfWeek, calendarWeekRule);

        return (dayNumber - previousAnchor) / 7 + 1;
    }

    /// <param name="date">The date to calculate week numbers from.</param>
    extension(DateOnly date)
    {
        /// <summary>
        /// Calculates the week of the month of a given <see cref="DateOnly"/> under the supplied culture.
        /// </summary>
        /// <param name="culture">The culture that supplies the first day of the week. Required - there is no default value and no ambient culture is read.</param>
        /// <returns>The week number within the month of the date.</returns>
        /// <remarks>
        /// WeekOfMonth returns 0 for a date before the culture's first week start.
        /// </remarks>
        public int WeekOfMonth(CultureInfo culture) =>
            InternalWeekOfMonthCount(date, culture);

        /// <summary>
        /// Calculates the week in the year of a given <see cref="DateOnly"/> under the supplied culture.
        /// </summary>
        /// <param name="culture">The culture that supplies the week rule via <see cref="DateTimeFormatInfo.CalendarWeekRule"/>, the first day of the week, and the calendar. Required - there is no default value and no ambient culture is read.</param>
        /// <returns>
        /// The week number within the year of the date, matching
        /// <see cref="Calendar.GetWeekOfYear(DateTime, CalendarWeekRule, DayOfWeek)"/> for every
        /// rule: always at least 1 - dates before week 1 report the previous year's trailing
        /// week number (52 or 53), never 0.
        /// </returns>
        public int WeekOfYear(CultureInfo culture) =>
            InternalWeekOfYearCount(date, culture);
    }
}
