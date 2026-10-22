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
/// Provides methods for verifying if a sequence or list of numbers follows a specific incremental pattern.
/// </summary>
public static class IncrementedNumberRange
{
    /// <param name="source">The list of numbers to check.</param>
    /// <typeparam name="TNumber">The type that represents the numeric class or struct used for manipulating numbers.</typeparam>
    extension<TNumber>(IList<TNumber> source) where TNumber : INumber<TNumber>
    {
        /// <summary>
        /// Determines if an expected amount starting from the first number in the list increments a list of numbers.
        /// </summary>
        /// <param name="expectedIncrement">The amount each number is expected to be incremented by.</param>
        /// <returns>True if each number in the list of numbers is incremented by the expected amount from the first number onwards, false otherwise.</returns>
        public bool IsIncrementedNumberRange(TNumber expectedIncrement)
        {
            ArgumentNullException.ThrowIfNull(source);

            for (int index = 1; index < source.Count; index++)
            {
                if (!TryAdvance(source[index - 1], expectedIncrement, source[index]))
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
        /// Determines if an expected amount starting from the first number in the sequence increments a sequence of numbers.
        /// </summary>
        /// <param name="expectedIncrement">The amount each number is expected to be incremented by.</param>
        /// <returns>True if each number in the sequence of numbers is incremented by the expected amount from the first number onwards, false otherwise.</returns>
        public bool IsIncrementedNumberRange(TNumber expectedIncrement)
        {
            ArgumentNullException.ThrowIfNull(source);

            if (source is IList<TNumber> list)
                return list.IsIncrementedNumberRange(expectedIncrement);

            bool foundFirstNumber = false;
            TNumber previous = TNumber.Zero;

            foreach (TNumber actual in source)
            {
                if (!foundFirstNumber)
                {
                    previous = actual;
                    foundFirstNumber = true;
                }
                else
                {
                    if (!TryAdvance(previous, expectedIncrement, actual))
                        return false;

                    previous = actual;
                }
            }

            return true;
        }
    }

    /// <summary>
    /// Advances <paramref name="previous"/> by <paramref name="increment"/> and reports whether the
    /// result equals <paramref name="actual"/> without ever accepting an overflowed value.
    /// </summary>
    /// <remarks>
    /// Unchecked integral arithmetic that wraps across a type's bound moves the result in the
    /// opposite direction of a non-zero increment (a positive increment yields a smaller value,
    /// and vice versa); that direction flip is detected and reported as a mismatch instead of
    /// comparing against the wrapped value. Arithmetic that throws on overflow (e.g. <see cref="decimal"/>)
    /// is likewise treated as a mismatch: a sequence that would have to cross the type's bounds
    /// cannot be a valid increment chain at that point.
    /// </remarks>
    private static bool TryAdvance<TNumber>(TNumber previous, TNumber increment, TNumber actual)
        where TNumber : INumber<TNumber>
    {
        TNumber expected;

        try
        {
            expected = previous + increment;
        }
        catch (OverflowException)
        {
            return false;
        }

        if (increment > TNumber.Zero)
        {
            if (expected < previous)
                return false;
        }
        else if (increment < TNumber.Zero)
        {
            if (expected > previous)
                return false;
        }

        return expected == actual;
    }
}