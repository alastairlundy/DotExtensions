/*
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using System.Linq;
using System.Text;

// ReSharper disable CheckNamespace
// ReSharper disable ConvertClosureToMethodGroup

namespace AlastairLundy.DotExtensions.Strings;

/// <summary>
/// Provides extension methods for handling special characters in strings and characters.
/// </summary>
public static class SpecialCharacterDetectionExtension
{
    
    extension(char ch)
    {
        /// <summary>
        /// Returns whether a character is a special character or not.
        /// </summary>
        /// <param name="c">The character to search</param>
        /// <returns>True if the char is a special character; false otherwise.</returns>
        public static bool IsSpecialCharacter(char c)
        {
            return !char.IsLetterOrDigit(c) && (char.IsPunctuation(c) || char.IsSymbol(c));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str">The string to search or modify.</param>
    extension(string str)
    {
        /// <summary>
        /// Returns whether a string contains a special character or not.
        /// </summary>
        /// <returns>True if the string contains a special character; false otherwise.</returns>
        public bool ContainsSpecialCharacter()
        {
            ArgumentException.ThrowIfNullOrEmpty(str);

            return str.Any(x => char.IsSpecialCharacter(x));
        }

        /// <summary>
        /// Removes special characters from a string.
        /// </summary>
        /// <returns>A new string with all the characters of the input string without special characters.</returns>
        /// <exception cref="ArgumentException">Thrown if the input string is null or empty.</exception>
        public string RemoveSpecialCharacters()
        {
            ArgumentException.ThrowIfNullOrEmpty(str);

            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in str)
            {
                if (!char.IsSpecialCharacter(c))
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }
    }
}
