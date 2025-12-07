/*
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

// ReSharper disable ConvertClosureToMethodGroup

namespace AlastairLundy.DotExtensions.MsExtensions.StringSegments;

/// <summary>
/// Provides extension methods for determining the case of <see cref="StringSegment"/> instances.
/// </summary>
public static class SegmentCaseExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="segment"></param>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Returns whether this <see cref="StringSegment"/> is upper case or not.
        /// </summary>
        public bool IsUpperCase()
        {
            ArgumentNullException.ThrowIfNull(segment);
            
            for (int i = 0; i < segment.Length; i++)
            {
                if (char.IsLower(segment[i]) || char.IsLetter(segment[i]) == false)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns whether a <see cref="StringSegment"/> is lower case or not.
        /// </summary>
        public bool IsLowerCase()
        {
            ArgumentNullException.ThrowIfNull(segment);

            for (int i = 0; i < segment.Length; i++)
            {
                if (char.IsUpper(segment[i]) || char.IsLetter(segment[i]) == false)
                    return false;
            }

            return true;
        }
    }
}
