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

namespace DotExtensions.Memory;

/// <summary>
/// Provides extension methods for converting spans and memory to lists or other types.
/// </summary>
public static class ToListExtensions
{
    /// <param name="source">The span to convert.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(Span<T> source)
    {
        /// <summary>
        /// Converts this <see cref="Span{T}"/> to a <see cref="List{T}"/>.
        /// </summary>
        /// <returns>A list containing the elements of the span.</returns>
        public List<T> ToList()
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            List<T> list = new(capacity: source.Length);

            foreach (T item in source)
            {
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// Converts this <see cref="Span{T}"/> to a <see cref="Memory{T}"/>
        /// </summary>
        /// <returns>A <see cref="Memory{T}"/> containing all the elements of the span.</returns>
        public Memory<T> ToMemory() => new(source.ToArray());
    }
    
    /// <param name="source"></param>
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    extension<T>(ReadOnlySpan<T> source)
    {
        /// <summary>
        /// Converts this <see cref="ReadOnlySpan{T}"/> to a <see cref="List{T}"/>.
        /// </summary>
        /// <returns>A list containing the elements of the span.</returns>
        public List<T> ToList()
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            List<T> list = new(capacity: source.Length);

            foreach (T item in source)
            {
                list.Add(item);
            }

            return list;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T">The type of elements in the Memory.</typeparam>
    extension<T>(Memory<T> source)
    {
        /// <summary>
        /// Converts this <see cref="Memory{T}"/> to a <see cref="List{T}"/>
        /// </summary>
        /// <returns>A list containing the elements of the Memory.</returns>
        public List<T> ToList()
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(source);
            List<T> list = new(capacity: source.Length);

            foreach (T item in source.Span)
            {
                list.Add(item);
            }

            return list;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    extension<T>(ReadOnlyMemory<T> source)
    {
        /// <summary>
        /// Converts the provided <see cref="ReadOnlyMemory{T}"/> of type T to a List of type T.
        /// </summary>
        /// <returns>A new List containing all elements from the <see cref="ReadOnlyMemory{T}"/>.</returns>
        public List<T> ToList()
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(source);
            List<T> list = new(capacity: source.Length);

            foreach (T item in source.Span)
            {
                list.Add(item);
            }

            return list;
        }
    }
}
