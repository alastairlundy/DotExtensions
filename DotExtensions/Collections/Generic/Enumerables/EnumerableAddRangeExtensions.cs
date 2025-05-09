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

using System.Collections.Generic;
using System.Linq;

using AlastairLundy.DotExtensions.Collections.ILists;
// ReSharper disable RedundantAssignment

namespace AlastairLundy.DotExtensions.Collections.Generic.Enumerables
{
    public static class EnumerableAddRangeExtensions
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
            if (source is IList<T> sourceList && toBeAdded is IList<T> listTwo)
            { 
                IListAddRangeExtensions.AddRange(sourceList, listTwo);
                return;
            }

            if (source is ICollection<T> sourceCollection)
            {
                foreach (T item in toBeAdded)
                {
                    sourceCollection.Add(item);
                }
            }
            else
            {
                List<T> list = source.ToList();
            
                foreach (T item in toBeAdded)
                {
                    list.Add(item);
                }

                source = list;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IEnumerable<T> RemoveRange<T>(this IEnumerable<T> source, int startIndex, int count)
        {
            #region Faster IList implementation
            if (source is IList<T> list)
            {
                IListRangeExtensions.RemoveRange(list, startIndex, count);
                return list;
            }
            #endregion

            #region Faster ICollection implementation

            if (source is ICollection<T> collection)
            {
                ICollections.GenericCollectionRangeExtensions.RemoveRange(collection, startIndex, count);
                return collection;
            }
            #endregion
            
            IList<T> enumerableList = source.ToList();

            int limit;

            if (enumerableList.Count >= (startIndex + count))
            {
                limit = startIndex + count;
            }
            else
            {
                throw new ArgumentException(Resources.Exceptions_Enumerables_CountArgumentTooLarge);
            }

            for (int index = startIndex; index < limit; index++)
            {
                enumerableList.RemoveAt(index);   
            }
            
            return enumerableList;
        }
    }
}