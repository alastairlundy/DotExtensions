/*
 * Copyright (c) 2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.MsExtensions.StringSegments;

/// <summary>
/// Provides extension methods for performing operations related to spaces within <see cref="StringSegment"/> instances.
/// </summary>
public static class SegmentContainsSpacesExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="segment"></param>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Whether a <see cref="StringSegment"/> contains space separated subsegments within it.
        ///
        /// <para>True if the <see cref="StringSegment"/> contains space separated <see cref="StringSegment"/>s within it; false otherwise.</para>
        /// </summary>
        public bool ContainsSpaceSeparatedSubSegments()
        {
            if (segment.IsEmpty)
                return false;

            StringTokenizer tokenizer = segment.Split([' ']);

            return segment.Contains(' ') && tokenizer.Count() > 1;
        }
    }
}
