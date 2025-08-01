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

using AlastairLundy.DotExtensions.MsExtensions.Localizations;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.DotExtensions.MsExtensions.System.StringSegments;

public static class SegmentFirstAndLastExtensions
{
    /// <summary>
    /// Returns the first char in the StringSegment.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <returns>The first char in the StringSegment.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the StringSegment contains zero chars.</exception>
    public static char First(this StringSegment target)
    {
        if (target.Length < 1)
            throw new InvalidOperationException(Resources.Exceptions_Enumerables_InvalidOperation_EmptySequence);
        
        return target[0];
    }

    /// <summary>
    /// Returns the first char in the StringSegment that matches the predicate condition.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <param name="predicate">The predicate func condition to be checked against each char in the StringSegment.</param>
    /// <returns>The first char in the StringSegment that matches the predicate condition.</returns>
    /// <exception cref="ArgumentException">Thrown if no characters in the StringSegment meet the predicate condition.</exception>
    public static char First(this StringSegment target, Func<char, bool> predicate)
    {
        IEnumerable<char> results = (from c in target
            where predicate.Invoke(c)
            select c);

        foreach (char result in results)
        {
            return result;
        }
        
        throw new ArgumentException(Resources.Exceptions_StringSegment_NoPredicateMatches);
    }

    /// <summary>
    /// Returns the first character of the specified <see cref="StringSegment"/> or null if the segment is empty.
    /// </summary>
    /// <param name="target">The <see cref="StringSegment"/> from which to retrieve the first character.</param>
    /// <returns>The first character of the segment if it exists; otherwise, null.</returns>
    public static char? FirstOrDefault(this StringSegment target)
    {
        if (target.IsEmpty())
            return default;

        return target[0];
    }

    /// <summary>
    /// Returns the first character of the specified <see cref="StringSegment"/> that meets the predicate condition or null if the segment is empty.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <param name="predicate">The predicate func condition to be checked against each char in the StringSegment.</param>
    /// <returns>The first character of the segment that meets the predicate condition if any match; otherwise, null.</returns>
    public static char? FirstOrDefault(this StringSegment target, Func<char, bool> predicate)
    {
        IEnumerable<char> results = (from c in target
            where predicate(c)
            select c);

        foreach (char result in results)
        {
            return result;
        }
        
        return default;
    }

    /// <summary>
    /// Returns the last char in the StringSegment.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <returns>The last char in the StringSegment.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the StringSegment contains zero chars.</exception>
    public static char Last(this StringSegment target)
    {
        if (target.IsEmpty())
            throw new InvalidOperationException(Resources.Exceptions_Enumerables_InvalidOperation_EmptySequence);

#if NET6_0_OR_GREATER
            return target[^1];
#else
        // ReSharper disable once UseIndexFromEndExpression
        return target[target.Length - 1];
#endif
    }

    /// <summary>
    /// Returns the last character of the specified <see cref="StringSegment"/> that meets the predicate condition.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <param name="predicate">The predicate func condition to be checked against each char in the StringSegment.</param>
    /// <returns>The last character of the segment that meets the predicate condition if any match.</returns>
    /// <exception cref="ArgumentException">Thrown if no characters in the StringSegment meet the predicate condition.</exception>
    public static char Last(this StringSegment target, Func<char, bool> predicate)
    {
        return First(target.Reverse(), predicate);
    }

    /// <summary>
    /// Returns the last character of the specified <see cref="StringSegment"/> that meets the predicate condition or a null if the segment is empty.
    /// </summary>
    /// <param name="target">The <see cref="StringSegment"/> from which to retrieve the last character.</param>
    /// <returns>The last character of the segment if it contains any characters; otherwise, null.</returns>
    public static char? LastOrDefault(this StringSegment target)
    {
        if (target.IsEmpty())
        {
            return null;
        }
        
#if NET6_0_OR_GREATER || NETSTANDARD2_1
        return target[^1];
#else
        return target[target.Length - 1];
#endif
    }

    /// <summary>
    /// Returns the last character of the specified <see cref="StringSegment"/> that matches the predicate condition or a default value if the segment is empty.
    /// </summary>
    /// <param name="target">The StringSegment to be searched.</param>
    /// <param name="predicate">The predicate func condition to be checked against each char in the StringSegment.</param>
    /// <returns>The last character of the segment that meets the predicate condition if any match; otherwise, null.</returns>
    public static char? LastOrDefault(this StringSegment target, Func<char, bool> predicate)
    {
        return FirstOrDefault(target.Reverse(), predicate);
    }
}