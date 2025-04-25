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

using AlastairLundy.DotExtensions.Localizations;

namespace AlastairLundy.DotExtensions.Collections.Generic.Enumerables;

public static class EnumerableGetExtensions
{
    /// <summary>
    /// Returns a range of elements from the startIndex to the number of elements required.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="startIndex"></param>
    /// <param name="count"></param>
    /// <typeparam name="T">The type of object stored in the collection.</typeparam>
    /// <returns>The items specified starting from the start index, with the specified number of additional items.</returns>
    public static IEnumerable<T> GetRange<T>(this IEnumerable<T> source, int startIndex, int count)
    {
        List<T> output = new();
            
        int i = 0;

        T[] enumerable = source as T[] ?? source.ToArray();
        
        if (enumerable.Length < count || startIndex < 0 || count <= 0 || count > enumerable.Length || startIndex > enumerable.Length)
        {
            throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                .Replace("{x}", $"{startIndex}"
                    .Replace("{y}", $"0")
                    .Replace("{z}", $"{enumerable.Length}")));
        }
        
        int limit = startIndex + count;
            
        foreach (T item in enumerable)
        {
            if (i >= startIndex && i <= limit)
            {
                output.Add(item);
            }

            i++;
        }

        return output;
    }
}