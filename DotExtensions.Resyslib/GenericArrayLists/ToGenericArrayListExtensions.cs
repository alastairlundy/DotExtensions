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
using System.Collections.Generic;

using AlastairLundy.Resyslib.Collections.Generics.ArrayLists;
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable MemberCanBePrivate.Global

namespace AlastairLundy.DotExtensions.Resyslib.GenericArrayLists
{
    public static class ToGenericArrayListExtensions
    {
        /// <summary>
        /// Converts an ArrayList to a GenericArrayList that supports generics.
        /// </summary>
        /// <param name="arrayList">The arraylist to convert.</param>
        /// <typeparam name="T">The type of Type the ArrayList stores.</typeparam>
        /// <returns>A new GenericArrayList of type T with the items from the ArrayList.</returns>
        /// <exception cref="ArgumentException">Thrown if the type specified is not the type stored in the ArrayList.</exception>
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

        /// <summary>
        /// Converts an ArrayList to an IGenericArrayList that supports generics.
        /// </summary>
        /// <param name="arrayList">The arraylist to convert.</param>
        /// <typeparam name="T">The type of Type the ArrayList stores.</typeparam>
        /// <returns>A new IGenericArrayList of type T with the items from the ArrayList.</returns>
        public static IGenericArrayList<T> ToIGenericArrayList<T>(this ArrayList arrayList)
        {
            return ToGenericArrayList<T>(arrayList);
        }
    }
}