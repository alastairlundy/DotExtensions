/*
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

// ReSharper disable CheckNamespace


#if NETSTANDARD2_0
using System.Linq;
#endif

namespace AlastairLundy.DotExtensions.Strings;

/// <summary>
/// Provides extension methods for working with strings to evaluate the presence of
/// space-separated substrings within them.
/// </summary>
public static class ContainsSpacesExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    extension(string s)
    {
        /// <summary>
        /// Determines if a string contains space separated substrings within it.
        ///
        /// <para>True if the string contains space separated strings within it; false otherwise. </para>
        /// </summary>
        public bool ContainsSpaceSeparatedSubStrings()
        {
            ArgumentException.ThrowIfNullOrEmpty(s);
            
#if NET8_0_OR_GREATER
            return s.Contains(' ') && s.Split(' ').Length > 1;
#else
            return s.Contains(' ') && s.Split(' ').Length > 1;
#endif
        }
    }
}
