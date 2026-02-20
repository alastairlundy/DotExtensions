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

namespace DotExtensions.MsExtensions.StringSegments;

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
        /// <returns>A <see cref="StringSegment"/> where all the characters occurring after the specified index
        /// have been removed.</returns>
        /// <exception cref="NullReferenceException">Thrown if the segment is null or whitespace.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the segment is empty.</exception>
        /// <exception cref="ArgumentException">Thrown if the index is less than 0 or greater than or equal to the length of the <see cref="StringSegment"/>.</exception>
        public StringSegment Remove(int startIndex)
        {
            ArgumentException.ThrowIfNullOrEmpty(segment);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(startIndex, segment.Length);

            int length = segment.Length - startIndex - 1;

            return segment.Subsegment(startIndex, length);
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
                return segment.Remove(startIndex);

            int firstSegmentEnd = startIndex - 1;

            int secondSegmentStart = startIndex + count + 1;
            int secondSegmentEnd = segment.Length - secondSegmentStart;

            StringSegment firstSegment = segment.Subsegment(0, firstSegmentEnd);
            StringSegment secondSegment = segment.Subsegment(secondSegmentStart, secondSegmentEnd);

            return new StringSegment($"{firstSegment}{secondSegment}");
        }
    }
}