/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

// ReSharper disable MemberCanBePrivate.Global

namespace AlastairLundy.DotExtensions.MsExtensions.StringSegments;

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

            if (source.IsEmpty && !segment.IsEmpty || segment.IsEmpty && !source.IsEmpty)
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
