/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
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