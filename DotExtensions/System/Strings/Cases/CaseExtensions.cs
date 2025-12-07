/*
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using System.Linq;

// ReSharper disable ConvertClosureToMethodGroup

namespace AlastairLundy.DotExtensions.Strings;

/// <summary>
/// Provides extension methods for string case-related operations.
/// </summary>
public static class CaseExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    extension(string s)
    {
        /// <summary>
        /// Returns whether this string is in the upper case or not.
        ///
        /// <para>True if the string is upper case; false otherwise.</para>
        /// </summary>
        public bool IsUpperCase()
        {
            ArgumentException.ThrowIfNullOrEmpty(s);

            return s.All(x => char.IsUpper(x));
        }

        /// <summary>
        /// Returns whether a string is lower case or not.
        ///
        /// <para>True if a string is lower case; false otherwise.</para>
        /// </summary>
        bool IsLowerCase()
        {
            ArgumentException.ThrowIfNullOrEmpty(s);

            return s.All(x => char.IsLower(x));
        }
    }
}
