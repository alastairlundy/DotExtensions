/*
        MIT License

       Copyright (c) 2026 Alastair Lundy

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


// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable RedundantBoolCompare

namespace DotExtensions.MsExtensions.StringSegments;

/// <summary>
/// A class to hold extension methods for checking if a StringSegment Contains an item.
/// </summary>
public static class SegmentContainsExtensions
{
    /// <param name="source">The string segment to search.</param>
    extension(StringSegment source)
    {
        /// <summary>
        /// Returns whether the String Segment contains a character.
        /// </summary>
        /// <param name="character">The char to search for.</param>
        /// <returns>True if the character is found in the StringSegment, false otherwise.</returns>
        public bool Contains(char character)
        {
            ArgumentNullException.ThrowIfNull(source);

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == character)
                    return true;
            }

            return false;
        }
        
        /// <summary>
        /// Returns whether the String Segment contains another String Segment.
        /// </summary>
        /// <returns>True if the string segment contains the specified string segment; false otherwise.</returns>
        public bool Contains(StringSegment segment)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(segment);

            if ((source.IsEmpty && !segment.IsEmpty) || (segment.IsEmpty && !source.IsEmpty))
                return false;
            
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
}
