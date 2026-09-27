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

using System.Globalization;
using System.Runtime.CompilerServices;
using DotExtensions.Localizations;

namespace DotExtensions.Exceptions;

/// <summary>
/// The v11 guard module: the single <c>ArgumentExceptionExtensions</c> type that carries the
/// settled guard survivors over <see cref="StringSegment"/>, <see cref="StringValues"/>, and char
/// span/memory receivers. Every member validates its argument and throws; none has any other
/// behaviour.
/// </summary>
/// <remarks>
/// <para><b>Guard</b> (glossary definition, verbatim): An argument-validation member whose only
/// behaviour is throwing when content violates a predicate; the predicate's semantics are owned by
/// the BCL contract it cites, never redefined locally.</para>
/// <para><b>No-construction rule (ADR 0002):</b> guards in v11 do not construct disposable state.
/// Predicate evaluation is a pure function of the arguments handed over, so a guard call site never
/// acquires a disposal obligation, including on throw paths.</para>
/// </remarks>
public static class ArgumentExceptionExtensions
{
    /// <summary>
    /// Provides the eight public guard members over StringSegment, StringValues, and char
    /// span/memory receivers.
    /// </summary>
    extension(ArgumentException)
    {
        #region StringSegment

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the specified <see cref="StringSegment"/>
        /// is null or has a length of zero.
        /// </summary>
        /// <param name="target">The <see cref="StringSegment"/> to validate.</param>
        /// <param name="paramName">
        /// The name of the parameter being validated. Captured automatically from the caller's
        /// argument expression when not supplied.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/> is null or has a length of zero. The message stays
        /// bare: this predicate restates BCL-owned null-or-empty semantics.
        /// </exception>
        public static void ThrowIfNullOrEmpty(StringSegment? target,
            [CallerArgumentExpression(nameof(target))] string? paramName = null)
        {
            ArgumentNullException.ThrowIfNull(target, paramName);

            if (StringSegment.IsNullOrEmpty(target))
                throw new ArgumentNullException(paramName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the specified <see cref="StringSegment"/>
        /// is null, and an <see cref="ArgumentException"/> if it is empty or consists solely of
        /// whitespace characters.
        /// </summary>
        /// <param name="target">The <see cref="StringSegment"/> to validate.</param>
        /// <param name="paramName">
        /// The name of the parameter being validated. Captured automatically from the caller's
        /// argument expression when not supplied.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/> is null. The message stays bare.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="target"/> is empty or contains only whitespace characters.
        /// The message is localized: the whitespace predicate is library-owned.
        /// </exception>
        public static void ThrowIfNullOrWhiteSpace(StringSegment? target,
            [CallerArgumentExpression(nameof(target))] string? paramName = null)
        {
            ArgumentNullException.ThrowIfNull(target, paramName);

            if (StringSegment.IsNullOrWhiteSpace(target))
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture,
                        Resources.Exceptions_NullOrWhiteSpace_CannotBeNullOrWhiteSpace, paramName),
                    paramName);
        }

        #endregion

        #region StringValues

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the specified <see cref="StringValues"/>
        /// is null or contains no values.
        /// </summary>
        /// <param name="target">The <see cref="StringValues"/> to validate.</param>
        /// <param name="paramName">
        /// The name of the parameter being validated. Captured automatically from the caller's
        /// argument expression when not supplied.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/> is null or contains no values. The message stays
        /// bare: this predicate restates BCL-owned null-or-empty semantics.
        /// </exception>
        public static void ThrowIfNullOrEmpty(StringValues? target,
            [CallerArgumentExpression(nameof(target))] string? paramName = null)
        {
            ArgumentNullException.ThrowIfNull(target, paramName);

            if (StringValues.IsNullOrEmpty(target.Value))
                throw new ArgumentNullException(paramName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the specified <see cref="StringValues"/>
        /// is null, and an <see cref="ArgumentException"/> if any value it contains is null or
        /// consists solely of whitespace characters.
        /// </summary>
        /// <param name="target">The <see cref="StringValues"/> to validate.</param>
        /// <param name="paramName">
        /// The name of the parameter being validated. Captured automatically from the caller's
        /// argument expression when not supplied.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/> is null. The message stays bare.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when any value in <paramref name="target"/> is null or contains only whitespace
        /// characters. The message is localized: the whitespace predicate is library-owned.
        /// </exception>
        /// <remarks>
        /// <b>Any-value whitespace doctrine:</b> the predicate is satisfied when <b>any</b> value in
        /// the collection is null or whitespace-only — not only when all values are. A
        /// <see cref="StringValues"/> holding no values contains no offending value and therefore
        /// passes this member; use <c>ThrowIfNullOrEmpty</c> to reject empty collections.
        /// </remarks>
        public static void ThrowIfNullOrWhiteSpace(StringValues? target,
            [CallerArgumentExpression(nameof(target))] string? paramName = null)
        {
            ArgumentNullException.ThrowIfNull(target, paramName);

            if (StringValues.IsNullOrWhiteSpace(target))
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture,
                        Resources.Exceptions_NullOrWhiteSpace_CannotBeNullOrWhiteSpace, paramName),
                    paramName);
        }

        #endregion

        #region char span/memory

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided <see cref="Span{T}"/> of
        /// characters is empty or contains only whitespace characters.
        /// </summary>
        /// <param name="target">The span of characters to validate.</param>
        /// <param name="paramName">
        /// The name of the parameter being validated. Captured automatically from the caller's
        /// argument expression when not supplied.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="target"/> is empty or contains only whitespace characters.
        /// Emptiness is thrown by the module's internal emptiness worker and whitespace-only
        /// content by this member's BCL-cited predicate (<c>MemoryExtensions.IsWhiteSpace</c>);
        /// both messages are localized.
        /// </exception>
        public static void ThrowIfEmptyOrWhiteSpace(Span<char> target,
            [CallerArgumentExpression(nameof(target))] string? paramName = null)
        {
            ArgumentException.ThrowIfSpanIsEmpty(target, paramName);

            if (target.IsWhiteSpace())
                throw new ArgumentException(Resources.Exceptions_Argument_WhiteSpace_Span, paramName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided
        /// <see cref="ReadOnlySpan{T}"/> of characters is empty or contains only whitespace
        /// characters.
        /// </summary>
        /// <param name="target">The read-only span of characters to validate.</param>
        /// <param name="paramName">
        /// The name of the parameter being validated. Captured automatically from the caller's
        /// argument expression when not supplied.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="target"/> is empty or contains only whitespace characters.
        /// Emptiness is thrown by the module's internal emptiness worker and whitespace-only
        /// content by this member's BCL-cited predicate (<c>MemoryExtensions.IsWhiteSpace</c>);
        /// both messages are localized.
        /// </exception>
        public static void ThrowIfEmptyOrWhiteSpace(ReadOnlySpan<char> target,
            [CallerArgumentExpression(nameof(target))] string? paramName = null)
        {
            ArgumentException.ThrowIfSpanIsEmpty(target, paramName);

            if (target.IsWhiteSpace())
                throw new ArgumentException(Resources.Exceptions_Argument_WhiteSpace_Span, paramName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided <see cref="Memory{T}"/> of
        /// characters is empty or contains only whitespace characters.
        /// </summary>
        /// <param name="target">The memory of characters to validate.</param>
        /// <param name="paramName">
        /// The name of the parameter being validated. Captured automatically from the caller's
        /// argument expression when not supplied.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="target"/> is empty or contains only whitespace characters.
        /// Emptiness is thrown by the module's internal emptiness worker and whitespace-only
        /// content by this member's BCL-cited predicate (<c>MemoryExtensions.IsWhiteSpace</c>);
        /// both messages are localized.
        /// </exception>
        public static void ThrowIfEmptyOrWhiteSpace(Memory<char> target,
            [CallerArgumentExpression(nameof(target))] string? paramName = null)
        {
            ArgumentException.ThrowIfMemoryIsEmpty(target, paramName);

            if (target.Span.IsWhiteSpace())
                throw new ArgumentException(Resources.Exceptions_Argument_WhiteSpace_Memory, paramName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided
        /// <see cref="ReadOnlyMemory{T}"/> of characters is empty or contains only whitespace
        /// characters.
        /// </summary>
        /// <param name="target">The read-only memory of characters to validate.</param>
        /// <param name="paramName">
        /// The name of the parameter being validated. Captured automatically from the caller's
        /// argument expression when not supplied.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="target"/> is empty or contains only whitespace characters.
        /// Emptiness is thrown by the module's internal emptiness worker and whitespace-only
        /// content by this member's BCL-cited predicate (<c>MemoryExtensions.IsWhiteSpace</c>);
        /// both messages are localized.
        /// </exception>
        public static void ThrowIfEmptyOrWhiteSpace(ReadOnlyMemory<char> target,
            [CallerArgumentExpression(nameof(target))] string? paramName = null)
        {
            ArgumentException.ThrowIfMemoryIsEmpty(target, paramName);

            if (target.Span.IsWhiteSpace())
                throw new ArgumentException(Resources.Exceptions_Argument_WhiteSpace_Memory, paramName);
        }

        #endregion
    }

    #region internal emptiness workers

    /// <summary>
    /// Internalized span/memory emptiness workers (ledger T014): the four former public
    /// emptiness guards leave the v11 public surface and survive here as internal members,
    /// reworked from their original <see cref="InvalidOperationException"/> to the module's
    /// <see cref="ArgumentException"/> tier (T013's rule preserved into T014, T026). The public
    /// char-family guards call them first to handle the emptiness half of their predicate; the
    /// whitespace half is stated through the BCL <c>MemoryExtensions.IsWhiteSpace</c> contract.
    /// </summary>
    extension<T>(ArgumentException)
    {
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided <see cref="Span{T}"/> is empty.
        /// </summary>
        /// <param name="span">The span to check for emptiness.</param>
        /// <param name="paramName">
        /// The name of the parameter being validated. Captured automatically from the caller's
        /// argument expression when not supplied.
        /// </param>
        /// <exception cref="ArgumentException">Thrown when the provided span is empty.</exception>
        internal static void ThrowIfSpanIsEmpty(Span<T> span,
            [CallerArgumentExpression(nameof(span))] string? paramName = null)
        {
            if (span.IsEmpty)
                throw new ArgumentException(Resources.Exceptions_InvalidOperation_EmptySpan, paramName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided
        /// <see cref="ReadOnlySpan{T}"/> is empty.
        /// </summary>
        /// <param name="span">The read-only span to check for emptiness.</param>
        /// <param name="paramName">
        /// The name of the parameter being validated. Captured automatically from the caller's
        /// argument expression when not supplied.
        /// </param>
        /// <exception cref="ArgumentException">Thrown when the provided span is empty.</exception>
        internal static void ThrowIfSpanIsEmpty(ReadOnlySpan<T> span,
            [CallerArgumentExpression(nameof(span))] string? paramName = null)
        {
            if (span.IsEmpty)
                throw new ArgumentException(Resources.Exceptions_InvalidOperation_EmptySpan, paramName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided <see cref="Memory{T}"/> is
        /// empty.
        /// </summary>
        /// <param name="memory">The memory to check for emptiness.</param>
        /// <param name="paramName">
        /// The name of the parameter being validated. Captured automatically from the caller's
        /// argument expression when not supplied.
        /// </param>
        /// <exception cref="ArgumentException">Thrown when the provided memory is empty.</exception>
        internal static void ThrowIfMemoryIsEmpty(Memory<T> memory,
            [CallerArgumentExpression(nameof(memory))] string? paramName = null)
        {
            if (memory.IsEmpty)
                throw new ArgumentException(Resources.Exceptions_InvalidOperation_EmptyMemory, paramName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided
        /// <see cref="ReadOnlyMemory{T}"/> is empty.
        /// </summary>
        /// <param name="memory">The read-only memory to check for emptiness.</param>
        /// <param name="paramName">
        /// The name of the parameter being validated. Captured automatically from the caller's
        /// argument expression when not supplied.
        /// </param>
        /// <exception cref="ArgumentException">Thrown when the provided memory is empty.</exception>
        internal static void ThrowIfMemoryIsEmpty(ReadOnlyMemory<T> memory,
            [CallerArgumentExpression(nameof(memory))] string? paramName = null)
        {
            if (memory.IsEmpty)
                throw new ArgumentException(Resources.Exceptions_InvalidOperation_EmptyMemory, paramName);
        }
    }

    #endregion
}
