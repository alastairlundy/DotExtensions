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
using System.Text;
// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable RedundantBoolCompare

namespace AlastairLundy.DotExtensions.Strings;

/// <summary>
/// Provides extension methods for converting strings to title case and checking if a string is in title case.
/// </summary>
public static class TitleCaseExtensions
{
    /// <summary>
    /// Converts a string to Title Case.
    /// </summary>
    /// <param name="str">The string to be converted.</param>
    /// <returns>The title case version of the input string.</returns>
    public static string ToTitleCase(this string str)
    {
        string[] words = str.Split(' ');
        
        StringBuilder stringBuilder = new StringBuilder();
            
        foreach (string word in words)
        {
            stringBuilder.Append(word.IsTitleCase() ? word :
                word.CapitalizeChar(1));
        }
        
        return stringBuilder.ToString();
    }
        
    /// <summary>
    /// Returns whether the specified phrase to be checked is in Title Case or not.
    /// </summary>
    /// <param name="phrase">The phrase to be checked.</param>
    /// <returns>True if the specified phrase is in Title Case, false otherwise.</returns>
    public static bool IsTitleCase(this string phrase)
    {
        string[] words = phrase.Split();
            
        return words.All(x => char.IsUpper(x[0]) && x.Substring(1, x.Length - 1).IsLowerCase());
    }
}