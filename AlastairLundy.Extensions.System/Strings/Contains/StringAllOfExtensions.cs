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

using System.Collections.Generic;
using System.Linq;
// ReSharper disable CheckNamespace

// ReSharper disable RedundantBoolCompare
// ReSharper disable ConvertClosureToMethodGroup

namespace AlastairLundy.Extensions.System.Strings
{
    public static class StringAllOfExtensions
    {
        /// <summary>
        /// Returns whether an item of type T contains all the specified values.
        /// </summary>
        /// <param name="source">The item to be searched.</param>
        /// <param name="values">The values to search for.</param>
        /// <returns>true if all the values are found in the string; returns false otherwise.</returns>
        public static bool ContainsAllOf(this string source, IEnumerable<char> values)
        {
            return values.Select(t => source.Contains(t)).All(containsSource => containsSource == true);
        }
    
        /// <summary>
        /// Returns whether an item of type T contains all the specified values.
        /// </summary>
        /// <param name="source">The item to be searched.</param>
        /// <param name="values">The values to search for.</param>
        /// <returns>true if all the values are found in the string; returns false otherwise.</returns>
        public static bool ContainsAllOf(this string source, IEnumerable<string> values)
        {
            return values.Select(t => source.Contains(t)).All(containsSource => containsSource == true);
        }
    }
}