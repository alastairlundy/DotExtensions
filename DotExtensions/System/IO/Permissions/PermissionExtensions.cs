/*
 * Copyright (c) 2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using System.IO;
using System.Runtime.Versioning;
using AlastairLundy.DotPrimitives.IO.Permissions.Windows;

namespace AlastairLundy.DotExtensions.IO.Permissions;

/// <summary>
/// Provides extension methods related to Windows and Unix file mode permission checks.
/// </summary>
public static partial class PermissionExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileInfo"></param>
    extension(FileInfo fileInfo)
    {
        /// <summary>
        /// Determines whether the specified file has execute permission.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the file has execute permission; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <see cref="FileInfo"/> object is null.
        /// </exception>
        [SupportedOSPlatform("windows")]
        [SupportedOSPlatform("macos")]
        [SupportedOSPlatform("maccatalyst")]
        [SupportedOSPlatform("linux")]
        [SupportedOSPlatform("freebsd")]
        [SupportedOSPlatform("android")]
        public bool HasExecutePermission()
        {
            ArgumentNullException.ThrowIfNull(fileInfo);

            try
            {
                if (OperatingSystem.IsWindows())
                {
                    WindowsFilePermission filePermission = WindowsFilePermissionManager
                        .GetFilePermission(fileInfo.FullName);

                    return filePermission.HasExecutePermission;
                }
                
                UnixFileMode unixFileMode = File.GetUnixFileMode(fileInfo.FullName);

                return unixFileMode.HasExecutePermission;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether the specified file has read permission.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the file has read permission; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <see cref="FileInfo"/> object is null.
        /// </exception>
        [SupportedOSPlatform("windows")]
        [SupportedOSPlatform("macos")]
        [SupportedOSPlatform("maccatalyst")]
        [SupportedOSPlatform("linux")]
        [SupportedOSPlatform("freebsd")]
        [SupportedOSPlatform("android")]
        public bool HasReadPermission()
        {
            ArgumentNullException.ThrowIfNull(fileInfo);

            try
            {
                if (OperatingSystem.IsWindows())
                {
                    WindowsFilePermission filePermission = WindowsFilePermissionManager
                        .GetFilePermission(fileInfo.FullName);

                    return filePermission.HasReadPermission;
                }
                
                UnixFileMode unixFileMode = File.GetUnixFileMode(fileInfo.FullName);

                return unixFileMode.HasReadPermission;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether the specified file has write permission.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the file has write permission; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <see cref="FileInfo"/> object is null.
        /// </exception>
        [SupportedOSPlatform("windows")]
        [SupportedOSPlatform("macos")]
        [SupportedOSPlatform("maccatalyst")]
        [SupportedOSPlatform("linux")]
        [SupportedOSPlatform("freebsd")]
        [SupportedOSPlatform("android")]
        public bool HasWritePermission()
        {
            ArgumentNullException.ThrowIfNull(fileInfo);

            try
            {
                if (OperatingSystem.IsWindows())
                {
                    WindowsFilePermission filePermission = WindowsFilePermissionManager
                        .GetFilePermission(fileInfo.FullName);

                    return filePermission.HasWritePermission;
                }
                
                UnixFileMode unixFileMode = File.GetUnixFileMode(fileInfo.FullName);

                return unixFileMode.HasWritePermission;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
    }
}