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

namespace DotExtensions.MsExtensions.Primitives;

/// <summary>
/// Provides extension methods for performing removal and replacement operations on <see cref="StringSegment"/> objects.
/// </summary>
public static class SegmentRemoveAndReplaceExtensions
{
    /// <param name="segment">The segment to remove characters from.</param>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Removes all characters from the <see cref="StringSegment"/> that occur at or after a specified index.
        /// </summary>
        /// <param name="startIndex">The zero-based index at which removal starts. Characters from this index to the end of the
        /// <see cref="StringSegment"/> are removed, so the characters occurring before it are kept.</param>
        /// <returns>A <see cref="StringSegment"/> holding the first <paramref name="startIndex"/> characters of the segment: the range
        /// [0, <paramref name="startIndex"/>), matching <c>string.Remove(int)</c> semantics. If <paramref name="startIndex"/> is 0 the
        /// result is empty; if it equals the length of the <see cref="StringSegment"/> the whole segment is kept.</returns>
        /// <exception cref="ArgumentException">Thrown if the segment is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> is less than 0 or greater than the length of the <see cref="StringSegment"/>.
        /// </exception>
        public StringSegment Remove(int startIndex)
        {
            ArgumentException.ThrowIfNullOrEmpty(segment);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex, nameof(startIndex));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, segment.Length, nameof(startIndex));

            return segment.Subsegment(0, startIndex);
        }

        /// <summary>
        /// Removes <paramref name="count"/> characters from the <see cref="StringSegment"/>, beginning at a specified index.
        /// </summary>
        /// <param name="startIndex">The zero-based index at which removal starts.</param>
        /// <param name="count">The number of characters to remove from the <see cref="StringSegment"/> starting at <paramref name="startIndex"/>.
        /// May be 0, in which case the characters of the <see cref="StringSegment"/> are unchanged.</param>
        /// <returns>A <see cref="StringSegment"/> with the characters in the range [<paramref name="startIndex"/>, <paramref name="startIndex"/> + <paramref name="count"/>)
        /// removed: the characters occurring before <paramref name="startIndex"/> followed by the characters occurring after the removed range.</returns>
        /// <remarks>
        /// <see cref="StringSegment"/> has no concatenation operation, so the retained prefix and suffix are copied into a single
        /// new string before the result is constructed; this method therefore always allocates.
        /// </remarks>
        /// <exception cref="ArgumentException">Thrown if the segment is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="startIndex"/> or <paramref name="count"/> is negative,
        /// or if <paramref name="startIndex"/> plus <paramref name="count"/> is greater than the length of the <see cref="StringSegment"/>.
        /// </exception>
        public StringSegment Remove(int startIndex, int count)
        {
            ArgumentException.ThrowIfNullOrEmpty(segment);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex, nameof(startIndex));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, segment.Length, nameof(startIndex));
            ArgumentOutOfRangeException.ThrowIfNegative(count, nameof(count));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, segment.Length - startIndex, nameof(count));

            string text = segment.ToString();
            string prefix = text.Substring(0, startIndex);
            string suffix = text.Substring(startIndex + count);

            return new StringSegment(prefix + suffix);
        }

        /// <summary>
        /// Removes a subsegment from the <see cref="StringSegment"/>, defined by the specified start and end indices.
        /// </summary>
        /// <param name="startIndex">The zero-based start index of the subsegment to remove. From-end (<c>^</c>) indexes are supported.</param>
        /// <param name="endIndex">The zero-based end index of the subsegment to remove. This index is exclusive, and from-end (<c>^</c>) indexes are supported.</param>
        /// <returns>A new <see cref="StringSegment"/> with the specified range removed. Equal resolved indexes leave the segment's characters unchanged.</returns>
        /// <exception cref="ArgumentException">Thrown if the segment is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the resolved start index is negative, the resolved end index is greater than the length of the <see cref="StringSegment"/>,
        /// or the resolved start index is greater than the resolved end index.
        /// </exception>
        public StringSegment Remove(Index startIndex, Index endIndex)
        {
            int start = startIndex.GetOffset(segment.Length);
            int end = endIndex.GetOffset(segment.Length);

            ArgumentOutOfRangeException.ThrowIfNegative(start, nameof(startIndex));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(start, end, nameof(startIndex));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(end, segment.Length, nameof(endIndex));

            return segment.Remove(start, end - start);
        }

        /// <summary>
        /// Removes a subsegment from the <see cref="StringSegment"/>, defined by the specified range.
        /// </summary>
        /// <param name="range">The <see cref="Range"/> of indices of the subsegment to remove. The range's end is exclusive, and from-end (<c>^</c>) indexes are supported.</param>
        /// <returns>A new <see cref="StringSegment"/> with the specified range removed. An empty range leaves the segment's characters unchanged.</returns>
        /// <exception cref="ArgumentException">Thrown if the segment is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the resolved start index is negative, the resolved end index is greater than the length of the <see cref="StringSegment"/>,
        /// or the resolved start index is greater than the resolved end index.
        /// </exception>
        public StringSegment Remove(Range range)
            => segment.Remove(range.Start, range.End);
    }
}