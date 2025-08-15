using System.Collections.Generic;
using System.Numerics;

namespace AlastairLundy.DotExtensions.Collections;

public static class CollectionCount
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static TNumber Count<TNumber, TSource>(this IEnumerable<TSource> source) where TNumber : INumber<TNumber>
    {
        TNumber count = TNumber.Zero;

        foreach (TSource item in source)
        {
            count += TNumber.One;
        }

        return count;
    }
}