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

using System;

using AlastairLundy.DotExtensions.Localizations;

namespace AlastairLundy.DotExtensions.Linq.Spans;

public static class SpanFirstAndLastExtensions
{
    /// <summary>
    /// Returns the first element in the Span.
    /// </summary>
    /// <param name="target">The span to be searched.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>The first item in the span if any items are in the Span.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the Span contains zero items.</exception>
    public static T First<T>(this Span<T> target)
    {
        if (target.IsEmpty)
            throw new InvalidOperationException(
                Resources.Exceptions_Enumerables_InvalidOperation_EmptySequence);
        
        return target[0];
    }

    /// <summary>
    /// Returns the first element of a span that satisfies a specified condition.
    /// </summary>
    /// <param name="target">The span to search for the first element.</param>
    /// <param name="predicate">A function that defines the condition to be met.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>The first element of the span that satisfies the condition.</returns>
    /// <exception cref="ArgumentException">Thrown when no element satisfies the condition.</exception>
    public static T First<T>(this Span<T> target, Func<T, bool> predicate)
    {
        for (int index = 0; index < target.Length; index++)
        {
            T item = target[index];
            if (predicate.Invoke(item))
            {
                return item;
            }
        }

        throw new ArgumentException();
    }

    /// <summary>
    /// Returns the first element of a span that satisfies a specified condition, or null if the Span is empty.
    /// </summary>
    /// <param name="target">The span to search for the first element.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>The first element of the span that satisfies the condition, or null if the span is empty.</returns>
    public static T? FirstOrDefault<T>(this Span<T> target)
    {
        return target.IsEmpty == false ? target[0] : default;
    }

    /// <summary>
    /// Returns the first element of a span that satisfies a specified condition,
    /// or a default value if no such element is found.
    /// </summary>
    /// <param name="target">The span to search for the first element.</param>
    /// <param name="predicate">A function that defines the condition to be met.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>The first element of the span that satisfies the condition, or null if no such element is found.</returns>
    /// <exception cref="ArgumentException">Thrown when the span is empty or no element satisfies the condition.</exception>
    public static T? FirstOrDefault<T>(this Span<T> target, Func<T, bool> predicate)
    {
        for (int index = 0; index < target.Length; index++)
        {
            T item = target[index];
            if (predicate.Invoke(item))
            {
                return item;
            }
        }

        return default;
    }

    /// <summary>
    /// Returns the last element in the Span.
    /// </summary>
    /// <param name="target">The span to be searched.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>The last item in the span if any items are in the Span.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the Span contains zero items.</exception>
    public static T Last<T>(this Span<T> target)
    {
        if (target.IsEmpty)
            throw new InvalidOperationException(Resources.Exceptions_Enumerables_InvalidOperation_EmptySequence);

#if NET6_0_OR_GREATER
        return target.Length > 1 ? target[^1] : target.First();
#else
        return target.Length > 1 ? target[target.Length - 1] : target.First();
#endif
    }

    /// <summary>
    /// Returns the last element of a span that satisfies a specified condition.
    /// </summary>
    /// <param name="target">The span to search for the last element.</param>
    /// <param name="predicate">A function that defines the condition to be met.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>The last element of the span that satisfies the condition.</returns>
    /// <exception cref="ArgumentException">Thrown when no element satisfies the condition.</exception>
    public static T Last<T>(this Span<T> target, Func<T, bool> predicate)
    {
        Span<T> newTarget = target;
        newTarget.Reverse();
        
        for (int index = 0; index < target.Length; index++)
        {
            T item = newTarget[index];
            if (predicate.Invoke(item))
            {
                return item;
            }
        }

        throw new ArgumentException();
    }

    /// <summary>
    /// Returns the last element of a span that satisfies a specified condition,
    /// or null if the Span is empty.
    /// </summary>
    /// <param name="target">The span to search for the last element.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>The last element of the span, or null if the span is empty.</returns>
    public static T? LastOrDefault<T>(this Span<T> target)
    {
        if (target.IsEmpty)
            return default;
        
#if NET6_0_OR_GREATER
        return target.Length > 1 ? target[^1] : target.FirstOrDefault();
#else

       return target.Length > 1 ? target[target.Length - 1] : target.FirstOrDefault();
#endif
    }

    /// <summary>
    /// Returns the last element of a span that satisfies a specified condition, or a default value if no such element is found.
    /// </summary>
    /// <param name="target">The span to search for the last element.</param>
    /// <param name="predicate">A function that defines the condition to be met.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <returns>The last element of the span that satisfies the condition, or null if no such element is found.</returns>
    public static T? LastOrDefault<T>(this Span<T> target, Func<T,  bool> predicate)
    {
        Span<T> newTarget = target;
        newTarget.Reverse();
        
        for (int index = 0; index < target.Length; index++)
        {
            T item = newTarget[index];
            if (predicate.Invoke(item))
            {
                return item;
            }
        }

        return default;
    }
}