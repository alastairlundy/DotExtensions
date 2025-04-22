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

// ReSharper disable CheckNamespace
namespace AlastairLundy.DotExtensions.Strings
{
    public static class ContainsSpacesExtensions
    {

        /// <summary>
        /// Determines if a string contains space separated substrings within it.
        /// </summary>
        /// <param name="s">The string to search.</param>
        /// <returns>True if the string contains space separated strings within it; false otherwise.</returns>
        public static bool ContainsSpaceSeparatedSubStrings(this string s)
        {
#if NETSTANDARD2_0 || NETSTANDARD2_1
            return s.Contains(" ") && s.Split(' ').Length > 1;
#else
            return s.Contains(' ') && s.Split(' ').Length > 1;
#endif
        }
    }
}