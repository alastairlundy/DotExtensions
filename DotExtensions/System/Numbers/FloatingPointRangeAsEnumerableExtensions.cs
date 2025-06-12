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

namespace AlastairLundy.DotExtensions.Numbers;

/// <summary>
/// 
/// </summary>
public static class FloatingPointRangeAsEnumerableExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static IEnumerable<double> RangeAsEnumerable(this double start, double count)
    {
        List<double> list = new List<double>();

        for (double i = start; i < count; i += 1.0 )
        {
            list.Add(i);
        }
        
        return list;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static IEnumerable<float> RangeAsEnumerable(this float start, float count)
    {
        List<float> list = new List<float>();

        for (float i = start; i < count; i += 1.0F )
        {
            list.Add(i);
        }
        
        return list;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static IEnumerable<decimal> RangeAsEnumerable(this decimal start, decimal count)
    {
        List<decimal> list = new List<decimal>();

        for (decimal i = start; i < count; i += decimal.One )
        {
            list.Add(i);
        }
        
        return list;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="count"></param>
    /// <param name="numbersToSkip"></param>
    /// <returns></returns>
    public static IEnumerable<double> RangeAsEnumerable(this double start, double count, IEnumerable<double> numbersToSkip)
    {
        List<double> list = new List<double>();
        IList<double> numbersToSkipList = numbersToSkip as IList<double> ?? numbersToSkip.ToList();

        for (double i = start; i < count; i += 1.0)
        {
            if (numbersToSkipList.Contains(i) == false)
            {
                list.Add(i);
            }
        }
        
        return list;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="count"></param>
    /// <param name="numbersToSkip"></param>
    /// <returns></returns>
    public static IEnumerable<float> RangeAsEnumerable(this float start, float count, IEnumerable<float> numbersToSkip)
    {
        List<float> list = new List<float>();
        IList<float> numbersToSkipList = numbersToSkip as IList<float> ?? numbersToSkip.ToList();

        for (float i = start; i < count; i += 1.0F)
        {
            if (numbersToSkipList.Contains(i) == false)
            {
                list.Add(i);
            }
        }
        
        return list;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="count"></param>
    /// <param name="numbersToSkip"></param>
    /// <returns></returns>
    public static IEnumerable<decimal> RangeAsEnumerable(this decimal start, decimal count, IEnumerable<decimal> numbersToSkip)
    {
        List<decimal> list = new List<decimal>();
        IList<decimal> numbersToSkipList = numbersToSkip as IList<decimal> ?? numbersToSkip.ToList();

        for (decimal i = start; i < count; i += decimal.One)
        {
            if (numbersToSkipList.Contains(i) == false)
            {
                list.Add(i);
            }
        }
        
        return list;
    }
}