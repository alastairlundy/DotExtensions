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

using AlastairLundy.DotPrimitives.IO.Permissions;

namespace AlastairLundy.DotExtensions.DotPrimitives.IO.Unix;

/// <summary>
/// 
/// </summary>
internal static class UnixFilePermissionExecuteExtensions
{
    /// <summary>
    /// Determines whether the specified Unix file mode has execute permission.
    /// </summary>
    /// <param name="permission">The Unix file permission to check.</param>
    /// <returns>True if the permission includes execute permission, false otherwise.</returns>
    public static bool HasExecutePermission(this UnixFilePermission permission)
    {
        return permission.HasFlag(UnixFilePermission.GroupExecute) 
               || permission.HasFlag(UnixFilePermission.OtherExecute)
               || permission.HasFlag(UnixFilePermission.UserExecute);
    }

    /// <summary>
    /// Determines whether the specified Unix file permission has write permission.
    /// </summary>
    /// <param name="permission">The Unix file permission to check.</param>
    /// <returns>True if the permission includes write permission, false otherwise.</returns>
    public static bool HasWritePermission(this UnixFilePermission permission)
    {
        return permission.HasFlag(UnixFilePermission.GroupWrite) 
               || permission.HasFlag(UnixFilePermission.OtherWrite)
               || permission.HasFlag(UnixFilePermission.UserWrite);
    }

    /// <summary>
    /// Determines whether the specified Unix file permission has read permission.
    /// </summary>
    /// <param name="permission">The Unix file permission to check.</param>
    /// <returns>True if the permission includes read permission, false otherwise.</returns>
    public static bool HasReadPermission(this UnixFilePermission permission)
    {
        return permission.HasFlag(UnixFilePermission.GroupRead) 
               || permission.HasFlag(UnixFilePermission.OtherRead)
               || permission.HasFlag(UnixFilePermission.UserRead);
    }
}