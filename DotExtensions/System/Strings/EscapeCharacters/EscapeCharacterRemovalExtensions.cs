/*
        MIT License

       Copyright (c) 2020-2025 Alastair Lundy

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

// ReSharper disable CheckNamespace

using System.Collections.Generic;
using System.Linq;

using AlastairLundy.DotExtensions.Collections.Strings.Enumerables;

namespace AlastairLundy.DotExtensions.Strings;

public static class EscapeCharacterRemovalExtensions
{
    /// <summary>
    /// Returns whether the string contains an Escape Character.
    /// </summary>
    /// <param name="str">The string to be searched.</param>
    /// <returns>True if the string contains an Escape Character; returns false otherwise.</returns>
    public static bool ContainsEscapeCharacters(this string str)
    {
        // ReSharper disable once ConvertClosureToMethodGroup
        return CharacterConstants.EscapeCharacters.Any(x => str.Contains(x));
    }
        
    /// <summary>
    /// Removes escape characters from a string.
    /// </summary>
    /// <param name="str">The string to be searched.</param>
    /// <returns>The modified string, if one or more escape characters were found, returns the original string otherwise.</returns>
    public static string RemoveEscapeCharacters(this string str)
    {
        if (ContainsEscapeCharacters(str))
        {
            foreach (string escapeChar in CharacterConstants.EscapeCharacters)
            {
                if (str.Contains(escapeChar))
                {
                    str = str.Replace(escapeChar, string.Empty);
                    
                    if(str.EndsWith(" "))
                        str = str.Remove(str.LastIndexOf(' '), 1);
                }
            }
        }

        return str;
    }
}