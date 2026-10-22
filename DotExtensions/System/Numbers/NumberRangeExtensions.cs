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

namespace DotExtensions.Numbers;

/// <summary>
/// Provides static extension methods for working with number ranges.
/// </summary>
public static class NumberRangeExtensions
{
    /// <param name="start">The starting index or integer value of the range.</param>
    extension(int start)
    {
        /// <summary>
        /// Creates a new instance of the Range struct based on the given start value and length.
        /// </summary>
        /// <param name="count">The number of elements in the range.</param>
        /// <returns>A new Range instance representing the specified sequence of values.</returns>
        public Range AsRange(int count) =>
            new(new Index(start), new Index(start + count));
    }

    /// <param name="start">The starting Index value.</param>
    extension(Index start)
    {
        /// <summary>
        /// Creates a new instance of the Range struct based on the given Index value and length.
        /// </summary>
        /// <remarks>
        /// When <paramref name="start"/> is from-end (for example <c>^2</c>), the resulting range is also
        /// anchored to the end of the sequence: the end index is <c>^0</c> minus the requested count from
        /// the end, so <c>new Index(2, true).AsRange(2)</c> on a 10-element sequence yields <c>^2..^0</c>
        /// covering the last two elements.
        /// </remarks>
        /// <param name="count">The number of elements in the range.</param>
        /// <returns>A new Range instance representing the specified sequence of values.</returns>
        public Range AsRange(int count)
        {
            Index end = start.IsFromEnd
                ? new Index(start.Value - count, fromEnd: true)
                : new Index(start.Value + count);

            return new Range(start, end);
        }
    }
}