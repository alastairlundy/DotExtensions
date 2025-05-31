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
using AlastairLundy.DotExtensions.Collections.Generic.ICollections;
using AlastairLundy.DotExtensions.Collections.ILists;

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
        {
            foreach (T item in items)
            {
                bool result = collection.TryAdd(item);

                if (result == false)
                {
                    throw new InvalidOperationException("Could not add item to the collection.");
                }
            }
        }
    }
}