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
/// Provides extension methods for working with strings to evaluate the presence of
/// space-separated substrings within them.
/// </summary>
public static class ContainsSubstringsExtensions
{
    /// <summary>
    /// Provides a polyfill implementation of string-related utility methods for
    /// compatibility with newer .NET features in older frameworks.
    /// </summary>
    extension(string s)
    {
        /// <summary>
        /// Determines if a string contains substrings separated by the specified delimiter.
        /// </summary>
        /// <param name="delimiter">The character used to separate substrings within the input string.</param>
        /// <returns>
        /// True if the string contains multiple substrings separated by the delimiter; false otherwise.
        /// The method checks for both the presence of the delimiter and that at least two distinct parts
        /// exist after splitting by it.
        /// </returns>
        public bool ContainsDelimitedSubstrings(char delimiter)
        {
            ArgumentException.ThrowIfNullOrEmpty(s);
            
            return s.Contains(delimiter) && s.Split(delimiter).Length > 1;
        }
    }
}