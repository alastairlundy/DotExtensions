/*
        MIT License

       Copyright (c) 2026 Alastair Lundy

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


namespace DotExtensions.Strings;

/// <summary>
/// Provides extension methods for working with strings related to escape character detection and removal.
/// </summary>
public static class EscapeCharacterRemovalExtensions
{
    /// <param name="str">The string to be searched.</param>
    extension(string str)
    {
        /// <summary>
        /// Whether this string contains an Escape Character.
        ///
        /// <para>True if the string contains an Escape Character; returns false otherwise.</para>
        /// </summary>
        /// <returns><c>true</c> if the string contains any escape characters, <c>false</c> otherwise.</returns>
        public bool ContainsEscapeCharacters()
        {
            ArgumentException.ThrowIfNullOrEmpty(str);

            return CharacterConstants.EscapeCharacters.Any(x => str.Contains(x));
        }

        /// <summary>
        /// Removes escape characters from a string.
        /// </summary>
        /// <returns>Returns the modified string if one or more escape characters were found, returns the original string otherwise.</returns>
        public string RemoveEscapeCharacters()
        {
            ArgumentException.ThrowIfNullOrEmpty(str);

            StringBuilder strBuilder = new StringBuilder(str);
            
            foreach (string escapeCharacter in CharacterConstants.EscapeCharacters)
            {
                strBuilder.Replace(escapeCharacter, string.Empty);
            }
            
            return strBuilder.ToString().Trim(' ').TrimEnd(' ');
        }   
    }

    extension(string)
    {
        /// <summary>
        /// Determines whether the specified string contains an escape character.
        /// </summary>
        /// <param name="s">The string to check.</param>
        /// <returns><c>true</c> if the string contains any escape characters; otherwise, <c>false</c>.</returns>
        public static bool IsEscapeCharacter(string s)
        {
            ArgumentException.ThrowIfNullOrEmpty(s);
            
            return CharacterConstants.EscapeCharacters.Any(x => string.Equals(x, s, StringComparison.Ordinal));
        }
    }
}