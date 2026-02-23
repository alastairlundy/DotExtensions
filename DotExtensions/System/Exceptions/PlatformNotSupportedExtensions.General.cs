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

// ReSharper disable InconsistentNaming

namespace DotExtensions.Exceptions;

/// <summary>
/// Contains extension methods for enhancing the handling of <see cref="PlatformNotSupportedException"/> exceptions.
/// </summary>
public static class PlatformNotSupportedExceptionExtensions
{
    /// <summary>
    /// Provides extension methods for handling <see cref="PlatformNotSupportedException"/> based on platform-specific checks.
    /// </summary>
    extension(PlatformNotSupportedException)
    {
        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform matches the
        /// specified <see cref="OSPlatform"/>.
        /// </summary>
        /// <param name="osPlatform">The <see cref="OSPlatform"/> to check against the current platform.</param>
        /// <param name="exceptionMessage">
        /// An optional custom exception message to include in the thrown <see cref="PlatformNotSupportedException"/>.
        /// If not provided, a default message is used.
        /// </param>
        /// <exception cref="PlatformNotSupportedException">
        /// Thrown when the current platform matches the specified <see cref="OSPlatform"/>.
        /// </exception>
        public static void ThrowIfOSPlatform(OSPlatform osPlatform, string? exceptionMessage = null)
        {
            if (RuntimeInformation.IsOSPlatform(osPlatform))
            {
                if (exceptionMessage is not null)
                    throw new PlatformNotSupportedException(exceptionMessage);
                    
                throw new PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform is one of the specified
        /// <see cref="OSPlatform"/> values.
        /// </summary>
        /// <param name="exceptionMessage">
        /// An optional custom exception message to include in the thrown <see cref="PlatformNotSupportedException"/>.
        /// If not provided, a default message is used.
        /// </param>
        /// <param name="platforms">An array of <see cref="OSPlatform"/> values to check against the current platform.</param>
        /// <exception cref="PlatformNotSupportedException">
        /// Thrown when the current platform matches any of the specified <see cref="OSPlatform"/> values.
        /// </exception>
        public static void ThrowIfOSPlatform(OSPlatform[] platforms, string? exceptionMessage = null)
        {
            foreach (OSPlatform platform in platforms)
            {
                if (RuntimeInformation.IsOSPlatform(platform))
                {
                    if (exceptionMessage is not null)
                        throw new PlatformNotSupportedException(exceptionMessage);
                    
                    throw new PlatformNotSupportedException();
                }
            }
        }

        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform does not match the
        /// specified <see cref="OSPlatform"/>.
        /// </summary>
        /// <param name="osPlatform">The <see cref="OSPlatform"/> to check against the current platform.</param>
        /// <param name="exceptionMessage">
        /// An optional custom exception message to include in the thrown <see cref="PlatformNotSupportedException"/>.
        /// If not provided, a default message is used.
        /// </param>
        /// <exception cref="PlatformNotSupportedException">
        /// Thrown when the current platform does not match the specified <see cref="OSPlatform"/>.
        /// </exception>
        public static void ThrowIfNotOSPlatform(OSPlatform osPlatform, string? exceptionMessage = null)
        {
            if (!RuntimeInformation.IsOSPlatform(osPlatform))
            {
                if (exceptionMessage is not null)
                    throw new PlatformNotSupportedException(exceptionMessage);
                
                throw new PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform does not match the
        /// specified <see cref="OSPlatform"/>.
        /// </summary>
        /// <param name="platforms">The platforms to check against the current platform.</param>
        /// <param name="exceptionMessage">
        ///     An optional custom exception message to include in the thrown <see cref="PlatformNotSupportedException"/>.
        ///     If not provided, a default message is used.
        /// </param>
        /// <exception cref="PlatformNotSupportedException">
        /// Thrown when the current platform does not match the specified <see cref="OSPlatform"/>.
        /// </exception>
        public static void ThrowIfNotOSPlatform(OSPlatform[] platforms, string? exceptionMessage = null)
        {
            bool foundOsPlatform = false;
            
            foreach (OSPlatform platform in platforms)
            {
                if (RuntimeInformation.IsOSPlatform(platform))
                {
                    foundOsPlatform = true;
                    break;
                }    
            }

            if (!foundOsPlatform)
            {
                if (exceptionMessage is not null)
                    throw new PlatformNotSupportedException(exceptionMessage);
                    
                throw new PlatformNotSupportedException();
            }
        }
    }
}