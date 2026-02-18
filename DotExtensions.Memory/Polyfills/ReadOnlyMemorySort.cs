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

namespace DotExtensions.Memory;

/// <summary>
/// Provides extension methods for sorting operations on <see cref="ReadOnlyMemory{T}"/> and <see cref="ReadOnlySpan{T}"/>.
/// This class contains utilities to perform sorting directly on <see cref="ReadOnlyMemory{T}"/> and <see cref="ReadOnlySpan{T}"/> instances
/// without converting them to other data structures.
/// </summary>
public static class ReadOnlyMemorySort
{
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
        /// Sorts the elements within a <see cref="ReadOnlySpan{T}"/> based on the associated keys using the specified comparer.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys used for sorting.</typeparam>
        /// <typeparam name="TValue">The type of the values to be sorted.</typeparam>
        /// <typeparam name="TComparer">The type of the comparer used for comparing the keys.</typeparam>
        /// <param name="values">The <see cref="ReadOnlySpan{T}"/> of values to be sorted in accordance with the order of the keys.</param>
        /// <param name="comparer">The comparer to use for comparing the keys while sorting.</param>
        /// <exception cref="ArgumentNullException">Thrown when the specified <see cref="TComparer"/> is null.</exception>
        public void Sort<TComparer>(ref ReadOnlySpan<TValue> values,
            TComparer comparer)
            where TComparer : IComparer<TKey>
        {
            ArgumentNullException.ThrowIfNull(comparer);

            if (keys.Length != values.Length)
                throw new ArgumentException();

            TKey[] keysArray = new TKey[keys.Length];
            TValue[] valsArray = new TValue[values.Length];
            
            keys.CopyTo(keysArray);
            values.CopyTo(valsArray);

            Array.Sort(keysArray, valsArray, comparer);

            keys = keysArray.AsSpan();
            values = valsArray.AsSpan();
        }
    }
    
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to sort.</param>
    /// <typeparam name="T">The type of element in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    extension<T>(ref ReadOnlySpan<T> source)
        where T : IComparable<T>
    {
        /// <summary>
        /// Sorts the elements within a <see cref="ReadOnlySpan{T}"/> using the default comparer.
        /// </summary>
        public void Sort()
            => source.Sort(Comparer<T>.Default);

        /// <summary>
        /// Sorts the elements within a <see cref="ReadOnlySpan{T}"/> using the specified comparer.
        /// </summary>
        /// <param name="comparer">The comparer to use to sort the <see cref="ReadOnlySpan{T}"/>.</param>
        /// <typeparam name="TComparer">The type of comparer to use to sort the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        public void Sort<TComparer>(TComparer comparer)
            where TComparer : IComparer<T>
        {
            ArgumentNullException.ThrowIfNull(comparer);
            T[] array = new T[source.Length];
            
            source.CopyTo(array);
            Array.Sort(array, comparer);

            source = array.AsSpan();
        }

        /// <summary>
        /// Sorts the elements within a <see cref="ReadOnlySpan{T}"/> using the specified <see cref="Comparison{T}"/> delegate.
        /// </summary>
        /// <param name="comparison">The <see cref="Comparison{T}"/> delegate to use to sort the <see cref="ReadOnlySpan{T}"/>.</param>
        public void Sort(Comparison<T> comparison)
        {
            ArgumentNullException.ThrowIfNull(comparison);
            T[] array = new T[source.Length];
            
            source.CopyTo(array);
            Array.Sort(array, comparison);

            source = array.AsSpan();
        }
    }
}