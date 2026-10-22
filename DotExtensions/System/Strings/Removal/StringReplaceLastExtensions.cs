/*
        MIT License

       Copyright (c) 2026 Alastair Lundy

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

namespace DotExtensions.Strings;

/// <summary>
/// Provides extension methods for string manipulations focused on replacing the first or last occurrence of a substring.
/// </summary>
public static class StringReplaceLastExtensions
{
    /// <param name="str">The input string to replace a section of.</param>
    extension(string str)
    {
        /// <summary>
        /// Replaces the first occurrence of a specified substring with a new value in the given string.
        /// </summary>
        /// <param name="oldValue">The substring to be replaced.</param>
        /// <param name="newValue">The substring to replace the first occurrence of <paramref name="oldValue"/>. May be empty to remove the occurrence.</param>
        /// <param name="stringComparison"></param>
        /// <returns>
        /// A new string where the first occurrence of <paramref name="oldValue"/> is replaced with <paramref name="newValue"/>.
        /// Returns the original string if <paramref name="oldValue"/> is not found in the input string.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> or <paramref name="newValue"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="oldValue"/> is null or empty.</exception>
        public string ReplaceFirst(
            string oldValue, string newValue,
            StringComparison stringComparison = StringComparison.CurrentCulture)
        {
            ArgumentNullException.ThrowIfNull(str);
            ArgumentException.ThrowIfNullOrEmpty(oldValue);
            ArgumentNullException.ThrowIfNull(newValue);

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
        /// <param name="newValue">The substring to replace the last occurrence of <paramref name="oldValue"/>. May be empty to remove the occurrence.</param>
        /// <param name="stringComparison"></param>
        /// <returns>
        /// A new string where the last occurrence of <paramref name="oldValue"/> is replaced with <paramref name="newValue"/>.
        /// Returns the original string if <paramref name="oldValue"/> is not found.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> or <paramref name="newValue"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="oldValue"/> is null or empty.</exception>
        public string ReplaceLast(
            string oldValue, string newValue,
            StringComparison stringComparison = StringComparison.CurrentCulture)
        {
            ArgumentNullException.ThrowIfNull(str);
            ArgumentException.ThrowIfNullOrEmpty(oldValue);
            ArgumentNullException.ThrowIfNull(newValue);

            int lastIndex = str.LastIndexOf(oldValue, stringComparison);

            if (lastIndex == -1)
                return str;

            str = str.Remove(lastIndex, oldValue.Length);

            str = str.Insert(lastIndex, newValue);
            return str;
        }
    }
}