﻿/*
        MIT License
       
       Copyright (c) 2024 Alastair Lundy
       
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

namespace AlastairLundy.Extensions.System.Strings.Contains;

public static class ContainsSpacesExtensions
{
    /// <summary>
    /// Determine if a string contains a space character.
    /// </summary>
    /// <param name="s">The string to search.</param>
    /// <returns>true if a string contains a space character; false otherwise.</returns>
    public static bool ContainsSpace(this string s)
    {
        #if NET6_0_OR_GREATER
            return s.Contains(' ') || s.Contains(" ");
        #else
            return s.Contains(" ");
        #endif
    }
    
    /// <summary>
    /// Determine if a string contains space separated substrings within it.
    /// </summary>
    /// <param name="s">The string to search.</param>
    /// <returns>true if the string contains space separated strings within it; false otherwise.</returns>
    public static bool ContainsSpaceSeparatedSubStrings(this string s)
    {
        return s.Split(' ').Length > 0;
    }
}