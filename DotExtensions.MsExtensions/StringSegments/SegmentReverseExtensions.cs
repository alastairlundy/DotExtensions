/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.MsExtensions.StringSegments;

/// <summary>
/// Provides extension methods for reversing the content of StringSegment objects.
/// </summary>
public static class SegmentReverseExtensions
{
    /// <param name="target">The StringSegment to reverse.</param>
    extension(StringSegment target)
    {
        /// <summary>
        /// Reverses the contents of the StringSegment.
        /// </summary>
        /// <returns>The reversed StringSegment.</returns>
        /// <exception cref="ArgumentException">Thrown if the target is null or empty.</exception>
        public StringSegment Reverse()
        {
            ArgumentException.ThrowIfNullOrEmpty(target);
            
            StringBuilder stringBuilder = new();

            for (int i = 0; i < target.Length; i++)
            {
                if (target.Length - 1 - i >= 0)
                    stringBuilder.Append(target[target.Length - 1 - i]);
            }

            return new StringSegment(stringBuilder.ToString());
        }
    }
}
