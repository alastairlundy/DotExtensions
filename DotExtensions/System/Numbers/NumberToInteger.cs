using System;
using System.Numerics;

namespace AlastairLundy.DotExtensions.Numbers;

/// <summary>
/// 
/// </summary>
public static class NumberToInteger
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <typeparam name="TNumber"></typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static int ToInt32<TNumber>(this TNumber number) where TNumber : INumber<TNumber>
    {
        int result = int.Parse(number.ToString() ?? throw new InvalidOperationException());
        
        return result;
    }
}