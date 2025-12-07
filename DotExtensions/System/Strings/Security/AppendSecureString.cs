/*
 * Copyright (c) 2020-2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using System.Collections.Generic;
using System.Security;
using AlastairLundy.DotExtensions.Exceptions;

namespace AlastairLundy.DotExtensions.Strings.Security;

/// <summary>
/// Provides extension methods for securely modifying instances of <see cref="SecureString"/>.
/// </summary>
public static class AppendSecureStringExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="secureString">The <see cref="SecureString"/> to append a value to.</param>
    extension(SecureString secureString)
    {
        /// <summary>
        /// Appends a string to this <see cref="SecureString"/>.
        /// </summary>
        /// <param name="value">The string to append to this <see cref="SecureString"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown if this <see cref="SecureString"/> is read-only.</exception>
        public void AppendString(string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(secureString);
            ArgumentException.ThrowIfNullOrEmpty(value);
            
            if (secureString.IsReadOnly())
                throw new InvalidOperationException();

            foreach (char c in value)
            {
                secureString.AppendChar(c);
            }
        }

        /// <summary>
        /// Appends a sequence of characters to this <see cref="SecureString"/>.
        /// </summary>
        /// <param name="chars">The sequence of characters to append to this <see cref="SecureString"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown if this <see cref="SecureString"/> is read-only.</exception>
        public void AppendChars(params IEnumerable<char> chars)
        {
            ArgumentException.ThrowIfNullOrEmpty(secureString);
            ArgumentNullException.ThrowIfNull(chars);

            if (secureString.IsReadOnly())
                throw new InvalidOperationException();

            foreach (char c in chars)
            {
                secureString.AppendChar(c);
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="secureString">The <see cref="SecureString"/> to set a value to.</param>
    extension(SecureString secureString)
    {
        /// <summary>
        /// Sets this <see cref="SecureString"/> to the specified characters.
        /// </summary>
        /// <param name="chars">The characters to set to this <see cref="SecureString"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown if the <see cref="SecureString"/> is read-only and the number of characters
        /// to set is greater than the length of this <see cref="SecureString"/>.</exception>
        public void SetChars(params ICollection<char> chars)
        {
            ArgumentException.ThrowIfNullOrEmpty(secureString);
            ArgumentNullException.ThrowIfNull(chars);

            if (secureString.IsReadOnly() && chars.Count > secureString.Length)
                throw new InvalidOperationException();

            if (secureString.IsReadOnly() && chars.Count == secureString.Length)
            {
                int i = 0;
                foreach (char c in chars)
                {
                    secureString.SetAt(i, c);
                    i++;
                }
            }
            else if (secureString.IsReadOnly())
            {
                secureString.Clear();
                secureString = new SecureString();
                secureString.AppendChars(chars);
                secureString.MakeReadOnly();
            }
            else
            {
                secureString.Clear();

                secureString.AppendChars(chars);
            }

            foreach (char c in chars)
            {
                secureString.AppendChar(c);
            }
        }

        /// <summary>
        /// Sets this <see cref="SecureString"/> to the specified value.
        /// </summary>
        /// <param name="value">The value to set to the <see cref="SecureString"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown if the <see cref="SecureString"/> is read-only and the new value is
        /// greater than the length of this <see cref="SecureString"/>.</exception>
        public void SetString(string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(secureString);
            ArgumentException.ThrowIfNullOrEmpty(value);
            
            if (secureString.IsReadOnly() && value.Length > secureString.Length)
                throw new InvalidOperationException();

            if (secureString.IsReadOnly() && value.Length == secureString.Length)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    secureString.SetAt(i, value[i]);
                }
            }
            else if (secureString.IsReadOnly())
            {
                secureString.Clear();
                secureString = new SecureString();
                secureString.AppendString(value);
                secureString.MakeReadOnly();
            }
            else
            {
                secureString.Clear();

                secureString.AppendString(value);
            }
        }
    }
}
