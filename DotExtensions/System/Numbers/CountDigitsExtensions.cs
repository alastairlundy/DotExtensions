/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

#if NET8_0_OR_GREATER
using System.Numerics;
#endif

// ReSharper disable ConvertClosureToMethodGroup

namespace AlastairLundy.DotExtensions.Numbers;

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
        /// <returns>The number of digits in the numerical value, returned as an integer.</returns>
        public int CountNumberOfDigits()
        {
            if (number < TNumber.Zero)
            {
                number *= (-1.ToNumber<TNumber>());
            }

            int digits = number < TNumber.Zero ? 2 : 1;

            while ((number /= 10.ToNumber<TNumber>()) != TNumber.Zero)
            {
                ++digits;
            }

            return digits;
        }
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
            if (number < 0)
            {
                number *= -1;
            }

            int digits = number < 0 ? 2 : 1;

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
            if (number < 0)
            {
                number *= -1;
            }

            int digits = number < 0 ? 2 : 1;

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
        /// <returns>The number of digits in the numerical value, returned as a double precision floating point number.</returns>
        public int CountNumberOfDigits()
        {
            if (number < 0.0)
            {
                number *= -1.0;
            }

            int digits = number < 0 ? 2 : 1;

            while ((number /= 10.0) != 0.0)
            {
                ++digits;
            }

            return digits;
        }
    }
#endif
}
