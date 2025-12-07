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

using System.Collections.Generic;

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
