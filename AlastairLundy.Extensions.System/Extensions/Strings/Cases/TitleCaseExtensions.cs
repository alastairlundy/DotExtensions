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

using System.Collections.Generic;
using System.Linq;
using System.Text;
// ReSharper disable CheckNamespace

// ReSharper disable RedundantBoolCompare

namespace AlastairLundy.Extensions.System.Strings
{
    public static class TitleCaseExtensions
    {
        /// <summary>
        /// Checks whether a word is capitalized.
        /// </summary>
        /// <param name="word">The word to be checked.</param>
        /// <returns>true if the word is capitalized; returns false otherwise.</returns>
        public static bool IsWordTitleCase(this string word)
        {
            bool[] letterCapitalization = new bool[word.Length];

            letterCapitalization[0] = word[0].IsUpperCaseCharacter();
            
            for (int index = 1; index < word.Length; index++)
            {
                letterCapitalization[index] = word[index].IsLowerCaseCharacter();
            }

            return letterCapitalization.All(x => x == true);
        }
        
        /// <summary>
        /// Converts an enumerable of regular strings to an enumerable of Title Case String Like This.
        /// </summary>
        /// <param name="words">The array of strings to be converted.</param>
        /// <returns>the converted title case string enumerable.</returns>
        public static string ToTitleCase(this IEnumerable<string> words)
        {
            StringBuilder stringBuilder = new StringBuilder();

            string[] enumerable = words as string[] ?? words.ToArray();
            
            for (int index = 0; index < enumerable.Length; index++)
            {
                stringBuilder.Append(enumerable[index].IsWordTitleCase() ? enumerable[index] :
                    enumerable[index].CapitalizeFirstLetter());
            }

            return stringBuilder.ToString();
        }
        
        /// <summary>
        /// Returns whether the specified phrase to be checked is in Title Case or not.
        /// </summary>
        /// <param name="phrase">The phrase to be checked.</param>
        /// <returns>true if the specified phrase is in Title Case; returns false otherwise.</returns>
        public static bool IsTitleCase(this string phrase)
        {
            string[] words = phrase.Split();
            
            bool[] results = new bool[words.Length];
            
            for (int index = 0; index < words.Length; index++)
            {
                results[index] = words[index].IsWordTitleCase();
            }

            return results.All(x => x == true);
        }
    }
}