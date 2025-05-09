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

using AlastairLundy.DotExtensions.Collections.ILists;
using AlastairLundy.DotExtensions.Localizations;

namespace AlastairLundy.DotExtensions.Collections.Generic.ICollections
{
    public static class GenericCollectionAddRangeExtensions
    {

        /// <summary>
        /// Appends elements from another collection to the end of the specified collection.
        /// </summary>
        /// <param name="collection">The collection into which elements will be appended.</param>
        /// <param name="collectionToAdd">The collection containing elements to append to the original collection.</param>
        /// <typeparam name="T">The type of elements in both collections.</typeparam>
        public static void AddRange<T>(this ICollection<T> collection, ICollection<T> collectionToAdd)
        {
            if (collection is IList<T> list && collectionToAdd is IList<T> listToAdd)
            {
                IListAddRangeExtensions.AddRange(list, listToAdd);
                return;
            }

            foreach (T item in collectionToAdd)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentException"></exception>
        public static void RemoveRange<T>(this ICollection<T> collection, int startIndex, int count)
        {
            if (collection is IList<T> list)
            {
                IListRangeExtensions.RemoveRange(list, startIndex, count);
                return;
            }
            else
            {
                if (collection.Count > startIndex + count)
                {
                    throw new ArgumentException(Resources.Exceptions_Enumerables_CountArgumentTooLarge);
                }
                
                for (int index = startIndex; index < startIndex + count; index++)
                {
                   collection = collection.RemoveAt(index);
                }
            }
        }
    }
}