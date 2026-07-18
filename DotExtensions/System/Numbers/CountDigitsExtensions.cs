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

#if NET8_0_OR_GREATER
using System.Numerics;
using System.Runtime.CompilerServices;
#endif

namespace DotExtensions.Numbers;

/// <summary>
/// Provides an extension method for counting the number of digits in numerical values that implement Generic Numerics' INumber{TSelf}.
/// </summary>
public static class CountDigitsExtensions
{
#if NET8_0_OR_GREATER
    /// <param name="number">The numerical value to count the digits of.</param>
    /// <typeparam name="TNumber">The type inheriting from <see cref="INumber{TSelf}"/></typeparam>
    extension<TNumber>(TNumber number) where TNumber : INumber<TNumber>
    {
        /// <summary>
        /// Counts the number of digits in the numerical value.
        /// </summary>
        /// <returns>The number of digits in the numerical value, returned as an integer; returns -1 if the value is <see cref="double.NaN"/> or <see cref="double.PositiveInfinity"/>.</returns>
        public int CountNumberOfDigits()
        {
            // Dispatch common numeric types to specialized implementations
            // to avoid the cost of generic virtual dispatch over the static-abstract
            // INumber operators. The JIT eliminates the dead branches after the
            // type check, so this is effectively free for the common types.
            if (typeof(TNumber) == typeof(int))
            {
                return CountDigitsInt32(Unsafe.As<TNumber, int>(ref number));
            }

            if (typeof(TNumber) == typeof(long))
            {
                return CountDigitsInt64(Unsafe.As<TNumber, long>(ref number));
            }

            if (typeof(TNumber) == typeof(double))
            {
                return CountDigitsDouble(Unsafe.As<TNumber, double>(ref number));
            }

            if (typeof(TNumber) == typeof(float))
            {
                return CountDigitsSingle(Unsafe.As<TNumber, float>(ref number));
            }

            if (typeof(TNumber) == typeof(decimal))
            {
                return CountDigitsDecimal(Unsafe.As<TNumber, decimal>(ref number));
            }

            return CountDigitsGeneric(number);
        }
    }

    /// <summary>
    /// Counts the number of digits in a signed 32-bit integer.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CountDigitsInt32(int number)
    {
        // int.MinValue cannot be safely negated (would overflow); it has 10 digits.
        if (number == int.MinValue)
        {
            return 10;
        }

        if (number < 0)
        {
            number = -number;
        }

        return CountDigitsPositiveInt32(number);
    }

    /// <summary>
    /// Counts the number of digits in a non-negative 32-bit integer using a comparison ladder.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CountDigitsPositiveInt32(int number)
    {
        if (number < 10) return 1;
        if (number < 100) return 2;
        if (number < 1_000) return 3;
        if (number < 10_000) return 4;
        if (number < 100_000) return 5;
        if (number < 1_000_000) return 6;
        if (number < 10_000_000) return 7;
        if (number < 100_000_000) return 8;
        if (number < 1_000_000_000) return 9;
        return 10;
    }

    /// <summary>
    /// Counts the number of digits in a signed 64-bit integer.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CountDigitsInt64(long number)
    {
        // long.MinValue cannot be safely negated (would overflow); it has 19 digits.
        if (number == long.MinValue)
        {
            return 19;
        }

        if (number < 0)
        {
            number = -number;
        }

        return CountDigitsPositiveInt64(number);
    }

    /// <summary>
    /// Counts the number of digits in a non-negative 64-bit integer using a comparison ladder.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CountDigitsPositiveInt64(long number)
    {
        if (number < 10L) return 1;
        if (number < 100L) return 2;
        if (number < 1_000L) return 3;
        if (number < 10_000L) return 4;
        if (number < 100_000L) return 5;
        if (number < 1_000_000L) return 6;
        if (number < 10_000_000L) return 7;
        if (number < 100_000_000L) return 8;
        if (number < 1_000_000_000L) return 9;
        if (number < 10_000_000_000L) return 10;
        if (number < 100_000_000_000L) return 11;
        if (number < 1_000_000_000_000L) return 12;
        if (number < 10_000_000_000_000L) return 13;
        if (number < 100_000_000_000_000L) return 14;
        if (number < 1_000_000_000_000_000L) return 15;
        if (number < 10_000_000_000_000_000L) return 16;
        if (number < 100_000_000_000_000_000L) return 17;
        if (number < 1_000_000_000_000_000_000L) return 18;
        return 19;
    }

    /// <summary>
    /// Counts the number of digits in a double-precision floating-point value using a division loop.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CountDigitsDouble(double number)
    {
        if (double.IsNaN(number))
        {
            return -1;
        }

        if (number < 0.0 || (number == 0.0 && 1.0 / number < 0.0))
        {
            number = -number;
        }

        if (double.IsPositiveInfinity(number))
        {
            return -1;
        }

        int digits = 1;
        double ten = 10.0;

        while ((number /= ten) != 0.0)
        {
            ++digits;
        }

        return digits;
    }

    /// <summary>
    /// Counts the number of digits in a single-precision floating-point value using a division loop.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CountDigitsSingle(float number)
    {
        if (float.IsNaN(number))
        {
            return -1;
        }

        if (number < 0.0f || (number == 0.0f && 1.0f / number < 0.0f))
        {
            number = -number;
        }

        if (float.IsPositiveInfinity(number))
        {
            return -1;
        }

        int digits = 1;
        float ten = 10.0f;

        while ((number /= ten) != 0.0f)
        {
            ++digits;
        }

        return digits;
    }

    /// <summary>
    /// Counts the number of digits in a decimal value using a division loop.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CountDigitsDecimal(decimal number)
    {
        decimal ten = 10m;
        decimal zero = decimal.Zero;

        if (number < zero)
        {
            number = -number;
        }

        int digits = 1;

        while ((number /= ten) != zero)
        {
            ++digits;
        }

        return digits;
    }

    /// <summary>
    /// Generic fallback for INumber{T} types without a specialized fast path.
    /// </summary>
    private static int CountDigitsGeneric<TNumber>(TNumber number) where TNumber : INumber<TNumber>
    {
        // Hoist constants out of the loop: previous implementation called
        // 10.ToNumber<TNumber>() each iteration, which round-tripped through
        // ToString() and Parse() and dominated the cost of this method.
        TNumber ten = TNumber.CreateChecked(10);
        TNumber zero = TNumber.Zero;

        if (TNumber.IsNaN(number))
        {
            return -1;
        }

        if (number < zero)
        {
            // Use zero - number rather than -number so that MinValue-style
            // values for types that define "infinite" semantics still produce
            // a well-defined positive counterpart.
            number = zero - number;
        }

        int digits = 1;

        while ((number /= ten) != zero)
        {
            ++digits;
        }

        return digits;
    }
#else
    /// <param name="number">The numerical value to count the digits of.</param>
    extension(int number)
    {
        /// <summary>
        /// Counts the number of digits in the integer value.
        /// </summary>
        /// <returns>The number of digits in the numerical value, returned as an integer.</returns>
        public int CountNumberOfDigits()
        {
            // int.MinValue cannot be safely negated (would overflow); it has 10 digits.
            if (number == int.MinValue)
            {
                return 10;
            }

            if (number < 0)
            {
                number = -number;
            }

            int digits = 1;

            while ((number /= 10) != 0)
            {
                ++digits;
            }

            return digits;
        }
    }

    /// <param name="number">The numerical value to count the digits of.</param>
    extension(long number)
    {
        /// <summary>
        /// Counts the number of digits in the numerical value.
        /// </summary>
        /// <returns>The number of digits in the numerical value, returned as an integer.</returns>
        public int CountNumberOfDigits()
        {
            // long.MinValue cannot be safely negated (would overflow); it has 19 digits.
            if (number == long.MinValue)
            {
                return 19;
            }

            if (number < 0)
            {
                number = -number;
            }

            int digits = 1;

            while ((number /= 10) != 0)
            {
                ++digits;
            }

            return digits;
        }
    }

    /// <param name="number">The numerical value to count the digits of.</param>
    extension(double number)
    {
        /// <summary>
        /// Counts the number of digits in the numerical value.
        /// </summary>
        /// <returns>The number of digits in the numerical value, returned as a double precision floating point number; returns -1 if the value is <see cref="double.NaN"/> or <see cref="double.PositiveInfinity"/>.</returns>
        public int CountNumberOfDigits()
        {
            if (double.IsNaN(number))
            {
                return -1;
            }

            if (number < 0.0 || (number == 0.0 && 1.0 / number < 0.0))
            {
                number = -number;
            }

            if (double.IsPositiveInfinity(number))
            {
                return -1;
            }

            int digits = 1;

            while ((number /= 10.0) != 0.0)
            {
                ++digits;
            }

            return digits;
        }
    }
#endif
}