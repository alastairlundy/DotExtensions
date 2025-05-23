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

namespace AlastairLundy.DotExtensions.Memory.Spans
{
    public static class SpanRangeExtensions
    {
        /// <summary>
        /// Returns a new Span with the specified range of elements,
        /// starting from the given start index and ending at the given end index.
        /// </summary>
        /// <param name="target">The original span to extract the range of items from.</param>
        /// <param name="start">The zero-based starting index of the range.</param>
        /// <param name="end">The one-based ending index of the range (inclusive).</param>
        /// <typeparam name="T">The type of elements in the span.</typeparam>
        /// <returns>A new span containing the specified range of elements.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the start or end indices are out of range for the span.</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown if the start index is greater than the length of the span, or if the end index exceeds the span's capacity.</exception>
        public static Span<T> GetRange<T>(this Span<T> target, int start, int end)
        {
            if ((end - start) > target.Length)
            {
                throw new ArgumentOutOfRangeException(Resources.Exceptions_Span_SkipCountTooLarge);
            }

            if (start < 0 || start >= target.Length)
            {
                throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                    .Replace("{x}", $"{start}")
                    .Replace("{y}", $"0")
                    .Replace("{z}", $"{target.Length}"));
            }
            
            int count = end - start;

            T[] array = new T[count];

            int newIndex = 0;
            for (int i = start; i < end; i++)
            {
                array[newIndex] = target[i];
            }

            return new Span<T>(array);
        }

        
        /// <summary>
        /// Retrieves a range of elements within the specified span.
        /// </summary>
        /// <param name="target">The initial span to search.</param>
        /// <param name="indices">A collection of indices specifying the positions of interest in the span.</param>
        /// <typeparam name="T">The type of the elements within the span.</typeparam>
        /// <returns>A new Span containing only the elements at the specified indices.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if any index in indices is out of range for the target span.</exception>
        public static Span<T> GetRange<T>(this Span<T> target, ICollection<int> indices)
        {
            T[] array = new T[indices.Count];
            
            int newIndex = 0;
            foreach (int index in indices)
            {
                if (index < 0 || index >= target.Length)
                {
                    throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                        .Replace("{x}", $"{index}")
                        .Replace("{y}", $"0")
                        .Replace("{z}", $"{target.Length}"));
                }
                
                target[index] = target[newIndex];
                newIndex++;
            }
            
            return new Span<T>(array);
        }
    }
}