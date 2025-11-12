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

// ReSharper disable ConvertClosureToMethodGroup

namespace AlastairLundy.DotExtensions.Strings;

/// <summary>
/// Provides extension methods for string case-related operations.
/// </summary>
public static class CaseExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    extension(string s)
    {
        /// <summary>
        /// Returns whether this string is in the upper case or not.
        ///
        /// <para>True if the string is upper case; false otherwise.</para>
        /// </summary>
        public bool IsUpperCase() => s.All(x => char.IsUpper(x));

        /// <summary>
        /// Returns whether a string is lower case or not.
        ///
        /// <para>True if a string is lower case; false otherwise.</para>
        /// </summary>
        bool IsLowerCase() => s.All(x => char.IsLower(x));
    }
}
