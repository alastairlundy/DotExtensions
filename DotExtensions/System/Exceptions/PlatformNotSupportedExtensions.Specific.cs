/*
 * Copyright (c) 2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

// ReSharper disable InconsistentNaming
namespace AlastairLundy.DotExtensions.Exceptions;

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
        /// <param name="exceptionMessage">Optional exception message to include in the thrown exception. If null, a default message will be used.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown if the application is running on the iOS platform.</exception>
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
        /// <param name="exceptionMessage">Optional exception message to include in the thrown exception. If null, a default message will be used.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown if the application is running on the watchOS platform.</exception>
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
        /// <param name="exceptionMessage">Optional exception message to include in the thrown exception. If null, a default message will be used.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown if the application is running on the tvOS platform.</exception>
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
        /// <param name="exceptionMessage">Optional exception message to include in the thrown exception. If null, a default message will be used.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown if the application is running on a browser platform.</exception>
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
        /// <param name="exceptionMessage">Optional exception message to include in the thrown exception. If null, a default message will be used.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown if the application is running on the Android platform.</exception>
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
        /// Optional exception message to include in the thrown exception. If null, a default message will be used.
        /// </param>
        /// <exception cref="PlatformNotSupportedException">
        /// Thrown if the application is running on the macOS or Mac Catalyst platform.
        /// </exception>
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
        /// <param name="exceptionMessage">Optional exception message to include in the thrown exception. If null, a default message will be used.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown if the application is running on the Linux platform.</exception>
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
        /// <param name="exceptionMessage">Optional exception message to include in the thrown exception. If null, a default message will be used.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown if the application is running on the FreeBSD platform.</exception>
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
        /// <param name="exceptionMessage">Optional exception message to include in the thrown exception. If null, a default message will be used.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown if the application is running on the Windows platform.</exception>
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