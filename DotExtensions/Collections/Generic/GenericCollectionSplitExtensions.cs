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

namespace AlastairLundy.DotExtensions.Collections.Generic;

public static class GenericCollectionSplitExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static ICollection<ICollection<T>> SplitByProcessorCount<T>(this ICollection<T> source)
    {
        if (source.Count == 0)
            throw new ArgumentException(Resources.Exceptions_EnumerablesSplit_Empty);
        
        double itemsPerThread = source.Count / Convert.ToDouble(Environment.ProcessorCount);
        
        int enumerableLimit = Convert.ToInt32(Math.Round(itemsPerThread, MidpointRounding.AwayFromZero));

        return SplitByCount(source, enumerableLimit);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IList<IList<T>> SplitByProcessorCount<T>(this IList<T> source)
    {
        if (source.Count == 0)
            throw new ArgumentException(Resources.Exceptions_EnumerablesSplit_Empty);
        
        double itemsPerThread = source.Count / Convert.ToDouble(Environment.ProcessorCount);
        
        int enumerableLimit = Convert.ToInt32(Math.Round(itemsPerThread, MidpointRounding.AwayFromZero));

        return SplitByCount(source, enumerableLimit);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="maxCount"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static ICollection<ICollection<T>> SplitByCount<T>(this ICollection<T> source, int maxCount)
    {
        if (source.Count == 0)
            throw new ArgumentException(Resources.Exceptions_EnumerablesSplit_Empty);

        if (source.Count <= maxCount)
            return [[..source]];
        
        int currentEnumerableCount = 0;
            
        ICollection<ICollection<T>> outputList = new List<ICollection<T>>();
        List<T> currentList = new List<T>();
        
        foreach (T item in source)
        {
            if (currentEnumerableCount < maxCount)
            {
                currentList.Add(item);
            }
            else if (currentEnumerableCount == maxCount)
            {
                outputList.Add(currentList);
                currentList.Clear();
                currentEnumerableCount = 0;
            }
        }

        return outputList;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="maxCount"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IList<IList<T>> SplitByCount<T>(this IList<T> source, int maxCount)
    {
        if (source.Count == 0)
            throw new ArgumentException(Resources.Exceptions_EnumerablesSplit_Empty);

        if (source.Count <= maxCount)
            return [[..source]];
        
        int currentEnumerableCount = 0;
            
        IList<IList<T>> outputList = new List<IList<T>>();
        List<T> currentList = new List<T>();
        
        foreach (T item in source)
        {
            if (currentEnumerableCount < maxCount)
            {
                currentList.Add(item);
            }
            else if (currentEnumerableCount == maxCount)
            {
                outputList.Add(currentList);
                currentList.Clear();
                currentEnumerableCount = 0;
            }
        }

        return outputList;
    }
}