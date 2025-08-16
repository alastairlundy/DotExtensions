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

/// <summary>
/// 
/// </summary>
public static class SpanResizeExtensions
{
    /// <summary>
    /// Resizes the span to the specified new size and optimistically copies the elements of the old span to the new Span.
    /// </summary>
    /// <param name="target">The span to be resized.</param>
    /// <param name="newSize">The desired new size of the span.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the new size is less than or equal to zero.</exception>
    public static void Resize<T>(this ref Span<T> target, int newSize)
    {
        if (newSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(newSize));

        if (newSize < target.Length)
        {
            target = target.Slice(0, newSize);
            return;
        }
        
        T[] newTargetArray = new T[newSize];
        
        Span<T> destination = new  Span<T>(newTargetArray);
        
        int endCopy = target.Length < newSize ? target.Length : newSize;
        
        target.OptimisticCopy(ref destination, 0, endCopy);
        
        target = destination;
    }
}