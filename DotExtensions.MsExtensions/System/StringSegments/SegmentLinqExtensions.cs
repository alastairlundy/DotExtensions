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

namespace AlastairLundy.DotExtensions.MsExtensions.System.StringSegments
{
    public static class SegmentLinqExtensions
    {

        /// <summary>
        /// Returns the first element in the StringSegment.
        /// </summary>
        /// <param name="target">The StringSegment to be searched.</param>
        /// <returns>The first char in the StringSegment.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the StringSegment contains zero chars.</exception>
        public static char First(this StringSegment target)
        {
            if (target.Length == 1)
            {
                return target[0];
            }

            throw new InvalidOperationException(Resources.Exceptions_Enumerables_InvalidOperation_EmptySequence);
        }

        /// <summary>
        /// Returns the last char in the StringSegment.
        /// </summary>
        /// <param name="target">The StringSegment to be searched.</param>
        /// <returns>The last char in the StringSegment.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the StringSegment contains zero chars.</exception>
        public static char Last(this StringSegment target)
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
        /// Returns whether any char in a StringSegment matches the predicate condition.
        /// </summary>
        /// <param name="target">The StringSegment to be searched.</param>
        /// <param name="predicate">The predicate func to be invoked on each char in the StringSegment.</param>
        /// <returns>True if any char in the StringSegment matches the predicate; false otherwise.</returns>
        public static bool Any(this StringSegment target, Func<char, bool> predicate)
        {
            for(int index = 0; index < target.Length; index++)
            {
                char c = target[index];
                
                bool result = predicate.Invoke(c);

                if (result)
                {
                    return true;
                }
            }

            return false;
        }
    
        /// <summary>
        /// Returns whether all chars in a StringSegment match the predicate condition.
        /// </summary>
        /// <param name="target">The StringSegment to be searched.</param>
        /// <param name="predicate">The predicate func to be invoked on each item in the StringSegment.</param>
        /// <returns>True if all chars in the StringSegment match the predicate; false otherwise.</returns>
        public static bool All(this StringSegment target, Func<char, bool> predicate)
        {
            for (int index = 0; index < target.Length; index++)
            {
                char c = target[index];
                
                bool result = predicate.Invoke(c);

                if (result == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}