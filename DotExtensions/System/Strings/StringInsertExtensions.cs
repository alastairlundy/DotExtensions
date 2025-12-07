/*
 * Copyright (c) 2020-2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using System.Collections.Generic;
using System.Text;

namespace AlastairLundy.DotExtensions.Strings;

/// <summary>
/// Provides extension methods for inserting characters or a range of characters into a string at a specified position.
/// </summary>
public static class StringInsertExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="str">The string to insert characters into.</param>
    extension(string str)
    {
        /// <summary>
        /// Inserts a character in a string at a specified index.
        /// </summary>
        /// <param name="index">The index to insert the new character at.</param>
        /// <param name="c">The character to insert in the string.</param>
        /// <returns>The updated string with the inserted character at the specified index.</returns>
        public string Insert(int index, char c)
        {
            ArgumentException.ThrowIfNullOrEmpty(str);
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, str.Length);
            
            if (index < 0 || index >= str.Length)
                throw new IndexOutOfRangeException();

            if (str[index] == c)
                return str;

            StringBuilder stringBuilder = new StringBuilder();

            int start = 0;

            if (index > 1)
            {
                stringBuilder.Append(str.Substring(0, index - 1));
                start = index;
            }

            for (int i = start; i < str.Length; i++)
            {
                stringBuilder.Append(i == index ? c : str[i]);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Inserts characters in a string at a specified index.
        /// </summary>
        /// <param name="index">The index to insert the new characters at.</param>
        /// <param name="chars">The characters to insert starting at the specified index.</param>
        /// <returns>The updated string with the inserted characters starting at the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public string InsertRange(int index, IEnumerable<char> chars)
        {
            ArgumentException.ThrowIfNullOrEmpty(str);
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, str.Length);

            if (index < 0 || index >= str.Length)
                throw new IndexOutOfRangeException();

            StringBuilder stringBuilder = new StringBuilder();

            int start = 0;

            if (index > 1)
            {
                stringBuilder.Append(str.Substring(0, index - 1));
                start = index;
            }

            foreach (char ch in chars)
            {
                stringBuilder.Append(ch);
            }

            stringBuilder.Append(str.Substring(start + 1, str.Length - (start + 1)));

            return stringBuilder.ToString();
        }
    }
}
