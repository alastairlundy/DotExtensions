/*
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.MsExtensions.StringSegments;

/// <summary>
/// Provides extension methods for modifying the capitalization of characters in <see cref="StringSegment"/> instances.
/// </summary>
public static class SegmentCapitalizationExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="segment">The StringSegment to be modified.</param>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Capitalizes the char at the specified index in the specified <see cref="StringSegment"/>.
        /// </summary>
        /// <param name="index">The index of the char to be made upper case.</param>
        /// <returns>The specified <see cref="StringSegment"/> with the specified char made upper case.</returns>
        public StringSegment CapitalizeChar(int index)
        {
            ArgumentException.ThrowIfNullOrEmpty(segment);
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, segment.Length);
            
            char c = segment[index];

            if (char.IsUpper(c))
                return segment;

            return new StringSegment(
                $"{segment.Substring(0, index)}{char.ToUpper(c)}{segment.Substring(index + 1)}"
            );
        }

        /// <summary>
        /// Capitalizes the chars at the specified indices in the specified <see cref="StringSegment"/>.
        /// </summary>
        /// <param name="indices">The indices of the chars to be made upper case.</param>
        /// <returns>The specified <see cref="StringSegment"/> with the specified chars made upper case.</returns>
        public StringSegment CapitalizeChars(IEnumerable<int> indices)
        {
            ArgumentException.ThrowIfNullOrEmpty(segment);
            ArgumentNullException.ThrowIfNull(indices);
            
            StringBuilder stringBuilder = new(capacity: segment.Length);

            for (int i = 0; i < segment.Length; i++)
            {
                stringBuilder.Append(segment[i]);
            }

            foreach (int index in indices)
            {
                ArgumentOutOfRangeException.ThrowIfNegative(index);
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, segment.Length);
                
                stringBuilder[index] = char.ToUpper(stringBuilder[index]);
            }

            return new StringSegment(stringBuilder.ToString());
        }
    }
}
