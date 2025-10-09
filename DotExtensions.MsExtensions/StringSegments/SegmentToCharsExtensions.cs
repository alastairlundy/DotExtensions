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
using System.Collections.Generic;
using AlastairLundy.DotExtensions.MsExtensions.Internal;
using AlastairLundy.DotExtensions.MsExtensions.Internal.Localizations;
using Microsoft.Extensions.Primitives;

namespace AlastairLundy.DotExtensions.MsExtensions.StringSegments;

public static class SegmentToCharsExtensions
{
    /// <summary>
    /// Returns the <see cref="StringSegment"/> as a Char Array.
    /// </summary>
    /// <param name="segment">The string segment to enumerate.</param>
    /// <returns>The string segment as a char array.</returns>
    /// <exception cref="ArgumentException">Thrown if the StringSegment is null or empty.</exception>
    public static char[] ToCharArray(this StringSegment segment)
    {
        if (StringSegment.IsNullOrEmpty(segment))
            throw new ArgumentException(Resources.Exceptions_Arguments_Segment_Empty);

        char[] charArray = new char[segment.Length];

        for (int i = 0; i < segment.Length; i++)
        {
            charArray[i] = segment[i];
        }
        
        return charArray;
    }
    
    /// <summary>
    /// Returns the <see cref="StringSegment"/> as a List of type <see cref="char"/>.
    /// </summary>
    /// <param name="segment">The segment to enumerate as a list.</param>
    /// <returns>A list of characters from the StringSegment if any characters are in the StringSegment.</returns>
    /// <exception cref="ArgumentException">Thrown if the StringSegment is null or empty.</exception>
    public static List<char> ToList(this StringSegment segment)
    {
        if (StringSegment.IsNullOrEmpty(segment))
            throw new ArgumentException(Resources.Exceptions_Arguments_Segment_Empty);
        
        List<char> list = new List<char>();
            
        for (int i = 0; i < segment.Length; i++)
        {
            list.Add(segment[i]);
        }
        
        return list;
    }
}