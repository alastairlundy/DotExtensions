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

namespace DotExtensions.MsExtensions.StringSegments;

/// <summary>
/// Provides extension methods for performing operations related to spaces within <see cref="StringSegment"/> instances.
/// </summary>
public static class SegmentContainsSubsegmentsExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="segment"></param>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Determines whether the specified string segment contains delimited subsegments.
        /// </summary>
        /// <param name="delimiter">The delimiter character to check for.</param>
        /// <returns>True if the string segment contains delimited subsegments; false otherwise.</returns>
        public bool ContainsDelimitedSubSegments(char delimiter)
        {
            if (segment.IsEmpty || StringSegment.IsWhiteSpace(segment))
                return false;
            
            StringTokenizer tokenizer = segment.Split([delimiter]);
            
            int count = 0;
            foreach (StringSegment unused in tokenizer)
            {
                count++;

                if (count > 1)
                    break;
            }
            
            return segment.Contains(delimiter) && count > 1;
        }
    }
}