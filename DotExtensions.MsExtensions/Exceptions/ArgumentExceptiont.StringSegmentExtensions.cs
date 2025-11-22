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

using AlastairLundy.DotExtensions.MsExtensions.StringSegments;

namespace AlastairLundy.DotExtensions.MsExtensions.Exceptions;

/// <summary>
/// 
/// </summary>
public static class ArgumentExceptionStringSegmentExtensions
{
    /// <summary>
    /// 
    /// </summary>
    extension(ArgumentException)
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the specified <see cref="StringSegment"/> is null
        /// or has a length of zero.
        /// </summary>
        /// <param name="target">The <see cref="StringSegment"/> to validate.</param>
        /// <param name="paramName">The name of the parameter being validated. Defaults to the name of the target parameter.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="target"/> is null or has a length of zero.
        /// </exception>
        public static void ThrowIfNullOrEmpty(StringSegment? target, string paramName = "")
        {
            if (paramName == "")
                paramName = nameof(target);

            ArgumentNullException.ThrowIfNull(target, paramName);

            if(StringSegment.IsNullOrEmpty(target))
                throw new ArgumentNullException(paramName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the specified <see cref="StringSegment"/> is null
        /// or consists solely of whitespace characters.
        /// </summary>
        /// <param name="target">The <see cref="StringSegment"/> to validate.</param>
        /// <param name="paramName">The name of the parameter being validated. Defaults to the name of the target parameter.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="target"/> is null or contains only whitespace characters.
        /// </exception>
        public static void ThrowIfNullOrWhitespace(StringSegment? target, string paramName = "")
        {
            if (paramName == "")
                paramName = nameof(target);

            ArgumentNullException.ThrowIfNull(target, paramName);
            
            if(StringSegment.IsNullOrWhiteSpace(target))
                throw new ArgumentNullException(paramName);
        }
    }
}