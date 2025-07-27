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

using AlastairLundy.DotExtensions.Collections.Strings.Enumerables;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.DotExtensions.MsExtensions.System.StringSegments;

public static class SegmentIndicesOfExtensions
{
    /// <summary>
    /// Finds all occurrences of the specified character within the provided StringSegment.
    /// </summary>
    /// <param name="this">The string segment to be searched.</param>
    /// <param name="c">The character to search for.</param>
    /// <returns>An IEnumerable of Indices for all occurrences specified character within the String Segment;
    /// empty if not found within the String Segment.
    /// </returns>
    public static IEnumerable<int> IndicesOf(this StringSegment @this, char c)
    {
        List<int> indices = new List<int>();
        
        for(int i = 0; i < @this.Length; i++)
        {
            if (@this[i] == c)
            {
                indices.Add(i);
            }
        }

        if (indices.Count == 0)
            indices = [-1];
        
        return indices;
    }

    /// <summary>
    /// Finds all occurrences of the specified character within the provided StringSegment.
    /// </summary>
    /// <param name="this">The string segment to be searched.</param>
    /// <param name="c">The character to search for.</param>
    /// <returns>An array of Indices for all occurrences specified character within the String Segment;
    /// empty if not found within the String Segment.
    /// </returns>
    public static int[] IndicesOfAsArray(this StringSegment @this, char c)
    {
        List<int> indices = new List<int>();
        
        for(int i = 0; i < @this.Length; i++)
        {
            if (@this[i] == c)
            {
                indices.Add(i);
            }
        }

        if (indices.Count == 0)
            indices = [-1];
        
        return indices.ToArray();
    }

    
    /// <summary>
    /// Finds the index of a specified StringSegment within another StringSegment.
    /// </summary>
    /// <param name="this">The StringSegment to be searched.</param>
    /// <param name="segment">The StringSegment to search for.</param>
    /// <returns>The index at which the specified StringSegment can be found, or -1 if not found.</returns>
    public static int IndexOf(this StringSegment @this, StringSegment segment)
    {
        if (@this.Length < segment.Length || segment.Length == 0)
            return -1;
        
        IEnumerable<int> indexes = IndicesOf(@this, segment.First()).Where(x  => x != -1);

        foreach (int index in indexes)
        {
            StringSegment indexSegment = segment.Subsegment(index, segment.Length);

            if (indexSegment.Equals(@this))
            {
                return index;
            }
        }

        return -1;
    }

    /// <summary>
    /// Gets an IEnumerable of Indices for all occurrences of the specified character within the provided StringSegment.
    /// </summary>
    /// <param name="this">The string segment to be searched.</param>
    /// <param name="segment">The StringSegment to search for.</param>
    /// <returns>An IEnumerable of Indices for all occurrences specified StringSegment within the String Segment;
    /// an empty sequence otherwise.
    /// </returns>
    public static IEnumerable<int> IndicesOf(this StringSegment @this, StringSegment segment)
    {
        List<int> indices = new();

        if (@this.Length < segment.Length || segment.Length == 0)
            return [-1];

        IEnumerable<int> indexes = IndicesOf(@this, segment.First()).Where(x => x != -1);

        foreach (int index in indexes)
        {
            StringSegment indexSegment = segment.Subsegment(index, segment.Length);

            if (indexSegment.Equals(@this))
            {
                indices.Add(index);
            }
        }

        if(indices.Count == 0)
            indices = [-1];
        
        return indices;
    }
    
    /// <summary>
    /// Finds all occurrences of the specified character within the provided StringSegment.
    /// </summary>
    /// <param name="this">The string segment to be searched.</param>
    /// <param name="segment">The StringSegment to search for.</param>
    /// <returns>An array of Indices for all occurrences of the specified StringSegment within the String Segment;
    /// an empty array otherwise.
    /// </returns>
    public static int[] IndicesOfAsArray(this StringSegment @this, StringSegment segment)
    {
        List<int> indices = new();

        if (@this.Length < segment.Length || segment.Length == 0)
            return [];

        IEnumerable<int> indexes = IndicesOf(@this, segment.First()).Where(x => x != -1);

        foreach (int index in indexes)
        {
            StringSegment indexSegment = segment.Subsegment(index, segment.Length);

            if (indexSegment.Equals(@this))
            {
                indices.Add(index);
            }
        }
        
        return indices.ToArray();
    }
    
    /// <summary>
    /// Finds the index of a specified StringSegment within a string.
    /// </summary>
    /// <param name="str">The string to be searched.</param>
    /// <param name="segment">The StringSegment to search for.</param>
    /// <returns>The index at which the specified StringSegment can be found, or -1 if not found.</returns>
    public static int IndexOf(this string str, StringSegment segment)
    {
        if (str.Length < segment.Length || segment.Length == 0)
            return -1;
        
        IEnumerable<int> indexes = str.IndicesOf(segment.First()).Where(x  => x != -1);

        foreach (int index in indexes)
        {
            StringSegment indexSegment = segment.Subsegment(index, segment.Length);

            if (indexSegment.Equals(str))
            {
                return index;
            }
        }

        return -1;
    }


    /// <summary>
    /// Finds all occurrences of a specified StringSegment within a string.
    /// </summary>
    /// <param name="str">The string to be searched.</param>
    /// <param name="segment">The StringSegment to search for.</param>
    /// <returns>An IEnumerable of Indices for all occurrences of the specified StringSegment within the string; empty if not found within the String Segment.</returns>
    public static IEnumerable<int> IndicesOf(this string str, StringSegment segment)
    {
        List<int> indices = new();

        if (str.Length < segment.Length || segment.IsEmpty())
            return [-1];

        IEnumerable<int> indexes = str.IndicesOf(segment.First()).Where(x => x != -1);

        foreach (int index in indexes)
        {
            StringSegment indexSegment = segment.Subsegment(index, segment.Length);

            if (indexSegment.Equals(str))
            {
                indices.Add(index);
            }
        }

        if(indices.Count == 0)
            indices = [-1];
        
        return indices;
    }
    
    /// <summary>
    /// Finds all occurrences of a specified StringSegment within a string.
    /// </summary>
    /// <param name="str">The string to be searched.</param>
    /// <param name="segment">The StringSegment to search for.</param>
    /// <returns>An array of Indices for all occurrences of the specified StringSegment within the string; empty if not found within the String Segment.</returns>
    public static int[] IndicesOfAsArray(this string str, StringSegment segment)
    {
        List<int> indices = new();

        if (str.Length < segment.Length || segment.IsEmpty())
            return [];

        IEnumerable<int> indexes = str.IndicesOf(segment.First()).Where(x => x != -1);

        foreach (int index in indexes)
        {
            StringSegment indexSegment = segment.Subsegment(index, segment.Length);

            if (indexSegment.Equals(str))
            {
                indices.Add(index);
            }
        }
        
        return indices.ToArray();
    }
}