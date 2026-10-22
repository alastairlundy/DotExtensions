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
/// Provides functionality for reversing the contents of a string.
/// </summary>
public static class StringReverseExtensions
{
    /// <param name="str">The string to reverse.</param>
    extension(string str)
    {
        /// <summary>
        /// Reverses the contents of a string.
        /// </summary>
        /// <remarks>This method builds a reversed string using a <see cref="StringBuilder"/> for improved performance.
        /// The string is reversed one Unicode scalar value (rune) at a time so surrogate pairs are not split.</remarks>
        /// <returns>A new string with the reversed contents of the source string.</returns>
        /// <exception cref="ArgumentException">Thrown if the source string is null or empty.</exception>
        public string Reverse()
        {
            ArgumentException.ThrowIfNullOrEmpty(str);

            StringBuilder stringBuilder = new(capacity: str.Length);

            ReadOnlySpan<char> remaining = str.AsSpan();

            while (!remaining.IsEmpty)
            {
                // DecodeLastFromUtf16 always consumes at least one char; invalid data yields the replacement rune.
                Rune.DecodeLastFromUtf16(remaining, out Rune rune, out int charsConsumed);

                stringBuilder.Append(rune);

                remaining = remaining[..^charsConsumed];
            }

            return stringBuilder.ToString();
        }
    }
}