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

namespace DotExtensions.MsExtensions.Primitives;

/// <summary>
/// Extension methods for working with the length of <see cref="StringValues"/> objects.
/// </summary>
public static class StringValuesLengthExtensions
{
    /// <param name="stringValues">The <see cref="StringValues"/> object to search.</param>
    extension(StringValues stringValues)
    {
        /// <summary>
        /// The total length of all strings in a <see cref="StringValues"/> object combined.
        /// </summary>
        public int TotalLength
        {
            get
            {
                int length = 0;

                foreach (string? value in stringValues)
                {
                    if(value is not null)
                        length += value.Length;
                }

                return length;
            }  
        }
    }
}