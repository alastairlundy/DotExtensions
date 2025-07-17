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
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using AlastairLundy.DotExtensions.Exceptions;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace AlastairLundy.DotExtensions.Collections.Generic.Enumerables;

public static class EnumerableIndexExtensions
{
    /// <summary>
    /// Returns the index of an object in an IEnumerable.
    /// </summary>
    /// <param name="source">The IEnumerable to be searched.</param>
    /// <param name="obj">The object to get the index of.</param>
    /// <typeparam name="T">The type of object in the IEnumerable.</typeparam>
    /// <returns>The index of an object in an IEnumerable, if the IEnumerable contains the object, throws an exception otherwise.</returns>
    /// <exception cref="ValueNotFoundException">Thrown if the IEnumerable does not contain the specified object.</exception>
    public static int IndexOf<T>(this IEnumerable<T> source, T obj)
    {
        if (source is IList<T> list)
        {
            return list.IndexOf(obj);
        }
            
        int index = 0;
                
        foreach (T item in source)
        {
            if (item is not null && item.Equals(obj))
            {
                return index;
            }
                    
            index++;
        }
        
        return -1;
    }

    
    /// <summary>
    /// Gets the indices of the specified item within an IEnumerable.
    /// </summary>
    /// <param name="source">The IEnumerable to be searched.</param>
    /// <param name="obj">The item to search for.</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>The indices if the object is found; a single element Enumerable with a value of -1 otherwise.</returns>
    public static IEnumerable<int> IndicesOf<T>(this IEnumerable<T> source, T obj)
    {
        List<int> indices = new List<int>();
            
        int index = 0;
        foreach (T item in source)
        {
            if (obj is not null && obj.Equals(item))
            {
                indices.Add(index);
            }

            index += 1;
        }

        if (indices.Count == 0)
            return [-1];

        return indices;
    }
        

    /// <summary>
    /// Attempts to find the first index of a specified item in a collection.
    /// </summary>
    /// <param name="source">The collection of items to search through.</param>
    /// <param name="item">The item to find within the collection.</param>
    /// <param name="index">An output parameter that will contain the index of the found item, or null if the item is not found.</param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <returns>True if the item was found, otherwise false.</returns>
    public static bool TryGetIndexOf<T>(this IEnumerable<T> source, T item, 
#if NET5_0_OR_GREATER
        [NotNullWhen(returnValue: true)]
#endif
        out int? index)
    {
        try
        {
            index = IndexOf(source, item);
            return true;
        }
        catch
        {
            index = null;
            return false;
        }
    }

        
    /// <summary>
    /// Attempts to find all the indices of a specified item in a collection.
    /// </summary>
    /// <param name="source">The collection of items to search through.</param>
    /// <param name="item">The item to find within the collection.</param>
    /// <param name="indices">An output parameter that will contain the indices of the found item, or null if the item is not found.</param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <returns>True if the item was found, otherwise false.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the item is not found in the collection.</exception>
    public static bool TryGetIndicesOf<T>(this IEnumerable<T> source, T item, 
        #if NET5_0_OR_GREATER
        [NotNullWhen(returnValue: true)]
        #endif
        out IEnumerable<int>? indices)
    {
        try
        {
            indices = IndicesOf(source, item);

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