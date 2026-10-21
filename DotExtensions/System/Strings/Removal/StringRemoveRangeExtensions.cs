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
        /// <param name="startIndex">The zero-based start index of the substring to remove. From-end (<c>^</c>) indexes are supported.</param>
        /// <param name="endIndex">The zero-based end index of the substring to remove. This index is exclusive. From-end (<c>^</c>) indexes are supported.</param>
        /// <returns>A new string with the specified range removed. If the start and end indices resolve to the same position, the string is returned unchanged.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the resolved start index is negative, the resolved end index is greater than the length of the string,
        /// or the resolved start index is greater than the resolved end index.
        /// </exception>
        public string Remove(Index startIndex, Index endIndex)
        {
            int start = startIndex.GetOffset(str.Length);
            int end = endIndex.GetOffset(str.Length);

            ArgumentOutOfRangeException.ThrowIfNegative(start, nameof(startIndex));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(start, end, nameof(startIndex));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(end, str.Length, nameof(endIndex));

            StringBuilder stringBuilder = new StringBuilder(str);

            stringBuilder.Remove(start, end - start);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Removes a substring from the string, defined by the specified range of indices.
        /// </summary>
        /// <param name="range">The <see cref="Range"/> of indices of the substring to remove. The range's end is exclusive, and from-end (<c>^</c>) indexes are supported.</param>
        /// <returns>A new string with the specified range removed. An empty range returns the string unchanged.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the resolved start index is negative, the resolved end index is greater than the length of the string,
        /// or the resolved start index is greater than the resolved end index.
        /// </exception>
        public string Remove(Range range)
            => str.Remove(range.Start, range.End);

        /// <summary>
        /// Removes multiple substrings from the string, defined by the specified sequence of ranges.
        /// </summary>
        /// <param name="ranges">A collection of ranges representing the substrings to remove. Each range's end is exclusive, and from-end (<c>^</c>) indexes are supported. All ranges are resolved against the original string.</param>
        /// <returns>A new string with all specified ranges removed. An empty collection returns the original string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="ranges"/> collection is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any range's resolved start index is negative, its resolved end index is greater than the length of the string,
        /// or its resolved start index is greater than its resolved end index.
        /// </exception>
        public string RemoveRange(IEnumerable<Range> ranges)
        {
            ArgumentNullException.ThrowIfNull(ranges);

            List<(int Start, int End)> resolvedRanges = [];

            foreach (Range range in ranges)
            {
                int start = range.Start.GetOffset(str.Length);
                int end = range.End.GetOffset(str.Length);

                ArgumentOutOfRangeException.ThrowIfNegative(start, nameof(ranges));
                ArgumentOutOfRangeException.ThrowIfGreaterThan(start, end, nameof(ranges));
                ArgumentOutOfRangeException.ThrowIfGreaterThan(end, str.Length, nameof(ranges));

                resolvedRanges.Add((start, end));
            }

            StringBuilder stringBuilder = new StringBuilder(str);

            // Apply removals right-to-left so that positions to the left of the
            // current removal remain valid while the builder shrinks.
            foreach ((int start, int end) in resolvedRanges.OrderByDescending(range => range.Start))
            {
                // Clamp the end so that overlapping ranges cannot remove beyond
                // the builder's current length.
                int effectiveEnd = Math.Min(end, stringBuilder.Length);

                stringBuilder.Remove(start, effectiveEnd - start);
            }

            return stringBuilder.ToString();
        }
    }
}