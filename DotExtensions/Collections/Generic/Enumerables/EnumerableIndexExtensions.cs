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
    /// Gets the indices of the specified object within an IEnumerable of an object.
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

        if (indices.Count > 0)
        {
            return indices; 
        }
        else
        {
            return [-1];
        }
    }
        

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="item"></param>
    /// <param name="index"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool TryGetIndexOf<T>(this IEnumerable<T> source, T item, out int? index)
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
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="item"></param>
    /// <param name="indices"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public static bool TryGetIndicesOf<T>(this IEnumerable<T> source, T item, out IEnumerable<int>? indices)
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