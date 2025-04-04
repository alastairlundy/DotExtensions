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

namespace AlastairLundy.DotExtensions.Memory.Spans
{
    public static class SpanLinqExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Span<T> Except<T>(this Span<T> first, Span<T> second) where T : IEquatable<T>?
        {
            List<T> list = new();
        
            foreach (T item in first)
            {
                if (second.Contains(item) == false)
                {
                    list.Add(item);
                }
            }

            foreach (T item in second)
            {
                if (first.Contains(item) == false)
                {
                    list.Add(item);
                }
            }
        
            return new Span<T>(list.ToArray());
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Span<T> Skip<T>(this Span<T> target, int count)
        {
            T[] array = new T[target.Length - count];
        
            for (int i = 0; i < target.Length; i++)
            {
                if ((i <= count) == false)
                {
                    array[i] = target[i];
                }
            }
        
            return new Span<T>(array);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Span<T> SkipLast<T>(this Span<T> target, int count)
        {
            T[] array = new T[target.Length - count];

            int limit = target.Length - count;

            for (int i = 0; i < limit; i++)
            {
                array[i] = target[i];
            }

            return new Span<T>(array);
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Span<T> SkipWhile<T>(this Span<T> target, Func<T, bool> predicate)
        {
            List<T> list = new();
        
            foreach (T item in target)
            {
                if (predicate.Invoke(item) == false)
                {
                    list.Add(item);
                }
            }
        
            return new Span<T>(list.ToArray());
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Span<T> Where<T>(this Span<T> target, Func<T, bool> predicate)
        {
            List<T> list = new();

            foreach (T item in target)
            {
                if (predicate.Invoke(item))
                {
                    list.Add(item);
                }
            }
        
            return new Span<T>(list.ToArray());
        } 
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Any<T>(this Span<T> target, Func<T, bool> predicate)
        {
            foreach (T item in target)
            {
                bool result = predicate.Invoke(item);

                if (result)
                {
                    return true;
                }
            }

            return false;
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool All<T>(this Span<T> target, Func<T, bool> predicate)
        {
            List<bool> results = new();

            foreach (T item in target)
            {
                results.Add(predicate.Invoke(item));
            }

            foreach (bool result in results)
            {
                if (result == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}