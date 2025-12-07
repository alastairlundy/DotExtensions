/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.Dates;

#if NET8_0_OR_GREATER

/// <summary>
/// Provides extension methods for subtracting days, months, and years from a DateOnly object.
/// </summary>
public static class DateOnlySubtractionExtensions
{
    /// <param name="dateOnly">The DateOnly object to be subtracted from.</param>
    extension(DateOnly dateOnly)
    {
        /// <summary>
        /// Subtract a specified number of days from a DateOnly object.
        /// </summary>
        /// <param name="days">The number of days to subtract from the specified DateOnly object.</param>
        /// <returns>the modified DateOnly object.</returns>
        public DateOnly SubtractDays(int days)
        {
            DateTime result = DateOnly.ToDateTime(dateOnly).Subtract(TimeSpan.FromDays(days));
            return DateOnly.FromDateTime(result);
        }

        /// <summary>
        /// Subtract a specified number of months from a DateOnly object.
        /// </summary>
        /// <param name="months">The number of months to subtract from the specified DateOnly.</param>
        /// <returns>the modified DateOnly object.</returns>
        public DateOnly SubtractMonths(int months)
        {
            DateTime result = DateOnly.ToDateTime(dateOnly).SubtractMonths(Convert.ToDouble(months));
            return DateOnly.FromDateTime(result);
        }

        /// <summary>
        /// Subtract a specified number of years from a DateOnly object.
        /// </summary>
        /// <param name="years">The number of years to subtract from the specified DateOnly object.</param>
        /// <returns>the modified DateOnly object.</returns>
        public DateOnly SubtractYears(int years)
        {
            DateTime result = DateOnly.ToDateTime(dateOnly).SubtractYears(Convert.ToDouble(years));
            return DateOnly.FromDateTime(result);
        }
    }
}

#endif
