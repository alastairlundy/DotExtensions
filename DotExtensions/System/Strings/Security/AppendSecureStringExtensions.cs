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

using System.Security;

namespace DotExtensions.Strings.Security;

/// <summary>
/// Provides extension methods for securely modifying instances of <see cref="SecureString"/>.
/// </summary>
public static class AppendSecureStringExtensions
{
    /// <param name="secureString">The <see cref="SecureString"/> to append a value to.</param>
    extension(SecureString secureString)
    {
        /// <summary>
        /// Appends a sequence of characters to this <see cref="SecureString"/>.
        /// An empty (zero-length) <see cref="SecureString"/> is a valid target and may be appended to.
        /// </summary>
        /// <param name="chars">The sequence of characters to append to this <see cref="SecureString"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if this <see cref="SecureString"/> or <paramref name="chars"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if this <see cref="SecureString"/> is read-only.</exception>
        public void AppendChars(params IEnumerable<char> chars)
        {
            ArgumentNullException.ThrowIfNull(secureString);
            ArgumentNullException.ThrowIfNull(chars);

            if (secureString.IsReadOnly())
                throw new InvalidOperationException();

            foreach (char c in chars)
            {
                secureString.AppendChar(c);
            }
        }
    }

    /// <param name="secureString">The <see cref="SecureString"/> to set a value to.</param>
    extension(SecureString secureString)
    {
        /// <summary>
        /// Sets this <see cref="SecureString"/> to the specified characters by clearing its existing content and appending the new characters.
        /// An empty (zero-length) <see cref="SecureString"/> is a valid target. If this method throws, the original content is left intact.
        /// </summary>
        /// <param name="chars">The characters to set to this <see cref="SecureString"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if this <see cref="SecureString"/> or <paramref name="chars"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if this <see cref="SecureString"/> is read-only.
        /// Characters cannot be set in-place on a read-only instance, regardless of its length, and its content is not modified when this is thrown.</exception>
        public void SetChars(params ICollection<char> chars)
        {
            ArgumentNullException.ThrowIfNull(secureString);
            ArgumentNullException.ThrowIfNull(chars);

            if (secureString.IsReadOnly())
                throw new InvalidOperationException("Cannot set the characters of a read-only SecureString. Characters cannot be set in-place on a read-only instance.");

            char[] newChars = new char[chars.Count];
            chars.CopyTo(newChars, 0);

            secureString.Clear();

            foreach (char c in newChars)
            {
                secureString.AppendChar(c);
            }
        }
    }
}