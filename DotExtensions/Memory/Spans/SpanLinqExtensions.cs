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

using AlastairLundy.DotExtensions.Localizations;

namespace AlastairLundy.DotExtensions.Memory.Spans
{
    public static class SpanLinqExtensions
    {

        /// <summary>
        /// Returns a new Span with all the elements of two Spans that are only in one Span and not the other.
        /// </summary>
        /// <param name="first">The first Span to search.</param>
        /// <param name="second">The second Span to search.</param>
        /// <typeparam name="T">The type of items stored in the span.</typeparam>
        /// <returns>A new Span with all the elements of Span One and Span Two that were not in the other Span.</returns>
        public static Span<T> Except<T>(this Span<T> first, Span<T> second) where T : IEquatable<T>
        {
            List<T> list = new();
        
            foreach (T item in first)
            {
                if (second.Contains(item) == false)
                {
                    list.Add(item);
                }
            }

            foreach (T item in second)
            {
                if (first.Contains(item) == false)
                {
                    list.Add(item);
                }
            }
        
            return new Span<T>(list.ToArray());
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
            {
                throw new ArgumentOutOfRangeException(Resources.Exceptions_Span_SkipCountTooLarge);    
            }
            
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
            {
                throw new ArgumentOutOfRangeException(Resources.Exceptions_Span_SkipCountTooLarge);    
            }
            
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
            List<T> list = new();
        
            foreach (T item in target)
            {
                if (predicate.Invoke(item) == false)
                {
                    list.Add(item);
                }
            }
        
            return new Span<T>(list.ToArray());
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
            if (target.Length == 1)
            {
                return target[0];
            }

            throw new InvalidOperationException(Resources.Exceptions_Enumerables_InvalidOperation_EmptySequence);
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
            if (target.Length == 1)
            {
#if NET6_0_OR_GREATER
                return target[^1];
#else
                return target[target.Length - 1];
#endif
            }

            throw new InvalidOperationException(Resources.Exceptions_Enumerables_InvalidOperation_EmptySequence);
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
            foreach (T item in target)
            {
                bool result = predicate.Invoke(item);

                if (result)
                {
                    return true;
                }
            }

            return false;
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
            List<bool> results = new();

            foreach (T item in target)
            {
                results.Add(predicate.Invoke(item));
            }

            foreach (bool result in results)
            {
                if (result == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}