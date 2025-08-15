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

namespace AlastairLundy.DotExtensions.Memory.Spans;

public static class SpanCopyExtensions
{
    /// <summary>
    /// Resizes the span to the specified new size and copies the elements of the old span to the new Span.
    /// </summary>
    /// <param name="target">The span to be resized.</param>
    /// <param name="newSize">The desired new size of the span.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the new size is less than or equal to zero.</exception>
    public static void Resize<T>(this ref Span<T> target, int newSize)
    {
        if (newSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(newSize));

        T[] newTargetArray = new T[newSize];

        if (newSize < target.Length)
        {
            target = target.Slice(0, newSize);
            return;
        }
        
        int endCopy = target.Length < newSize ? target.Length : newSize;

        for (int i = 0; i < endCopy; i++)
        {
            newTargetArray[i] = target[i];
        }
        
        target = new Span<T>(newTargetArray);
    }

    /// <summary>
    /// Copies the elements of the span to a destination span, starting from a specified index.
    /// </summary>
    /// <param name="source">The source span.</param>
    /// <param name="destination">The destination span.</param>
    /// <param name="startIndex">The zero-based starting index of the range (inclusive).</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the start or end indices are out of range for the span.</exception>
    public static void CopyTo<T>(this Span<T> source, ref Span<T> destination, int startIndex)
    {
        for (int i = 0; i < source.Length; i++)
        {
            destination[startIndex] = source[i];
            startIndex++;
        }
    }

    /// <summary>
    /// Copies a specified range of elements from the source span to the destination span.
    /// </summary>
    /// <param name="source">The source span.</param>
    /// <param name="destination">The destination span.</param>
    /// <param name="startIndex">The zero-based starting index of the range (inclusive).</param>
    /// <param name="length">The number of elements to copy from the start index to the end index (exclusive).</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the start or end indices are out of range for the span, or if the length exceeds the remaining elements.</exception>
    public static void CopyTo<T>(this Span<T> source, ref Span<T> destination, int startIndex, int length)
    {
        for (int i = 0; i < length; i++)
        {
            destination[startIndex] = source[i];
            startIndex++;
        }
    }
}