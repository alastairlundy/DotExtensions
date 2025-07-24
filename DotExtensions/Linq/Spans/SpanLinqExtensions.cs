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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AlastairLundy.DotExtensions.Localizations;
using AlastairLundy.DotExtensions.Memory.Spans;
using AlastairLundy.DotPrimitives.Collections.Groupings;
#if NET5_0_OR_GREATER
#endif

// ReSharper disable UseIndexFromEndExpression

namespace AlastairLundy.DotExtensions.Linq.Spans;

/// <summary>
/// 
/// </summary>
public static class SpanLinqExtensions
{

    /// <summary>
    /// Applies the given action to each element of this Span.
    /// </summary>
    /// <param name="action">The action to apply to each element in the span.</param>
    /// <param name="target">The span to apply the elements to.</param>
    /// <typeparam name="T">The type of items in the Span.</typeparam>
    public static void ForEach<T>(this ref Span<T> target, Action<T> action)
    {
        for (int index = 0; index < target.Length; index++)
        {
            action.Invoke(target[index]);
        }
    }

    /// <summary>
    /// Applies the given func to each element of this Span.
    /// </summary>
    /// <param name="target">The span to apply the elements to.</param>
    /// <param name="action">The func to apply to each element in the span.</param>
    /// <typeparam name="T">The type of items in the Span.</typeparam>
    public static void ForEach<T>(this Span<T> target, Func<T, T> action)
    {
        for (int i = 0; i < target.Length; i++)
        {
            target[i] = action.Invoke(target[i]);
        }
    }
    
    /// <summary>
    /// Returns a new Span with all the elements of two Spans that are only in one Span and not the other.
    /// </summary>
    /// <param name="first">The first Span to search.</param>
    /// <param name="second">The second Span to search.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>A new Span with all the elements of Span One and Span Two that were not in the other Span.</returns>
    public static Span<T> Except<T>(this Span<T> first, Span<T> second) where T : IEquatable<T>
    {
        T[] firstArray = first.ToArray();
        T[] secondArray = second.ToArray();

        List<T> resultOne = first
            .SkipWhile(x => secondArray.Contains(x))
            .ToList();

        List<T> resultTwo = second
            .SkipWhile(x => firstArray.Contains(x))
            .ToList();

        resultOne.AddRange(resultTwo);
        
        return new Span<T>(resultOne.ToArray());
    }
    
    /// <summary>
    /// Returns a new Span with all the elements of the span except the specified first number of elements.
    /// </summary>
    /// <param name="target">The span to make a new span from.</param>
    /// <param name="count">The number of items to skip from the beginning of the span.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>A new Span with all the elements of the original span except the specified number of first elements to skip.</returns>
    public static Span<T> Skip<T>(this Span<T> target, int count)
    {
        if (count > target.Length)
            throw new ArgumentOutOfRangeException(Resources.Exceptions_Span_SkipCountTooLarge);
            
        int end = target.Length - count;

        return target.GetRange(start: count, end: end);
    }

    /// <summary>
    /// Returns a new Span with all the elements of the span except the specified last number of elements.
    /// </summary>
    /// <param name="target">The span to make a new span from.</param>
    /// <param name="count">The number of items to skip from the end of the span.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>A new Span with all the elements of the original span except the specified last number of elements to skip.</returns>
    public static Span<T> SkipLast<T>(this Span<T> target, int count)
    {
        if (count > target.Length)
            throw new ArgumentOutOfRangeException(Resources.Exceptions_Span_SkipCountTooLarge);
            
        return target.GetRange(start: 0, end: target.Length - count);
    }

    /// <summary>
    /// Returns a new Span with all the elements of the original span that do not match the specified predicate func.
    /// </summary>
    /// <param name="target">The span to make a new span from.</param>
    /// <param name="predicate">The condition to use to determine whether to skip items or not in the span.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>A new Span with all the elements of the original Span that did not match the specified predicate func.</returns>
    public static Span<T> SkipWhile<T>(this Span<T> target, Func<T, bool> predicate)
    {
        return from item in target
            where predicate.Invoke(item) == false
            select item;
    }

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
    
    /// <summary>
    /// Returns a new Span with all items in the Span that match the predicate condition.
    /// </summary>
    /// <param name="target">The Span to be searched.</param>
    /// <param name="predicate">The predicate func to be invoked on each item in the Span.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>A new Span with the items that match the predicate condition.</returns>
    public static Span<T> Where<T>(this Span<T> target, Func<T, bool> predicate)
    {
        List<T> list = new();

        foreach (T item in target)
        {
            if (predicate.Invoke(item))
            {
                list.Add(item);
            }
        }
        
        return new Span<T>(list.ToArray());
    } 
    
    /// <summary>
    /// Returns whether any item in a Span matches the predicate condition.
    /// </summary>
    /// <param name="target">The Span to be searched.</param>
    /// <param name="predicate">The predicate func to be invoked on each item in the Span.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>True if any item in the span matches the predicate; false otherwise.</returns>
    public static bool Any<T>(this Span<T> target, Func<T, bool> predicate)
    {
        Span<bool> groups = (from c in target
                group c by predicate.Invoke(c)
                into g
                where g.Key
                select g.Any()
            );

        bool? result = groups.FirstOrDefault();

        return result ?? false;
    }

    /// <summary>
    /// Groups the elements of the source span by a specified key selector function.
    /// </summary>
    /// <param name="source">The source span to group elements from.</param>
    /// <param name="keySelector">A function to extract the key for each element.</param>
    /// <typeparam name="TKey">The type of the key returned by the key selector function.</typeparam>
    /// <typeparam name="TElement">The type of elements in the source span.</typeparam>
    /// <returns>A span of groups, each containing a key and the elements that share that key.</returns>
    public static Span<IGrouping<TKey, TElement>> GroupBy<TKey, TElement>(
#if NET5_0_OR_GREATER
        [NotNull]
#endif
        this Span<TElement> source,
#if NET5_0_OR_GREATER
        [NotNull]
#endif
        Func<TElement, TKey> keySelector) where TKey : notnull
    {
        Dictionary<TKey, List<TElement>> dictionary = new();

        foreach (TElement item in source)
        {
            TKey key = keySelector.Invoke(item);
            
            if (dictionary.ContainsKey(key))
            {
                dictionary[key].Add(item);
            }
            else
            {
                dictionary.Add(key, new List<TElement>());
                dictionary[key].Add(item);
            }
        }

        IEnumerable<IGrouping<TKey, TElement>> groups = (from kvp in dictionary
            select new GroupByEnumerable<TKey, TElement>(kvp.Key, kvp.Value));
        
        return new  Span<IGrouping<TKey, TElement>>(groups.ToArray());
    }

    /// <summary>
    /// Transforms elements of a Span according to behaviour defined by the Selector.
    /// </summary>
    /// <param name="source">The span to search.</param>
    /// <param name="selector">The selector to use.</param>
    /// <typeparam name="TSource">The type of elements in the source Span.</typeparam>
    /// <typeparam name="TResult">The type of elements the selector transforms elements into.</typeparam>
    /// <returns>The newly created Span with the elements transformed by the selector.</returns>
    public static Span<TResult> Select<TSource, TResult>(
#if NET5_0_OR_GREATER
        [NotNull]
#endif
        this Span<TSource> source,
#if NET5_0_OR_GREATER
[NotNull]
#endif
        Func<TSource, TResult> selector)
    {
        TResult[] array = new  TResult[source.Length];
        
        for (int index = 0; index < source.Length; index++)
        {
            TSource item = source[index];
            TResult res = selector.Invoke(item);

            array[index] = res;
        }

        return new Span<TResult>(array);
    }

    /// <summary>
    /// Returns a new span containing distinct elements from the source span, using the default equality comparer.
    /// </summary>
    /// <param name="source">The source span from which to extract distinct elements.</param>
    /// <typeparam name="T">The type of elements in the source span.</typeparam>
    /// <returns>A span containing the distinct elements from the source span.</returns>
    public static Span<T> Distinct<T>(this Span<T> source)
    {
#if NETSTANDARD2_1 || NET5_0_OR_GREATER
        HashSet<T> set = new(capacity: source.Length);
#else
        HashSet<T> set = new();
#endif
        foreach (T item in source)
        {
            set.Add(item);
        }
        
        return new Span<T>(set.ToArray());
    }

    /// <summary>
    /// Returns a new span containing distinct elements from the source span, using the specified equality comparer.
    /// </summary>
    /// <param name="source">The source span from which to extract distinct elements.</param>
    /// <param name="comparer">The equality comparer to use for comparing elements.</param>
    /// <typeparam name="T">The type of elements in the source span.</typeparam>
    /// <returns>A span containing the distinct elements from the source span.</returns>
    public static Span<T> Distinct<T>(this Span<T> source, IEqualityComparer<T> comparer)
    {
#if NET5_0_OR_GREATER || NETSTANDARD2_1
        HashSet<T> set = new(capacity: source.Length, comparer: comparer);
#else
        HashSet<T> set = new(comparer: comparer);

#endif
        foreach (T item in source)
        {
            set.Add(item);
        }
        
        return new Span<T>(set.ToArray());
    }

    /// <summary>
    /// Returns the number of elements in a given span that satisfy a condition.
    /// </summary>
    /// <param name="source">The span to search.</param>
    /// <param name="predicate">A func that takes an element and returns a boolean indicating whether it should be counted.</param>
    /// <typeparam name="TSource">The type of elements in the span.</typeparam>
    /// <returns>The number of elements that satisfy the predicate.</returns>
    public static int Count<TSource>(this Span<TSource> source, Func<TSource, bool> predicate)
    {
        int count = 0;

        foreach (TSource item in source)
        {
            if (predicate.Invoke(item))
            {
                count++;
            }
        }
        
        return count;
    }
    
    /// <summary>
    /// Returns whether all items in a Span match the predicate condition.
    /// </summary>
    /// <param name="target">The span to be searched.</param>
    /// <param name="predicate">The predicate func to be invoked on each item in the Span.</param>
    /// <typeparam name="T">The type of items stored in the span.</typeparam>
    /// <returns>True if all items in the span match the predicate; false otherwise.</returns>
    public static bool All<T>(this Span<T> target, Func<T, bool> predicate)
    {      
        Span<bool> groups = (from c in target
                group c by predicate.Invoke(c)
                into g
                where g.Key
                select g.Any()
            );

        return groups.Distinct().Length ==  1;
    }
}