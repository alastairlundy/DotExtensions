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

using System.Globalization;

namespace DotExtensions.Dates;

/// <summary>
/// Provides extension methods for calculating week information from a given DateTime object.
/// </summary>
public static class WeekOfExtensions
{
    private static int InternalWeekOfMonthCount(DateTime date, int year, int month)
    {
        DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        int daysInMonth = DateTime.DaysInMonth(year, month);

        int weekCount = 0;

        for (int day = 1; day < daysInMonth; day++)
        {
            DateTime currentDate = new(year, month, day);

            if (currentDate.DayOfWeek == firstDayOfWeek)
                weekCount++;

            if (currentDate.Day == date.Day)
                break;
        }

        return weekCount;
    }
    
    private static int InternalWeekOfYearCount(DateTime date, CalendarWeekRule calendarWeekRule, int year, int month)
    {
        DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        int daysInYear = CultureInfo.CurrentCulture.Calendar.IsLeapYear(year) ? 366 : 365;
        int weekCount = 0;

        for (int day = 1; day < daysInYear; day++)
        {
            DateTime currentDate = new(year, month, day);

            if (day == 1 && calendarWeekRule == CalendarWeekRule.FirstDay)
                weekCount = 1;
            else if (day == 4 && calendarWeekRule == CalendarWeekRule.FirstFourDayWeek)
                weekCount = 1;
            else if (day == 7 && calendarWeekRule == CalendarWeekRule.FirstFullWeek)
                weekCount = 1;
            else
            {
                if (currentDate.DayOfWeek == firstDayOfWeek)
                    weekCount++;
            }

            if (currentDate.Day == date.Day)
                break;
        }

        return weekCount;
    }
    
    /// <param name="date">The date to be used.</param>
    extension(DateTime date)
    {
        /// <summary>
        /// Calculates the week of the month.
        /// </summary>
        /// <returns>The week number in a given month.</returns>
        public int WeekOfMonth()
        {
            return InternalWeekOfMonthCount(date, date.Year, date.Month);
        }

        /// <summary>
        /// Calculates the week in the year of a given DateTime.
        /// </summary>
        /// <param name="calendarWeekRule">The rule to use to determine what counts as the 1st week of the year.</param>
        /// <returns>The week number in a given year.</returns>
        public int WeekOfYear(CalendarWeekRule calendarWeekRule = CalendarWeekRule.FirstFullWeek)
        {
            return InternalWeekOfYearCount(date, calendarWeekRule, date.Year, date.Month);
        }
    }

#if NET8_0_OR_GREATER
    extension(DateOnly date)
    {
        /// <summary>
        /// Calculates the week of the month.
        /// </summary>
        /// <returns>The week number in a given month.</returns>
        public int WeekOfMonth()
        {
            return InternalWeekOfMonthCount(DateOnly.ToDateTime(date), date.Year, date.Month);
        }

        /// <summary>
        /// Calculates the week in the year of a given DateTime.
        /// </summary>
        /// <param name="calendarWeekRule">The rule to use to determine what counts as the 1st week of the year.</param>
        /// <returns>The week number in a given year.</returns>
        public int WeekOfYear(CalendarWeekRule calendarWeekRule = CalendarWeekRule.FirstFullWeek)
        {
            return InternalWeekOfYearCount(DateOnly.ToDateTime(date),
                calendarWeekRule, date.Year, date.Month);
        }
    }
#endif
}
