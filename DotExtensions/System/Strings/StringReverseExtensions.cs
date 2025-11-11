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

using System;
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
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException();

            StringBuilder stringBuilder = new(capacity: str.Length);

            for (int i = str.Length - 1; i >= 0; i--)
            {
                stringBuilder.Append(str[i]);
            }

            return stringBuilder.ToString();
        }
    }
}
