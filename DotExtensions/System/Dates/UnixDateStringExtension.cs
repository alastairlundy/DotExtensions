/*
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.Dates;

/// <summary>
/// Provides an extension method to format a DateTime object
/// as a Unix-compatible date string.
/// </summary>
public static class UnixDateStringExtension
{
    /// <summary>
    /// Gets the current date in the format of the unix Date command.
    /// </summary>
    /// <param name="dateTime">The dateTime object to be used.</param>
    /// <returns>the current date in the format of the unix Date command.</returns>
    public static string ToUnixDateString(this DateTime dateTime) =>
        dateTime.ToString("R").Replace(",", string.Empty);
}
