/*
        MIT License

       Copyright (c) 2024-2025 Alastair Lundy

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


// ReSharper disable once CheckNamespace
namespace DotExtensions.Memory.Exceptions;

/// <summary>
/// Provides extension methods for validating and throwing an <see cref="InvalidOperationException"/>
/// if a collection or enumerable is empty. These extensions simplify
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