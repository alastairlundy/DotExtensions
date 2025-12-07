/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.MsExtensions.StringSegments;

/// <summary>
/// Provides extension methods for converting <see cref="StringSegment"/> objects into various character collections.
/// </summary>
public static class SegmentToCharsExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="segment"></param>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Returns the <see cref="StringSegment"/> as a Char Array.
        /// </summary>
        /// <returns>The string segment as a char array.</returns>
        /// <exception cref="ArgumentException">Thrown if the StringSegment is null or empty.</exception>
        public char[] ToCharArray()
        { 
            ArgumentException.ThrowIfNullOrEmpty(segment);
            
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
        /// <returns>A list of characters from the StringSegment if any characters are in the StringSegment.</returns>
        /// <exception cref="ArgumentException">Thrown if the StringSegment is null or empty.</exception>
        public List<char> ToList()
        {
            ArgumentException.ThrowIfNullOrEmpty(segment);

            List<char> list = [];

            for (int i = 0; i < segment.Length; i++)
            {
                list.Add(segment[i]);
            }

            return list;
        }
    }
}
