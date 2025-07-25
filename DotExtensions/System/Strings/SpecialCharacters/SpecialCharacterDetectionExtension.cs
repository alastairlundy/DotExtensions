﻿/*
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
// ReSharper disable CheckNamespace
// ReSharper disable ConvertClosureToMethodGroup

namespace AlastairLundy.DotExtensions.Strings;

/// <summary>
/// 
/// </summary>
public static class SpecialCharacterDetectionExtension
{
    /// <summary>
    /// Returns whether a string contains a special character or not.
    /// </summary>
    /// <param name="s">The string to be checked.</param>
    /// <returns>True if the string contains a special character; false otherwise.</returns>
    public static bool ContainsSpecialCharacter(this string s)
    {
        return s.Any(x => IsSpecialCharacter(x));
    }
        
    /// <summary>
    /// Returns whether a character is a special character or not.
    /// </summary>
    /// <param name="c">The character to be checked.</param>
    /// <returns>True if the character is a special character; false otherwise.</returns>
    public static bool IsSpecialCharacter(this char c)
    {
        return CharacterConstants.SpecialCharacters.Any(sc => sc == c);
    }
}