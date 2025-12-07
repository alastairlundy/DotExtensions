/*
 * Copyright (c) 2020-2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.Strings;

/// <summary>
/// Provides extension methods for string manipulations focused on replacing the first or last occurrence of a substring.
/// </summary>
public static class StringReplaceLastExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="str">The input string in which the replacement is performed.</param>
    extension(string str)
    {
        /// <summary>
        /// Replaces the first occurrence of a specified substring with a new value in the given string.
        /// </summary>
        /// <param name="oldValue">The substring to be replaced.</param>
        /// <param name="newValue">The substring to replace the first occurrence of <paramref name="oldValue"/>.</param>
        /// <param name="stringComparison"></param>
        /// <returns>
        /// A new string where the first occurrence of <paramref name="oldValue"/> is replaced with <paramref name="newValue"/>.
        /// If <paramref name="oldValue"/> is not found in the input string, the original string is returned.
        /// </returns>
        public string ReplaceFirst(
            string oldValue,
            string newValue,
            StringComparison stringComparison = StringComparison.CurrentCulture
        )
        {
            ArgumentException.ThrowIfNullOrEmpty(oldValue);
            ArgumentException.ThrowIfNullOrEmpty(newValue);

            int lastIndex = str.IndexOf(oldValue, stringComparison);

            if (lastIndex == -1)
                return str;

            str = str.Remove(lastIndex, oldValue.Length);

            str = str.Insert(lastIndex, newValue);
            return str;
        }

        /// <summary>
        /// Replaces the last occurrence of a specified substring with a new value in the given string.
        /// </summary>
        /// <param name="oldValue">The substring to be replaced.</param>
        /// <param name="newValue">The substring to replace the last occurrence of <paramref name="oldValue"/>.</param>
        /// <param name="stringComparison"></param>
        /// <returns>
        /// A new string where the last occurrence of <paramref name="oldValue"/> is replaced with <paramref name="newValue"/>.
        /// If <paramref name="oldValue"/> is not found in the input string, the original string is returned.
        /// </returns>
        public string ReplaceLast(
            string oldValue,
            string newValue,
            StringComparison stringComparison = StringComparison.CurrentCulture
        )
        {
            ArgumentException.ThrowIfNullOrEmpty(oldValue);
            ArgumentException.ThrowIfNullOrEmpty(newValue);
            
            int lastIndex = str.LastIndexOf(oldValue, stringComparison);

            if (lastIndex == -1)
                return str;

            str = str.Remove(lastIndex, oldValue.Length);

            str = str.Insert(lastIndex, newValue);
            return str;
        }
    }
}
