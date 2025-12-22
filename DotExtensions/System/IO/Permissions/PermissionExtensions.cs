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

using System.IO;
using DotPrimitives.IO.Permissions.Windows;

namespace DotExtensions.IO.Permissions;

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