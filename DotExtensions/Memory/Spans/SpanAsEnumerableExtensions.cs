

using System;
using System.Collections.Generic;

namespace AlastairLundy.DotExtensions.Memory.Spans;

public static class SpanAsEnumerableExtensions
{
    /// <summary>
    /// Converts a <see cref="Span{T}"/> to an <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <remarks>This method works by creating a new Array of the same length as the span, copies files to the array, and returns it as an IEnumerable.
    /// <para>Conversion from a <see cref="Span{T}"/> to an <see cref="IEnumerable{T}"/> should not be done if performance is a concern. Spans are faster to iterate over and work with than IEnumerables.</para>
    /// </remarks>
    /// <param name="span">The span to convert.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>An IEnumerable collection containing the elements of the span.</returns>
    public static IEnumerable<T> AsEnumerable<T>(this Span<T> span)
    {
        T[] array = new T[span.Length];

        for (int i = 0; i < span.Length; i++)
        {
              array[i] = span[i];
        }
        
        return array;
    }
}