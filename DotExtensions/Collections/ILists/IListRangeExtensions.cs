/*
        MIT License
       
       Copyright (c) 2025 Alastair Lundy
       
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

using AlastairLundy.DotExtensions.Localizations;

// ReSharper disable InconsistentNaming

namespace AlastairLundy.DotExtensions.Collections.ILists
{
    public static class IListRangeExtensions
    {
    
        /// <summary>
        /// Adds a range of elements from an IEnumerable to the current list.
        /// </summary>
        /// <param name="list">The list into which elements will be added.</param>
        /// <param name="listToAdd">The IEnumerable containing elements to add to the list.</param>
        /// <typeparam name="T">The type of elements in both lists.</typeparam>
        /// <exception cref="OverflowException">Thrown if an attempt is made to add an element that exceeds the maximum allowed capacity of the list.</exception>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> listToAdd)
        {
            if (list.Count == int.MaxValue)
            {
                throw new OverflowException($"{nameof(list)} contains the maximum size of {int.MaxValue} and cannot be added to.");
            }
        
            if (list is List<T> actualList && listToAdd is IList<T> iListToAdd)
            {
                actualList.AddRange(iListToAdd);
                return;
            }
        
            foreach (T item in listToAdd)
            {
                list.Add(item);
            }
        }
    

        /// <summary>
        /// Appends elements from another collection to the end of the specified list.
        /// </summary>
        /// <param name="list">The list into which elements will be appended.</param>
        /// <param name="listToAdd">The collection containing elements to append to the list.</param>
        /// <typeparam name="T">The type of elements in both lists.</typeparam>
        /// <exception cref="OverflowException">Thrown if an attempt is made to add an element that exceeds the maximum allowed capacity of the list.</exception>
        public static void AddRange<T>(this IList<T> list, IList<T> listToAdd)
        {
            if (list.Count == int.MaxValue)
            {
                throw new OverflowException($"{nameof(list)} contains the maximum size of {int.MaxValue} and cannot be added to.");
            }
            else if (listToAdd.Count == int.MaxValue)
            {
                throw new OverflowException($"{nameof(listToAdd)} contains the maximum size of {int.MaxValue} and cannot be added to {nameof(list)}.");
            }
        
            if (list is List<T> actualList)
            {
                actualList.AddRange(listToAdd);
                return;
            }
        
            foreach (T item in listToAdd)
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="values"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static void InsertRange<T>(this IList<T> list, int index, IEnumerable<T> values)
        {
            if (index < 0 || index > list.Count)
            {
                throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                    .Replace("{x}", $"{index}")
                    .Replace("{y}", $"0")
                    .Replace("z", $"{list.Count}"));
            }
            
            int newIndex = index;

            foreach (T value in values)
            {
                if (newIndex >= list.Count)
                {
                    list.Add(value);       
                }
                else
                {
                    list.Insert(newIndex, value);
                }
                
                newIndex++;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static IList<T> GetRange<T>(this IList<T> list, int startIndex, int count)
        {
            List<T> output = new List<T>();
            int limit;
            
            if (list.Count < startIndex + count)
            {
                limit = startIndex + count;
            }
            else
            {
                throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                    .Replace("{x}", $"{count}")
                    .Replace("{y}", "0")
                    .Replace("{z}", $"{list.Count}"));
            }
                
            for (int index = startIndex; index < limit; index++)
            {
                output.Add(list[index]);
            }
                
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void RemoveRange<T>(this IList<T> list, int startIndex, int count)
        {
            int limit;
            
            if (list.Count < startIndex + count)
            {
                limit = startIndex + count;
            }
            else if (startIndex >= list.Count || startIndex < 0 ) 
            {
                throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                    .Replace("{x}", $"{startIndex}")
                    .Replace("{y}", "0")
                    .Replace("{z}", $"{list.Count}"));
            }
            else
            {
                throw new ArgumentException(Resources.Exceptions_Enumerables_CountArgumentTooLarge);
            }
            
            for (int index = startIndex; index < limit; index++)
            {
                list.RemoveAt(index);
            }
        }
    }
}