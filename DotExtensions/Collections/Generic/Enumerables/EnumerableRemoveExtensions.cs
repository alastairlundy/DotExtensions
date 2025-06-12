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

using AlastairLundy.DotExtensions.Collections.Generic.ICollections;

// ReSharper disable MemberCanBePrivate.Global

namespace AlastairLundy.DotExtensions.Collections.Generic.Enumerables
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumerableRemoveExtensions
    {
    
        /// <summary>
        /// Removes an item from an IEnumerable.
        /// </summary>
        /// <param name="source">The IEnumerable to have an item removed from it.</param>
        /// <param name="itemToBeRemoved">The item to be removed.</param>
        /// <typeparam name="T">The type of elements stored in the IEnumerable.</typeparam>
        /// <returns>The new IEnumerable with the specified item removed.</returns>
        public static IEnumerable<T> Remove<T>(this IEnumerable<T> source, T itemToBeRemoved)
        {
            #region IList Optimized Code
            if (source is IList<T> list)
            {
                list.Remove(itemToBeRemoved);
                return list;
            }
            #endregion
            
            #region ICollection Optimized Code
            if (source is ICollection<T> collection)
            {
                collection.Remove(itemToBeRemoved);
                return collection;
            }
            #endregion
            
            return from item in source
                where item.Equals(itemToBeRemoved) == false
                    select item;
        }

        /// <summary>
        /// Removes the item located at the specified index from an IEnumerable.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="index">The index of the item to be removed.</param>
        /// <typeparam name="T">The type of item stored in the IEnumerable.</typeparam>
        /// <returns>The IEnumerable with the index of the specified item removed.</returns>
        [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
        public static IEnumerable<T> RemoveAt<T>(this IEnumerable<T> source, int index)
        {
            #region Faster IList implementation
            if (source is IList<T> list)
            {
                 list.RemoveAt(index);
                 return list;
            }
            #endregion
            
            #region Faster ICollection implementation
            if (source is ICollection<T> collection)
            {
                return GenericCollectionRemoveExtensions.RemoveAt(collection, index);
            }
            #endregion
            
            IList<T> enumerable = source.ToList();
            
            enumerable.RemoveAt(index);
            return enumerable;
        }
    }
}