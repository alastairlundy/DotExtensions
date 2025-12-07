/*
 * Copyright (c) 2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using System.Collections.Generic;
#if NET8_0_OR_GREATER
using System.Numerics;
#endif

namespace AlastairLundy.DotExtensions.Numbers;

/// <summary>
/// Provides methods for verifying if a sequence or list of numbers follows a specific incremental pattern.
/// </summary>
public static class IncrementedNumberRange
{
#if NET8_0_OR_GREATER
    /// <param name="source">The list of numbers to check.</param>
    /// <typeparam name="TNumber">The type that represents the numeric class or struct used for manipulating numbers.</typeparam>
    extension<TNumber>(IList<TNumber> source) where TNumber : INumber<TNumber>
    {
        /// <summary>
        /// Determines if a list of numbers is incremented by an expected amount starting from the first number in the list.
        /// </summary>
        /// <param name="expectedIncrement">The amount each number is expected to be incremented by.</param>
        /// <returns>True if each number in the list of numbers is incremented by the expected amount from the first number onwards, false otherwise.</returns>
        public bool IsIncrementedNumberRange(TNumber expectedIncrement
        )
        {
            ArgumentNullException.ThrowIfNull(source);

            TNumber expectedNumber = source[0];

            for (int index = 0; index < source.Count; index++)
            {
                TNumber actual = source[index];

                if (index > 0)
                    expectedNumber += expectedIncrement;

                if (expectedNumber != actual)
                    return false;
            }

            return true;
        }
    }

    /// <param name="source">The sequence of numbers to check.</param>
    /// <typeparam name="TNumber">The type that represents the numeric class or struct used for manipulating numbers.</typeparam>
    extension<TNumber>(IEnumerable<TNumber> source) where TNumber : INumber<TNumber>
    {
        /// <summary>
        /// Determines if a sequence of numbers is incremented by an expected amount starting from the first number in the sequence.
        /// </summary>
        /// <param name="expectedIncrement">The amount each number is expected to be incremented by.</param>
        /// <returns>True if each number in the sequence of numbers is incremented by the expected amount from the first number onwards, false otherwise.</returns>
        public bool IsIncrementedNumberRange(TNumber expectedIncrement
        )
        {
            ArgumentNullException.ThrowIfNull(source);

            if (source is IList<TNumber> list)
                return IsIncrementedNumberRange(list, expectedIncrement);

            bool foundFirstNumber = false;

            TNumber expectedNumber = TNumber.Zero;

            foreach (TNumber actual in source)
            {
                if (!foundFirstNumber)
                {
                    expectedNumber = actual;
                    foundFirstNumber = true;
                }
                else
                {
                    expectedNumber += expectedIncrement;
                }

                if (expectedNumber != actual)
                    return false;
            }

            return true;
        }
    }
#else
    /// <param name="source">The list of numbers to check.</param>
    extension(IList<int> source)
    {
        /// <summary>
        /// Determines if a list of numbers is incremented by an expected amount starting from the first number in the list.
        /// </summary>
        /// <param name="expectedIncrement">The amount each number is expected to be incremented by.</param>
        /// <returns>True if each number in the list of numbers is incremented by the expected amount from the first number onwards, false otherwise.</returns>
        public bool IsIncrementedNumberRange(int expectedIncrement)
        {
            int expectedNumber = source[0];

            for (int index = 0; index < source.Count; index++)
            {
                int actual = source[index];

                if (index > 0)
                    expectedNumber += expectedIncrement;

                if (expectedNumber != actual)
                    return false;
            }

            return true;
        }
    }

    /// <param name="source">The sequence of numbers to check.</param>
    extension(IEnumerable<int> source)
    {
        /// <summary>
        /// Determines if a sequence of numbers is incremented by an expected amount starting from the first number in the sequence.
        /// </summary>
        /// <param name="expectedIncrement">The amount each number is expected to be incremented by.</param>
        /// <returns>True if each number in the sequence of numbers is incremented by the expected amount from the first number onwards, false otherwise.</returns>
        public bool IsIncrementedNumberRange(int expectedIncrement)
        {
            if (source is IList<int> list)
                return IsIncrementedNumberRange(list, expectedIncrement);

            bool foundFirstNumber = false;

            int expectedNumber = 0;

            foreach (int actual in source)
            {
                if (foundFirstNumber == false)
                {
                    expectedNumber = actual;
                    foundFirstNumber = true;
                }
                else
                {
                    expectedNumber += expectedIncrement;
                }

                if (expectedNumber != actual)
                    return false;
            }

            return true;
        }
    }
#endif
}
