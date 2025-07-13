/*
        MIT License
       
       Copyright (c) 2024-2025 Alastair Lundy
       
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

namespace AlastairLundy.DotExtensions.Collections.Strings.Enumerables;

public static class StringIndicesOfExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static IEnumerable<int> IndicesOf(this string str, char c)
    {
        List<int> indices = new List<int>();
        
        for(int i = 0; i < str.Length; i++)
        {
            if (str[i] == c)
            {
                indices.Add(i);
            }
        }

        if (indices.Count == 0)
            indices = [-1];
        
        return indices;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int IndexOf(this string str, string value)
    {
        if (str.Length < value.Length || value.Length == 0)
            return -1;

        IEnumerable<int> indexes = str.IndicesOf(value.First()).Where(x => x != -1);

        foreach (int index in indexes)
        {
            string indexValue = value.Substring(index, value.Length);

            if (indexValue.Equals(str))
            {
                return index;
            }
        }
        
        return -1;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IEnumerable<int> IndicesOf(this string str, string value)
    {
        List<int> indices = new();

        if (str.Length < value.Length || value.Length == 0)
            return [-1];

        IEnumerable<int> indexes = str.IndicesOf(value.First()).Where(x => x != -1);

        foreach (int index in indexes)
        {
            string indexValue = value.Substring(index, value.Length);

            if (indexValue.Equals(str))
            {
                indices.Add(index);
            }
        }

        if(indices.Count == 0)
            indices = [-1];
        
        return indices;
    }
}