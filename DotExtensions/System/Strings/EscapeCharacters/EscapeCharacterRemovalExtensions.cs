/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using System.Linq;

// ReSharper disable ConvertClosureToMethodGroup

namespace AlastairLundy.DotExtensions.Strings;

/// <summary>
/// Provides extension methods for working with strings related to escape character detection and removal.
/// </summary>
public static class EscapeCharacterRemovalExtensions
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="str">The string to be searched.</param>
    extension(string str)
    {
        /// <summary>
        /// Whether this string contains an Escape Character.
        ///
        /// <para>True if the string contains an Escape Character; returns false otherwise.</para>
        /// </summary>
        public bool ContainsEscapeCharacters()
        {
            ArgumentException.ThrowIfNullOrEmpty(str);

            return CharacterConstants.EscapeCharacters.Any(x => str.Contains(x));
        }

        /// <summary>
        /// Removes escape characters from a string.
        /// </summary>
        /// <returns>The modified string, if one or more escape characters were found, returns the original string otherwise.</returns>
        public string RemoveEscapeCharacters()
        {
            ArgumentException.ThrowIfNullOrEmpty(str);

            if (!str.ContainsEscapeCharacters()) 
                return str;
            
            foreach (string escapeChar in CharacterConstants.EscapeCharacters)
            {
                if (str.Contains(escapeChar))
                {
                    str = str.Replace(escapeChar, string.Empty);

                    if (str.EndsWith(" "))
                        str = str.Remove(str.LastIndexOf(' '), 1);
                }
            }

            return str;
        }   
    }
}
