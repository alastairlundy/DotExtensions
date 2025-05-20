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
using System.Collections;
using System.Collections.Generic;

namespace AlastairLundy.DotExtensions.Collections
{
    /// <summary>
    /// 
    /// </summary>
    public static class ToGenericsExtensions
    {
    
        /// <summary>
        /// Converts the specified collection to a generic collection of type T.
        /// </summary>
        /// <param name="source">The collection to convert.</param>
        /// <typeparam name="T">The type of elements in the resulting collection.</typeparam>
        /// <returns>A new generic collection containing all elements from the source collection.</returns>
        /// <exception cref="ArgumentException">Thrown if the source collection is null or cannot be converted to a generic collection.</exception>
        public static ICollection<T> ToGenericCollection<T>(this ICollection source)
        {
            ICollection<T> output = new List<T>();

            foreach (object item in source)
            {
                if (item is T t)
                {
                    output.Add(t);
                }
                else
                {
                    throw new ArgumentException($"Source does not contain objects of type {typeof(T).Name}");
                }
            }

            return output;
        }
    
        /// <summary>
        /// Converts a non-generic IList to a generic IList that stores objects of type T.
        /// </summary>
        /// <param name="list">The IList to convert.</param>
        /// <typeparam name="T">The type of items stored in the IList.</typeparam>
        /// <returns>A new IList that stores items of type T.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IList<T> ToGenericIList<T>(this IList list)
        {
            if (typeof(T) != list.GetType())
            {
                throw new ArgumentException(
                    $"Type specified of {typeof(T)} does not match IList of type {list.GetType()}.");
            }

            List<T> output = new();

            foreach (object obj in list)
            {
                if (obj is T t)
                {
                    output.Add(t);
                }
            }

            return output;
        }
    }
}