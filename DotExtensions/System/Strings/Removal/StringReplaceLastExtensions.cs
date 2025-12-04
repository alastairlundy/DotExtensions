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
