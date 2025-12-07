/*
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.Dates;

#if NET8_0_OR_GREATER

/// <summary>
/// Provides an extension method for the DateOnly struct to convert it to a DateTime object.
/// </summary>
public static class DateOnlyToDateTimeExtension
{
    /// <summary>
    /// 
    /// </summary>
    extension(DateOnly)
    {
        /// <summary>
        /// Creates a new DateTime object with the Date from a DateOnly object.
        /// </summary>
        /// <param name="dateOnly">The date to be converted to a DateTime object.</param>
        /// <returns>the newly created DateTime object.</returns>
        public static DateTime ToDateTime(DateOnly dateOnly) =>
            DateTime.Parse(dateOnly.ToLongDateString());
    }
}

#endif
