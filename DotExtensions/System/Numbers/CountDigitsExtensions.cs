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

#if !NET7_0_OR_GREATER
using System;
using System.Linq;
#else
using System.Globalization;
using System.Numerics;
#endif

// ReSharper disable ConvertClosureToMethodGroup

namespace AlastairLundy.DotExtensions.Numbers;

/// <summary>
/// 
/// </summary>
/// <remarks>The performance of this class' Digit Counting code in .NET Standard 2.0 and 2.1 may be slower than in .NET 7 and newer.
/// <para/>
/// <para>Try to target .NET 8 or newer as a TFM where possible if Digit Counting performance is important to your code.</para>
/// </remarks>
public static class CountDigitsExtensions
{
    
#if NET7_0_OR_GREATER
    /// <summary>
    /// Counts the number of digits in the numerical value.
    /// </summary>
    /// <param name="number">The numerical value to count the digits of.</param>
    /// <typeparam name="TNumber">The type inheriting from <see cref="INumber{TSelf}"/></typeparam>
    /// <returns>The number of digits in the numerical value, returned as an integer.</returns>
    public static int CountNumberOfDigits<TNumber>(this TNumber number) where TNumber : INumber<TNumber>
    {
        if (number < TNumber.Zero)
        {
            number *= TNumber.Parse("-1", NumberFormatInfo.InvariantInfo);
        }
        
        int digits = number < TNumber.Zero ? 2 : 1;

        while ((number /= TNumber.Parse("10", NumberFormatInfo.InvariantInfo)) != TNumber.Zero)
        {
            ++digits;
        }

        return digits;
    }
#else
    private static int CountNumberOfDigits<TNumber>(this TNumber number)
    {
        if(number is null)
            throw new  ArgumentNullException(nameof(number));

        double dNumber = double.Parse(number.ToString());
        
        if (dNumber < 0.0)
        {
            dNumber *= -1.0;
        }
        
        int digits = dNumber < 0.0 ? 2 : 1;

        while ((dNumber /= 10.0) != 0.0)
        {
            ++digits;
        }

        return digits;
    }

    /// <summary>
    /// Calculates the number of digits in a given 32-Bit Integer value.
    /// </summary>
    /// <param name="number">The 32-Bit Integer value to count the digits for.</param>
    /// <returns>The number of digits in the specified value.</returns>
    public static int CountNumberOfDigits(this int number)
    {
        return CountNumberOfDigits<int>(number);
    }

    /// <summary>
    /// Calculates the number of digits in a given 64-Bit Integer value.
    /// </summary>
    /// <param name="number">The 64-Bit Integer value to count the digits for.</param>
    /// <returns>The number of digits in the specified value.</returns>
    public static int CountNumberOfDigits(this long number)
    {
        return CountNumberOfDigits<long>(number);
    }

    /// <summary>
    /// Calculates the number of digits in a given double value.
    /// </summary>
    /// <param name="number">The double value to count the digits for.</param>
    /// <returns>The number of digits in the specified value.</returns>
    public static int CountNumberOfDigits(this double number)
    {
        return CountNumberOfDigits<double>(number);
    }
#endif

    
}