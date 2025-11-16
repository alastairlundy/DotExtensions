using System;
using System.IO;
using System.Linq;
using AlastairLundy.DotExtensions.Localizations;

namespace AlastairLundy.DotExtensions.IO.Permissions.Unix;

public static partial class UnixPermissionsExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="notation"></param>
    /// <returns></returns>
    public static bool IsValidNumericNotation(string notation)
    {
        if (notation.Length is >= 3 and <= 4 || int.TryParse(notation, out int result) == false) 
            return false;

        if (notation.Length == 4 && notation[0] != '0')
            return result is >= 0 and <= 4777 && notation.ToCharArray()
                .All(x => x != '8' && x != '9');
        else
            return result is >= 0 and <= 777 && notation.Length is >= 3 and <= 4;
    }
    
    private static UnixFileMode ParseNumericNotation(string notation)
    {
#if NET8_0_OR_GREATER
        ArgumentException.ThrowIfNullOrEmpty(notation, nameof(notation));
#endif
        
        if (!IsValidNumericNotation(notation))
            throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation);

        if(!notation.StartsWith("0"))
            notation =  notation.Remove(0, 1);
        
        int user = int.Parse(notation.First().ToString());
        int group = int.Parse(notation[^2].ToString());
        int others = int.Parse(notation.Last().ToString());
        
       UnixFileMode userPermissions = user switch
        {
            0 => UnixFileMode.None,
            1 => UnixFileMode.UserExecute,
            2 => UnixFileMode.UserWrite,
            3 => UnixFileMode.UserWrite | UnixFileMode.UserExecute,
            4 => UnixFileMode.UserRead,
            5 => UnixFileMode.UserRead | UnixFileMode.UserExecute,
            6 => UnixFileMode.UserRead | UnixFileMode.UserWrite,
            7 => UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute,
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation)
        };
        
        UnixFileMode groupPermissions =  group switch
        {
            0 => UnixFileMode.None,
            1 => UnixFileMode.GroupExecute,
            2 => UnixFileMode.GroupWrite,
            3 => UnixFileMode.GroupWrite | UnixFileMode.GroupExecute,
            4 => UnixFileMode.GroupRead,
            5 => UnixFileMode.GroupRead | UnixFileMode.GroupExecute,
            6 => UnixFileMode.GroupRead | UnixFileMode.GroupWrite,
            7 => UnixFileMode.GroupRead | UnixFileMode.GroupWrite | UnixFileMode.GroupExecute,
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation)
        };
        
        UnixFileMode othersPermissions = others switch
        {
            0 => UnixFileMode.None,
            1 => UnixFileMode.OtherExecute,
            2 => UnixFileMode.OtherWrite,
            3 => UnixFileMode.OtherWrite | UnixFileMode.OtherExecute,
            4 => UnixFileMode.OtherRead,
            5 => UnixFileMode.OtherRead | UnixFileMode.OtherExecute, 6 => UnixFileMode.OtherRead | UnixFileMode.OtherWrite,
            7 => UnixFileMode.OtherRead | UnixFileMode.OtherWrite | UnixFileMode.OtherExecute,
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation)
        };
        
        return othersPermissions | groupPermissions |  userPermissions;
    }
}