/*
 * Copyright (c) 2020-2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

// ReSharper disable RedundantCallerArgumentExpressionDefaultValue

namespace AlastairLundy.DotExtensions.Strings;

/// <summary>
/// Provides extension methods for removing substrings from strings.
/// </summary>
public static class StringRemoveExtensions
{
    /// <param name="str">The string to remove the substring from.</param>
    extension(string str)
    {
        /// <summary>
        /// Removes all occurrences of a specified substring from the given string,
        /// using the specified string comparison rules.
        /// </summary>
        /// <param name="value">The substring to remove.</param>
        /// <param name="stringComparison">The rules to use for the substring comparison.</param>
        /// <returns>A new string with all occurrences of the specified substring removed.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when either the source string or <paramref name="value"/> is null or empty.
        /// </exception>
        public string RemoveAll(
            string value,
            StringComparison stringComparison = StringComparison.CurrentCulture
        )
        {
            ArgumentException.ThrowIfNullOrEmpty(str);
            ArgumentException.ThrowIfNullOrEmpty(value);

            while (str.Contains(value, stringComparison))
            {
                str = str.RemoveFirst(value);
            }

            return str;
        }

        /// <summary>
        /// Removes the first occurrence of a specified substring from the given string,
        /// using the specified string comparison rules.
        /// </summary>
        /// <param name="value">The substring to remove.</param>
        /// <param name="stringComparison">The rules to use for the substring comparison.</param>
        /// <returns>A new string with the first occurrence of the specified substring removed.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when either the source string or <paramref name="value"/> is null or empty,
        /// or when the substring is not found in the string.
        /// </exception>
        public string RemoveFirst(
            string value,
            StringComparison stringComparison = StringComparison.CurrentCulture
        )
        {
            ArgumentException.ThrowIfNullOrEmpty(str);
            ArgumentException.ThrowIfNullOrEmpty(value);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, str.Length);

            int index = str.IndexOf(value, stringComparison);

            ArgumentOutOfRangeException.ThrowIfNegative(index);
            
            return str.Remove(index, value.Length);
        }

        /// <summary>
        /// Removes the last occurrence of a specified substring from the given string,
        /// using the specified string comparison rules.
        /// </summary>
        /// <param name="value">The substring to remove.</param>
        /// <param name="stringComparison">The rules to use for the substring comparison.</param>
        /// <returns>A new string with the last occurrence of the specified substring removed.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when either the source string or <paramref name="value"/> is null or empty,
        /// or when the substring is not found in the string.
        /// </exception>
        public string RemoveLast(
            string value,
            StringComparison stringComparison = StringComparison.CurrentCulture
        )
        {
            ArgumentException.ThrowIfNullOrEmpty(str);
            ArgumentException.ThrowIfNullOrEmpty(value);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, str.Length);

            int index = str.LastIndexOf(value, stringComparison);

            ArgumentOutOfRangeException.ThrowIfNegative(index);
            
            return str.Remove(index, value.Length);
        }
    }
}
