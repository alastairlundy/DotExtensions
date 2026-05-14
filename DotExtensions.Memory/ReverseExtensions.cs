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

namespace DotExtensions.Memory;

/// <summary>
/// Provides extension methods for reversing spans or sequences of elements in memory.
/// </summary>
public static class ReverseExtensions
{
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(ref ReadOnlySpan<T> span)
    {
        /// <summary>
        /// Reverses the contents of the specified read-only span,
        /// returning a new <see cref="ReadOnlySpan{T}"/> with the elements in reverse order.
        /// </summary>
        [Obsolete(DeprecationMessages.DeprecationV11)]
        public void Reverse()
        {
            T[] array = new T[span.Length];
            span.CopyTo(array);
            Array.Reverse(array);
            span = array.AsSpan();
        }
    }

    /// <summary>
    /// Provides extension methods for reversing the contents of memory.
    /// </summary>
    extension<T>(ref Memory<T> memory)
    {
        /// <summary>
        /// Reverses the contents of the specified memory,
        /// returning a new <see cref="Memory{T}"/> with the elements in reverse order.
        /// </summary>
        [Obsolete(DeprecationMessages.DeprecationV11)]
        public void Reverse()
        {
            int len = memory.Length;
            T[] array = ArrayPool<T>.Shared.Rent(len);

            try
            {
                Span<T> rentedSpan = new Span<T>(array, 0, len);
                memory.Span.CopyTo(rentedSpan);
                Array.Reverse(array, 0, len);
                rentedSpan.CopyTo(memory.Span);
            }
            finally
            {
                ArrayPool<T>.Shared.Return(array);
            }
        }
    }
    
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlyMemory{T}"/>.</typeparam>
    /// <param name="memory">The read-only memory span to reverse.</param>
    extension<T>(ref ReadOnlyMemory<T> memory)
    {
        /// <summary>
        /// Reverses the contents of the specified read-only memory,
        /// returning a new <see cref="ReadOnlyMemory{T}"/> with the elements in reverse order.
        /// </summary>
        [Obsolete(DeprecationMessages.DeprecationV11)]
        public void Reverse()
        {
            T[] array = new T[memory.Length];
            memory.Span.CopyTo(array);
            Array.Reverse(array);
            memory = array.AsMemory();
        }
    }
}