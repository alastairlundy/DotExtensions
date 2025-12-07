/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using AlastairLundy.DotExtensions.Exceptions;
using AlastairLundy.DotPrimitives.IO.Permissions.Windows;

namespace AlastairLundy.DotExtensions.IO.Permissions.Windows;

/// <summary>
/// 
/// </summary>
public static class WindowsFilePermissionsExtensions
{
    /// <param name="fileInfo">The FileInfo object for which to retrieve the permission.</param>
    extension(FileInfo fileInfo)
    {
        /// <summary>
        /// Retrieves the Windows file permission for a given FileInfo object.
        /// </summary>
        /// <returns>A WindowsFilePermission indicating the permission of the specified file or directory.</returns>
        /// <exception cref="PlatformNotSupportedException">Thrown when the operation is performed on a platform that is not Windows-based.</exception>
        [SupportedOSPlatform("windows")]
        [UnsupportedOSPlatform("macos")]
        [UnsupportedOSPlatform("linux")]
        [UnsupportedOSPlatform("freebsd")]
        [UnsupportedOSPlatform("browser")]
        [UnsupportedOSPlatform("android")]
        [UnsupportedOSPlatform("ios")]
        public WindowsFilePermission GetWindowsFilePermission()
        {
            PlatformNotSupportedException.ThrowIfNotOSPlatform(OSPlatform.Windows);
        
            return WindowsFilePermissionManager.GetFilePermission(fileInfo.FullName);
        }
    }

    /// <param name="fileInfo">The FileInfo object for which to set the permission.</param>
    extension(FileInfo fileInfo)
    {
        /// <summary>
        /// Sets the Windows file permission for a given FileInfo object.
        /// </summary>
        /// <param name="permission">A WindowsFilePermission indicating the new permission of the specified file or directory.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown when the operation is performed on a platform that is not Windows-based.</exception>
        [SupportedOSPlatform("windows")]
        [UnsupportedOSPlatform("macos")]
        [UnsupportedOSPlatform("linux")]
        [UnsupportedOSPlatform("freebsd")]
        [UnsupportedOSPlatform("browser")]
        [UnsupportedOSPlatform("android")]
        [UnsupportedOSPlatform("ios")]
        public void SetWindowsFilePermission(WindowsFilePermission permission)
        {
            PlatformNotSupportedException.ThrowIfNotOSPlatform(OSPlatform.Windows);

            WindowsFilePermissionManager.SetFilePermission(fileInfo.FullName, permission);
        }
    }

    /// <param name="directoryInfo">The DirectoryInfo object for which to set the permission.</param>
    extension(DirectoryInfo directoryInfo)
    {
        /// <summary>
        /// Sets the Windows file permission for a given DirectoryInfo object.
        /// </summary>
        /// <param name="permission">The WindowsFilePermission to be assigned.</param>
        /// <exception cref="PlatformNotSupportedException">Thrown when the operation is performed on a platform that is not Windows-based.</exception>
        [SupportedOSPlatform("windows")]
        [UnsupportedOSPlatform("macos")]
        [UnsupportedOSPlatform("linux")]
        [UnsupportedOSPlatform("freebsd")]
        [UnsupportedOSPlatform("browser")]
        [UnsupportedOSPlatform("android")]
        [UnsupportedOSPlatform("ios")]
        public void SetWindowsDirectoryPermission(WindowsFilePermission permission)
        {
            PlatformNotSupportedException.ThrowIfNotOSPlatform(OSPlatform.Windows);

            WindowsFilePermissionManager.SetDirectoryPermission(directoryInfo.FullName, permission);
        }

        /// <summary>
        /// Retrieves the Windows directory permission for a given DirectoryInfo object.
        /// </summary>
        /// <returns>A WindowsFilePermission indicating the permission of the specified directory.</returns>
        /// <exception cref="PlatformNotSupportedException">Thrown when the operation is performed on a platform that is not Windows-based.</exception>
        [SupportedOSPlatform("windows")]
        [UnsupportedOSPlatform("macos")]
        [UnsupportedOSPlatform("linux")]
        [UnsupportedOSPlatform("freebsd")]
        [UnsupportedOSPlatform("browser")]
        [UnsupportedOSPlatform("android")]
        [UnsupportedOSPlatform("ios")]
        public WindowsFilePermission GetWindowsDirectoryPermission()
        {
            PlatformNotSupportedException.ThrowIfNotOSPlatform(OSPlatform.Windows);
            
            return WindowsFilePermissionManager.GetDirectoryPermission(directoryInfo.FullName);
        }
    }
}