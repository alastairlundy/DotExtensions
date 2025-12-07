/*
 * Copyright (c) 2024-2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using AlastairLundy.DotExtensions.Memory.Internal.Localizations;

namespace AlastairLundy.DotExtensions.Memory;

/// <summary>
/// Provides extension methods for validating and throwing an <see cref="InvalidOperationException"/>
/// if a collection or enumerable is empty. These extensions are designed to simplify
/// defensive programming by reducing boilerplate code for common validation operations.
/// </summary>
public static class InvalidOperationThrowIfEmptyExtensions
{
   /// <summary>
   /// 
   /// </summary>
   /// <typeparam name="T">The type of elements in the Span or memory.</typeparam>
   extension<T>(InvalidOperationException)
   {
      /// <summary>
      /// Throws an <see cref="InvalidOperationException"/> if the provided <see cref="Span{T}"/> is empty.
      /// </summary>
      /// <param name="span">The span to check for emptiness.</param>
      /// <exception cref="InvalidOperationException">
      /// Thrown when the provided span is empty.
      /// </exception>
      public static void ThrowIfSpanIsEmpty(Span<T> span)
      {
         if (span.IsEmpty)
            throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptySpan);
      }

      /// <summary>
      /// Throws an <see cref="InvalidOperationException"/> if the provided <see cref="Span{T}"/> is empty.
      /// </summary>
      /// <param name="span">The span to check for emptiness.</param>
      /// <exception cref="InvalidOperationException">
      /// Thrown when the provided span is empty.
      /// </exception>
      public static void ThrowIfSpanIsEmpty(ReadOnlySpan<T> span)
      {
         if (span.IsEmpty)
            throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptySpan);
      }

      /// <summary>
      /// Throws an <see cref="InvalidOperationException"/> if the provided <see cref="Memory{T}"/> is empty.
      /// </summary>
      /// <param name="memory">The memory to check for emptiness.</param>
      /// <exception cref="InvalidOperationException">
      /// Thrown when the provided memory is empty.
      /// </exception>
      public static void ThrowIfMemoryIsEmpty(Memory<T> memory)
      {
         if (memory.IsEmpty)
            throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptyMemory);
      }

      /// <summary>
      /// Throws an <see cref="InvalidOperationException"/> if the provided <see cref="Memory{T}"/> is empty.
      /// </summary>
      /// <param name="memory">The memory to check for emptiness.</param>
      /// <exception cref="InvalidOperationException">
      /// Thrown when the provided memory is empty.
      /// </exception>
      public static void ThrowIfMemoryIsEmpty(ReadOnlyMemory<T> memory)
      {
         if (memory.IsEmpty)
            throw new InvalidOperationException(Resources.Exceptions_InvalidOperation_EmptyMemory);
      }
   }
}