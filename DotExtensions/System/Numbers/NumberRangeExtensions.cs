/*
        MIT License

       Copyright (c) 2025 Alastair Lundy

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

namespace AlastairLundy.DotExtensions.Numbers;

/// <summary>
/// Provides static extension methods for working with number ranges.
/// </summary>
public static class NumberRangeExtensions
{
#if NET8_0_OR_GREATER || NETSTANDARD2_1
    /// <param name="start">The starting index or integer value of the range.</param>
    extension(int start)
    {
        /// <summary>
        /// Creates a new instance of the Range struct based on the given start value and length.
        /// </summary>
        /// <param name="count">The number of elements in the range.</param>
        /// <returns>A new Range instance representing the specified sequence of values.</returns>
        public Range AsRange(int count) =>
            new(new(start), new(start + count));
    }

    /// <param name="start">The starting Index value.</param>
    extension(Index start)
    {
        /// <summary>
        /// Creates a new instance of the Range struct based on the given Index value and length.
        /// </summary>
        /// <param name="count">The number of elements in the range.</param>
        /// <returns>A new Range instance representing the specified sequence of values.</returns>
        public Range AsRange(int count) =>
            new(start, new(start.Value + count));
    }
#endif
}
