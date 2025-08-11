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

using System.Globalization;
using System.Numerics;

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
}