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

using System;
using AlastairLundy.DotPrimitives.IO.Permissions.Windows;

namespace AlastairLundy.DotExtensions.IO.Permissions;

public static partial class PermissionExtensions
{
    /// <param name="permission">The Windows file permission to check.</param>
    extension(WindowsFilePermission permission)
    {
        /// <summary>
        /// Whether the specified Windows file permission has execute permission.
        /// </summary>
        public bool HasExecutePermission =>
            permission.HasFlag(WindowsFilePermission.GroupReadAndExecute) ||
            permission.HasFlag(WindowsFilePermission.SystemReadAndExecute) ||
            permission.HasFlag(WindowsFilePermission.UserReadAndExecute) ||
            permission.HasFlag(WindowsFilePermission.GroupFullControl) ||
            permission.HasFlag(WindowsFilePermission.UserFullControl) ||
            permission.HasFlag(WindowsFilePermission.SystemFullControl);

        /// <summary>
        /// Whether the specified Windows file permission has execute permission.
        /// </summary>
        public bool HasWritePermission =>
            permission.HasFlag(WindowsFilePermission.GroupWrite) ||
            permission.HasFlag(WindowsFilePermission.SystemWrite) ||
            permission.HasFlag(WindowsFilePermission.UserWrite) ||
            permission.HasFlag(WindowsFilePermission.GroupFullControl) ||
            permission.HasFlag(WindowsFilePermission.UserFullControl) ||
            permission.HasFlag(WindowsFilePermission.SystemFullControl);

        /// <summary>
        /// Whether the specified Windows file permission has execute permission.
        /// </summary>
        public bool HasReadPermission =>
            permission.HasFlag(WindowsFilePermission.GroupRead) ||
            permission.HasFlag(WindowsFilePermission.SystemRead) ||
            permission.HasFlag(WindowsFilePermission.UserRead) ||
            permission.HasFlag(WindowsFilePermission.GroupReadAndExecute) ||
            permission.HasFlag(WindowsFilePermission.SystemReadAndExecute) ||
            permission.HasFlag(WindowsFilePermission.UserReadAndExecute) ||
            permission.HasFlag(WindowsFilePermission.GroupFullControl) ||
            permission.HasFlag(WindowsFilePermission.UserFullControl) ||
            permission.HasFlag(WindowsFilePermission.SystemFullControl);
    }
}