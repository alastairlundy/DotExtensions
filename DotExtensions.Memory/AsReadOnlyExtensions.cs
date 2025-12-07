/*
 * Copyright (c) 2024-2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.Memory;

/// <summary>
/// Provides a set of extension methods to facilitate the conversion of mutable memory structures to their read-only equivalents.
/// </summary>
public static class AsReadOnlyExtensions
{
    /// <summary>
    /// Provides extension methods for converting writable spans to read-only spans.
    /// </summary>
    extension<T>(ref Span<T> span)
    {
        /// <summary>
        /// Converts the provided writable span to a read-only span representation.
        /// </summary>
        /// <returns>A read-only span representation of the original writable span.</returns>
        public ReadOnlySpan<T> AsReadOnlySpan()
            => span;
    }

    /// <summary>
    /// Provides extension methods for converting writable memory to read-only memory.
    /// </summary>
    extension<T>(ref Memory<T> memory)
    {
        /// <summary>
        /// Converts the provided writable memory to a read-only memory representation.
        /// </summary>
        /// <returns>A read-only memory representation of the original writable memory.</returns>
        public ReadOnlyMemory<T> AsReadOnlyMemory()
            => memory;
    }
}