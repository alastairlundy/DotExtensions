/*
 * Copyright (c) 2020-2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.Dates;

/// <summary>
/// Provides extension methods for <see cref="DateTime"/> to compute absolute differences.
/// </summary>
public static class TimeSpanDifferenceExtensions
{
    /// <param name="dateTimeOne">The first date to be subtracted.</param>
    extension(DateTime dateTimeOne)
    {
        /// <summary>
        /// Determines the absolute difference of two <see cref="DateTime"/> objects.
        /// </summary>
        /// <param name="dateTimeTwo">The second date to be subtracted.</param>
        /// <returns>The absolute difference as a <see cref="TimeSpan"/>.</returns>
        public TimeSpan Difference(DateTime dateTimeTwo)
        {
            long absoluteTicks = Math.Abs(dateTimeOne.Ticks - dateTimeTwo.Ticks);

            return TimeSpan.FromTicks(absoluteTicks);
        }
    }

#if NET8_0_OR_GREATER
    /// <param name="timeOnlyOne">The first time to be subtracted.</param>
    extension(TimeOnly timeOnlyOne)
    {
        /// <summary>
        /// Determines the absolute difference of two <see cref="TimeOnly"/> objects.
        /// </summary>
        /// <param name="timeOnlyTwo">The second time to be subtracted.</param>
        /// <returns>The absolute difference as a <see cref="TimeSpan"/>.</returns>
        public TimeSpan Difference(TimeOnly timeOnlyTwo)
        {
            long absoluteTicks = Math.Abs(timeOnlyOne.Ticks - timeOnlyTwo.Ticks);

            return TimeSpan.FromTicks(absoluteTicks);
        }
    }

    /// <param name="dateOnlyOne">The first date to be subtracted.</param>
    extension(DateOnly dateOnlyOne)
    {
        /// <summary>
        /// Determines the absolute difference of two <see cref="DateOnly"/> objects.
        /// </summary>
        /// <param name="dateOnlyTwo">The second date to be subtracted.</param>
        /// <returns>The absolute difference as a <see cref="TimeSpan"/>.</returns>
        public TimeSpan Difference(DateOnly dateOnlyTwo) =>
            Difference(DateOnly.ToDateTime(dateOnlyOne).Date, DateOnly.ToDateTime(dateOnlyTwo).Date);
    }
#endif
}
