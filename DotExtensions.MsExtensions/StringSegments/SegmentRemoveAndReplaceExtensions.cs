/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.MsExtensions.StringSegments;

/// <summary>
/// Provides extension methods for performing removal and replacement operations on <see cref="StringSegment"/> objects.
/// </summary>
public static class SegmentRemoveAndReplaceExtensions
{
    /// <param name="segment">The segment to remove characters from.</param>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Removes characters from a <see cref="StringSegment"/> starting at a specified index.
        /// </summary>
        /// <param name="startIndex">The index to start removing characters at in the <see cref="StringSegment"/>.</param>
        /// <returns>A <see cref="StringSegment"/> where all the characters occurring after the specified index are removed.</returns>
        /// <exception cref="NullReferenceException">Thrown if the segment is null or whitespace.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the segment is empty.</exception>
        /// <exception cref="ArgumentException">Thrown if the index is less than 0 or greater than or equal to the length of the <see cref="StringSegment"/>.</exception>
        public StringSegment Remove(int startIndex)
        {
            ArgumentException.ThrowIfNullOrEmpty(segment);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(startIndex, segment.Length);

            int length = segment.Length - startIndex - 1;

            return segment.Subsegment(0, length);
        }

        /// <summary>
        /// Removes characters from a <see cref="StringSegment"/> starting at a specified index for <paramref name="count"/> number of characters.
        /// </summary>
        /// <param name="startIndex">The index to start removing characters at in the <see cref="StringSegment"/>.</param>
        /// <param name="count">The number of characters to remove from the StringSegment from the start index.</param>
        /// <returns>A <see cref="StringSegment"/> where the characters occurring within the <paramref name="count"/> number of characters after the specified index are removed.</returns>
        /// <exception cref="NullReferenceException">Thrown if the segment is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Thrown if the index is less than 0 or greater than or equal to the length of the <see cref="StringSegment"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the count is than 0 or greater than the length of the <see cref="StringSegment"/>.</exception>
        public StringSegment Remove(int startIndex, int count)
        {
            ArgumentException.ThrowIfNullOrEmpty(segment);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(startIndex, segment.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, segment.Length);
            
            if (startIndex + count > segment.Length)
                throw new ArgumentOutOfRangeException();

            if (startIndex + count == segment.Length - 1)
                return Remove(segment, startIndex);

            int firstSegmentEnd = startIndex - 1;

            int secondSegmentStart = startIndex + count + 1;
            int secondSegmentEnd = segment.Length - secondSegmentStart;

            StringSegment firstSegment = segment.Subsegment(0, firstSegmentEnd);
            StringSegment secondSegment = segment.Subsegment(secondSegmentStart, secondSegmentEnd);

            return new StringSegment($"{firstSegment}{secondSegment}");
        }
    }
}
