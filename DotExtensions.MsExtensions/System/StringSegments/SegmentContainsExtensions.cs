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

using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Primitives;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable RedundantBoolCompare

namespace AlastairLundy.DotExtensions.MsExtensions.System.StringSegments;

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
            {
                return true;
            }
        }
        
        return false;
    }

    /// <summary>
    /// Returns whether the String Segment contains another String Segment.
    /// </summary>
    /// <param name="this">The string segment to search.</param>
    /// <param name="segment">The string segment to search for.</param>
    /// <returns>True if the string segment contains the specified string segment; false otherwise.</returns>
    public static bool Contains(this StringSegment @this, StringSegment segment)
    {
        if (@this.Length == segment.Length)
        {
            return @this.Equals(segment);
        }
        if (segment.Length > @this.Length)
        {
            return false;
        }
        
        char[] segmentArray = segment.ToCharArray();

        bool containsAllChars = segmentArray.All(x => @this.Contains(x));
        
        if (containsAllChars == false)
        {
            return false;
        }
        
        int startIndex = 0;

        for (int originIndex = 0; originIndex < @this.Length; originIndex++)
        {
            char c = @this[originIndex];
            bool found = false;

            for (int i = startIndex; i < segment.Length; i++)
            {
                if (segmentArray[i] == c)
                {
                    found = true;
                    startIndex = i + 1;
                    break;
                }
            }

            if (found == false)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Returns whether a string segment contains a string.
    /// </summary>
    /// <param name="this">The StringSegment to be searched.</param>
    /// <param name="str">The string to search for.</param>
    /// <returns>True if the String Segment contains the specified string; false otherwise.</returns>
    public static bool Contains(this StringSegment @this, string str)
    {
        StringSegment val = new StringSegment(str);

        return Contains(@this, val);
    }
    
    /// <summary>
    /// Returns whether a StringSegment contains any of the specified possible chars.
    /// </summary>
    /// <param name="source">The string segment to be searched.</param>
    /// <param name="possibleValues">The possible chars to search for.</param>
    /// <returns>True if any of the possible values is found, false otherwise.</returns>
    public static bool ContainsAnyOf(this StringSegment source, IEnumerable<char> possibleValues)
    {
        return source.IndexOfAny([..possibleValues]) != -1;
    }
    
    /// <summary>
    /// Returns whether a StringSegment contains all the specified possible chars.
    /// </summary>
    /// <param name="source">The string segment to be searched.</param>
    /// <param name="possibleValues">The possible chars to search for.</param>
    /// <returns>True if all the values are found in the string segment; false otherwise.</returns>
    public static bool ContainsAllOf(this StringSegment source, IEnumerable<char> possibleValues)
    {
        return possibleValues.All(c => source.Contains(c));
    }
}