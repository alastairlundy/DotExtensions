/*
 * Copyright (c) 2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using System.Security;

namespace AlastairLundy.DotExtensions.Exceptions;

/// <summary>
/// Provides extension methods for working with <see cref="ArgumentException"/>
/// to enhance functionality and handling of exceptions in various contexts.
/// </summary>
public static partial class ArgumentExceptionExtensions
{
    /// <summary>
    /// Provides extension methods for handling <see cref="ArgumentNullException"/> related to <see cref="SecureString"/> values.
    /// </summary>
    extension(ArgumentException)
    {
        /// <summary>
        /// Throws an exception if a <see cref="SecureString"/> is null or has a length of zero.
        /// </summary>
        /// <param name="secureString">The <see cref="SecureString"/> to validate.</param>
        /// <param name="name">
        /// The name of the parameter being validated. If not provided, defaults to the name of the <see cref="SecureString"/> parameter.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="secureString"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="secureString"/> has a length of zero.</exception>
        public static void ThrowIfNullOrEmpty(SecureString? secureString, string name = "")
        {
            if (name.Length == 0)
                name = nameof(secureString);

            if (secureString is null)
                throw new ArgumentNullException(name);

            if (secureString.Length == 0)
                throw new ArgumentException(Resources.Exceptions_ThrowIfNullOrEmpty_Empty, name);
        }

        /// <summary>
        /// Throws an exception if a <see cref="SecureString"/> is null or contains only whitespace characters.
        /// </summary>
        /// <param name="secureString">The <see cref="SecureString"/> to validate.</param>
        /// <param name="name">
        /// The name of the parameter being validated. If not provided, defaults to the name of the <see cref="SecureString"/> parameter.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="secureString"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="secureString"/> contains only whitespace characters.</exception>
        public static void ThrowIfNullOrWhiteSpace(SecureString? secureString, string name = "")
        {
            if (name.Length == 0)
                name = nameof(secureString);

            if (secureString is null)
                throw new ArgumentNullException(name);

            SecureString whitespace = new();

            for (int i = 0; i < secureString.Length; i++)
            {
                whitespace.AppendChar(' ');
            }
            
            if (secureString.Equals(whitespace))
                throw new ArgumentException(Resources.Exceptions_ThrowIfNullOrEmpty_Empty, name);
        }
    }
}