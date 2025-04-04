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
using System.Collections;

using AlastairLundy.Resyslib.Collections.Generics.ArrayLists;

namespace AlastairLundy.DotExtensions.Resyslib.GenericArrayLists
{
    public static class ToGenericArrayListExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrayList"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static GenericArrayList<T> ToGenericArrayList<T>(this ArrayList arrayList)
        {
            if (typeof(T) != arrayList.GetType())
            {
                throw new ArgumentException(
                    $"Type specified of {typeof(T)} does not match array list of type {arrayList.GetType()}.");
            }

            GenericArrayList<T> output = new();

            foreach (object obj in arrayList)
            {
                if (obj is T t)
                {
                    output.Add(t);
                }
            }

            return output;
        }

    }
}