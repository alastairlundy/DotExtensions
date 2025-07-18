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

using AlastairLundy.DotExtensions.Deprecations;
using AlastairLundy.DotExtensions.Localizations;
using AlastairLundy.DotExtensions.Numbers;

namespace AlastairLundy.DotExtensions.Collections.Generic.ICollections;

/// <summary>
/// 
/// </summary>
public static class GenericCollectionRangeExtensions
{

    /// <summary>
    /// Appends elements from another collection to the end of the specified collection.
    /// </summary>
    /// <param name="source">The collection into which elements will be appended.</param>
    /// <param name="enumerableToAdd">The IEnumerable containing elements to append to the original collection.</param>
    /// <typeparam name="T">The type of elements in both collections.</typeparam>
    public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> enumerableToAdd)
    {
        if (source is IList<T> list1)
        {
            IListRangeExtensions.AddRange(list1, enumerableToAdd);
            return;
        }

        foreach (T item in enumerableToAdd)
        {
            source.Add(item);
        }
    }
        
    /// <summary>
    /// Appends elements from another collection to the end of the specified collection.
    /// </summary>
    /// <param name="source">The collection into which elements will be appended.</param>
    /// <param name="collectionToAdd">The collection containing elements to append to the original collection.</param>
    /// <typeparam name="T">The type of elements in both collections.</typeparam>
    public static void AddRange<T>(this ICollection<T> source, ICollection<T> collectionToAdd)
    {
        if (source is IList<T> list && collectionToAdd is IList<T> listToAdd)
        {
            IListRangeExtensions.AddRange(list, listToAdd);
            return;
        }

        foreach (T item in collectionToAdd)
        {
            source.Add(item);
        }
    }


    /// <summary>
    /// Inserts elements at a specified position in the collection.
    /// </summary>
    /// <param name="source">The collection into which elements will be inserted.</param>
    /// <param name="index">The index at which elements will be inserted.</param>
    /// <param name="values">The IEnumerable containing elements to insert at the specified index.</param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <exception cref="IndexOutOfRangeException">Thrown if the specified index is out of range for the collection.</exception>
    public static void InsertRange<T>(this ICollection<T> source, int index, IEnumerable<T> values)
    {
        if (index < 0 || index >= source.Count)
        {
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", index.ToString())
                .Replace("{y}", "0")
                .Replace("{z}", $"{source.Count}"));
        }
            
        #region Use IList Optimized Code Path if IList
        if (source is IList<T> list)
        {
            IListRangeExtensions.InsertRange(list, index, values);
            return;
        }
        #endregion

        int numberOfItemsToBeRemoved = source.Count - index;

        List<T> removedItems = new();

        int i = 0;
        foreach (T item in source)
        {
            if (i >= index && i < source.Count)
            {
                removedItems.Add(item);
            }

            i += 1;
        }

        List<T> newSource = source.ToList();
        newSource.RemoveRange(index, numberOfItemsToBeRemoved);

        newSource.AddRange(values);
        newSource.AddRange(removedItems);
        source.Clear();

        AddRange(source, newSource);
    }
    
}