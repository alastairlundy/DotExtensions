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

namespace DotExtensions.IO.Permissions.Unix;

/// <summary>
/// Provides extension methods for detecting and validating Unix file permission notations.
/// </summary>
internal static class UnixPermissionNotationDetectionExtensions
{
    /// <param name="notation">The symbolic notation string to validate.</param>
    extension(string notation)
    {
        /// <summary>
        /// Validates whether a given string represents a valid Unix file permission symbolic notation (rwx format).
        /// </summary>
        /// <remarks>
        /// The notation optionally starts with a single file type indicator character
        /// (<c>-</c>, <c>b</c>, <c>c</c>, <c>d</c>, <c>l</c>, <c>p</c> or <c>s</c>), which is ignored during parsing,
        /// followed by exactly nine permission characters grouped into three user/group/other triples
        /// where each character is either the expected letter (<c>r</c>, <c>w</c>, <c>x</c>) or <c>-</c>.
        /// </remarks>
        /// <returns>A boolean value indicating whether the input notation is valid.</returns>
        internal bool IsValidRwxSymbolNotation()
        {
            ArgumentException.ThrowIfNullOrEmpty(notation);

            int offset = 0;

            if (notation.Length == 10)
            {
                if (notation[0] is not ('-' or 'b' or 'c' or 'd' or 'l' or 'p' or 's'))
                    return false;

                offset = 1;
            }
            else if (notation.Length != 9)
            {
                return false;
            }

            for (int i = offset; i < notation.Length; i++)
            {
                bool isValid = (i - offset) switch
                {
                    0 or 3 or 6 => notation[i] is 'r' or '-',
                    1 or 4 or 7 => notation[i] is 'w' or '-',
                    _ => notation[i] is 'x' or '-'
                };

                if (!isValid)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Validates if the provided numeric notation string represents a valid Unix permission in numeric format.
        /// </summary>
        /// <remarks> The numeric notation can be in a three-digit (e.g. "777") or a four-digit (e.g. "0777") format.</remarks>
        /// <returns>
        /// True if the numeric notation string represents a valid Unix permission; otherwise, false.
        /// </returns>
        internal bool IsValidNumericNotation()
        {
            ArgumentException.ThrowIfNullOrEmpty(notation);

            if (notation.Length is < 3 or > 4)
                return false;

            foreach (char c in notation)
            {
                if (c is < '0' or > '9')
                    return false;
            }

            return true;
        }
    }
}