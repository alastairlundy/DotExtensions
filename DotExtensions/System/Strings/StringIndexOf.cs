/*
        MIT License

       Copyright (c) 2025 Alastair Lundy

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

using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.DotExtensions.Strings;

/// <summary>
/// 
/// </summary>
public static class StringIndexOf
{
    private static IEnumerable<int> IndicesOf(this string str, char c)
    {
        for(int i = 0; i < str.Length; i++)
        {
            if (str[i] == c)
            {
                yield return i;
            }
        }
    }
    
    /// <summary>
    /// Finds the first occurrence of a specified substring within a string, starting from the beginning of the string.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <param name="value">The substring to find.</param>
    /// <returns>The index of the found substring if it exists, otherwise -1.</returns>
    public static int IndexOf(this string str, string value)
    {
        if (str.Length < value.Length || value.Length == 0)
            return -1;

        IEnumerable<int> indices = IndicesOf(str, value.First());

        foreach (int index in indices)
        {
            string indexValue = value.Substring(index, value.Length);

            if (indexValue.Equals(str))
            {
                return index;
            }
        }
        
        return -1;
    }
}