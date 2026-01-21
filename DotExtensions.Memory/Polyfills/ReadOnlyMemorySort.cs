/*
        MIT License

       Copyright (c) 2026 Alastair Lundy

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

using System.Collections.Generic;

namespace DotExtensions.Memory;

/// <summary>
/// Provides extension methods for sorting operations on <see cref="ReadOnlyMemory{T}"/> and <see cref="ReadOnlySpan{T}"/>.
/// This class contains utilities to perform sorting directly on <see cref="ReadOnlyMemory{T}"/> and <see cref="ReadOnlySpan{T}"/> instances
/// without converting them to other data structures.
/// </summary>
public static class ReadOnlyMemorySort
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keys">The read-only span containing the keys to sort by.</param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    extension<TKey, TValue>(ref ReadOnlySpan<TKey> keys)
        where TKey : IComparable<TKey>
        where TValue : IComparable<TValue>
    {
        /// <summary>
        /// Sorts the elements within the span based on the associated keys using the default comparer.
        /// </summary>
        /// <param name="values">The read-only span of values to be sorted in accordance with the order of the keys.</param>
        public void Sort(ref ReadOnlySpan<TValue> values)
            => keys.Sort(ref values, Comparer<TKey>.Default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="TComparer"></typeparam>
        /// <exception cref="ArgumentException"></exception>
        public void Sort<TComparer>(ref ReadOnlySpan<TValue> values, 
            TComparer comparer)
            where TComparer : IComparer<TKey>
        {
            ArgumentNullException.ThrowIfNull(comparer);

            if(keys.Length != values.Length)
                throw new ArgumentException();

            TKey[] keysArray = ArrayPool<TKey>.Shared.Rent(keys.Length);
            TValue[] valsArray = ArrayPool<TValue>.Shared.Rent(values.Length);

            try
            {
                keys.CopyTo(keysArray);
                values.CopyTo(valsArray);

                Array.Sort(keysArray, valsArray, comparer);

                keys =  keysArray.AsSpan(0, keys.Length);
                values = valsArray.AsSpan(0, values.Length);
            }
            finally
            {
                ArrayPool<TKey>.Shared.Return(keysArray);
                ArrayPool<TValue>.Shared.Return(valsArray);
            }
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    extension<T>(ref ReadOnlySpan<T> source)
        where T: IComparable<T>
    {
        /// <summary>
        /// Sorts the elements within the span using the default comparer.
        /// </summary>
        public void Sort()
            => source.Sort(Comparer<T>.Default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparer"></param>
        /// <typeparam name="TComparer"></typeparam>
        public void Sort<TComparer>(TComparer comparer)
            where TComparer : IComparer<T>
        {
            ArgumentNullException.ThrowIfNull(comparer);
            T[] array = ArrayPool<T>.Shared.Rent(source.Length);
            
            try
            {
                source.CopyTo(array);
                Array.Sort(array, comparer);
                
                source = array.AsSpan(0, source.Length);
            }
            finally
            {
                ArrayPool<T>.Shared.Return(array);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparison"></param>
        public void Sort(Comparison<T> comparison)
        {
            ArgumentNullException.ThrowIfNull(comparison);
            T[] array = ArrayPool<T>.Shared.Rent(source.Length);
            
            try
            {
                source.CopyTo(array);
                Array.Sort(array, comparison);
                
                source = array.AsSpan(0, source.Length);
            }
            finally
            {
                ArrayPool<T>.Shared.Return(array);
            }
        }
    }
}