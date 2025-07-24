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

using System.Collections.Generic;
using System.Linq;

using AlastairLundy.DotPrimitives.Collections;

namespace AlastairLundy.DotExtensions.DotPrimitives.Collections;

/// <summary>
/// 
/// </summary>
public static class FlexKeyValuePairEnumerableSplitExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pairs"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TKey> ToKeys<TKey, TValue>(this IEnumerable<FlexibleKeyValuePair<TKey, TValue>> pairs)
    {
        return (from pair in pairs
            select pair.Key);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pairs"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TValue> ToValues<TKey, TValue>(this IEnumerable<FlexibleKeyValuePair<TKey, TValue>> pairs)
    {
        return (from pair in pairs
                select pair.Value);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pairs"></param>
    /// <param name="keys"></param>
    /// <param name="values"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public static void ToSplitPairs<TKey, TValue>(this IEnumerable<FlexibleKeyValuePair<TKey, TValue>> pairs, out IEnumerable<TKey> keys, out IEnumerable<TValue> values)
    {
        List<TKey> outputKeys = new List<TKey>();
        List<TValue> outputValues = new List<TValue>();

        foreach (FlexibleKeyValuePair<TKey, TValue> pair in pairs)
        {
            outputKeys.Add(pair.Key);
            outputValues.Add(pair.Value);
        }
        
        keys = outputKeys;
        values = outputValues;
    }
}