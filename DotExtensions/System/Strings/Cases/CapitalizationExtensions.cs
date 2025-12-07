/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using System.Collections.Generic;
using System.Text;

namespace AlastairLundy.DotExtensions.Strings;

/// <summary>
/// Provides extension methods for capitalizing characters within strings.
/// </summary>
public static class CapitalizationExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="str">The string to be modified.</param>
    extension(string str)
    {
        /// <summary>
        /// Capitalizes the char at the specified index in the specified string.
        /// </summary>
        /// <param name="index">The index of the char to be made upper case.</param>
        /// <returns>A string with the specified char made upper case.</returns>
        public string CapitalizeChar(int index)
        {
            ArgumentException.ThrowIfNullOrEmpty(str);
            
            char c = str[index];

            return char.IsUpper(c)
                ? str
                : new StringBuilder()
                    .Append(str, 0, index)
                    .Append(char.ToUpper(c))
                    .Append(str.Substring(index + 1))
                    .ToString();
        }

        /// <summary>
        /// Capitalizes the chars at the specified indices in the specified string.
        /// </summary>
        /// <param name="indices">The indices of the chars to capitalize.</param>
        /// <returns>A string with all the chars at the specified indices capitalized.</returns>
        public string CapitalizeChars(IEnumerable<int> indices)
        {
            ArgumentException.ThrowIfNullOrEmpty(str);
            
            StringBuilder stringBuilder = new StringBuilder(str);

            foreach (int index in indices)
            {
                if (!char.IsUpper(str[index]))
                    stringBuilder[index] = char.ToUpper(str[index]);
            }

            return stringBuilder.ToString();
        }
    }
}
