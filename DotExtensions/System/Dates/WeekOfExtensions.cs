/*
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using System.Globalization;

namespace AlastairLundy.DotExtensions.Dates;

/// <summary>
/// Provides extension methods for calculating week information from a given DateTime object.
/// </summary>
public static class WeekOfExtensions
{

    /// <param name="date">The date to be used.</param>
    extension(DateTime date)
    {
        /// <summary>
        /// Calculates the week of the month.
        /// </summary>
        /// <returns>The week number in a given month.</returns>
        public int WeekOfMonth()
        {
            DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

            int weekCount = 0;

            for (int day = 1; day < daysInMonth; day++)
            {
                DateTime currentDate = new DateTime(date.Year, date.Month, day);

                if (currentDate.DayOfWeek == firstDayOfWeek)
                    weekCount++;

                if (currentDate.Day == date.Day)
                    break;
            }

            return weekCount;
        }

        /// <summary>
        /// Calculates the week in the year of a given DateTime.
        /// </summary>
        /// <param name="calendarWeekRule">The rule to use to determine what counts as the 1st week of the year.</param>
        /// <returns>The week number in a given year.</returns>
        public int WeekOfYear(CalendarWeekRule calendarWeekRule = CalendarWeekRule.FirstFullWeek)
        {
            DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int daysInYear = CultureInfo.CurrentCulture.Calendar.IsLeapYear(date.Year) ? 366 : 365;
            int weekCount = 0;

            for (int day = 1; day < daysInYear; day++)
            {
                DateTime currentDate = new DateTime(date.Year, date.Month, day);

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
    }
}
