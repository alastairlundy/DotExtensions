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

namespace AlastairLundy.DotExtensions.Collections.Generic.Enumerables
{
    public static class EnumerableCombineExtensions
    {
        
        /// <summary>
        /// Combines an IEnumerable with another IEnumerable and returns the newly combined IEnumerable.
        /// </summary>
        /// <param name="source">The IEnumerable to be added to.</param>
        /// <param name="enumerableTwo">The IEnumerable to be added.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The newly created IEnumerable.</returns>
        /// <exception cref="OverflowException">Thrown if the size of the new IEnumerable is larger than the max size for IEnumerables.</exception>
        public static IEnumerable<T> Combine<T>(this IEnumerable<T> source, IEnumerable<T> enumerableTwo)
        {   
            T[] arrayOne = source as T[] ?? source.ToArray();
            T[] arrayTwo = enumerableTwo as T[] ?? enumerableTwo.ToArray();
            
#if NET6_0_OR_GREATER
            if (arrayOne.Length + arrayTwo.Length >= Array.MaxLength)
#else
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if((arrayOne.Length + arrayTwo.Length) >= int.MaxValue)
#endif
            {
                throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
            }
            
            T[] newArray = new T[arrayOne.Length + arrayTwo.Length];
            
            arrayOne.CopyTo(newArray, 0);
            arrayTwo.CopyTo(newArray, arrayOne.Length);

            return newArray;   
        }

        /// <summary>
        /// Combines an IEnumerable with another IEnumerable and provides the newly combined IEnumerable.
        /// </summary>
        /// <param name="source">The first IEnumerable to be added.</param>
        /// <param name="enumerableTwo">The second IEnumerable to be added.</param>
        /// <param name="destinationEnumerable">The combined IEnumerable.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        public static void Combine<T>(this IEnumerable<T> source, IEnumerable<T> enumerableTwo, out IEnumerable<T> destinationEnumerable)
        {
            destinationEnumerable = source.Combine(enumerableTwo);
        }

        /// <summary>
        /// Attempts to combine an IEnumerable with another IEnumerable and provides the newly combined IEnumerable.
        /// Returns whether combining the IEnumerables was successful or not.
        /// </summary>
        /// <param name="source">The first IEnumerable to be added.</param>
        /// <param name="enumerableTwo">The second IEnumerable to be added.</param>
        /// <param name="destinationEnumerable">The combined IEnumerable if successful; is null otherwise.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>True if combining the IEnumerables was successful; returns false otherwise.</returns>
        public static bool TryCombine<T>(this IEnumerable<T> source, IEnumerable<T> enumerableTwo, out IEnumerable<T>? destinationEnumerable)
        {
            try
            {
                destinationEnumerable = source.Combine(enumerableTwo);
                return true;
            }
            catch
            {
                destinationEnumerable = null;
                return false;
            }
        }
    }
}