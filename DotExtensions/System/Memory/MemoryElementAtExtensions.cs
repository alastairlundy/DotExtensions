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
using AlastairLundy.DotExtensions.Memory.Spans;

namespace AlastairLundy.DotExtensions.Memory;

/// <summary>
/// 
/// </summary>
public static class MemoryElementAtExtensions
{
    /// <summary>
    /// Returns the element at the specified index in the source <see cref="Memory{T}"/>.
    /// </summary>
    /// <param name="source">The source <see cref="Memory{T}"/> .</param>
    /// <param name="index">The zero-based index of the element to be retrieved.</param>
    /// <typeparam name="T">The type of items stored in the Memory.</typeparam>
    /// <returns>A new source <see cref="Memory{T}"/> containing a single element starting at the specified index in the Memory.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the source <see cref="Memory{T}"/> has no elements or the index is out of range.</exception>
    [Obsolete(DeprecationMessages.DeprecationV9)]
    public static T ElementAt<T>(this Memory<T> source, int index)
    {
        if (source.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        Memory<T> items = ElementsAt(source, index, 1);

        return items.Span.First();
    }
    
    /// <summary>
    /// Returns a new <see cref="Memory{T}"/> containing the specified number of elements starting at the specified index.
    /// </summary>
    /// <param name="source">The source <see cref="Memory{T}"/>.</param>
    /// <param name="index">The zero-based index of the element to be retrieved.</param>
    /// <param name="count">The number of elements to include in the returned Memory.</param>
    /// <typeparam name="T">The type of items stored in the Memory.</typeparam>
    /// <returns>A new <see cref="Memory{T}"/> containing the specified number of elements starting at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the source <see cref="Memory{T}"/> has no elements or the index is out of range.</exception>
    public static Memory<T> ElementsAt<T>(this Memory<T> source, int index, int count)
    {
        if (source.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(index));

#if NET8_0_OR_GREATER
        return source[new Range(index, index + count)];
#else
        return source.Slice(index, index + count);
#endif
    }
}