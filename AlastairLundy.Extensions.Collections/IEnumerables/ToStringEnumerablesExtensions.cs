﻿/*
        MIT License
       
       Copyright (c) 2024 Alastair Lundy
       
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

namespace AlastairLundy.Extensions.Collections.IEnumerables;

    public static class ToStringEnumerablesExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IEnumerable<string> ToStringEnumerable<T>(this IEnumerable<T> enumerable)
        {
            if (typeof(T).GetMethod("ToString")?.DeclaringType != typeof(object) && typeof(T) != typeof(object))
            {
                List<string> list = new List<string>();

                foreach (T item in enumerable)
                {
                    if (item?.GetType() != typeof(object))
                    {
                        list.Add(item?.ToString()!);
                    }
                }

                return list.ToArray();
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            {
                throw new ArgumentException();
            }
        }
    }