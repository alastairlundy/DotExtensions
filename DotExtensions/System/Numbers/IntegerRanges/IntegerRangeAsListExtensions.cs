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

using AlastairLundy.DotExtensions.Localizations;

namespace AlastairLundy.DotExtensions.Numbers;

/// <summary>
/// 
/// </summary>
public static class IntegerRangeAsListExtensions
{
    
    /// <summary>
    /// Generates a list of unsigned short (ushort) values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the list.</param>
    /// <param name="count">The number of values to generate in the list.</param>
    /// <returns>An <see cref="IList{T}"/> containing the generated list of unsigned short values,
    /// incremented by 1 from the starting point.</returns>
    /// <exception cref="ArgumentException">Thrown when the count + startIndex are greater than ushort.MaxValue </exception>
    public static IList<ushort> RangeAsIList(this ushort startIndex, ushort count)
    {
        if (startIndex + count > ushort.MaxValue)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        }
        
        List<ushort> list = new List<ushort>();

        for (ushort i = startIndex; i < count; i++)
        {
            list.Add(i);
        }
        
        return list;
    }
    
    /// <summary>
    /// Generates a list of short (short) values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the list.</param>
    /// <param name="count">The number of values to generate in the list.</param>
    /// <returns>An <see cref="IList{T}"/> containing the generated list of short values,
    /// incremented by 1 from the starting point.</returns>
    /// <exception cref="ArgumentException">Thrown if the count is less than zero.</exception>
    public static IList<short> RangeAsIList(this short startIndex, short count)
    {
        if (startIndex + count > short.MaxValue)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero);
        }
        if (count < 0)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero
                .Replace("{x}", $"{count}"));
        }
        
        List<short> list = new List<short>();

        for (short i = startIndex; i < count; i++)
        {
            list.Add(i);
        }
        
        return list;
    }
    
    /// <summary>
    /// Returns an enumerable list of integers from <paramref name="startIndex"/> up to start + count.
    /// </summary>
    /// <param name="startIndex">The starting integer of the list.</param>
    /// <param name="count">The number of integers to generate.</param>
    /// <returns>An IList list of integers from the start index up to count.</returns>
    /// <exception cref="ArgumentException">Thrown if the count is less than zero.</exception>
    public static IList<int> RangeAsIList(this int startIndex, int count)
    {
        if (count < 0)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero
                .Replace("{x}", $"{count}"));
        }
        
        List<int> list = new List<int>();

        for (int i = startIndex; i < count; i++)
        {
            list.Add(i);
        }
        
        return list;
    }
    
    /// <summary>
    /// Generates a list of long values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the list.</param>
    /// <param name="count">The number of values to generate in the list.</param>
    /// <returns>An <see cref="IList{T}"/> containing the generated list of long values,
    /// incremented by 1 from the starting point.</returns>
    /// <exception cref="ArgumentException">Thrown when the count is less than zero.</exception>
    public static IList<long> RangeAsIList(this long startIndex, long count)
    {
        if (count < 0)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero
                .Replace("{x}", $"{count}"));
        }
        
        List<long> list = new List<long>();

        for (long i = startIndex; i < count; i++)
        {
            list.Add(i);
        }
        
        return list;
    }
    

    /// <summary>
    /// Generates a list of unsigned integer (uint) values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the list.</param>
    /// <param name="count">The number of values to generate in the list.</param>
    /// <returns>An <see cref="IList{T}"/> containing the generated list of unsigned integer values,
    /// incremented by 1 from the starting point.</returns>
    public static IList<uint> RangeAsIList(this uint startIndex, uint count)
    {
        List<uint> list = new List<uint>();
        
        for (uint i = startIndex; i < count; i++)
        {
            list.Add(i);
        }
        
        return list;
    }
    


    /// <summary>
    /// Generates a list of unsigned long (ulong) values starting from a specified value and continuing for a specified count,
    /// with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the list.</param>
    /// <param name="count">The number of values to generate in the list.</param>
    /// <returns>An <see cref="IList{T}"/> containing the generated list of unsigned long values,
    /// incremented by 1 from the starting point.</returns>
    public static IList<ulong> RangeAsIList(this ulong startIndex, ulong count)
    {
        List<ulong> list = new List<ulong>();

        for (ulong i = startIndex; i < count; i++)
        {
            list.Add(i);
        }
        
        return list;
    }

    /// <summary>
    /// Returns an enumerable list of integers from <paramref name="startIndex"/> up to start + count.
    /// </summary>
    /// <param name="startIndex">The starting integer of the list.</param>
    /// <param name="count">The number of integers to generate.</param>
    /// <param name="numbersToSkip">The numbers to skip from the range.</param>
    /// <returns>An IList list of integers from the start index up to count.</returns>
    /// <exception cref="ArgumentException">Thrown if the count is less than zero.</exception>
    public static IList<int> RangeAsIList(this int startIndex, int count, IList<int> numbersToSkip)
    {
        if (count < 0)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero
                .Replace("{x}", $"{count}"));
        }
        
        List<int> list = new List<int>();
        
        for (int i = startIndex; i < count; i++)
        {
            if (numbersToSkip.Contains(i) == false)
            {
                list.Add(i);
            }
        }
        
        return list;
    }
    

    /// <summary>
    /// Generates a list of long values starting from a specified value and continuing for a specified count,
    /// while skipping specified numbers in the list, with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the list.</param>
    /// <param name="count">The number of values to generate in the list.</param>
    /// <param name="numbersToSkip">An <see cref="IList{T}"/> of long values to skip in the generated list.</param>
    /// <returns>An <see cref="IList{T}"/> containing the generated list of long values,
    /// excluding the skipped numbers.</returns>
    /// <exception cref="ArgumentException">Thrown if the count is less than zero.</exception>
    public static IList<long> RangeAsIList(this long startIndex, long count, IList<long> numbersToSkip)
    {
        if (count < 0)
        {
            throw new ArgumentException(Resources.Exceptions_Count_LessThanZero
                .Replace("{x}", $"{count}"));
        }
        
        List<long> list = new List<long>();

        for (long i = startIndex; i < count; i++)
        {
            if (numbersToSkip.Contains(i) == false)
            {
                list.Add(i);
            }
        }
        
        return list;
    }
    

    /// <summary>
    /// Generates a list of unsigned long (ulong) values starting from a specified value and continuing for a specified count,
    /// while skipping specified numbers in the list, with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the list.</param>
    /// <param name="count">The number of values to generate in the list.</param>
    /// <param name="numbersToSkip">An <see cref="IList{T}"/> of unsigned long values to skip in the generated list.</param>
    /// <returns>An <see cref="IList{T}"/> containing the generated list of unsigned long values,
    /// excluding the skipped numbers.</returns>
    public static IList<ulong> RangeAsIList(this ulong startIndex, ulong count, IList<ulong> numbersToSkip)
    {
        List<ulong> list = new List<ulong>();

        for (ulong i = startIndex; i < count; i++)
        {
            if (numbersToSkip.Contains(i) == false)
            {
                list.Add(i);
            }
        }
        
        return list;
    }
    
    
    /// <summary>
    /// Generates a list of unsigned integer (uint) values starting from a specified value and continuing for a specified count,
    /// while skipping specified numbers in the list, with each value incremented by 1 from the starting point.
    /// </summary>
    /// <param name="startIndex">The starting value of the list.</param>
    /// <param name="count">The number of values to generate in the list.</param>
    /// <param name="numbersToSkip">An <see cref="IList{T}"/> of unsigned integer values to skip in the generated list.</param>
    /// <returns>An <see cref="IList{T}"/> containing the generated list of unsigned integer values,
    /// excluding the skipped numbers.</returns>
    public static IList<uint> RangeAsIList(this uint startIndex, uint count, IList<uint> numbersToSkip)
    {
        List<uint> list = new List<uint>();

        for (uint i = startIndex; i < count; i++)
        {
            if (numbersToSkip.Contains(i) == false)
            {
                list.Add(i);
            }
        }
        
        return list;
    }
}