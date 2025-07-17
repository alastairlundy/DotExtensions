

using AlastairLundy.DotPrimitives.IO.Permissions;

namespace AlastairLundy.DotExtensions.IO.Unix;

public static class UnixFilePermissionExecuteExtensions
{
    /// <summary>
    /// Determines whether the specified Unix file mode has execute permission.
    /// </summary>
    /// <param name="permission">The Unix file mode to check.</param>
    /// <returns>True if the mode includes execute permission, false otherwise.</returns>
    public static bool HasExecutePermission(this UnixFilePermission permission)
    {
        return permission.HasFlag(UnixFilePermission.GroupExecute) 
               || permission.HasFlag(UnixFilePermission.OtherExecute)
               || permission.HasFlag(UnixFilePermission.UserExecute);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    public static bool HasWritePermission(this UnixFilePermission permission)
    {
        return permission.HasFlag(UnixFilePermission.GroupWrite) 
               || permission.HasFlag(UnixFilePermission.OtherWrite)
               || permission.HasFlag(UnixFilePermission.UserWrite);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    public static bool HasReadPermission(this UnixFilePermission permission)
    {
        return permission.HasFlag(UnixFilePermission.GroupRead) 
               || permission.HasFlag(UnixFilePermission.OtherRead)
               || permission.HasFlag(UnixFilePermission.UserRead);
    }
}