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
    /// <param name="number">The number to be converted.</param>
    extension(int number)
    {
        /// <summary>
        /// Converts a number of type <see cref="int"/> to a <see cref="INumber{TSelf}"/> .
        /// </summary>
        /// <remarks>If <see cref="INumber{TSelf}"/> type has a lower level of precision than <see cref="int"/>, this conversion may be lossy.</remarks>
        /// <typeparam name="TNumber">The destination number type to convert the source number to.</typeparam>
        /// <returns>The source number converted to the <see cref="INumber{TSelf}"/> type.</returns>
        public TNumber ToNumber<TNumber>()
            where TNumber : INumber<TNumber> 
            => ToDestinationNumber<int, TNumber>(number);
    }

    /// <param name="number">The number to be converted.</param>
    /// <typeparam name="TSourceNumber">The source number type to convert.</typeparam>
    extension<TSourceNumber>(TSourceNumber number) where TSourceNumber : INumber<TSourceNumber>
    {
        /// <summary>
        /// Converts a number of <see cref="INumber{TSourceNumber}"/> to a number of type <see cref="INumber{TDestinationNumber}"/>.
        /// </summary>
        /// <remarks>If <see cref="INumber{TDestinationNumber}"/> type has a lower level of precision than <see cref="INumber{TSourceNumber}"/>, this conversion may be lossy.</remarks>
        /// <typeparam name="TDestinationNumber">The destination number type to convert the source number to.</typeparam>
        /// <returns>The source number converted to the <see cref="INumber{TDestinationNumber}"/> type.</returns>
        public TDestinationNumber ToDestinationNumber<TDestinationNumber>()
            where TDestinationNumber : INumber<TDestinationNumber> 
            => TDestinationNumber.Parse(number.ToString(), NumberFormatInfo.CurrentInfo);
    }
}

#endif
