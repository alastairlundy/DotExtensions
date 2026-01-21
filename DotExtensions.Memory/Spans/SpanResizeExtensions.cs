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

namespace DotExtensions.Memory.Spans;

/// <summary>
/// Provides extension methods for resizing spans in a safe and efficient manner.
/// </summary>
public static class SpanResizeExtensions
{
    /// <param name="target">The span to be resized.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(ref Span<T> target)
    {
        /// <summary>
        /// Resizes the span to the specified new size and optimistically copies the elements of the old span to the new Span.
        /// </summary>
        /// <param name="newSize">The desired new size of the span.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the new size is less than or equal to zero.</exception>
        public void Resize(int newSize)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);

            if (newSize < target.Length)
            {
                target = target.Slice(0, newSize);
                return;
            }

            T[] newTargetArray = ArrayPool<T>.Shared.Rent(newSize);
            Span<T> destination = new(newTargetArray);

            int endCopy = target.Length < newSize ? target.Length : newSize;

            target.OptimisticCopy(ref destination, 0, endCopy);
            
            target = destination.Slice(0, newSize);
            ArrayPool<T>.Shared.Return(newTargetArray);
        }
    }
}
