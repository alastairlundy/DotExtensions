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

namespace AlastairLundy.DotExtensions.MsExtensions.StringSegments;

/// <summary>
/// Provides extension methods for converting <see cref="StringSegment"/> objects into various character collections.
/// </summary>
public static class SegmentToCharsExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="segment"></param>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Returns the <see cref="StringSegment"/> as a Char Array.
        /// </summary>
        /// <returns>The string segment as a char array.</returns>
        /// <exception cref="ArgumentException">Thrown if the StringSegment is null or empty.</exception>
        public char[] ToCharArray()
        { 
            ArgumentException.ThrowIfNullOrEmpty(segment);
            
            char[] charArray = new char[segment.Length];

            for (int i = 0; i < segment.Length; i++)
            {
                charArray[i] = segment[i];
            }

            return charArray;
        }

        /// <summary>
        /// Returns the <see cref="StringSegment"/> as a List of type <see cref="char"/>.
        /// </summary>
        /// <returns>A list of characters from the StringSegment if any characters are in the StringSegment.</returns>
        /// <exception cref="ArgumentException">Thrown if the StringSegment is null or empty.</exception>
        public List<char> ToList()
        {
            ArgumentException.ThrowIfNullOrEmpty(segment);

            List<char> list = [];

            for (int i = 0; i < segment.Length; i++)
            {
                list.Add(segment[i]);
            }

            return list;
        }
    }
}
