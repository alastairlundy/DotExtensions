/*
        MIT License
       
       Copyright (c) 2024-2025 Alastair Lundy
       
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

// ReSharper disable RedundantBoolCompare

namespace AlastairLundy.DotExtensions.Collections.Strings.Enumerables
{
    public static class ToStringEnumerablesExtensions
    {
        /// <summary>
        /// Converts an IEnumerable of objects to an IEnumerable of strings. 
        /// </summary>
        /// <param name="source">The IEnumerable to be converted.</param>
        /// <typeparam name="T">The type of objects stored.</typeparam>
        /// <returns>The IEnumerable of strings.</returns>
        /// <exception cref="ArgumentException">Thrown if the object of type T doesn't implement a ToString method.</exception>
        public static IEnumerable<string> ToStringEnumerable<T>(this IEnumerable<T> source)
        {
            bool typeOverridesToString = typeof(T).GetMethods().
                Any(m => m is { Name: "ToString", IsStatic: false }
                         && m.GetParameters().Length == 0);
            
            if (typeOverridesToString == true)
            {
                List<string> list = new List<string>();

                if (typeof(T) != typeof(object))
                {
                    T[] enumerable = source as T[] ?? source.ToArray();
                    
                    list.AddRange(enumerable.Select(o => o?.ToString())!);
                }

                return list;
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            {
                throw new ArgumentException($"Type {nameof(T)} does not override virtual ToString method.");
            }
        }
    }
}