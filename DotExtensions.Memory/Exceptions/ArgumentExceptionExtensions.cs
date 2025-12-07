/*
 * Copyright (c) 2024-2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using AlastairLundy.DotExtensions.Memory.Internal.Localizations;

namespace AlastairLundy.DotExtensions.Memory;

/// <summary>
/// Provides extension methods for working with instances of ArgumentException
/// and deriving additional behaviour or functionality.
/// </summary>
public static class ArgumentExceptionExtensions
{
    extension(ArgumentException)
    {
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided <see cref="Span{T}"/> of characters
        /// is empty or contains only whitespace characters.
        /// </summary>
        /// <param name="span">The span of characters to check for emptiness or whitespace.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the span is empty or contains only whitespace characters.
        /// </exception>
        public static void ThrowIfEmptyOrWhiteSpace(Span<char> span)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(span);

            if (Span<char>.IsEmptyOrWhiteSpace(span))
                throw new ArgumentException(Resources.Exceptions_Argument_WhiteSpace_Span);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided <see cref="Span{T}"/> of characters
        /// is empty or contains only whitespace characters.
        /// </summary>
        /// <param name="span">The span of characters to check for emptiness or whitespace.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the span is empty or contains only whitespace characters.
        /// </exception>
        public static void ThrowIfEmptyOrWhiteSpace(Span<string> span)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(span);

            if (Span<string>.IsEmptyOrWhiteSpace(span))
                throw new ArgumentException(Resources.Exceptions_Argument_WhiteSpace_Span);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided <see cref="ReadOnlySpan{T}"/> of characters
        /// is empty or contains only whitespace characters.
        /// </summary>
        /// <param name="span">The read-only span of characters to check for emptiness or whitespace.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the span is empty or contains only whitespace characters.
        /// </exception>
        public static void ThrowIfEmptyOrWhiteSpace(ReadOnlySpan<char> span)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(span);

            if (ReadOnlySpan<char>.IsEmptyOrWhiteSpace(span))
                throw new ArgumentException(Resources.Exceptions_Argument_WhiteSpace_Span);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided <see cref="Span{T}"/> of characters
        /// is empty or contains only whitespace characters.
        /// </summary>
        /// <param name="span">The span of characters to validate.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the span is empty or contains only whitespace characters.
        /// </exception>
        public static void ThrowIfEmptyOrWhiteSpace(ReadOnlySpan<string> span)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(span);

            if (ReadOnlySpan<string>.IsEmptyOrWhiteSpace(span))
                throw new ArgumentException(Resources.Exceptions_Argument_WhiteSpace_Span);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided <see cref="Memory{T}"/> of characters
        /// is empty or contains only whitespace characters.
        /// </summary>
        /// <param name="memory">The memory of characters to check for emptiness or whitespace.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the memory is empty or contains only whitespace characters.
        /// </exception>
        public static void ThrowIfEmptyOrWhiteSpace(Memory<char> memory)
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(memory);

            if (Memory<char>.IsEmptyOrWhiteSpace(memory))
                throw new ArgumentException(Resources.Exceptions_Argument_WhiteSpace_Memory);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided <see cref="Memory{T}"/> of strings
        /// is empty or contains any null or whitespace-only elements.
        /// </summary>
        /// <param name="memory">The memory to check for emptiness or invalid string elements.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the provided memory is empty.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the memory contains a null or whitespace-only string element.
        /// </exception>
        public static void ThrowIfEmptyOrWhiteSpace(Memory<string> memory)
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(memory);

            if (Memory<string>.IsEmptyOrWhiteSpace(memory))
                throw new ArgumentException(Resources.Exceptions_Argument_WhiteSpace_Memory);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided <see cref="ReadOnlyMemory{T}"/> of characters
        /// is empty or contains only whitespace characters.
        /// </summary>
        /// <param name="memory">The memory to check for emptiness or whitespace.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the memory is empty or contains only whitespace characters.
        /// </exception>
        public static void ThrowIfEmptyOrWhiteSpace(ReadOnlyMemory<char> memory)
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(memory);

            if (ReadOnlyMemory<char>.IsEmptyOrWhiteSpace(memory))
                throw new ArgumentException(Resources.Exceptions_Argument_WhiteSpace_Memory);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided <see cref="Memory{T}"/> of strings
        /// is empty or any of its elements are null or contain only whitespace.
        /// </summary>
        /// <param name="memory">The memory of strings to validate.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the provided memory is empty.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when any string in the memory is null or contains only whitespace characters.
        /// </exception>
        public static void ThrowIfEmptyOrWhiteSpace(ReadOnlyMemory<string> memory)
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(memory);

            if (ReadOnlyMemory<string>.IsEmptyOrWhiteSpace(memory))
                throw new ArgumentException(Resources.Exceptions_Argument_WhiteSpace_Memory);
        }
    }
}