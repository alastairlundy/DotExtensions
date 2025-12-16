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

namespace AlastairLundy.DotExtensions.MsExtensions.StringSegments.Collections;

/// <summary>
/// Provides string manipulation extensions for sequences of StringSegments.
/// </summary>
public static class SegmentEnumerableToStringExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="segments"></param>
    extension(IEnumerable<StringSegment> segments)
    {
        /// <summary>
        /// Converts this sequence of StringSegments into a single string.
        /// </summary>
        /// <param name="separator">Optional separator string between segments (default: space).</param>
        /// <returns>The concatenated string representation of the input segments.</returns>
        public string ToString(StringSegment separator)
        {
            ArgumentNullException.ThrowIfNull(segments);
            ArgumentException.ThrowIfNullOrEmpty(separator);
            
            StringBuilder stringBuilder = new();

            foreach (StringSegment segment in segments)
            {
                for (int i = 0; i < segment.Length; i++)
                {
                    stringBuilder.Append(segment[i]);
                }

                for (int i2 = 0; i2 < separator.Length; i2++)
                {
                    stringBuilder.Append(separator[i2]);
                }
            }

            stringBuilder.Remove(
                startIndex: stringBuilder.Length - separator.Length,
                length: separator.Length
            );

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts a sequence of StringSegments into a single string.
        /// </summary>
        /// <param name="separator">Optional separator character between segments (default: space).</param>
        /// <returns>The concatenated string representation of the input segments.</returns>
        public string ToString(char separator)
        {
            ArgumentNullException.ThrowIfNull(segments);

            StringBuilder stringBuilder = new();

            foreach (StringSegment segment in segments)
            {
                for (int i = 0; i < segment.Length; i++)
                {
                    stringBuilder.Append(segment[i]);
                }

                stringBuilder.Append(separator);
            }

            stringBuilder.Remove(startIndex: stringBuilder.Length - 1, length: 1);
            return stringBuilder.ToString();
        }
    }
}
