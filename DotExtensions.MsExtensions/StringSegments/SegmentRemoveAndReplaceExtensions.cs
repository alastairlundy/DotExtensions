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
    /// <param name="startIndex">The index to start removing characters at in the <see cref="StringSegment"/>.</param>
    /// <returns>A <see cref="StringSegment"/> where all the characters occurring after the specified index are removed.</returns>
    /// <exception cref="NullReferenceException">Thrown if the segment is null or whitespace.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the segment is empty.</exception>
    /// <exception cref="IndexOutOfRangeException">Thrown if the index is less than 0 or greater than or equal to the length of the <see cref="StringSegment"/>.</exception>
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
    /// Removes characters from a <see cref="StringSegment"/> starting at a specified index for <paramref name="count"/> number of characters.
    /// </summary>
    /// <param name="segment">The segment to remove characters from.</param>
    /// <param name="startIndex">The index to start removing characters at in the <see cref="StringSegment"/>.</param>
    /// <param name="count">The number of characters to remove from the StringSegment from the start index.</param>
    /// <returns>A <see cref="StringSegment"/> where the characters occurring within the <paramref name="count"/> number of characters after the specified index are removed.</returns>
    /// <exception cref="NullReferenceException">Thrown if the segment is null or whitespace.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the segment is empty.</exception>
    /// <exception cref="IndexOutOfRangeException">Thrown if the index is less than 0 or greater than or equal to the length of the <see cref="StringSegment"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the count is than 0 or greater than the length of the <see cref="StringSegment"/>.</exception>
    public static StringSegment Remove(this StringSegment segment, int startIndex, int count)
    {
        if (segment.IsNullOrWhiteSpace())
            throw new NullReferenceException();
        
        if(segment.IsEmpty())
            throw new InvalidOperationException();

        if (startIndex < 0 || startIndex >= segment.Length)
            throw new IndexOutOfRangeException();
        
        if(startIndex + count > segment.Length || count < 0 || count > segment.Length)
            throw new ArgumentOutOfRangeException();

        if (startIndex + count == segment.Length - 1)
            return Remove(segment, startIndex);
        
        int firstSegmentEnd = startIndex - 1;
        
        int secondSegmentStart = startIndex + count + 1;
        int secondSegmentEnd = segment.Length - secondSegmentStart;

        StringSegment firstSegment = segment.Subsegment(0, firstSegmentEnd);
        StringSegment secondSegment = segment.Subsegment(secondSegmentStart, secondSegmentEnd);

        return new StringSegment($"{firstSegment}{secondSegment}");
    }
}