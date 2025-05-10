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

namespace AlastairLundy.DotExtensions.Collections.Generic.Enumerables
{
    public static class EnumerableSplitExtensions
    {
    
        /// <summary>
        /// Splits an IEnumerable into an IEnumerable of IEnumerables by the CPU thread count.
        /// </summary>
        /// <param name="source">The IEnumerable to be split.</param>
        /// <typeparam name="T">The type of item stored in the source IEnumerable.</typeparam>
        /// <returns>An IEnumerable of IEnumerables split by the number of threads the CPU has.</returns>
        // TODO: Rename to SplitByProcessorCount in V7
        public static IEnumerable<IEnumerable<T>> SplitByProcessorCount<T>(this IEnumerable<T> source)
        {
            IList<T> array = source as IList<T> ?? source.ToArray();
        
            if (array.Count == 0)
            {
                throw new ArgumentException(Resources.Exceptions_EnumerablesSplit_Empty);
            }
        
            double itemsPerThread = array.Count / Convert.ToDouble(Environment.ProcessorCount);
        
            int enumerableLimit = Convert.ToInt32(Math.Round(itemsPerThread, MidpointRounding.AwayFromZero));

            return SplitByCount(array, enumerableLimit);
        }

        /// <summary>
        /// Splits an IEnumerable based on the maximum number of items allowed in each IEnumerable. 
        /// </summary>
        /// <param name="source">The IEnumerable to be split.</param>
        /// <param name="maxCount">The number of items allowed in each IEnumerable.</param>
        /// <typeparam name="T">The type of item stored in the source IEnumerable.</typeparam>
        /// <returns>An IEnumerable of IEnumerables split by the maximum number of items allowed in each IEnumerable.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IEnumerable<IEnumerable<T>> SplitByCount<T>(this IEnumerable<T> source, int maxCount)
        {
            List<List<T>> outputList = new List<List<T>>();
        
            IList<T> items = source as IList<T> ?? source.ToArray();

            if (items.Count == 0)
            {
                throw new ArgumentException(Resources.Exceptions_EnumerablesSplit_Empty);
            }
        
            int currentEnumerableCount = 0;

            List<T> currentList = new List<T>();
        
            foreach (T item in items)
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
}