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
/// Provides extension methods for detecting line endings.
/// </summary>
public static class LineEndingExtensions
{
    /// <param name="s">The string to be searched.</param>
    extension(string s)
    {
        /// <summary>
        /// Detects the line ending used in the string.
        /// </summary>
        /// <returns>The line ending used in the string.</returns>
        /// <exception cref="ArgumentException">Thrown if the string is null or empty.</exception>
        public string GetLineEnding()
        {
            ArgumentException.ThrowIfNullOrEmpty(s);
            
            bool containsR = s.Contains('\r');
            bool containsN = s.Contains('\n');

            char newLineChar;

            if (containsN || containsR || s.Contains(Environment.NewLine))
            {
                try
                {
                    newLineChar = s.First(c => c == Environment.NewLine.First());
                }
                catch
                {
                    switch (containsR)
                    {
                        case true:
                            newLineChar = s.First(c => c == '\r');
                            break;
                        case false:
                            newLineChar = containsN ? s.First(c => c == '\n') :
                                Environment.NewLine.First();
                            break;
                    }
                }
            }
            else
            {
                newLineChar = Environment.NewLine.First();
            }

            return newLineChar switch
            {
                '\n' => containsR ? "\r\n" : "\n",
                '\r' => containsN ? "\r\n" : "\r",
                _ => ""
            };
        }
    }
}