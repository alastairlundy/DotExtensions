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

using Microsoft.Extensions.Primitives;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable RedundantBoolCompare

namespace AlastairLundy.DotExtensions.MsExtensions.StringSegments;

/// <summary>
/// A class to hold extension methods for checking if a StringSegment Contains an item.
/// </summary>
public static class SegmentContainsExtensions
{
    
    /// <summary>
    /// Returns whether the String Segment contains a character. 
    /// </summary>
    /// <param name="this">The string segment to search.</param>
    /// <param name="character">The char to search for.</param>
    /// <returns>True if the character is found in the StringSegment, false otherwise.</returns>
    public static bool Contains(this StringSegment @this, char character)
    {
        for (int i = 0; i < @this.Length; i++)
        {
            if (@this[i] == character)
                return true;
        }
        
        return false;
    }

    /// <summary>
    /// Returns whether the String Segment contains another String Segment.
    /// </summary>
    /// <param name="source">The string segment to search.</param>
    /// <param name="segment">The string segment to search for.</param>
    /// <returns>True if the string segment contains the specified string segment; false otherwise.</returns>
    public static bool Contains(this StringSegment source, StringSegment segment)
    {
        if (source.Length == segment.Length)
            return source.Equals(segment);
        
        if (segment.Length > source.Length || segment.IsEmpty)
            return false;

        int start = -1;
        int index = 0;

        while (index != -1)
        { 
            index = source.IndexOf(segment[0], start + 1);
            start = index;

            if (index != -1)
            {
                StringSegment comparison = source.Subsegment(index, segment.Length);

                if (segment.Equals(comparison))
                    return true;
            }
        }

        return false;
    }
}