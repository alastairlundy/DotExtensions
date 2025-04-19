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

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.DotExtensions.MsExtensions.System.StringSegments;

public static class SegmentIndexesOfExtensions
{
    /// <summary>
    /// Gets an IEnumerable of indexes for all occurrences of the specified character within the provided StringSegment.
    /// </summary>
    /// <param name="this">The string segment to be searched.</param>
    /// <param name="c">The character to search for.</param>
    /// <returns>An IEnumerable of indexes for all occurrences specified character within the String Segment;
    /// empty if not found within the String Segment.</returns>
    public static IEnumerable<int> IndexesOf(this StringSegment @this, char c)
    {
        List<int> indexes = new List<int>();
        
        for(int i = 0; i < @this.Length; i++)
        {
            if (@this[i] == c)
            {
                indexes.Add(i);
            }
        }
        
        return indexes;
    }
}