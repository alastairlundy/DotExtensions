/*
 * Copyright (c) 2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.Memory.Spans;

/// <summary>
/// Provides extension methods for resizing spans in a safe and efficient manner.
/// </summary>
public static class SpanResizeExtensions
{
    /// <param name="target">The span to be resized.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(ref Span<T> target)
    {
        /// <summary>
        /// Resizes the span to the specified new size and optimistically copies the elements of the old span to the new Span.
        /// </summary>
        /// <param name="newSize">The desired new size of the span.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the new size is less than or equal to zero.</exception>
        public void Resize(int newSize)
        {
            InvalidOperationException.ThrowIfSpanIsEmpty(target);

            if (newSize < target.Length)
            {
                target = target.Slice(0, newSize);
                return;
            }

            T[] newTargetArray = new T[newSize];
            Span<T> destination = new(newTargetArray);

            int endCopy = target.Length < newSize ? target.Length : newSize;

            target.OptimisticCopy(ref destination, 0, endCopy);

            target = destination;
        }
    }
}
