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
    /// Counts the weeks of the year elapsed up to (and including) <paramref name="date"/>, where a new week
    /// begins on the culture's <see cref="DateTimeFormatInfo.FirstDayOfWeek"/> and the first week of the year is
    /// determined by the culture's <see cref="DateTimeFormatInfo.CalendarWeekRule"/>.
    /// </summary>
    /// <param name="date">The date whose week number within its year is calculated.</param>
    /// <param name="culture">The culture that supplies the week rule, first day of the week, and calendar; never ambient culture.</param>
    /// <returns>The week number within the year.</returns>
    private static int InternalWeekOfYearCount(DateOnly date, CultureInfo culture)
    {
        DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;
        CalendarWeekRule calendarWeekRule = culture.DateTimeFormat.CalendarWeekRule;
        int daysInYear = culture.Calendar.IsLeapYear(date.Year) ? 366 : 365;
        int weekCount = 0;

        DateOnly currentDate = new(date.Year, 1, 1);

        for (int dayIndex = 1; dayIndex <= daysInYear; dayIndex++)
        {
            if (dayIndex == 1 && calendarWeekRule == CalendarWeekRule.FirstDay)
                weekCount = 1;
            else if (dayIndex == 4 && calendarWeekRule == CalendarWeekRule.FirstFourDayWeek)
                weekCount = 1;
            else if (dayIndex == 7 && calendarWeekRule == CalendarWeekRule.FirstFullWeek)
                weekCount = 1;
            else if (currentDate.DayOfWeek == firstDayOfWeek)
                weekCount++;

            if (currentDate == date)
                break;

            currentDate = currentDate.AddDays(1);
        }

        return weekCount;
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
        /// <returns>The week number within the year of the date.</returns>
        public int WeekOfYear(CultureInfo culture) =>
            InternalWeekOfYearCount(date, culture);
    }
}
