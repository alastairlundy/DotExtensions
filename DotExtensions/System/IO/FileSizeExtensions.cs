/*
 * Copyright (c) 2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using System.IO;
using AlastairLundy.DotExtensions.Numbers;

namespace AlastairLundy.DotExtensions.IO;

/// <summary>
/// Provides extension methods for working with file sizes in a more human-readable format.
/// </summary>
public static class FileSizeExtensions
{

    /// <param name="file"></param>
    extension(FileInfo file)
    {
        /// <summary>
        /// Returns a string representation of the file size, including the most suitable unit.
        /// </summary>
        /// <returns>A string representation of the file size with the most suitable unit.</returns>
        public string GetFileSizeString()
        {
            long quantity = file.Length;

            while (quantity > 1000)
            {
                quantity /= 1000;
            }

            return $"{quantity}{file.GetFileSizeUnitString()}";
        }

        /// <summary>
        /// Gets a string representation of the most suitable file size unit.
        /// </summary>
        /// <returns>A string representation of the most suitable file size unit.</returns>
        public string GetFileSizeUnitString()
        {
            int numberOfDigits = file.Length.CountNumberOfDigits();

            return numberOfDigits switch
            {
                >= 16 => "PB",
                >= 13 and <= 15 => "TB",
                >= 10 and <= 12 => "GB",
                >= 7 and <= 9 => "MB",
                >= 4 and <= 6 => "KB",
                <= 3 => "B",
            };
        }
    }
}
