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

    /// <summary>
    /// 
    /// </summary>
    public static class DeDuplicateExtensions
    {
        /// <summary>
        /// Returns whether an IEnumerable contains duplicate instances of an object.
        /// </summary>
        /// <param name="enumerable">The IEnumerable to be searched.</param>
        /// <typeparam name="T">The type of objects in the IEnumerable.</typeparam>
        /// <returns>true if the IEnumerable contains duplicate objects; returns false otherwise.</returns>
        public static bool ContainsDuplicates<T>(this IEnumerable<T> enumerable)
        {
            Dictionary<T, int> frequency = enumerable.FrequencyOfAll();

            foreach (int frequencyValue in frequency.Values)
            {
                if (frequencyValue > 1)
                {
                    return true;
                }
            }

            return false;   
        }
        
        /// <summary>
        /// Returns an IEnumerable with duplicates removed.
        /// </summary>
        /// <param name="enumerable">The IEnumerable to be searched for duplicates.</param>
        /// <typeparam name="T">The type of objects in the IEnumerable.</typeparam>
        /// <returns>an IEnumerable with duplicate objects removed.</returns>
        /// <exception cref="NullReferenceException">Thrown if no objects exist in the IEnumerable.</exception>
        public static IEnumerable<T> DeDuplicate<T>(this IEnumerable<T> enumerable)
        {
            Dictionary<T, int> frequency = enumerable.FrequencyOfAll();

            if (frequency.Keys.Count == 0 || frequency.Keys.Count < 1)
            {
                throw new ArgumentException();
            }
            
            return frequency.Keys.ToArray();
        }

        /// <summary>
        /// Attempts to remove duplicates from an IEnumerable and returns whether it has succeeded or not.
        /// </summary>
        /// <param name="enumerableToBeDeDuplicated">The IEnumerable to be searched for duplicates.</param>
        /// <param name="destinationEnumerable">The IEnumerable with duplicates removed, if any duplicates were found.</param>
        /// <typeparam name="T">The type of objects in the IEnumerable.</typeparam>
        /// <returns>whether duplicates of an object were removed from the specified IEnumerable.</returns>
        public static bool TryDeDuplicate<T>(this IEnumerable<T> enumerableToBeDeDuplicated, out IEnumerable<T> destinationEnumerable)
        {
            T[] toBeDeDuplicated = enumerableToBeDeDuplicated as T[] ?? enumerableToBeDeDuplicated.ToArray();

            try
            {
                if (!ContainsDuplicates(toBeDeDuplicated))
                {
                    destinationEnumerable = toBeDeDuplicated;
                    return false;
                }

                destinationEnumerable = DeDuplicate(toBeDeDuplicated);
                return true;
            }
            catch
            {
                destinationEnumerable = toBeDeDuplicated;
                return false;
            }
        }
    }