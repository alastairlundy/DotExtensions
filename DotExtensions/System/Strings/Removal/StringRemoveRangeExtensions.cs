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
        /// Thrown if the startIndex or endIndex is negative, greater than or equal to the string length, or if the startIndex is greater than the endIndex.
        /// </exception>
        /// <exception cref="ArgumentException">Thrown if the calculated length of the substring to remove is negative.</exception>
        public string Remove(Index startIndex, Index endIndex)
        {
            if (startIndex.Value == endIndex.Value)
                return str.Remove(startIndex.Value, 1);
            
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex.Value, nameof(startIndex));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(endIndex.Value, nameof(endIndex));

            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex.Value, endIndex.Value, nameof(startIndex));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(startIndex.Value, str.Length, nameof(startIndex));
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(endIndex.Value, str.Length, nameof(endIndex));
            
            StringBuilder stringBuilder = new StringBuilder(str);

            int length = endIndex.Value - startIndex.Value;
            
            if(length < 0 || length > str.Length)
                throw new ArgumentException(Resources.Exceptions_Strings_ValueNotInString, nameof(startIndex));
            
            stringBuilder.Remove(startIndex.Value, length);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Removes a substring from the string, defined by the specified start and end indices.
        /// </summary>
        /// <param name="range">The <see cref="Range"/> of indices of the substring to remove.</param>
        /// <returns>A new string with the specified range removed.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <see cref="Range"/>'s Start <see cref="Index"/> or End <see cref="Index"/> is negative, greater than or equal to the string length,
        /// or if the Start <see cref="Index"/> is greater than or equal to the End <see cref="Index"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the calculated length of the substring to remove is negative.
        /// </exception>
        public string Remove(Range range)
            => str.Remove(range.Start, range.End);

        /// <summary>
        /// Removes multiple substrings from the string, defined by the specified sequence of ranges.
        /// </summary>
        /// <param name="ranges">A collection of ranges representing the substrings to remove. Each range specifies the start and end indices of a substring to be removed.</param>
        /// <returns>A new string with all specified ranges removed.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="ranges"/> collection is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any range specifies negative indices, indices that exceed the string's bounds, or if the start index of any range exceeds its end index.
        /// </exception>
        /// <exception cref="ArgumentException">Thrown if any specified range results in a length that is negative or exceeds the bounds of the original string.</exception>
        public string RemoveRange(IEnumerable<Range> ranges)
        {
            ArgumentNullException.ThrowIfNull(ranges);

            StringBuilder stringBuilder = new StringBuilder(str);

            foreach (Range range in ranges)
            {
                ArgumentOutOfRangeException.ThrowIfNegative(range.Start.Value, nameof(ranges));
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(range.End.Value, nameof(ranges));

                ArgumentOutOfRangeException.ThrowIfGreaterThan(range.Start.Value, range.End.Value, nameof(ranges));
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(range.Start.Value, str.Length, nameof(ranges));
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(range.End.Value, str.Length, nameof(ranges));

                int length = range.End.Value - range.Start.Value;
            
                if(length < 0 || length > str.Length || length > stringBuilder.Length)
                    throw new ArgumentException(Resources.Exceptions_Strings_ValueNotInString, nameof(ranges));
            
                stringBuilder = stringBuilder.Remove(range.Start.Value, length);
            }
            
            return stringBuilder.ToString();
        }
    }
}