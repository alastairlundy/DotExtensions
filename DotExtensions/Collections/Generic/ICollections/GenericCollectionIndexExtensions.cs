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

using AlastairLundy.DotExtensions.Collections.Generic.Enumerables;

// ReSharper disable MemberCanBePrivate.Global

namespace AlastairLundy.DotExtensions.Collections.Generic.ICollections;

public static class GenericCollectionIndexExtensions
{
    /// <summary>
    /// Determines the index of the specified element within the given collection.
    /// </summary>
    /// <param name="source">The collection to search in.</param>
    /// <param name="item">The element to find.</param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <returns>The zero-based index of the item if found, -1 otherwise.</returns>
    [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
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
    /// Retrieves a collection of indices where the specified element can be found within the given collection.
    /// </summary>
    /// <param name="source">The collection to search in.</param>
    /// <param name="item">The element to find.</param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <returns>A collection of zero-based indices where the item is found,
    /// or an empty collection if not found.</returns>
    [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
    public static ICollection<int> IndicesOf<T>(this ICollection<T> source, T item)
    {
        return (ICollection<int>)EnumerableIndexExtensions.IndicesOf(source, item);
    }
        
    /// <summary>
    /// Returns a dictionary where each key is an element in the source collection,
    /// and its corresponding value is a collection of indices
    /// where that element occurs within the source collection.
    /// </summary>
    /// <param name="source">The initial collection to search.</param>
    /// <typeparam name="T">The type of elements within the source collection.</typeparam>
    /// <returns>A dictionary mapping each element in the source collection,
    /// to its corresponding indices.</returns>
    [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
    public static Dictionary<T, ICollection<int>> IndicesOfElements<T>(this ICollection<T> source)
    {
        Dictionary<T, ICollection<int>> output = new Dictionary<T, ICollection<int>>();

        int index = 0;
        foreach (T item in source)
        {
            if (output.ContainsKey(item))
            {
                output[item].Add(index);
            }
            else
            {
                output.Add(item, new List<int>());
                output[item].Add(index);
            }

            index++;
        }
            
        return output;
    }
        
    /// <summary>
    /// Attempts to get the index of a specified element in a collection.
    /// </summary>
    /// <param name="collection">The collection to be searched.</param>
    /// <param name="item">The item to attempt to get the index of.</param>
    /// <param name="index">the index of an object in a collection if found; null otherwise.</param>
    /// <typeparam name="T">The type of the object in the collection.</typeparam>
    /// <returns>True if an index can be found for an item in a collection; false otherwise.</returns>
    [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
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
    /// Attempts to get the indices of a specified element in a collection.
    /// </summary>
    /// <param name="collection">The collection to be searched.</param>
    /// <param name="item">The item to attempt to get the indices of.</param>
    /// <param name="indices">the indices of an object in a collection if found; null otherwise.</param>
    /// <typeparam name="T">The type of the object in the collection.</typeparam>
    /// <returns>True if one or more indices can be found for an item in a collection; false otherwise.</returns>
    [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
    public static bool TryGetIndicesOf<T>(this ICollection<T> collection, T item, out IEnumerable<int>? indices)
    {
        try
        {
            indices = collection.IndicesOf(item);

            if (indices.Any() == false)
            {
                throw new KeyNotFoundException();    
            }
                
            return true;
        }
        catch
        {
            indices = null;
            return false;
        }
    }
}