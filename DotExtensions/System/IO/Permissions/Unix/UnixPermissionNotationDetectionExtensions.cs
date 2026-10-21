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
public static class UnixPermissionNotationDetectionExtensions
{
    /// <param name="notation">The symbolic notation string to validate.</param>
    extension(string notation)
    {
        /// <summary>
        /// Validates whether a given string represents a valid Unix file permission symbolic notation (rwx format).
        /// </summary>
        /// <remarks>
        /// A valid notation is exactly 10 characters: a leading file type character ('-', 'd', 'l', 'p', 'c', 's' or 'b')
        /// followed by three permission triplets for user, group and others. Within each triplet the read slot accepts
        /// 'r' or '-', the write slot accepts 'w' or '-' and the execute slot accepts 'x', 's', 't', 'S', 'T' or '-'.
        /// </remarks>
        /// <returns>
        /// A boolean value indicating whether the input notation is valid. Returns false when the input is null, empty or malformed.
        /// </returns>
        [Obsolete(DeprecationMessages.DeprecationV11)]
        public bool IsValidRwxSymbolNotation()
        {
            if (string.IsNullOrEmpty(notation) || notation.Length != 10)
                return false;

            if (notation[0] is not ('-' or 'd' or 'l' or 'p' or 'c' or 's' or 'b'))
                return false;

            for (int position = 1; position < notation.Length; position++)
            {
                char current = notation[position];

                bool isValid = (position % 3) switch
                {
                    1 => current is 'r' or '-',
                    2 => current is 'w' or '-',
                    _ => current is 'x' or 's' or 't' or 'S' or 'T' or '-'
                };

                if (!isValid)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Validates if the provided numeric notation string represents a valid Unix permission in numeric format.
        /// </summary>
        /// <remarks>
        /// The numeric notation can be three digits (e.g. "755") or four digits (e.g. "4755" or "0755"). An optional leading
        /// '0' is ignored. Within a four digit notation the leading digit encodes the special bits (1 = sticky bit,
        /// 2 = set-group-ID, 4 = set-user-ID, or the OR-combination thereof, 0 = none). Every remaining digit must be an
        /// octal digit (0-7).
        /// </remarks>
        /// <returns>
        /// True if the numeric notation string represents a valid Unix permission; otherwise, false.
        /// Returns false when the input is null or empty.
        /// </returns>
        [Obsolete(DeprecationMessages.DeprecationV11)]
        public bool IsValidNumericNotation()
        {
            if (string.IsNullOrEmpty(notation))
                return false;

            string digits = notation[0] == '0' ? notation.Remove(0, 1) : notation;

            if (digits.Length is < 3 or > 4)
                return false;

            foreach (char digit in digits)
            {
                if (digit < '0' || digit > '7')
                    return false;
            }

            return true;
        }
    }
}