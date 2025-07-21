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
using AlastairLundy.DotExtensions.Collections.ILists;

using AlastairLundy.DotExtensions.Deprecations;
using AlastairLundy.DotExtensions.Linq;
using AlastairLundy.DotExtensions.Localizations;

namespace AlastairLundy.DotExtensions.Collections.Generic.Enumerables;

/// <summary>
/// 
/// </summary>
public static class EnumerableRangeExtensions
{
    /// <summary>
    /// Adds multiple elements to the specified sequence of elements.
    /// </summary>
    /// <param name="source">The sequence to add items to.</param>
    /// <param name="toBeAdded">The elements to add to the sequence.</param>
    /// <typeparam name="T">The type of element in the sequence and elements being added.</typeparam>
    public static IEnumerable<T> AddRange<T>(this IEnumerable<T> source, IEnumerable<T> toBeAdded)
    {
        if (source is IList<T> sourceList && toBeAdded is IList<T> listTwo)
        { 
            IListRangeExtensions.AddRange(sourceList, listTwo);
            return sourceList;
        }
        if (source is ICollection<T> sourceCollection)
        {
            GenericCollectionRangeExtensions.AddRange(sourceCollection, toBeAdded);
            return sourceCollection;
        }
        else
        {
            List<T> list = new List<T>(source);
            
            foreach (T item in toBeAdded)
            {
                list.Add(item);
            }

            return list;
        }
    }
        

    /// <summary>
    /// Returns a sequence of elements from the specified start index to a distance of 'count' elements.
    /// </summary>
    /// <param name="source">The source sequence of elements.</param>
    /// <param name="startIndex">The starting index of the range (inclusive).</param>
    /// <param name="count">The number of elements to include in the range.</param>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <returns>A new sequence containing the specified range of elements from the source sequence.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown if the start index is out of range for the source sequence.</exception>
    [Obsolete(DeprecationMessages.DeprecationV8)]
    public static IEnumerable<T> GetRange<T>(this IEnumerable<T> source, int startIndex, int count)
    {
        List<T> output = new();

        #region Optimized IList Code
        if (source is IList<T> list)
        {
            return IListRangeExtensions.GetRange(list, startIndex, count);
        }
        #endregion

        #region Optimized ICollection Code
        if (source is ICollection<T> collection)
        {
            return GenericCollectionRangeExtensions.GetRange(collection, startIndex, count);
        }
        #endregion

        #region IEnumerable Code
        IList<T> newList = source.ToArray();
        
        if (newList.Count < count || startIndex < 0 || count <= 0 || count > newList.Count || startIndex > newList.Count)
        {
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", $"{startIndex}"
                    .Replace("{y}", $"0")
                    .Replace("{z}", $"{newList.Count}")));
        }
            
        int i = 0;
        int limit = startIndex + count;
            
        foreach (T item in newList)
        {
            if (i >= startIndex && i <= limit)
            {
                output.Add(item);
            }

            i++;
        }

        return output;
        #endregion
    }

    /// <summary>
    /// Removes a specified range of elements from the given sequence.
    /// </summary>
    /// <param name="source">The input sequence from which elements will be removed.</param>
    /// <param name="startIndex">Zero-based index of the first element in the range to remove.</param>
    /// <param name="count">Number of elements in the range to remove.</param>
    /// <typeparam name="T">Type of elements in the source sequence.</typeparam>
    /// <returns>An enumerable sequence containing all elements except those specified in the range.</returns>
    /// <exception cref="ArgumentException">Thrown when startIndex or count is out of range.</exception>
    [Obsolete(DeprecationMessages.DeprecationV8)]
    public static IEnumerable<T> RemoveRange<T>(this IEnumerable<T> source, int startIndex, int count)
    {
        #region Optimized IList implementation
        if (source is IList<T> list)
        {
            IListRangeExtensions.RemoveRange(list, startIndex, count);
            return list;
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
                throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
            }

            for (int index = startIndex; index < limit; index++)
            {
                enumerableList.RemoveAt(index);   
            }
        if (startIndex < 0 || startIndex > enumerableList.Count)
        {
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", $"{startIndex}")
                .Replace("{y}", "0")
                .Replace("{z}", $"{enumerableList.Count}"));
        }
            
        return enumerableList;
    }
        
    /// <summary>
    /// Removes items from an IEnumerable.
    /// </summary>
    /// <param name="source">The IEnumerable to have items removed from.</param>
    /// <param name="itemsToBeRemoved">The items to be removed.</param>
    /// <typeparam name="T">The type of elements stored in the IEnumerable.</typeparam>
    /// <returns>The new IEnumerable with the specified items removed.</returns>
    [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
    public static IEnumerable<T> Remove<T>(this IEnumerable<T> source, IEnumerable<T> itemsToBeRemoved)
    {
        return source.RemoveRange(itemsToBeRemoved);
    }
}