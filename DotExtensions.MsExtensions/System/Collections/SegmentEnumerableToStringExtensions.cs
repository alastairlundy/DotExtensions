﻿/*
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
using System.Text;

using AlastairLundy.DotExtensions.MsExtensions.System.StringSegments;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.DotExtensions.MsExtensions.System.Collections
{
    /// <summary>
    /// Provides string manipulation extensions for sequences of StringSegments.
    /// </summary>
    public static class SegmentEnumerableToStringExtensions
    {

        /// <summary>
        /// Converts a sequence of StringSegments into a single string.
        /// </summary>
        /// <param name="segments">The sequence of StringSegments to convert.</param>
        /// <param name="separator">Optional separator string between segments (default: space).</param>
        /// <returns>The concatenated string representation of the input segments.</returns>
        public static string ToString(this IEnumerable<StringSegment> segments, string separator)
        {   
            StringBuilder stringBuilder = new StringBuilder();
            
            foreach (StringSegment segment in segments)
            {
                stringBuilder.Append(segment.ToCharArray());
                stringBuilder.Append(separator);
            }
        
            stringBuilder.Remove(stringBuilder.Length - separator.Length, separator.Length);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts a sequence of StringSegments into a single string.
        /// </summary>
        /// <param name="segments">The sequence of StringSegments to convert.</param>
        /// <param name="separator">Optional separator character between segments (default: space).</param>
        /// <returns>The concatenated string representation of the input segments.</returns>
        public static string ToString(this IEnumerable<StringSegment> segments, char separator)
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            foreach(StringSegment segment in segments)
            {
                stringBuilder.Append(segment.ToCharArray());
                stringBuilder.Append(separator);
            }
            
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }
    }
}