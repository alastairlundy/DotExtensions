/*
 * Copyright (c) 2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
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
