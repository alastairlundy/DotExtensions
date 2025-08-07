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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable MemberCanBePrivate.Global

namespace AlastairLundy.DotExtensions.Collections.ICollections;

public static class CollectionIndexExtensions
{
    /// <summary>
    /// Attempts to get the index of a specified element in a collection.
    /// </summary>
    /// <param name="collection">The collection to be searched.</param>
    /// <param name="item">The item to get the index of.</param>
    /// <param name="index">The index position of an item to search for.</param>
    /// <returns>True if an index can be found for an item in a collection; false otherwise.</returns>
    [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
    public static bool TryGetIndexOf(this ICollection collection, object item, out int? index)
    {
        try
        {
            index = IndexOf(collection, item);
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
    /// <returns>True if one or more indices can be found for an item in a collection; false otherwise.</returns>
    [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
    public static bool TryGetIndicesOf(this ICollection collection, object item, out IEnumerable<int>? indices)
    {
        try
        {
            indices = IndicesOf(collection, item);

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
        
    /// <summary>
    /// Gets the index of the specified item in a collection.
    /// </summary>
    /// <param name="collection">The collection to be searched.</param>
    /// <param name="item">The item to get the index of.</param>
    /// <returns>The index of an object in the collection.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the item could not be found within the collection.</exception>
    [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
    public static int IndexOf(this ICollection collection, object item)
    {
        int index = 0;
            
        IEnumerator enumerator = collection.GetEnumerator();
        using var enumerator1 = enumerator as IDisposable;

        while (enumerator.MoveNext())
        {
            if (enumerator.Current is not null)
            {
                if (enumerator.Current.Equals(item))
                {
                    return index;
                }
            }

            index++;
        }

        throw new KeyNotFoundException();
    }
        
    /// <summary>
    /// Gets the indices of the specified item in a collection.
    /// </summary>
    /// <param name="collection">The collection to be searched.</param>
    /// <param name="item">The item to get the indices of.</param>
    /// <returns>The indices of an object in the collection.</returns>
    [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
    public static IEnumerable<int> IndicesOf(this ICollection collection, object item)
    {
        int index = 0;
            
        IEnumerator enumerator = collection.GetEnumerator();
        using var enumerator1 = enumerator as IDisposable;

        while (enumerator.MoveNext())
        {
            if (enumerator.Current is not null)
            {
                if (enumerator.Current.Equals(item))
                {
                    yield return index;
                }
            }

            index++;
        }
    }
}