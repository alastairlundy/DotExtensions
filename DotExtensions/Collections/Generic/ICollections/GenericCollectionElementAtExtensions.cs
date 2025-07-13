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
using System.Linq;

using AlastairLundy.DotExtensions.Collections.ILists;
using AlastairLundy.DotExtensions.Localizations;

namespace AlastairLundy.DotExtensions.Collections.Generic.ICollections;

public static class GenericCollectionElementAtExtensions
{
    /// <summary>
    /// Retrieves the element at a specified index from the collection.
    /// </summary>
    /// <param name="source">The collection to retrieve the element from.</param>
    /// <param name="index">The zero-based index of the element to retrieve.</param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <returns>The element at the specified index in the sequence, or throws an exception if no such element exists.</returns>
    /// <exception cref="ArgumentException">Thrown when no element is found at the specified index.</exception>
    public static T ElementAt<T>(this ICollection<T> source, int index)
    {
        if (source is IList<T> list)
        {
            return list[index];
        }
        
        int i = 0;

        foreach (T item in source)
        {
            if (i == index)
            {
                return item;
            }

            i++;
        }

        throw new ArgumentException(Resources.Exceptions_ValueNotFound_AtIndex.Replace("{y}", nameof(source))
            .Replace("{x}",$"{index}"));
    }

        
    /// <summary>
    /// Returns the elements at the specified indexes from the given collection.
    /// </summary>
    /// <param name="source">The source collection to extract elements from.</param>
    /// <param name="indices">The indexes of the elements to extract.</param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <returns>The extracted elements as a generic collection.</returns>
    [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
    public static ICollection<T> ElementsAt<T>(this ICollection<T> source, IEnumerable<int> indices)
    {
        #region IList Performance Optimizations
        if (source is IList<T> list)
        {
            return IListElementsAtExtensions.ElementsAt(list, indices);
        }
        #endregion

        List<T> output = new();
        IList<int> indicesList = indices as IList<int> ?? [..indices];
        
        int i = 0;
        foreach (T item in source)
        {
            if (indicesList.Contains(i))
            {
                output.Add(item);
            }

            i++;
        }
            
        return output;
    }
}