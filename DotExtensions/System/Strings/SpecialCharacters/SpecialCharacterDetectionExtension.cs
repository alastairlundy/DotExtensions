/*
        MIT License
       
       Copyright (c) 2020-2025 Alastair Lundy
       
       Permission is hereby granted, free of charge, to any person obtaining a copy
       of this software and associated documentation files (the "Software"), to deal
       in the Software without restriction, including without limitation the rights
       to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
       copies of the Software, and to permit persons to whom the Software is
       furnished to do so, subject to the following conditions:
       
       The above copyright notice and this permission notice shall be included in all
       copies or substantial portions of the Software.
       
       THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
       IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
       FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
       AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
       LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
       OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
       SOFTWARE.
   */

using System.Linq;

namespace AlastairLundy.DotExtensions.Strings;

/// <summary>
/// Provides extension methods for handling special characters in strings and characters.
/// </summary>
public static class SpecialCharacterDetectionExtension
{
    /// <summary>
    /// Provides extension methods for handling special characters in strings and characters.
    /// </summary>
    extension(char)
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
