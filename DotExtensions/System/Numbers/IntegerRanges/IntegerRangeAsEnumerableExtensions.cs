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

using System;
using System.Collections.Generic;
using System.Linq;

using AlastairLundy.DotExtensions.Localizations;
// ReSharper disable ConvertClosureToMethodGroup

namespace AlastairLundy.DotExtensions.Numbers;

/// <summary>
/// 
/// </summary>
public static class IntegerRangeAsEnumerableExtensions
{

    /// <summary>
    /// Generates a sequence of unsigned short (ushort) values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the sequence.</param>
    /// <param name="count">The number of values to generate in the sequence.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing the generated sequence of unsigned short values,
    /// incremented by 1 from the starting point.</returns>
    /// <exception cref="ArgumentException">Thrown when the count + startIndex are greater than ushort.MaxValue </exception>
    public static IEnumerable<ushort> RangeAsEnumerable(this ushort startIndex, ushort count)
    {
        if (startIndex + count > ushort.MaxValue)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        }
        
        for (ushort i = startIndex; i < count; i++)
        {
            yield return i;
        }
    }
    
    /// <summary>
    /// Generates a sequence of short (short) values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="start">The starting value of the sequence.</param>
    /// <param name="count">The number of values to generate in the sequence.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing the generated sequence of short values,
    /// incremented by 1 from the starting point.</returns>
    /// <exception cref="ArgumentException">Thrown if the count is less than zero.</exception>
    public static IEnumerable<short> RangeAsEnumerable(this short start, short count)
    {
        if (start + count > short.MaxValue)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        }
        if (count < 0)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero
                .Replace("{x}", $"{count}"));
        }
        
        for (short i = start; i < count; i++)
        {
            yield return i;
        }
    }
    
    /// <summary>
    /// Returns an enumerable sequence of integers from <paramref name="start"/> up to start + count.
    /// </summary>
    /// <param name="start">The starting integer of the sequence.</param>
    /// <param name="count">The number of integers to generate.</param>
    /// <returns>An IEnumerable sequence of integers from the start index up to count.</returns>
    /// <exception cref="ArgumentException">Thrown if the count is less than zero.</exception>
    public static IEnumerable<int> RangeAsEnumerable(this int start, int count)
    {
        if (count < 0)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero
                .Replace("{x}", $"{count}"));
        }
        
        for (int i = start; i < count; i++)
        {
            yield return i;
        }
    }
    
    /// <summary>
    /// Generates a sequence of long values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="start">The starting value of the sequence.</param>
    /// <param name="count">The number of values to generate in the sequence.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing the generated sequence of long values,
    /// incremented by 1 from the starting point.</returns>
    /// <exception cref="ArgumentException">Thrown when the count is less than zero.</exception>
    public static IEnumerable<long> RangeAsEnumerable(this long start, long count)
    {
        if (count < 0)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero
                .Replace("{x}", $"{count}"));
        }
        
        for (long i = start; i < count; i++)
        {
            yield return i;
        }
    }
    

    /// <summary>
    /// Generates a sequence of unsigned integer (uint) values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="start">The starting value of the sequence.</param>
    /// <param name="count">The number of values to generate in the sequence.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing the generated sequence of unsigned integer values,
    /// incremented by 1 from the starting point.</returns>
    public static IEnumerable<uint> RangeAsEnumerable(this uint start, uint count)
    {
        for (uint i = start; i < count; i++)
        {
            yield return i;
        }
    }
    


    /// <summary>
    /// Generates a sequence of unsigned long (ulong) values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="start">The starting value of the sequence.</param>
    /// <param name="count">The number of values to generate in the sequence.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing the generated sequence of unsigned long values,
    /// incremented by 1 from the starting point.</returns>
    public static IEnumerable<ulong> RangeAsEnumerable(this ulong start, ulong count)
    {
        for (ulong i = start; i < count; i++)
        {
            yield return i;
        }
    }

    /// <summary>
    /// Returns an enumerable sequence of integers from <paramref name="start"/> up to start + count.
    /// </summary>
    /// <param name="start">The starting integer of the sequence.</param>
    /// <param name="count">The number of integers to generate.</param>
    /// <param name="numbersToSkip">The numbers to skip from the range.</param>
    /// <returns>An IEnumerable sequence of integers from the start index up to count.</returns>
    /// <exception cref="ArgumentException">Thrown if the count is less than zero.</exception>
    public static IEnumerable<int> RangeAsEnumerable(this int start, int count, IEnumerable<int> numbersToSkip)
    {
        return RangeAsEnumerable(start, count).SkipWhile(x => numbersToSkip.Contains(x));
    }
    

    /// <summary>
    /// Generates a sequence of long values starting from a specified value and continuing for a specified count,
    /// while skipping specified numbers in the sequence, with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="start">The starting value of the sequence.</param>
    /// <param name="count">The number of values to generate in the sequence.</param>
    /// <param name="numbersToSkip">An <see cref="IEnumerable{T}"/> of long values to skip in the generated sequence.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing the generated sequence of long values,
    /// excluding the skipped numbers.</returns>
    /// <exception cref="ArgumentException">Thrown if the count is less than zero.</exception>
    public static IEnumerable<long> RangeAsEnumerable(this long start, long count, IEnumerable<long> numbersToSkip)
    {
        return RangeAsEnumerable(start, count).SkipWhile(x => numbersToSkip.Contains(x));
    }
    

    /// <summary>
    /// Generates a sequence of unsigned long (ulong) values starting from a specified value and continuing for a specified count,
    /// while skipping specified numbers in the sequence, with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="start">The starting value of the sequence.</param>
    /// <param name="count">The number of values to generate in the sequence.</param>
    /// <param name="numbersToSkip">An <see cref="IEnumerable{T}"/> of unsigned long values to skip in the generated sequence.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing the generated sequence of unsigned long values,
    /// excluding the skipped numbers.</returns>
    public static IEnumerable<ulong> RangeAsEnumerable(this ulong start, ulong count, IEnumerable<ulong> numbersToSkip)
    {
        return RangeAsEnumerable(start, count).SkipWhile(x => numbersToSkip.Contains(x));
    }
    
    
    /// <summary>
    /// Generates a sequence of unsigned integer (uint) values starting from a specified value and continuing for a specified count,
    /// while skipping specified numbers in the sequence, with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="start">The starting value of the sequence.</param>
    /// <param name="count">The number of values to generate in the sequence.</param>
    /// <param name="numbersToSkip">An <see cref="IEnumerable{T}"/> of unsigned integer values to skip in the generated sequence.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing the generated sequence of unsigned integer values,
    /// excluding the skipped numbers.</returns>
    public static IEnumerable<uint> RangeAsEnumerable(this uint start, uint count, IEnumerable<uint> numbersToSkip)
    {
        return RangeAsEnumerable(start, count).SkipWhile(x => numbersToSkip.Contains(x));
    }
}