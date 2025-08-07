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

using System;

namespace AlastairLundy.DotExtensions.Strings;

public static class UpperCaseExtensions
{
    /// <summary>
    /// Returns whether a character is an upper case character or not.
    /// </summary>
    /// <param name="c">The character to be checked.</param>
    /// <returns>True, if the character is an upper case character, false otherwise.</returns>
    [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
    public static bool IsUpperCaseCharacter(this char c) => IsUpperCase(c.ToString());

    /// <summary>
    /// Returns whether a string is upper case or not.
    /// </summary>
    /// <param name="s">The string to be checked.</param>
    /// <returns>True if the string is upper case; false otherwise.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static bool IsUpperCase(this string s)
    { 
        return s.Equals(s.ToUpper());
    }
}