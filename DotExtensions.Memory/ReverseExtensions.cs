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

namespace AlastairLundy.DotExtensions.Memory;

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
        public void Reverse()
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(span);
            
            T[] array = ArrayPool<T>.Shared.Rent(span.Length);

            try
            {
                for (int index = span.Length - 1; index > 0; index--)
                {
                    array[(span.Length - 1) - index] = span[index];
                }

                span = new ReadOnlySpan<T>(array);
            }
            finally
            {
                ArrayPool<T>.Shared.Return(array);
            }
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
        public void Reverse()
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(memory);
            
            T[] array = ArrayPool<T>.Shared.Rent(memory.Length);

            try
            {
                for (int index = memory.Length - 1; index > 0; index--)
                {
                    array[(memory.Length - 1) - index] = memory.Span[index];
                }

                for (int i = 0; i < memory.Length; i++)
                {
                    memory.Span[i] = array[i];
                }
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
        public void Reverse()
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(memory);
            T[] array = ArrayPool<T>.Shared.Rent(memory.Length);

            try
            {
                for (int index = memory.Length - 1; index > 0; index--)
                {
                    array[(memory.Length - 1) - index] = memory.Span[index];
                }

                memory = new ReadOnlyMemory<T>(array);
            }
            finally
            {
                ArrayPool<T>.Shared.Return(array);
            }
        }
    }
}