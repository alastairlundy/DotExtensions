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
