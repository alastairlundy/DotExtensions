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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using AlastairLundy.DotExtensions.Localizations;

namespace AlastairLundy.DotExtensions.Collections.Concurrent
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConcurrentRangeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="concurrentBag"></param>
        /// <param name="items"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddRange<T>(this ConcurrentBag<T> concurrentBag, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                concurrentBag.Add(item);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="items"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="InvalidOperationException"></exception>
        public static void AddRange<T>(this IProducerConsumerCollection<T> collection, ICollection<T> items)
        public static bool TryAddRange<T>(this IProducerConsumerCollection<T> collection, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                bool result = collection.TryAdd(item);

                if (result == false)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="concurrentBag"></param>
        /// <param name="items"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ConcurrentBag<T> RemoveRange<T>(this ConcurrentBag<T> concurrentBag,
            IEnumerable<T> itemsToRemove)
        {
            IEnumerable<T> newCollection = from item in concurrentBag
                where itemsToRemove.Contains(item) == false
                select item;
            
            return new ConcurrentBag<T>(newCollection);
        }

        /// <summary>
        /// 
        /// </summary>
        {
            ConcurrentBag<T> output = new ConcurrentBag<T>();

            int limit = startIndex + count;

            if (limit > concurrentBag.Count)
            {
                throw new ArgumentException(Resources.Exceptions_Enumerables_CountArgumentTooLarge);
            }

            if (startIndex < 0 || startIndex >= concurrentBag.Count && startIndex != 0 ||
                startIndex > concurrentBag.Count)
            {
                throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                    .Replace("{x}", $"{startIndex}")
                    .Replace("{y}", $"0")
                    .Replace("{z}", $"{limit}"));
            }

            if (count < 0)
            {
                //TODO: Add CountOutOfRange in the future
            }
            
            int actualIndex = 0;
            foreach (T item in concurrentBag)
            {
                if (actualIndex < startIndex || actualIndex > limit)
                {
                    output.Add(item);
                }
                actualIndex++;
            }
           
            return output;
        }

        /// <summary>
        /// 
        public static IProducerConsumerCollection<T> RemoveRange<T>(this IProducerConsumerCollection<T> collection,
            IEnumerable<T> itemsToRemove)
        {
            IEnumerable<T> newCollection = from item in collection
                where itemsToRemove.Contains(item) == false
                select item;
            
            return new ConcurrentBag<T>(newCollection);
        }
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="items"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IProducerConsumerCollection<T> RemoveRange<T>(this IProducerConsumerCollection<T> collection,
            int startIndex, int count)
        {
            ConcurrentBag<T> output = new ConcurrentBag<T>();

            int limit = startIndex + count;

            if (limit > collection.Count)
            {
                throw new ArgumentException(Resources.Exceptions_Enumerables_CountArgumentTooLarge);
            }

            if (startIndex < 0 || startIndex >= collection.Count && startIndex != 0 ||
                startIndex > collection.Count)
            {
                throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                    .Replace("{x}", $"{startIndex}")
                    .Replace("{y}", $"0")
                    .Replace("{z}", $"{limit}"));
            }

            if (count < 0)
            {
                //TODO: Add CountOutOfRange in the future
            }
            
            int actualIndex = 0;
            foreach (T item in collection)
            {
                if (items.Contains(item) == false)
                {
                    output.Add(item);
                }
            }
           
            return output;
        }


        public static IProducerConsumerCollection<T> GetRange<T>(this IProducerConsumerCollection<T> collection,
            int startIndex, int count)
        {
            ConcurrentBag<T> output = new ConcurrentBag<T>();

            int limit = startIndex + count;

            if (limit > collection.Count)
            {
                throw new ArgumentException(Resources.Exceptions_Enumerables_CountArgumentTooLarge);
            }

            if (startIndex < 0 || startIndex >= collection.Count && startIndex != 0 ||
                startIndex > collection.Count)
            {
                throw new IndexOutOfRangeException(Resources.Exceptions_IndexOutOfRange
                    .Replace("{x}", $"{startIndex}")
                    .Replace("{y}", $"0")
                    .Replace("{z}", $"{limit}"));
            }

            if (count < 0)
            {
                //TODO: Add CountOutOfRange in the future
            }
            
            
            int actualIndex = 0;
            foreach (T item in collection)
            {
                if (actualIndex >= startIndex || actualIndex <= limit)
                {
                    output.Add(item);
                }

                if (actualIndex == limit)
                {
                    break;
                }

                actualIndex++;
            }
           
            return output;
        }

    }
}