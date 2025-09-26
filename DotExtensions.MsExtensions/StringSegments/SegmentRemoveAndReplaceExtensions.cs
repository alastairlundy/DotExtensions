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

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.DotExtensions.MsExtensions.StringSegments;

public static class SegmentRemoveAndReplaceExtensions
{
    
    /// <summary>
    /// Removes characters from a <see cref="StringSegment"/> starting at a specified index.
    /// </summary>
    /// <param name="segment">The segment to remove characters from.</param>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException">Thrown if the segment is null or whitespace.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the segment is empty.</exception>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public static StringSegment Remove(this StringSegment segment, int startIndex)
    {
        if (segment.IsNullOrWhiteSpace())
            throw new NullReferenceException();
        
        if(segment.IsEmpty())
            throw new InvalidOperationException();
        
        if (startIndex < 0 || startIndex >= segment.Length)
            throw new IndexOutOfRangeException();
            
        int length = segment.Length - startIndex - 1;

        return segment.Subsegment(0, length);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException">Thrown if the segment is null or whitespace.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the segment is empty.</exception>
    /// <exception cref="IndexOutOfRangeException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static StringSegment Remove(this StringSegment segment, int startIndex, int length)
    {
        if (segment.IsNullOrWhiteSpace())
            throw new NullReferenceException();
        
        if(segment.IsEmpty())
            throw new InvalidOperationException();

        if (startIndex < 0 || startIndex >= segment.Length)
            throw new IndexOutOfRangeException();
        
        if(startIndex + length > segment.Length || length < 0 || length > segment.Length)
            throw new ArgumentOutOfRangeException();
        
        int firstSegmentEnd = startIndex - 1;
        
        int secondSegmentStart = startIndex + length + 1;
        int secondSegmentEnd = segment.Length - secondSegmentStart;

        StringSegment firstSegment = segment.Subsegment(0, firstSegmentEnd);
        StringSegment secondSegment = segment.Subsegment(secondSegmentStart, secondSegmentEnd);

        return new StringSegment($"{firstSegment}{secondSegment}");
    }
}