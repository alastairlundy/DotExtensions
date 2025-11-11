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
using System.Linq;

namespace AlastairLundy.DotExtensions.Memory.Spans;

/// <summary>
/// Provides extension methods for working with spans.
/// </summary>
public static class SpanIsEmptyExtensions
{
    /// <summary>
    /// Provides extension methods for checking if a span is empty.
    /// </summary>
    extension<T>(Span<T> source)
    {
        /// <summary>
        /// Determines if a span is empty.
        /// </summary>
        /// <typeparam name="T">The type of elements in the Span.</typeparam>
        /// <returns>True if the span is empty, false otherwise.</returns>
        public bool IsEmpty
        {
            get
            {
                if (source.Length == 0 || source == Span<T>.Empty)
                    return true;

                return false;
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="span"></param>
    extension(Span<char> span)
    {
        /// <summary>
        /// Determines if a span is empty or whitespace.
        /// </summary>
        /// <param name="source">The span to search.</param>
        /// <returns>True if the span is empty or whitespace, false otherwise.</returns>
        public static bool IsEmptyOrWhiteSpace(Span<char> source)
        {
            if (source.IsEmpty)
                return true;

            bool[] isWhiteSpace = new bool[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                isWhiteSpace[i] = char.IsWhiteSpace(source[i]);
            }

            return isWhiteSpace.All(x => x);
        }
    }
}
