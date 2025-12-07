/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using System.Collections.Generic;

namespace AlastairLundy.DotExtensions.Memory;

/// <summary>
/// Provides extension methods for converting spans and memory to lists or other types.
/// </summary>
public static class ToListExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(Span<T> source)
    {
        /// <summary>
        /// Converts this <see cref="Span{T}"/> to a <see cref="List{T}"/>.
        /// </summary>
        /// <returns>A list containing the elements of the span.</returns>
        public List<T> ToList()
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(source);
            List<T> list = new List<T>();

            foreach (T item in source)
            {
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// Converts this <see cref="Span{T}"/> to a <see cref="Memory{T}"/>
        /// </summary>
        /// <returns>A <see cref="Memory{T}"/> containing all the elements of the span.</returns>
        public Memory<T> ToMemory() => new(source.ToArray());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T">The type of elements in the Memory.</typeparam>
    extension<T>(Memory<T> source)
    {
        /// <summary>
        /// Converts this <see cref="Memory{T}"/> to a <see cref="List{T}"/>
        /// </summary>
        /// <returns>A list containing the elements of the Memory.</returns>
        public List<T> ToList()
        {
            InvalidOperationException.ThrowIfMemoryIsEmpty(source);
            List<T> list = new List<T>();

            foreach (T item in source.Span)
            {
                list.Add(item);
            }

            return list;
        }
    }
}
