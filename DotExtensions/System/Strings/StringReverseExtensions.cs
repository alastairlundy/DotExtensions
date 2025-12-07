/*
 * Copyright (c) 2020-2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using System.Text;

namespace AlastairLundy.DotExtensions.Strings;

/// <summary>
/// Provides functionality for reversing the contents of a string.
/// </summary>
public static class StringReverseExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="str">The string to reverse.</param>
    extension(string str)
    {
        /// <summary>
        /// Reverses the contents of a string.
        /// </summary>
        /// <remarks>This method builds a reversed string using a <see cref="StringBuilder"/> for improved performance.</remarks>
        /// <returns>A new string with the reversed contents of the source string.</returns>
        /// <exception cref="ArgumentException">Thrown if the source string is null or empty.</exception>
        public string Reverse()
        {
            ArgumentException.ThrowIfNullOrEmpty(str);
            
            StringBuilder stringBuilder = new(capacity: str.Length);

            for (int i = str.Length - 1; i >= 0; i--)
            {
                stringBuilder.Append(str[i]);
            }

            return stringBuilder.ToString();
        }
    }
}
