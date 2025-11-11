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
            char c = str[index];

            if (char.IsUpper(c))
                return str;

            return $"{str.Substring(0, index)}{char.ToUpper(c)}{str.Substring(index + 1)}";
        }

        /// <summary>
        /// Capitalizes the chars at the specified indices in the specified string.
        /// </summary>
        /// <param name="indices">The indices of the chars to capitalize.</param>
        /// <returns>A string with all the chars at the specified indices capitalized.</returns>
        public string CapitalizeChars(IEnumerable<int> indices)
        {
            StringBuilder stringBuilder = new StringBuilder(str);

            foreach (int index in indices)
            {
                if (char.IsUpper(str[index]) == false)
                    stringBuilder[index] = char.ToUpper(str[index]);
            }

            return stringBuilder.ToString();
        }
    }
}
