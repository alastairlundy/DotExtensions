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
        /// <remarks>An adjacent CRLF (<c>\r\n</c>) pair takes precedence. If the string contains no CRLF pair,
        /// a string containing bare LF and bare CR characters is classified by whichever of the two appears.
        /// Strings with no line endings at all fall back to the current platform's line ending convention.</remarks>
        /// <returns>The line ending used in the string.</returns>
        /// <exception cref="ArgumentException">Thrown if the string is null or empty.</exception>
        public string GetLineEnding()
        {
            ArgumentException.ThrowIfNullOrEmpty(s);

            // An actual adjacent CRLF pair takes precedence over bare character presence.
            if (s.Contains("\r\n", StringComparison.Ordinal))
                return "\r\n";

            bool containsR = false;
            bool containsN = false;

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];

                if (c == '\r')
                    containsR = true;
                else if (c == '\n')
                    containsN = true;

                if (containsR && containsN)
                    break;
            }

            if (containsN)
                return "\n";

            if (containsR)
                return "\r";

            // No line endings present: fall back to the platform default.
            return Environment.NewLine[0] switch
            {
                '\n' => "\n",
                '\r' => "\r",
                _ => Environment.NewLine
            };
        }
    }
}