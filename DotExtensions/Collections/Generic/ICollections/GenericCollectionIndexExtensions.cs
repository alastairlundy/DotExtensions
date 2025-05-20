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

using AlastairLundy.DotExtensions.Collections.Generic.Enumerables;

// ReSharper disable MemberCanBePrivate.Global

namespace AlastairLundy.DotExtensions.Collections.Generic.ICollections
{
    public static class GenericCollectionIndexExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int IndexOf<T>(this ICollection<T> source, T item)
        {
            int index = 0;
            foreach (T item1 in source)
            {
                if (item is not null && item.Equals(item1))
                {
                    return index;
                }
                index++;
            }
            
            return -1;
        }
        
        /// <summary>
        /// Attempts to get the index of a specified element in a collection.
        /// </summary>
        /// <param name="collection">The collection to be searched.</param>
        /// <param name="item">The item to attempt to get the index of.</param>
        /// <param name="index">the index of an object in a collection if found; null otherwise.</param>
        /// <typeparam name="T">The type of the object in the collection.</typeparam>
        /// <returns>True if an index can be found for an item in a collection; false otherwise.</returns>
        public static bool TryGetIndexOf<T>(this ICollection<T> collection, T item, out int? index)
        {
            try
            {
                index = collection.IndexOf(item);
                return true;
            }
            catch
            {
                index = null;
                return false;
            }
        }

        /// <summary>
        /// Attempts to get the indexes of a specified element in a collection.
        /// </summary>
        /// <param name="collection">The collection to be searched.</param>
        /// <param name="item">The item to attempt to get the indexes of.</param>
        /// <param name="indexes">the indexes of an object in a collection if found; null otherwise.</param>
        /// <typeparam name="T">The type of the object in the collection.</typeparam>
        /// <returns>True if one or more indexes can be found for an item in a collection; false otherwise.</returns>
        public static bool TryGetIndexesOf<T>(this ICollection<T> collection, T item, out IEnumerable<int>? indexes)
        {
            try
            {
                indexes = collection.IndexesOf(item);

                if (indexes.Any() == false)
                {
                    throw new KeyNotFoundException();    
                }
                
                return true;
            }
            catch
            {
                indexes = null;
                return false;
            }
        }
    }
}