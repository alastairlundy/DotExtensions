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

namespace AlastairLundy.Extensions.System.Strings.Cases;

    public static class CapitalizationExtensions
    {
        /// <summary>
        /// Capitalizes the first letter in a word.
        /// </summary>
        /// <param name="word">The word to be modified.</param>
        /// <returns>the updated word with the first letter capitalized.</returns>
        public static string CapitalizeFirstLetter(this string word)
        {
            return word.CapitalizeALetterInAWord(0);
        }
        
        /// <summary>
        /// Capitalizes the specified letter in the specified word.
        /// </summary>
        /// <param name="index">The letter position to be made upper case.</param>
        /// <param name="word">The word to be modified.</param>
        /// <returns>the specified word with the specified letter made upper case.</returns>
        public static string CapitalizeALetterInAWord(this string word, int index)
        {
            char oldChar = word[index]; 
            string newWord = word.Remove(index, 1);
            newWord = newWord.Insert(index, oldChar.ToString().ToUpper());

            return newWord;
        }
    }