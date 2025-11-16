/*
        MIT License
       
       Copyright (c) 2024-2025 Alastair Lundy
       
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
            bool HasExecutePermission =>
                mode.HasFlag(UnixFileMode.UserExecute)
                || mode.HasFlag(UnixFileMode.GroupExecute)
                || mode.HasFlag(UnixFileMode.OtherExecute);
        
        /// <summary>
        /// Whether the specified Unix file mode has read permission.
        /// </summary>
#if NET8_0_OR_GREATER
        public
#else
            internal
#endif
            bool HasReadPermission =>
                mode.HasFlag(UnixFileMode.UserRead)
                || mode.HasFlag(UnixFileMode.GroupRead)
                || mode.HasFlag(UnixFileMode.OtherRead);
        
        /// <summary>
        /// Whether the specified Unix file mode has write permission.
        /// </summary>
#if NET8_0_OR_GREATER
        public
#else
            internal
#endif
        bool HasWritePermission =>
            mode.HasFlag(UnixFileMode.UserWrite)
            || mode.HasFlag(UnixFileMode.GroupWrite)
            || mode.HasFlag(UnixFileMode.OtherWrite);
    }
    
}
