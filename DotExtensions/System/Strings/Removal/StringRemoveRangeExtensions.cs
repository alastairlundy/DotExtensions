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

namespace DotExtensions.Strings;

/// <summary>
/// Provides extension methods for removing specified ranges of characters from a string.
/// </summary>
public static class StringRemoveRangeExtensions
{
    /// <param name="str">The string to remove a range of characters from.</param>
    extension(string str)
    {
        /// <summary>
        /// Removes a substring from the string, defined by the specified start and end indices.
        /// </summary>
        /// <param name="startIndex">The zero-based start index of the substring to remove.</param>
        /// <param name="endIndex">The zero-based end index of the substring to remove. This index is inclusive.</param>
        /// <returns>A new string with the specified range removed.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the startIndex or endIndex is negative, if either index is greater than or equal to the string length,
        /// or if the endIndex comes before the startIndex once both indices are resolved.
        /// </exception>
        public string Remove(Index startIndex, Index endIndex)
        {
            int start = startIndex.GetOffset(str.Length);
            int end = endIndex.GetOffset(str.Length);

            ArgumentOutOfRangeException.ThrowIfNegative(start, nameof(startIndex));
            ArgumentOutOfRangeException.ThrowIfNegative(end, nameof(endIndex));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(start, str.Length, nameof(startIndex));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(end, str.Length, nameof(endIndex));
            ArgumentOutOfRangeException.ThrowIfLessThan(end, start, nameof(endIndex));

            StringBuilder stringBuilder = new StringBuilder(str);

            // endIndex is inclusive so the length is one greater than the distance between the indices.
            int length = end - start + 1;

            stringBuilder.Remove(start, length);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Removes a substring from the string, defined by the specified start and end indices.
        /// </summary>
        /// <param name="range">The <see cref="Range"/> of indices of the substring to remove. The Range's End <see cref="Index"/> is inclusive.</param>
        /// <returns>A new string with the specified range removed.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <see cref="Range"/>'s Start <see cref="Index"/> or End <see cref="Index"/> is negative, greater than or equal to the string length,
        /// or if the Start <see cref="Index"/> is greater than the End <see cref="Index"/> once both indices are resolved.
        /// </exception>
        public string Remove(Range range)
            => str.Remove(range.Start, range.End);

        /// <summary>
        /// Removes multiple substrings from the string, defined by the specified sequence of ranges.
        /// </summary>
        /// <param name="ranges">A collection of ranges representing the substrings to remove. Each range specifies the start and end indices (inclusive) of a substring to be removed.</param>
        /// <returns>A new string with all specified ranges removed.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="ranges"/> collection is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any range specifies negative indices, indices that exceed the bounds of the string as it stands when that range is applied,
        /// or if the start index of any range exceeds its end index.
        /// </exception>
        public string RemoveRange(IEnumerable<Range> ranges)
        {
            ArgumentNullException.ThrowIfNull(ranges);

            StringBuilder stringBuilder = new StringBuilder(str);

            foreach (Range range in ranges)
            {
                int start = range.Start.GetOffset(stringBuilder.Length);
                int end = range.End.GetOffset(stringBuilder.Length);

                ArgumentOutOfRangeException.ThrowIfNegative(start, nameof(ranges));
                ArgumentOutOfRangeException.ThrowIfNegative(end, nameof(ranges));
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(start, stringBuilder.Length, nameof(ranges));
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(end, stringBuilder.Length, nameof(ranges));
                ArgumentOutOfRangeException.ThrowIfLessThan(end, start, nameof(ranges));

                // Each range's End index is inclusive so the length is one greater than the distance between the indices.
                int length = end - start + 1;

                stringBuilder = stringBuilder.Remove(start, length);
            }

            return stringBuilder.ToString();
        }
    }
}