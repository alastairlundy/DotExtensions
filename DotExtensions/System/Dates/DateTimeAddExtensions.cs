/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.Dates;

/// <summary>
/// A static class to Add to DateTime values.
/// </summary>
public static class DateTimeAddExtensions
{
    /// <param name="dateTimeOne">The starting DateTime.</param>
    extension(DateTime dateTimeOne)
    {
        /// <summary>
        /// Adds two dates together and returns the resulting DateTime.
        /// </summary>
        /// <param name="dateTimeTwo">The date and time to add.</param>
        /// <returns>The sum of the two input dates and times.</returns>
        public DateTime Add(DateTime dateTimeTwo)
        {
            TimeSpan timeSpan = dateTimeTwo.Difference(dateTimeOne);
            return dateTimeOne.Add(timeSpan);
        }
    }

    /// <param name="dateTime">The initial date and time to add days to.</param>
    extension(DateTime dateTime)
    {
        /// <summary>
        /// Adds a specified number of days to a given date and time.
        /// </summary>
        /// <param name="days">The number of days to add.</param>
        /// <returns>The resulting date and time after adding the specified number of days.</returns>
        public DateTime AddDays(int days) =>
            dateTime.AddDays(Convert.ToDouble(days));
    }
}
