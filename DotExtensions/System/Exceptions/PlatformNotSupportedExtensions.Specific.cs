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

// ReSharper disable InconsistentNaming
namespace DotExtensions.Exceptions;

public static partial class PlatformNotSupportedExceptionExtensions
{
    /// <summary>
    /// Provides extension methods to throw <see cref="PlatformNotSupportedException"/>
    /// for specific operating system platforms.
    /// </summary>
    extension(PlatformNotSupportedException)
    {
        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform is iOS.
        /// </summary>
        /// <param name="exceptionMessage">Optional exception message to include in the thrown exception. If null, a default message is used.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown if the application is running on the iOS platform.</exception>
        [Obsolete(DeprecationMessages.DeprecationV10)]
        public static void ThrowIfIOS(string? exceptionMessage = null)
        {
            if (OperatingSystem.IsIOS())
            {
                if (exceptionMessage is not null)
                    throw new PlatformNotSupportedException(exceptionMessage);
                
                throw new PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform is watchOS.
        /// </summary>
        /// <param name="exceptionMessage">Optional exception message to include in the thrown exception.
        /// If null, a default message is used.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown if the application is running on the watchOS platform.</exception>
        [Obsolete(DeprecationMessages.DeprecationV10)]
        public static void ThrowIfWatchOS(string? exceptionMessage = null)
        {
            if (OperatingSystem.IsWatchOS())
            {
                if (exceptionMessage is not null)
                    throw new PlatformNotSupportedException(exceptionMessage);
                
                throw new PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform is tvOS.
        /// </summary>
        /// <param name="exceptionMessage">Optional exception message to include in the thrown exception. If null, a default message is used.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown if the application is running on the tvOS platform.</exception>
        [Obsolete(DeprecationMessages.DeprecationV10)]
        public static void ThrowIfTvOS(string? exceptionMessage = null)
        {
            if (OperatingSystem.IsTvOS())
            {
                if (exceptionMessage is not null)
                    throw new PlatformNotSupportedException(exceptionMessage);
                
                throw new PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform is a browser.
        /// </summary>
        /// <param name="exceptionMessage">Optional exception message to include in the thrown exception. If null, a default message is used.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown if the application is running on a browser platform.</exception>
        [Obsolete(DeprecationMessages.DeprecationV10)]
        public static void ThrowIfBrowser(string? exceptionMessage = null)
        {
            if (OperatingSystem.IsBrowser())
            {
                if (exceptionMessage is not null)
                    throw new PlatformNotSupportedException(exceptionMessage);
                
                throw new PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform is Android.
        /// </summary>
        /// <param name="exceptionMessage">Optional exception message to include in the thrown exception. If null, a default message is used.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown if the application is running on the Android platform.</exception>
        [Obsolete(DeprecationMessages.DeprecationV10)]
        public static void ThrowIfAndroid(string? exceptionMessage = null)
        {
            if (OperatingSystem.IsAndroid())
            {
                if (exceptionMessage is not null)
                    throw new PlatformNotSupportedException(exceptionMessage);

                throw new PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform is macOS or Mac Catalyst.
        /// </summary>
        /// <param name="exceptionMessage">
        /// Optional exception message to include in the thrown exception. If null, a default message is used.
        /// </param>
        /// <exception cref="PlatformNotSupportedException">
        /// Thrown if the application is running on the macOS or Mac Catalyst platform.
        /// </exception>
        [Obsolete(DeprecationMessages.DeprecationV10)]
        public static void ThrowIfMacOs(string? exceptionMessage = null)
        {
            if (OperatingSystem.IsMacOS() || OperatingSystem.IsMacCatalyst())
            {
                if (exceptionMessage is not null)
                    throw new PlatformNotSupportedException(exceptionMessage);

                throw new PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform is Linux.
        /// </summary>
        /// <param name="exceptionMessage">Optional exception message to include in the thrown exception. If null, a default message is used.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown if the application is running on the Linux platform.</exception>
        [Obsolete(DeprecationMessages.DeprecationV10)]
        public static void ThrowIfLinux(string? exceptionMessage = null)
        {
            if (OperatingSystem.IsLinux())
            {
                if (exceptionMessage is not null)
                    throw new PlatformNotSupportedException(exceptionMessage);

                throw new PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform is FreeBSD.
        /// </summary>
        /// <param name="exceptionMessage">Optional exception message to include in the thrown exception. If null, a default message is used.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown if the application is running on the FreeBSD platform.</exception>
        [Obsolete(DeprecationMessages.DeprecationV10)]
        public static void ThrowIfFreeBSD(string? exceptionMessage = null)
        {
            if (OperatingSystem.IsFreeBSD())
            {
                if (exceptionMessage is not null)
                    throw new PlatformNotSupportedException(exceptionMessage);
                
                throw new PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform is Windows.
        /// </summary>
        /// <param name="exceptionMessage">Optional exception message to include in the thrown exception. If null, a default message is used.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown if the application is running on the Windows platform.</exception>
        [Obsolete(DeprecationMessages.DeprecationV10)]
        public static void ThrowIfWindows(string? exceptionMessage = null)
        {
            if (OperatingSystem.IsWindows())
            {
                if (exceptionMessage is not null)
                    throw new PlatformNotSupportedException(exceptionMessage);
                
                throw new PlatformNotSupportedException();
            }
        }
    }
}