/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

#if NET8_0_OR_GREATER

namespace AlastairLundy.DotExtensions.Numbers;

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
        /// <param name="count">The number of elements in the range.</param>
        /// <returns>A new Range instance representing the specified sequence of values.</returns>
        public Range AsRange(int count) =>
            new(start, new Index(start.Value + count));
    }
}
#endif
