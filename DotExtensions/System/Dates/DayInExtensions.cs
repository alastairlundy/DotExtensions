/*
 * Copyright (c) 2020-2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.Dates;

/// <summary>
/// 
/// </summary>
public static class DayInExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="year"></param>
    /// <returns></returns>
    internal static int DaysInYear(int year)
    {
        int days = 0;
            
        for (int month = 1; month <= 12; month++)
        {
            days += DateTime.DaysInMonth(year, month);
        }
            
        return days;
    }

    /// <summary>
    /// Provides extension methods for working with DateTime objects.
    /// </summary>
    extension(DateTime)
    {
        /// <summary>
        /// Calculates the total number of days in the year of the specified DateTime object.
        /// </summary>
        /// <param name="dateTime">The DateTime object representing the year to calculate the total number of days for.</param>
        /// <returns>The total number of days in the year of the specified DateTime.</returns>
        public static int DaysInYear(DateTime dateTime)
            => DaysInYear(dateTime.Year);
    }
}