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

using System.Numerics;

namespace DotExtensions.Numbers;

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
        /// <remarks>
        /// If <see cref="INumber{TSelf}"/> type has a lower level of precision than <see cref="int"/>, this conversion may be lossy.
        /// The conversion is culture-independent (no text round-trip). A value the destination type
        /// cannot represent - out of range, or <see cref="double.NaN"/> for non-floating destinations -
        /// throws <see cref="OverflowException"/>.
        /// </remarks>
        /// <typeparam name="TNumber">The destination number type to convert the source number to.</typeparam>
        /// <returns>The source number converted to the <see cref="INumber{TSelf}"/> type.</returns>
        public TNumber ToNumber<TNumber>()
            where TNumber : INumber<TNumber>
            => TNumber.CreateChecked(number);
    }

    /// <param name="number">The number to be converted.</param>
    /// <typeparam name="TSourceNumber">The source number type to convert.</typeparam>
    extension<TSourceNumber>(TSourceNumber number) where TSourceNumber : INumber<TSourceNumber>
    {
        /// <summary>
        /// Converts a number of <see cref="INumber{TSourceNumber}"/> to a number of type <see cref="INumber{TDestinationNumber}"/>.
        /// </summary>
        /// <remarks>
        /// If <see cref="INumber{TDestinationNumber}"/> type has a lower level of precision than <see cref="INumber{TSourceNumber}"/>, this conversion may be lossy.
        /// The conversion is culture-independent (no text round-trip), so exponent notation such as
        /// <c>1E+21</c> and all number-separator conventions are handled without parsing. A value the
        /// destination type cannot represent - out of range (e.g. <c>1E+30</c> → <see cref="decimal"/>),
        /// or <see cref="double.NaN"/>/∞ for destinations without those representations - throws
        /// <see cref="OverflowException"/>; floating destinations preserve NaN and ∞.
        /// </remarks>
        /// <typeparam name="TDestinationNumber">The destination number type to convert the source number to.</typeparam>
        /// <returns>The source number converted to the <see cref="INumber{TDestinationNumber}"/> type.</returns>
        /// <exception cref="OverflowException">Thrown when the value cannot be represented by the destination type.</exception>
        public TDestinationNumber ToDestinationNumber<TDestinationNumber>()
            where TDestinationNumber : INumber<TDestinationNumber>
            => TDestinationNumber.CreateChecked(number);
    }
}
