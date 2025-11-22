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

namespace AlastairLundy.DotExtensions.Memory.Spans;

/// <summary>
/// Provides extension methods for performing copy operations on <see cref="Span{T}"/> instances.
/// </summary>
public static class SpanCopyExtensions
{
    /// <param name="source">The source span.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(Span<T> source)
    {
        /// <summary>
        /// Optimistically copies elements from a source <see cref="Span{T}"/> to a destination <see cref="Span{T}"/>
        /// </summary>
        /// <remarks>The source and destination <see cref="Span{T}"/> do not have to be the same size.</remarks>
        /// <param name="destination">The destination span.</param>
        /// <param name="startIndex">The zero-based starting index of the range (inclusive).</param>
        public void OptimisticCopy(ref Span<T> destination,
            int startIndex
        ) => OptimisticCopy(source, ref destination, startIndex, destination.Length);

        /// <summary>
        /// Optimistically copies elements from a source <see cref="Span{T}"/> to a destination <see cref="Span{T}"/>
        /// </summary>
        /// <remarks>The source and destination <see cref="Span{T}"/> do not have to be the same size.</remarks>
        /// <param name="destination">The destination span.</param>
        /// <param name="startIndex">The zero-based starting index of the range (inclusive).</param>
        /// <param name="length">The number of elements to copy from the start index to the end index (exclusive).</param>
        public void OptimisticCopy(ref Span<T> destination,
            int startIndex,
            int length
        )
        {
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, source.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(length);
            
            if (destination.Length < startIndex + length)
            {
                destination = source.Slice(startIndex, length);
                return;
            }

            int expectedEnd = startIndex + length;
            int actualEnd = destination.Length;

            int end = actualEnd;

            if (actualEnd <= expectedEnd)
                end = actualEnd;
            if (expectedEnd <= actualEnd)
                end = expectedEnd;

            for (int i = startIndex; i < end; i++)
            {
                destination[i] = source[i];
            }
        }

        /// <summary>
        /// Copies the elements of the span to a destination span, starting from a specified index.
        /// </summary>
        /// <remarks>Copying from a larger source <see cref="Span{T}"/> to a smaller destination <see cref="Span{T}"/> is not supported.</remarks>
        /// <param name="destination">The destination span.</param>
        /// <param name="startIndex">The zero-based starting index of the range (inclusive).</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the start or end indices are out of range for the span.</exception>
        /// <exception cref="ArgumentException">Thrown if the source <see cref="Span{T}"/> is larger than the destination <see cref="Span{T}"/>.</exception>
        public void CopyTo(ref Span<T> destination, int startIndex) =>
            source.CopyTo(ref destination, startIndex, Math.Abs(source.Length - startIndex));

        /// <summary>
        /// Copies a specified range of elements from the source span to the destination span.
        /// </summary>
        /// <param name="destination">The destination span.</param>
        /// <param name="startIndex">The zero-based starting index of the range (inclusive).</param>
        /// <param name="length">The number of elements to copy from the start index to the end index (exclusive).</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the start or end indices are out of range for the span or if the length exceeds the remaining elements.</exception>
        /// <exception cref="ArgumentException">Thrown if the source <see cref="Span{T}"/> is larger than the destination <see cref="Span{T}"/>.</exception>
        /// <remarks>Copying from a larger source <see cref="Span{T}"/> to a smaller destination <see cref="Span{T}"/> is not supported.</remarks>
        public void CopyTo(ref Span<T> destination,
            int startIndex,
            int length
        )
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(startIndex);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(length);
            
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, source.Length);
            
            if (destination.Length < source.Length || destination.Length < startIndex + length)
                throw new ArgumentException();

            for (int i = 0; i < length; i++)
            {
                destination[startIndex] = source[i];
                startIndex++;
            }
        }
    }

    /// <summary>
    /// Attempts to copy elements from one <see cref="Span{T}"/> to another.
    /// </summary>
    /// <param name="source">The source span.</param>
    /// <param name="destination">The destination span.</param>
    /// <param name="startIndex">The zero-based starting index of the range (inclusive).</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>True if copying to the destination <see cref="Span{T}"/> was successful; false otherwise.</returns>
    public static bool TryCopyTo<T>(this Span<T> source, ref Span<T> destination, int startIndex)
    {
        if (startIndex < 0 || startIndex > source.Length)
            return false;

        try
        {
            OptimisticCopy(source, ref destination, startIndex);

            for (int i = startIndex; i < source.Length; i++)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (destination[i] is not null && !destination[i]
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                        .Equals(source[i]))
                {
                    return false;
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Attempts to copy elements from one <see cref="Span{T}"/> to another.
    /// </summary>
    /// <param name="source">The source span.</param>
    /// <param name="destination">The destination span.</param>
    /// <param name="startIndex">The zero-based starting index of the range (inclusive).</param>
    /// <param name="length">The number of elements to copy from the start index to the end index (exclusive).</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>True if copying to the destination <see cref="Span{T}"/> was successful; false otherwise.</returns>
    public static bool TryCopyTo<T>(
        this Span<T> source,
        ref Span<T> destination,
        int startIndex,
        int length
    )
    {
        if (startIndex < 0 || startIndex > source.Length)
            return false;

        try
        {
            OptimisticCopy(source, ref destination, startIndex, length);

            for (int i = startIndex; i < source.Length; i++)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (destination[i] is not null && !destination[i]
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                        .Equals(source[i]))
                {
                    return false;
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}
