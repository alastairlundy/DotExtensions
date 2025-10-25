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

namespace AlastairLundy.DotExtensions.Collections;

/// <summary>
/// 
/// </summary>
public static class CollectionAddRange
{
    /// <summary>
    /// Appends elements from another collection to the end of the specified collection.
    /// </summary>
    /// <param name="source">The collection into which elements will be appended.</param>
    /// <param name="enumerableToAdd">The IEnumerable containing elements to append to the original collection.</param>
    /// <typeparam name="T">The type of elements in both collections.</typeparam>
    /// <exception cref="NotSupportedException">Thrown if adding to the collection is not supported.</exception>
    [Obsolete(DeprecationMessages.DeprecationV9)]
    public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> enumerableToAdd)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(enumerableToAdd);
#endif

        if (source.IsReadOnly)
            throw new NotSupportedException();
        
        if (source.Count == int.MaxValue)
        {
            throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
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
    [Obsolete(DeprecationMessages.DeprecationV9)]
    public static void AddRange<T>(this ICollection<T> source, ICollection<T> collectionToAdd)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(collectionToAdd);
#endif        

        if (source.IsReadOnly)
            throw new NotSupportedException();
        
        if (source.Count == int.MaxValue)
        {
            throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
        }
        else if (collectionToAdd.Count == int.MaxValue)
        {
            throw new OverflowException($"{nameof(collectionToAdd)} contains the maximum size of {int.MaxValue} and cannot be added to {nameof(source)}.");
        }
        
        if (source is List<T> { Count: 0 })
        {
            source = new List<T>(capacity: collectionToAdd.Count);
        }
        
        foreach (T item in collectionToAdd)
        {
            source.Add(item);
        }
    }
}