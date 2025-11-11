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
using System.Text;

using Microsoft.Extensions.Primitives;

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
            char c = segment[index];

            if (char.IsUpper(c)) 
                return segment;

            return new StringSegment($"{segment.Substring(0, index)}{char.ToUpper(c)}{segment.Substring(index + 1)}");
        }

        /// <summary>
        /// Capitalizes the chars at the specified indices in the specified <see cref="StringSegment"/>.
        /// </summary>
        /// <param name="indices">The indices of the chars to be made upper case.</param>
        /// <returns>The specified <see cref="StringSegment"/> with the specified chars made upper case.</returns>
        public StringSegment CapitalizeChars(IEnumerable<int> indices)
        {
            StringBuilder stringBuilder = new(capacity: segment.Length);
        
            for (int i = 0; i < segment.Length; i++)
            {
                stringBuilder.Append(segment[i]);
            }
        
            foreach (int index in indices)
            {
                stringBuilder[index] = char.ToUpper(stringBuilder[index]);
            }
        
            return new StringSegment(stringBuilder.ToString());
        }   
    }
}