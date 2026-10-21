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
/// Provides extension methods for performing copy operations on <see cref="Span{T}"/> instances.
/// </summary>
public static class SpanCopyExtensions
{
    private static void ValidateCopyRange(int sourceLength, int startIndex, int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, sourceLength);
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        if (length > sourceLength - startIndex)
            throw new ArgumentOutOfRangeException(nameof(length));
    }

    #region Span version
    /// <param name="source">The source span.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(Span<T> source)
    {
        /// <summary>
        /// Optimistically copies elements from a source <see cref="Span{T}"/> to a destination <see cref="Span{T}"/>
        /// </summary>
        /// <remarks>The source and destination <see cref="Span{T}"/> do not have to be the same size. As many elements as fit are copied: the element count is the smaller of the number of source elements remaining from <paramref name="startIndex"/> and the destination length. The destination is never reallocated or replaced.</remarks>
        /// <param name="destination">The destination span.</param>
        /// <param name="startIndex">The zero-based starting index of the range (inclusive).</param>
        public void OptimisticCopy(ref Span<T> destination,
            int startIndex
        )
        {
            int availableElements = Math.Max(source.Length - startIndex, 0);

            source.OptimisticCopy(ref destination, startIndex, Math.Min(availableElements, destination.Length));
        }

        /// <summary>
        /// Optimistically copies elements from a source <see cref="Span{T}"/> to a destination <see cref="Span{T}"/>
        /// </summary>
        /// <remarks>The source and destination <see cref="Span{T}"/> do not have to be the same size. If the destination is shorter than <paramref name="length"/>, it is replaced with a newly allocated span holding the copied elements; the original destination buffer is left unchanged.</remarks>
        /// <param name="destination">The destination span.</param>
        /// <param name="startIndex">The zero-based starting index of the range (inclusive).</param>
        /// <param name="length">The number of elements to copy from the start index to the end index (exclusive).</param>
        public void OptimisticCopy(ref Span<T> destination,
            int startIndex,
            int length)
        {
            ValidateCopyRange(source.Length, startIndex, length);

            if (destination.Length >= length)
            {
                source.Slice(startIndex, length).CopyTo(destination);
                return;
            }

            // The destination is too small to hold the range: copy into a newly allocated buffer and re-point the destination at it.
            T[] outputArray = GC.AllocateUninitializedArray<T>(length);
            source.Slice(startIndex, length).CopyTo(outputArray);
            destination = outputArray.AsSpan();
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
            source.CopyTo(ref destination, startIndex, source.Length - startIndex);

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
            int length)
        {
            ValidateCopyRange(source.Length, startIndex, length);

            if (destination.Length < length)
                throw new ArgumentException(Resources.Exceptions_Spans_Copy_DestinationShorterThanSource, nameof(destination));

            source.Slice(startIndex, length).CopyTo(destination);
        }
    }

    /// <summary>
    /// Attempts to copy elements from one <see cref="Span{T}"/> to another.
    /// </summary>
    /// <param name="source">The source span.</param>
    /// <param name="destination">The destination span.</param>
    /// <param name="startIndex">The zero-based starting index of the range (inclusive).</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>True if the source elements remaining from <paramref name="startIndex"/> were copied into <paramref name="destination"/>; false if <paramref name="startIndex"/> is out of range or the destination is too small, in which case the destination is left unmodified.</returns>
    public static bool TryCopyTo<T>(this Span<T> source, ref Span<T> destination, int startIndex)
    {
        if (startIndex < 0 || startIndex > source.Length)
            return false;

        int length = source.Length - startIndex;

        if (destination.Length < length)
            return false;

        source.Slice(startIndex, length).CopyTo(destination);

        return true;
    }

    /// <summary>
    /// Attempts to copy elements from one <see cref="Span{T}"/> to another.
    /// </summary>
    /// <param name="source">The source span.</param>
    /// <param name="destination">The destination span.</param>
    /// <param name="startIndex">The zero-based starting index of the range (inclusive).</param>
    /// <param name="length">The number of elements to copy from the start index to the end index (exclusive).</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>True if the requested range was copied into <paramref name="destination"/>; false if the indices are out of range or the destination is too small, in which case the destination is left unmodified.</returns>
    public static bool TryCopyTo<T>(
        this Span<T> source,
        ref Span<T> destination,
        int startIndex,
        int length)
    {
        if (startIndex < 0 || startIndex > source.Length)
            return false;

        if (length < 0 || length > source.Length - startIndex)
            return false;

        if (destination.Length < length)
            return false;

        source.Slice(startIndex, length).CopyTo(destination);

        return true;
    }
    #endregion

    #region ReadOnlySpan version

    /// <param name="source">The source <see cref="ReadOnlySpan{T}"/>.</param>
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    extension<T>(ReadOnlySpan<T> source)
    {
        /// <summary>
        /// Optimistically copies elements from a source <see cref="ReadOnlySpan{T}"/> to a destination <see cref="Span{T}"/>
        /// </summary>
        /// <remarks>The <see cref="ReadOnlySpan{T}"/> source and <see cref="Span{T}"/> destination do not have to be the same size. As many elements as fit are copied: the element count is the smaller of the number of source elements remaining from <paramref name="startIndex"/> and the destination length. The destination is never reallocated or replaced.</remarks>
        /// <param name="destination">The destination <see cref="Span{T}"/>.</param>
        /// <param name="startIndex">The zero-based starting index of the range (inclusive).</param>
        public void OptimisticCopy(ref Span<T> destination,
            int startIndex
        )
        {
            int availableElements = Math.Max(source.Length - startIndex, 0);

            source.OptimisticCopy(ref destination, startIndex, Math.Min(availableElements, destination.Length));
        }

        /// <summary>
        /// Optimistically copies elements from a source <see cref="ReadOnlySpan{T}"/> to a destination <see cref="Span{T}"/>
        /// </summary>
        /// <remarks>The <see cref="ReadOnlySpan{T}"/> source and <see cref="Span{T}"/> destination do not have to be the same size. If the destination is shorter than <paramref name="length"/>, it is replaced with a newly allocated span holding the copied elements; the original destination buffer is left unchanged.</remarks>
        /// <param name="destination">The destination <see cref="Span{T}"/>.</param>
        /// <param name="startIndex">The zero-based starting index of the range (inclusive).</param>
        /// <param name="length">The number of elements to copy from the start index to the end index (exclusive).</param>
        public void OptimisticCopy(ref Span<T> destination,
            int startIndex,
            int length
        )
        {
            ValidateCopyRange(source.Length, startIndex, length);

            if (destination.Length >= length)
            {
                source.Slice(startIndex, length).CopyTo(destination);
                return;
            }

            T[] outputArray = GC.AllocateUninitializedArray<T>(length);
            source.Slice(startIndex, length).CopyTo(outputArray);
            destination = outputArray.AsSpan();
        }
    }

    #endregion
}
