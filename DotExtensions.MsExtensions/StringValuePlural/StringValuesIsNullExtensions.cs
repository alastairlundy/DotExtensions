/*
 * Copyright (c) 2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

// ReSharper disable ConvertClosureToMethodGroup
// ReSharper disable RedundantBoolCompare

namespace AlastairLundy.DotExtensions.MsExtensions.StringValuePlural;

/// <summary>
/// Provides extension methods for working with <see cref="StringValues"/> to determine
/// if they are null, empty, or contain only null or whitespace values.
/// </summary>
public static class StringValuesIsNullExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strValues"></param>
    extension(StringValues strValues)
    {
        /// <summary>
        /// Whether this <see cref="StringValues"/> is empty.
        /// </summary>
        /// <returns>True if it is empty, false otherwise.</returns>
        public bool IsEmpty => strValues.Equals(StringValues.Empty);
    }

    /// <summary>
    /// 
    /// </summary>
    extension(StringValues)
    {
        /// <summary>
        /// Determines whether a <see cref="StringValues"/> contains any strings that are null or whitespace./>
        /// </summary>
        /// <param name="other"></param>
        /// <returns>True if any of the strings is WhiteSpace or null.</returns>
        public static bool IsNullOrWhiteSpace(StringValues? other)
        {
            if (other is null)
                return true;

            return StringValues.IsWhiteSpace((StringValues)other);
        }

        /// <summary>
        /// Determines whether a <see cref="StringValues"/> contains only strings that consist entirely of whitespace characters.
        /// </summary>
        /// <param name="other">The <see cref="StringValues"/> to check for whitespace characters.</param>
        /// <returns>True if all strings in the <see cref="StringValues"/> consist entirely of whitespace characters; otherwise, false.</returns>
        public static bool IsWhiteSpace(StringValues other)
        {
            bool[] vals = new bool[other.Count];

            for (int index = 0; index < other.Count; index++)
            {
                string? val = other[index];

                ArgumentNullException.ThrowIfNull(val);
                
                vals[index] = string.IsNullOrWhiteSpace(val);
            }

            return vals.Any(x => x == true);
        }
    }
}
