/*
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using System.Globalization;

namespace AlastairLundy.DotExtensions.Dates;

/// <summary>
/// Provides extension methods for working with day-related functionality for <see cref="DateTime"/> instances.
/// </summary>
public static class DayOfExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="date">The date </param>
    extension(DateTime date)
    {
        /// <summary>
        /// Returns the day of the week as a number from 1 to 7 using the current culture to determine what day is considered the first day of the week.
        /// </summary>
        /// <returns>The day of the week as a 32-Bit integer.</returns>
        public int DayOfWeekInt()
        {
            DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

            int dayOfWeek;

            if (DayOfWeek.Sunday == firstDayOfWeek)
            {
                dayOfWeek = (int)date.DayOfWeek + 1;
            }
            else if (DayOfWeek.Monday == firstDayOfWeek)
            {
                switch (date.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        dayOfWeek = 7;
                        break;
                    default:
                        dayOfWeek = (int)date.DayOfWeek;
                        break;
                }
            }
            else
            {
                dayOfWeek = (int)date.DayOfWeek + 1;
            }

            return dayOfWeek;
        }
    }
}
