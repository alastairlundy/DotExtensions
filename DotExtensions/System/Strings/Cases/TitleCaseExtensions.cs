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
using System.Collections.Generic;
using System.Linq;
using System.Text;
// ReSharper disable CheckNamespace

// ReSharper disable RedundantBoolCompare

namespace AlastairLundy.DotExtensions.Strings;

/// <summary>
/// 
/// </summary>
public static class TitleCaseExtensions
{
    /// <summary>
    /// Checks whether a word is capitalized.
    /// </summary>
    /// <param name="word">The word to be checked.</param>
    /// <returns>True if the word is capitalized; false otherwise.</returns>
    [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
    public static bool IsWordTitleCase(this string word)
        => IsTitleCase(word);

    /// <summary>
    /// Converts a string to Title Case.
    /// </summary>
    /// <param name="str">The string to be converted.</param>
    /// <returns>The title case version of the input string.</returns>
    public static string ToTitleCase(this string str)
    {
        StringBuilder stringBuilder = new StringBuilder();
        
        string[] words = str.Split(' ');
            
        foreach (string word in words)
        {
            stringBuilder.Append(word.IsTitleCase() ? word :
                word.CapitalizeChar(0));
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Converts an IEnumerable of regular strings to an IEnumerable of Title Case Strings Like This.
    /// </summary>
    /// <param name="words">The array of strings to be converted.</param>
    /// <returns>The converted title case string enumerable.</returns>
    [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
    public static string ToTitleCase(this IEnumerable<string> words) 
        => string.Join(" ", words.Select(x => x.ToTitleCase()));

    /// <summary>
    /// Returns whether the specified phrase to be checked is in Title Case or not.
    /// </summary>
    /// <param name="phrase">The phrase to be checked.</param>
    /// <returns>True if the specified phrase is in Title Case, false otherwise.</returns>
    public static bool IsTitleCase(this string phrase)
    {
        string[] words = phrase.Split(' ');
            
        bool[] results = new bool[words.Length];
            
        for (int index = 0; index < words.Length; index++)
        {
            string word = words[index];

            bool[] letterCapitalization = new bool[word.Length];
            
            letterCapitalization[0] = char.IsUpper(word.First());
            
            for (int cIndex = 1; cIndex < word.Length; cIndex++)
            {
                letterCapitalization[cIndex] = char.IsLower(word[cIndex]);
            }
            
            results[index] = letterCapitalization.All(x => x == true);

            if (results[index] == false)
                return false;
        }

        return results.All(x => x == true);
    }
}