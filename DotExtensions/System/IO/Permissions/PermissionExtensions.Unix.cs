/*
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using System.IO;

namespace AlastairLundy.DotExtensions.IO.Permissions;

public static partial class PermissionExtensions
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mode"></param>
    extension(UnixFileMode mode)
    {
        /// <summary>
        /// Whether the specified Unix file mode has execute permission.
        /// </summary>
#if NET8_0_OR_GREATER
        public
#else
            internal
#endif
            bool HasExecutePermission
        {
            get
            {
                if (mode.HasFlag(UnixFileMode.UserExecute)
                    || mode.HasFlag(UnixFileMode.GroupExecute)
                    || mode.HasFlag(UnixFileMode.OtherExecute)) return true;
                return false;
            }
        }

        /// <summary>
        /// Whether the specified Unix file mode has read permission.
        /// </summary>
#if NET8_0_OR_GREATER
        public
#else
            internal
#endif
            bool HasReadPermission
        {
            get
            {
                if (mode.HasFlag(UnixFileMode.UserRead)
                    || mode.HasFlag(UnixFileMode.GroupRead)
                    || mode.HasFlag(UnixFileMode.OtherRead)) return true;
                return false;
            }
        }

        /// <summary>
        /// Whether the specified Unix file mode has write permission.
        /// </summary>
#if NET8_0_OR_GREATER
        public
#else
            internal
#endif
        bool HasWritePermission
        {
            get
            {
                if (mode.HasFlag(UnixFileMode.UserWrite)
                    || mode.HasFlag(UnixFileMode.GroupWrite)
                    || mode.HasFlag(UnixFileMode.OtherWrite)) return true;
                return false;
            }
        }
    }
    
}
