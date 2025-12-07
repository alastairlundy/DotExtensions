/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

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