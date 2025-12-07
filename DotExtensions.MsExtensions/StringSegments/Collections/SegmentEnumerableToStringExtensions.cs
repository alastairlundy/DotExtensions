/*
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.MsExtensions.StringSegments.Collections;

/// <summary>
/// Provides string manipulation extensions for sequences of StringSegments.
/// </summary>
public static class SegmentEnumerableToStringExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="segments"></param>
    extension(IEnumerable<StringSegment> segments)
    {
        /// <summary>
        /// Converts this sequence of StringSegments into a single string.
        /// </summary>
        /// <param name="separator">Optional separator string between segments (default: space).</param>
        /// <returns>The concatenated string representation of the input segments.</returns>
        public string ToString(StringSegment separator)
        {
            ArgumentNullException.ThrowIfNull(segments);
            ArgumentException.ThrowIfNullOrEmpty(separator);
            
            StringBuilder stringBuilder = new StringBuilder();

            foreach (StringSegment segment in segments)
            {
                for (int i = 0; i < segment.Length; i++)
                {
                    stringBuilder.Append(segment[i]);
                }

                for (int i2 = 0; i2 < separator.Length; i2++)
                {
                    stringBuilder.Append(separator[i2]);
                }
            }

            stringBuilder.Remove(
                startIndex: stringBuilder.Length - separator.Length,
                length: separator.Length
            );

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts a sequence of StringSegments into a single string.
        /// </summary>
        /// <param name="separator">Optional separator character between segments (default: space).</param>
        /// <returns>The concatenated string representation of the input segments.</returns>
        public string ToString(char separator)
        {
            ArgumentNullException.ThrowIfNull(segments);

            StringBuilder stringBuilder = new StringBuilder();

            foreach (StringSegment segment in segments)
            {
                for (int i = 0; i < segment.Length; i++)
                {
                    stringBuilder.Append(segment[i]);
                }

                stringBuilder.Append(separator);
            }

            stringBuilder.Remove(startIndex: stringBuilder.Length - 1, length: 1);
            return stringBuilder.ToString();
        }
    }
}
