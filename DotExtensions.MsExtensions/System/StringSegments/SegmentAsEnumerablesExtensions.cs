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

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.DotExtensions.MsExtensions.System.StringSegments;

public static class SegmentAsEnumerablesExtensions
{
    
    /// <summary>
    /// Returns the String segment as a Char Array.
    /// </summary>
    /// <param name="segment">The string segment to enumerate.</param>
    /// <returns>The string segment as a char array.</returns>
    public static char[] ToCharArray(this StringSegment segment)
    {
        char[] charArray = new char[segment.Length];

        for (int i = 0; i < segment.Length; i++)
        {
            charArray[i] = segment[i];
        }
        
        return charArray;
    }

    /// <summary>
    /// Returns the StringSegment as an IEnumerable.
    /// </summary>
    /// <remarks>Internally calls DotExtensions' ToCharArray extension method.</remarks>
    /// <param name="segment">The StringSegment to enumerate.</param>
    /// <returns>The StringSegment as an IEnumerable.</returns>
    [Obsolete("This code is deprecated and will be removed in v8. Use ToCharArray instead.")]
    public static IEnumerable<char> ToEnumerable(this StringSegment segment) => ToCharArray(segment);


    /// <summary>
    /// Returns the StringSegment as an IEnumerable.
    /// </summary>
    /// <remarks>Internally calls DotExtensions' ToCharArray extension method.</remarks>
    /// <param name="segment">The StringSegment to enumerate.</param>
    /// <returns>The StringSegment as an IEnumerable.</returns>
    [Obsolete("This code is deprecated and will be removed in v8. Use ToCharArray instead.")]
    public static IEnumerable<char> AsEnumerable(this StringSegment segment)
    {
        for (int i = 0; i < segment.Length; i++)
        {
            yield return segment[i];
        }
    }
}