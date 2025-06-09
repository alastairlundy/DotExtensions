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

#if NET5_0_OR_GREATER || NETSTANDARD2_1
using System;
#endif
using System.Collections.Generic;

namespace AlastairLundy.DotExtensions.Numbers;

/// <summary>
/// Provides static extension methods for working with number ranges.
/// </summary>
public static class NumberRangeExtensions
{
    /// <summary>
    /// Returns an enumerable sequence of integers from <paramref name="start"/> up to start + count.
    /// </summary>
    /// <param name="start">The starting integer of the sequence.</param>
    /// <param name="count">The number of integers to generate.</param>
    /// <returns>An IEnumerable sequence of integers from the start index up to count.</returns>
    public static IEnumerable<int> IntegerRangeAsEnumerable(this int start, int count)
    {
        List<int> list = new List<int>();

        for (int i = start; i < count; i++)
        {
            list.Add(i);
        }
        
        return list;
    }

#if NETSTANDARD2_1 || NET5_0_OR_GREATER
    /// <summary>
    /// Creates a new instance of the Range struct based on the given start value and length.
    /// </summary>
    /// <param name="start">The starting index or integer value of the range.</param>
    /// <param name="count">The number of elements in the range.</param>
    /// <returns>A new Range instance representing the specified sequence of values.</returns>
    public static Range AsRange(this int start, int count)
    {
        return new Range(start, count);
    }
    
    /// <summary>
    /// Creates a new instance of the Range struct based on the given Index value and length.
    /// </summary>
    /// <param name="start">The starting Index value.</param>
    /// <param name="count">The number of elements in the range.</param>
    /// <returns>A new Range instance representing the specified sequence of values.</returns>
    public static Range AsRange(this Index start, int count)
    {
        return new Range(start, count);
    }
#endif
}