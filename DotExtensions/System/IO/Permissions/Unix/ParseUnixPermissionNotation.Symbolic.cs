using System;
using System.IO;
using AlastairLundy.DotExtensions.Localizations;

namespace AlastairLundy.DotExtensions.IO.Permissions.Unix;

public static partial class UnixPermissionsExtensions
{
    /// <summary>
    /// Validates whether a given string represents a valid Unix file permission symbolic notation (rwx format).
    /// </summary>
    /// <param name="notation">The symbolic notation string to validate. The expected format is a 10-character string.</param>
    /// <returns>A boolean value indicating whether the input notation is valid.</returns>
    public static bool IsValidRwxSymbolNotation(string notation)
    {
        if (notation.Length != 10) 
            return false;
        
        return notation switch
        {
            "----------" or
                "---x--x--x" or
                "--w--w--w-" or
                "--wx-wx-wx" or
                "-r--r--r--" or
                "-r-xr-xr-x" or
                "-rw-rw-rw-" or
                "-rwx------" or
                "-rwxr-----" or
                "-rwxrwx---" or
                "-rwxrwxrwx" => true,
            _ => false
        };
    }

    /// <summary>
    /// Parses a Unix file permission symbolic notation string (rwx format) into a corresponding <see cref="UnixFileMode"/> value.
    /// </summary>
    /// <param name="notation">The symbolic notation string representing Unix file permissions. The expected format is a 10-character string starting with a file type indicator (e.g., "-", "d"), followed by three permission segments for user, group, and others.</param>
    /// <returns>A <see cref="UnixFileMode"/> value representing the permissions described by the notation.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided <paramref name="notation"/> is not a valid symbolic notation format.</exception>
    private static UnixFileMode ParseRwxSymbolNotation(string notation)
    {
        if (!IsValidRwxSymbolNotation(notation))
            throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidSymbolicNotation);

        notation = notation.Remove(0, 1);
        string userPermissionStr = notation.Substring(0, 3);
        string groupPermissionStr = notation.Substring(3, 3);
        string otherPermissionStr = notation.Substring(6, 3);

        UnixFileMode userPermissions = userPermissionStr switch
        {
            "---" => UnixFileMode.None,
            "--x" => UnixFileMode.UserExecute,
            "-w-" => UnixFileMode.UserWrite,
            "-wx" => UnixFileMode.UserWrite | UnixFileMode.UserExecute,
            "r--" => UnixFileMode.UserRead,
            "rw-" => UnixFileMode.UserRead | UnixFileMode.UserWrite,
            "rwx" => UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute,
            "r-x" => UnixFileMode.UserRead | UnixFileMode.UserExecute,
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidSymbolicNotation)
        };

        UnixFileMode groupPermissions = groupPermissionStr switch
        {
            "---" => UnixFileMode.None,
            "--x" => UnixFileMode.GroupExecute,
            "-w-" => UnixFileMode.GroupWrite,
            "-wx" => UnixFileMode.GroupWrite | UnixFileMode.GroupExecute,
            "r--" => UnixFileMode.GroupRead,
            "rw-" => UnixFileMode.GroupRead | UnixFileMode.GroupWrite,
            "rwx" => UnixFileMode.GroupRead | UnixFileMode.GroupWrite | UnixFileMode.GroupExecute,
            "r-x" => UnixFileMode.GroupRead | UnixFileMode.GroupExecute,
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidSymbolicNotation)
        };

        UnixFileMode othersPermissions = otherPermissionStr switch
        {
            "---" => UnixFileMode.None,
            "--x" => UnixFileMode.OtherExecute,
            "-w-" => UnixFileMode.OtherWrite,
            "-wx" => UnixFileMode.OtherWrite | UnixFileMode.OtherExecute,
            "r--" => UnixFileMode.OtherRead,
            "rw-" => UnixFileMode.OtherRead | UnixFileMode.OtherWrite,
            "rwx" => UnixFileMode.OtherRead | UnixFileMode.OtherWrite | UnixFileMode.OtherExecute,
            "r-x" => UnixFileMode.OtherRead | UnixFileMode.OtherExecute,
            _ => UnixFileMode.None
        };

        return userPermissions | groupPermissions | othersPermissions;

    }
}