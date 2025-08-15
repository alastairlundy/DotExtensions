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
using System.Numerics;

namespace AlastairLundy.DotExtensions.Numbers;

/// <summary>
/// 
/// </summary>
public static class ContinuallyIncrementedNumbers
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="expectedIncrement"></param>
    /// <typeparam name="TNumber"></typeparam>
    /// <returns></returns>
    public static bool IsContinuallyIncrementedRange<TNumber>(this IList<TNumber> source,
        TNumber expectedIncrement) where TNumber : INumber<TNumber>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="expectedIncrement"></param>
    /// <typeparam name="TNumber"></typeparam>
    /// <returns></returns>
    public static bool IsContinuallyIncrementedRange<TNumber>(this IEnumerable<TNumber> source, TNumber expectedIncrement) where TNumber : INumber<TNumber>
    {
        ArgumentNullException.ThrowIfNull(source);

        if (source is IList<TNumber> list)
            return IsContinuallyIncrementedRange(list, expectedIncrement);
        
        bool foundFirstNumber = false;
        
        TNumber expectedNumber = TNumber.Zero;
        
        foreach (TNumber actual in source)
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