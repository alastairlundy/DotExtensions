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
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <param name="newSize"></param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void Resize<T>(this ref Span<T> target, int newSize)
    {
        if (newSize > 0 == false)
            throw new ArgumentOutOfRangeException(nameof(newSize));
        
        T[] newTargetArray = new T[newSize];

        int endCopy = target.Length < newSize ? target.Length : newSize;

        for (int i = 0; i < endCopy; i++)
        {
            newTargetArray[i] = target[i];
        }
        
        target = new Span<T>(newTargetArray);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="startIndex"></param>
    /// <typeparam name="T"></typeparam>
    public static void CopyTo<T>(this Span<T> source, out Span<T> destination, int startIndex)
    {
        destination = source.GetRange(startIndex, source.Length - startIndex);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <typeparam name="T"></typeparam>
    public static void CopyTo<T>(this Span<T> source, out Span<T> destination, int startIndex, int length)
    {
        destination = source.GetRange(startIndex, length);
    }
}