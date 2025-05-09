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

using AlastairLundy.DotExtensions.Collections.ILists;
using AlastairLundy.DotExtensions.Localizations;

// ReSharper disable RedundantAssignment

namespace AlastairLundy.DotExtensions.Collections.Generic.Enumerables
{
    public static class EnumerableRangeExtensions
    {
        /// <summary>
        /// Adds a single element to the specified sequence of elements.
        /// </summary>
        /// <param name="source">The sequence from which the element will be removed.</param>
        /// <param name="item">The element to add to the sequence.</param>
        /// <typeparam name="T">The type of element in the sequence and item being added.</typeparam>
        public static void Add<T>(this IEnumerable<T> source, T item)
        {
            if (source is ICollection<T> collection)
            {
                collection.Add(item);
            }
            else
            {
                source = source.Append(item);
            }
        }
    
        /// <summary>
        /// Adds multiple elements to the specified sequence of elements.
        /// </summary>
        /// <param name="source">The sequence from which no elements will be removed.</param>
        /// <param name="toBeAdded">The elements to add to the sequence.</param>
        /// <typeparam name="T">The type of element in the sequence and elements being added.</typeparam>
        public static void AddRange<T>(this IEnumerable<T> source, IEnumerable<T> toBeAdded)
        {
            #region Faster IList Implementation
            if (source is IList<T> sourceList && toBeAdded is IList<T> listTwo)
            { 
                IListRangeExtensions.AddRange(sourceList, listTwo);
                return;
            }
            #endregion

            if (source is ICollection<T> sourceCollection)
            {
                foreach (T item in toBeAdded)
                {
                    sourceCollection.Add(item);
                }
            }
            else
            {
                foreach (T item in toBeAdded)
                {
                   source = source.Append(item);
                }
            }
        }
        
        /// <summary>
        /// Returns a range of elements from the startIndex to the number of elements required.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <typeparam name="T">The type of object stored in the collection.</typeparam>
        /// <returns>The items specified starting from the start index, with the specified number of additional items.</returns>
        public static IEnumerable<T> GetRange<T>(this IEnumerable<T> source, int startIndex, int count)
        {
            List<T> output = new();
            
            int i = 0;

            #region Faster IList implementation
            if (source is IList<T> list)
            {
                // Uses a faster IList GetRange implementation that avoids unnecessary ToArray enumeration.
               return IListRangeExtensions.GetRange(list, startIndex, count);
            }
            #endregion
            
            IList<T> enumerable = source as IList<T> ?? source.ToArray();
        
            if (enumerable.Count < count || startIndex < 0 || count <= 0 || count > enumerable.Count || startIndex > enumerable.Count)
            {
                throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                    .Replace("{x}", $"{startIndex}"
                        .Replace("{y}", $"0")
                        .Replace("{z}", $"{enumerable.Count}")));
            }
        
            int limit = startIndex + count;
            
            foreach (T item in enumerable)
            {
                if (i >= startIndex && i <= limit)
                {
                    output.Add(item);
                }

                i++;
            }

            return output;
        }

    }
}