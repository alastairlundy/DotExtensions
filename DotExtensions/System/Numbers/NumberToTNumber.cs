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
using System.Globalization;
using System.Numerics;
#endif

namespace AlastairLundy.DotExtensions.Numbers;

#if NET8_0_OR_GREATER

/// <summary>
/// Provides extension methods for converting numbers between different number types.
/// </summary>
public static class NumberToTNumber
{
    /// <summary>
    /// Converts a number of type <see cref="int"/> to a <see cref="TNumber"/> .
    /// </summary>
    /// <remarks>If <see cref="TNumber"/> type has a lower level of precision than <see cref="int"/>, this conversion may be lossy.</remarks>
    /// <param name="number">The number to be converted.</param>
    /// <typeparam name="TNumber">The destination number type to convert the source number to.</typeparam>
    /// <returns>The source number converted to the <see cref="TNumber"/> type.</returns>
    public static TNumber ToNumber<TNumber>(this int number) where TNumber : INumber<TNumber> 
        => ToDestinationNumber<int, TNumber>(number);

    /// <summary>
    /// Converts a number of <see cref="TSourceNumber"/> to a number of type <see cref="TDestinationNumber"/>.
    /// </summary>
    /// <remarks>If <see cref="TDestinationNumber"/> type has a lower level of precision than <see cref="TSourceNumber"/>, this conversion may be lossy.</remarks>
    /// <param name="number">The number to be converted.</param>
    /// <typeparam name="TSourceNumber">The source number type to convert.</typeparam>
    /// <typeparam name="TDestinationNumber">The destination number type to convert the source number to.</typeparam>
    /// <returns>The source number converted to the <see cref="TDestinationNumber"/> type.</returns>
    public static TDestinationNumber ToDestinationNumber<TSourceNumber, TDestinationNumber>(this TSourceNumber number)
    where TSourceNumber : INumber<TSourceNumber>
    where TDestinationNumber : INumber<TDestinationNumber>
    {
        return TDestinationNumber.Parse(number.ToString(), NumberFormatInfo.CurrentInfo);
    }
}

#endif