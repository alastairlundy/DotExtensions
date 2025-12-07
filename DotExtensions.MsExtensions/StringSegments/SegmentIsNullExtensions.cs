/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.MsExtensions.StringSegments;

/// <summary>
/// A class to hold StringSegment extension methods to check if a StringSegment is null, empty,
/// whitespace, or a combination thereof.
/// </summary>
public static class SegmentIsNullExtensions
{
    
    extension(StringSegment segment)
    {
        /// <summary>
        /// Returns true if this string segment is empty.
        /// </summary>
        /// <returns>True if the string segment is empty; otherwise, false.</returns>
        public bool IsEmpty => segment.Length == 0;
        
        /// <summary>
        /// Checks whether the specified string segment is null or whitespace.
        /// </summary>
        /// <paramref name="other"></paramref>
        /// <returns>True if the string segment is null or empty; otherwise, false.</returns>
        public static bool IsNullOrEmpty(StringSegment? other)
        {
            if (!other.HasValue)
                return true;

            return other.Value.IsEmpty;
        }
        
        /// <summary>
        /// Checks whether the specified string segment is null or whitespace.
        /// </summary>
        /// <paramref name="other"></paramref>
        /// <returns>True if the string segment is null or empty; otherwise, false.</returns>
        public static bool IsNullOrWhiteSpace(StringSegment? other)
        {
            if (other is null)
                return true;

            return StringSegment.IsWhiteSpace((StringSegment)other);
        }

        /// <summary>
        /// Determines whether the specified string segment consists entirely of whitespace characters.
        /// </summary>
        /// <param name="other">The string segment to evaluate.</param>
        /// <returns>True if the string segment consists only of whitespace characters; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the provided <see cref="StringSegment"/> is null.</exception>
        public static bool IsWhiteSpace(StringSegment other)
        {
            ArgumentNullException.ThrowIfNull(other);

            if (other.IsEmpty)
                return false;
            
            for (int index = 0; index < other.Length; index++)
            {
                char c = other[index];

                if (!char.IsWhiteSpace(c))
                    return false;
            }

            return true;
        }
    }
}
