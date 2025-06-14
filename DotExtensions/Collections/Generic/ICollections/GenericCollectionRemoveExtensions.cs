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

namespace AlastairLundy.DotExtensions.Collections.Generic.ICollections;

public static class GenericCollectionRemoveExtensions
{
    /// <summary>
    /// Removes an item at the specified index from a collection.
    /// </summary>
    /// <param name="collection">The collection to remove the item from.</param>
    /// <param name="index">The index of the item to be removed.</param>
    /// <typeparam name="T">The type of elements stored in the collection.</typeparam>
    /// <returns>The new collection with the specified item removed.</returns>
    public static ICollection<T> RemoveAt<T>(this ICollection<T> collection, int index)
    {
        if (index < 0 || index >= collection.Count)
        {
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", $"{index}")
                .Replace("{y}", $"0")
                .Replace("{z}", $"{collection.Count}"));
        }
            
        T item = collection.ElementAt(index);

        collection.Remove(item);
            
        return collection;
    }
}