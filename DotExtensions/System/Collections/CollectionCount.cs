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

using System.Collections.Generic;
using System.Linq;

#if NET8_0_OR_GREATER
using System.Numerics;
#endif

using AlastairLundy.DotExtensions.Numbers;

namespace AlastairLundy.DotExtensions.Collections;

/// <summary>
/// Provides extension methods to get the count of a collection or sequence as a specified numeric type.
/// </summary>
public static class CollectionCount
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Determines the number of elements in a <see cref="ICollection{T}"/> and returns it as a <see cref="TNumber"/>.
    /// </summary>
    /// <param name="source">The collection to be checked.</param>
    /// <typeparam name="TNumber">The type of number to represent the count of <see cref="ICollection{T}"/>.</typeparam>
    /// <typeparam name="TSource">The type of elements in the collection.</typeparam>
    /// <returns>The number of elements in the <see cref="ICollection{T}"/> as a <see cref="TNumber"/> number.</returns>
    public static TNumber Count<TNumber, TSource>(this ICollection<TSource> source) where TNumber : INumber<TNumber>
        => source.Count.ToDestinationNumber<int, TNumber>();

    /// <summary>
    /// Determines the number of elements in a <see cref="IEnumerable{T}"/> and returns it as a <see cref="TNumber"/>.
    /// </summary>
    /// <remarks>This method uses LINQ's Count method if the source type is <see cref="IEnumerable{T}"/>.</remarks>
    /// <param name="source">The sequence to be checked.</param>
    /// <typeparam name="TNumber">The type of number to represent the count of <see cref="IEnumerable{T}"/>.</typeparam>
    /// <typeparam name="TSource">The type of elements in the sequence.</typeparam>
    /// <returns>The number of elements in the <see cref="IEnumerable{T}"/> as a <see cref="TNumber"/> number.</returns>
    public static TNumber Count<TNumber, TSource>(this IEnumerable<TSource> source) where TNumber : INumber<TNumber>
    {
        // Faster code path if the source implements ICollection<T>
        if (source is ICollection<TSource> collection)
        {
            return Count<TNumber, TSource>(collection);
        }

        return source.Count().ToDestinationNumber<int, TNumber>();
    }
#endif
}