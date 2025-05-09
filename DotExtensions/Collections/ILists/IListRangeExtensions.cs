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

    }
}