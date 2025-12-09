/*
        MIT License
       
       Copyright (c) 2025 Alastair Lundy
       
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

namespace AlastairLundy.DotExtensions.Exceptions;

/// <summary>
/// 
/// </summary>
public static class ArgumentExceptionExtensions
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