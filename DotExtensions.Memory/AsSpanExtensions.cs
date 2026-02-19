/*
        MIT License
       
       Copyright (c) 2026 Alastair Lundy
       
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
/// Provides extension methods for converting Collections to Spans.
/// </summary>
public static class AsSpanExtensions
{
    /// <param name="collection">The collection to convert.</param>
    /// <typeparam name="TSource">The type of elements in the collection.</typeparam>
    extension<TSource>(ICollection<TSource> collection)
    {
        /// <summary>
        /// Converts the current collection into a <see cref="Span{T}"/>.
        /// This method creates a writable span representation of the collection, maintaining the order of the elements.
        /// </summary>
        /// <returns>A <see cref="Span{T}"/> representing the elements of the collection.</returns>
        public Span<TSource> AsSpan()
        {
            ArgumentNullException.ThrowIfNull(collection);    
            
            TSource[] array =new  TSource[collection.Count];

            int index = 0;
            foreach (TSource item in collection)
            {
                array[index] = item;
                index++;
            }

            return array.AsSpan();
        }

        /// <summary>
        /// Converts the current collection into a <see cref="ReadOnlySpan{T}"/>.
        /// This method creates a read-only span representation of the collection, maintaining the order of the elements.
        /// </summary>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> representing the elements of the collection.</returns>
        public ReadOnlySpan<TSource> AsReadOnlySpan()
        {
            ArgumentNullException.ThrowIfNull(collection);    
            
            TSource[] array = new TSource[collection.Count];

            int index = 0;
            foreach (TSource item in collection)
            {
                array[index] = item;
                index++;
            }

            return array.AsSpan();
        }
    }
}