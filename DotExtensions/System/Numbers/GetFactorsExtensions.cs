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

using AlastairLundy.DotExtensions.Numbers.Helpers.FloatingPointNumbers;
using AlastairLundy.DotExtensions.Numbers.Helpers.Integers;

// ReSharper disable RedundantBoolCompare

namespace AlastairLundy.DotExtensions.Numbers;

/// <summary>
/// 
/// </summary>
public static class GetFactorsExtensions
{
    #region IsFactor Private Methods
    private static bool IsFactor<TNumber>(Integer<TNumber> number, Integer<TNumber> factor)
    {
        try
        {
            return number.Modulo(factor).Value.Equals(number.Zero);
        }
        catch
        {
            return false;
        }
    }
    
    private static bool IsFactor<TNumber>(FloatingPointNumber<TNumber> number, FloatingPointNumber<TNumber> factor)
    {
        return number.Modulo(factor).Value.Equals(number.Zero);
    }
#endregion
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    /// <exception cref="DivideByZeroException"></exception>
    public static IEnumerable<int> GetFactors(this int number)
    {
        if (number == 0)
            throw new DivideByZeroException();
        
        int limit = number > 2 ? (number / 2) + 1 : 2;

        return number.RangeAsEnumerable(limit, [0])
            .Where(x => IsFactor<int>(Int32Integer.New(number),
                Int32Integer.New(x)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    /// <exception cref="DivideByZeroException"></exception>
    public static IEnumerable<uint> GetFactors(this uint number)
    {
        if (number == 0)
            throw new DivideByZeroException();
        
        uint limit = number > 2 ? (number / 2) + 1 : 2;

        return number.RangeAsEnumerable(limit, [0])
            .Where(x => IsFactor<uint>(new UInt32Integer(number),
                new UInt32Integer(x)));
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    /// <exception cref="DivideByZeroException"></exception>
    public static IEnumerable<long> GetFactors(this long number)
    {
        if (number == 0)
            throw new DivideByZeroException();
        
        long limit = number > 2 ? (number / 2) + 1 : 2;

        return number.RangeAsEnumerable(limit, [0])
            .Where(x => IsFactor<long>(new Int64Integer(number),
                new Int64Integer(x)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    /// <exception cref="DivideByZeroException"></exception>
    public static IEnumerable<ulong> GetFactors(this ulong number)
    {
        if (number == 0)
            throw new DivideByZeroException();
        
        ulong limit = number > 2 ? (number / 2) + 1 : 2;
        
        return number.RangeAsEnumerable(limit, [0])
            .Where(x =>  IsFactor<ulong>(new UInt64Integer(number),
                new UInt64Integer(x)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    /// <exception cref="DivideByZeroException"></exception>
    public static IEnumerable<double> GetFactors(this double number)
    {
        if (number == 0.0)
            throw new DivideByZeroException();
        
        double limit = number > 2 ? (number / 2) + 1 : 2;
        
        return number.RangeAsEnumerable(limit, [0])
            .Where(x =>  IsFactor<double>(new DoubleFloatingPointNumber(number),
                new DoubleFloatingPointNumber(x)));
    }

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    /// <exception cref="DivideByZeroException"></exception>
    public static IEnumerable<decimal> GetFactors(this decimal number)
    {
        if (number == 0)
            throw new DivideByZeroException();
        
        decimal limit = number > decimal.Parse("2") ? decimal.Add(decimal.Divide(number, decimal.Parse("2")), decimal.One)
                : decimal.Parse("2");

        return number.RangeAsEnumerable(limit, [0])
            .Where(x =>  IsFactor<decimal>(new DecimalFloatingPointNumber(number),
                new DecimalFloatingPointNumber(x)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    /// <exception cref="DivideByZeroException"></exception>
    public static IEnumerable<float> GetFactors(this float number)
    {
        if (number == 0.0)
            throw new DivideByZeroException();
        
        float limit = number > 2 ? (number / 2) + 1 : 2;

        return number.RangeAsEnumerable(limit, [0])
            .Where(x =>  IsFactor<float>(new SingleFloatingPointNumber(number),
                new SingleFloatingPointNumber(x)));
    }
}