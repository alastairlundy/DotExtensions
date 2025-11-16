using System;
using System.IO;
using AlastairLundy.DotExtensions.Localizations;

namespace AlastairLundy.DotExtensions.IO.Permissions.Unix;

public static partial class UnixPermissionsExtensions
{
    public static bool IsValidSymbolicNotation(string notation)
    {
        
    }

    private static UnixFileMode ParseSymbolicNotation(string notation)
    {
        if (!IsValidSymbolicNotation(notation))
            throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidSymbolicNotation);

    }
}