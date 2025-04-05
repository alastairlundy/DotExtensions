/*
        MIT License
       
       Copyright (c) 2024-2025 Alastair Lundy
       
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
// ReSharper disable MemberCanBePrivate.Global

namespace AlastairLundy.DotExtensions.Collections.Generic.Enumerables;

public static class EnumerableSplitExtensions
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="enumerable"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<IEnumerable<T>> SplitByThreadCount<T>(this IEnumerable<T> enumerable)
    {
        T[] items = enumerable as T[] ?? enumerable.ToArray();

        if (items.Length == 0)
        {
            throw new ArgumentException(Resources.Exceptions_EnumerablesSplit_Empty);
        }
        
        double itemsPerThread = items.Length / Convert.ToDouble(Environment.ProcessorCount);
        
        int enumerableLimit = Convert.ToInt32(Math.Round(itemsPerThread, MidpointRounding.AwayFromZero));

        return SplitByCount(items, enumerableLimit);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enumerable"></param>
    /// <param name="maxCount"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<IEnumerable<T>> SplitByCount<T>(this IEnumerable<T> enumerable, int maxCount)
    {
        List<List<T>> outputList = new List<List<T>>();
        
        T[] items = enumerable as T[] ?? enumerable.ToArray();

        if (items.Length == 0)
        {
            throw new ArgumentException(Resources.Exceptions_EnumerablesSplit_Empty);
        }
        
        int currentEnumerable = 0;

        List<T> currentList = new List<T>();
        
        foreach (T item in items)
        {
            if (currentEnumerable < maxCount)
            {
                currentList.Add(item);
            }
            else if (currentEnumerable == maxCount)
            {
                outputList.Add(currentList);
                currentList.Clear();
                currentEnumerable = 0;
            }
        }

        return outputList;
    }
}