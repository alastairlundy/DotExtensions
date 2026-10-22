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
        /// </summary>
        /// <param name="chars">The sequence of characters to append to this <see cref="SecureString"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if this <see cref="SecureString"/> or <paramref name="chars"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if this <see cref="SecureString"/> is read-only.</exception>
        public void AppendChars(params IEnumerable<char> chars)
        {
            ArgumentNullException.ThrowIfNull(secureString);

            ArgumentNullException.ThrowIfNull(chars);

            if (secureString.IsReadOnly())
                throw new InvalidOperationException(Resources.Exceptions_Security_SecureString_ReadOnly);

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
        /// Sets this <see cref="SecureString"/> to the specified characters.
        /// </summary>
        /// <remarks>This method clears the existing contents of the <see cref="SecureString"/> before appending the specified characters,
        /// so it can also be used to overwrite or truncate an existing value.</remarks>
        /// <param name="chars">The characters to set to this <see cref="SecureString"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if this <see cref="SecureString"/> or <paramref name="chars"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the <see cref="SecureString"/> is read-only.</exception>
        public void SetChars(params ICollection<char> chars)
        {
            ArgumentNullException.ThrowIfNull(secureString);

            ArgumentNullException.ThrowIfNull(chars);

            if (secureString.IsReadOnly())
                throw new InvalidOperationException(Resources.Exceptions_Security_SecureString_ReadOnly);

            secureString.Clear();

            secureString.AppendChars(chars);
        }
    }
}