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

namespace AlastairLundy.DotExtensions.Collections.Generic.ICollections
{
    public static class GenericCollectionRangeExtensions
    {

        /// <summary>
        /// Appends elements from another collection to the end of the specified collection.
        /// </summary>
        /// <param name="collection">The collection into which elements will be appended.</param>
        /// <param name="enumerableToAdd">The IEnumerable containing elements to append to the original collection.</param>
        /// <typeparam name="T">The type of elements in both collections.</typeparam>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> enumerableToAdd)
        {
            if (collection is IList<T> list && enumerableToAdd is IList<T> listToAdd)
            {
                IListRangeExtensions.AddRange(list, listToAdd);
                return;
            }
            else if (collection is IList<T> list1)
            {
                IListRangeExtensions.AddRange(list1, enumerableToAdd);
                return;
            }

            foreach (T item in enumerableToAdd)
            {
                collection.Add(item);
            }
        }
        
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
                IListRangeExtensions.AddRange(list, listToAdd);
                return;
            }

            foreach (T item in collectionToAdd)
            {
                collection.Add(item);
            }
        }

        
        public static void InsertRange<T>(this ICollection<T> collection, int index, IEnumerable<T> values)
        {
            #region Use IList Optimized Code Path if IList
            if (collection is IList<T> list)
            {
               IListRangeExtensions.InsertRange(list, index, values);
               return;
            }
            #endregion
            else
            {
                int numberOfItemsToBeRemoved = collection.Count - index;

                List<T> removedItems = new();

                int i = 0;
                foreach (T item in collection)
                {
                    if (i >= index && i < collection.Count)
                    {
                        removedItems.Add(item);
                    }

                    i += 1;
                }

                collection.RemoveRange(index, numberOfItemsToBeRemoved);

                collection.AddRange(values);

                collection.AddRange(removedItems);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="indexes"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ICollection<T> GetRange<T>(this ICollection<T> collection, IEnumerable<int> indexes)
        {
            #region Optimized IList code
            if (collection is IList<T> list)
            {
                List<T> output = new();
                
                foreach (int index in indexes)
                {
                    output.Add(list[index]);
                }
                
                return output;
            }
            #endregion
            else
            {
                List<T> output = new();

                IList<T> collectionList = collection as IList<T> ?? collection.ToArray();

                foreach (int index in indexes)
                {
                    output.Add(collectionList[index]);
                }
                
                return output;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static ICollection<T> GetRange<T>(this ICollection<T> collection, int startIndex, int count)
        {
            int endIndex = startIndex + count;
            
            if (endIndex > collection.Count)
            {
                throw new ArgumentException(Resources.Exceptions_Enumerables_CountArgumentTooLarge);
            }

            if (startIndex < 0 || startIndex >= collection.Count)
            {
                throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                    .Replace("{x}", $"{startIndex}")
                    .Replace("{y}", $"0")
                    .Replace("{z}", $"{endIndex}"));
            }
            
            #region Optimized IList code
            if (collection is IList<T> list)
            {
               return IListRangeExtensions.GetRange(list, startIndex,  count);
            }
            #endregion
            else
            {
                List<int> numbers = new();
                
                for (int i = startIndex; i < endIndex; i++)
                {
                    numbers.Add(i);
                }
                
                return GetRange(collection, numbers);
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