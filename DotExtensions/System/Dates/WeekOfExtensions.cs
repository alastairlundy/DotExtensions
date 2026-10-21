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
/// Provides extension methods for calculating week information from a given DateTime object.
/// </summary>
public static class WeekOfExtensions
{
    private static int InternalWeekOfMonthCount(DateTime date)
    {
        DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        DayOfWeek firstDayOfMonth = new DateTime(date.Year, date.Month, 1).DayOfWeek;

        //0-based offset of the month's first day within a week that starts on firstDayOfWeek.
        int daysFromWeekStart = ((int)firstDayOfMonth - (int)firstDayOfWeek + 7) % 7;

        //The week containing the 1st of the month is week 1, so the result is always 1 or greater.
        return (daysFromWeekStart + date.Day - 1) / 7 + 1;
    }

    private static int InternalWeekOfYearCount(DateTime date, CalendarWeekRule calendarWeekRule)
    {
        Calendar calendar = CultureInfo.CurrentCulture.Calendar;

        //Fall back to the Gregorian calendar for dates outside the culture calendar's supported DateTime
        //range (for example DateOnly.MinValue under ar-SA, whose UmAlQuraCalendar supports 1900-2077 CE)
        //so this never throws for any culture.
        if (date < calendar.MinSupportedDateTime || date > calendar.MaxSupportedDateTime)
        {
            calendar = new GregorianCalendar();
        }

        return calendar.GetWeekOfYear(date, calendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
    }

    /// <param name="date">The date to be used.</param>
    extension(DateTime date)
    {
        /// <summary>
        /// Calculates the week of the month of a given <see cref="DateTime"/>, using the current culture to determine the first day of the week.
        /// The week containing the 1st day of the month is week 1, so the result is always 1 or greater.
        /// </summary>
        /// <returns>The 1-based week number in a given month.</returns>
        public int WeekOfMonth =>
            InternalWeekOfMonthCount(date);

        /// <summary>
        /// Calculates the week in the year of a given <see cref="DateTime"/>, using the current culture's calendar and first day of the week.
        /// The result matches <see cref="Calendar.GetWeekOfYear(DateTime, CalendarWeekRule, DayOfWeek)"/> for the current culture.
        /// </summary>
        /// <param name="calendarWeekRule">The rule to use to determine what counts as the 1st week of the year.</param>
        /// <returns>The week number in a given year.</returns>
        public int WeekOfYear(CalendarWeekRule calendarWeekRule = CalendarWeekRule.FirstFullWeek) =>
            InternalWeekOfYearCount(date, calendarWeekRule);
    }

#if NET8_0_OR_GREATER
    /// <param name="date"></param>
    extension(DateOnly date)
    {
        /// <summary>
        /// Calculates the week of the month of a given <see cref="DateOnly"/>, using the current culture to determine the first day of the week.
        /// The week containing the 1st day of the month is week 1, so the result is always 1 or greater.
        /// </summary>
        /// <returns>The 1-based week number in a given month.</returns>
        public int WeekOfMonth =>
            InternalWeekOfMonthCount(DateOnly.ToDateTime(date));

        /// <summary>
        /// Calculates the week in the year of a given <see cref="DateOnly"/>, using the current culture's calendar and first day of the week.
        /// The result matches <see cref="Calendar.GetWeekOfYear(DateTime, CalendarWeekRule, DayOfWeek)"/> for the current culture.
        /// </summary>
        /// <param name="calendarWeekRule">The rule to use to determine what counts as the 1st week of the year.</param>
        /// <returns>The week number in a given year.</returns>
        public int WeekOfYear(CalendarWeekRule calendarWeekRule = CalendarWeekRule.FirstFullWeek) =>
            InternalWeekOfYearCount(DateOnly.ToDateTime(date), calendarWeekRule);
    }
#endif
}
