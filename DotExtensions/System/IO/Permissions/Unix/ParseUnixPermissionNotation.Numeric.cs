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
using System.Linq;

namespace AlastairLundy.DotExtensions.IO.Permissions.Unix;

/// <summary>
/// 
/// </summary>
public static partial class UnixPermissionsExtensions
{
    /// <summary>
    /// Validates if the provided numeric notation string represents a valid Unix permission in numeric format.
    /// </summary>
    /// <param name="notation">The numeric notation string to validate. It can be in three-digit (e.g., "777") or four-digit (e.g., "0777") formats.</param>
    /// <returns>
    /// True if the numeric notation string represents a valid Unix permission; otherwise, false.
    /// </returns>
    public static bool IsValidNumericNotation(string notation)
    {
        ArgumentException.ThrowIfNullOrEmpty(notation);

        if (notation.Length is >= 3 and <= 4 || int.TryParse(notation, out int result) == false) 
            return false;

        if (notation.Length == 4 && notation[0] != '0')
            return result is >= 0 and <= 4777 && notation.ToCharArray()
                .All(x => x != '8' && x != '9');
        else
            return result is >= 0 and <= 777 && notation.Length is >= 3 and <= 4;
    }

    /// <summary>
    /// Parses a numeric notation string representing Unix file permissions into a corresponding <see cref="UnixFileMode"/> value.
    /// </summary>
    /// <param name="notation">
    /// The numeric notation string to parse. It can be in three-digit (e.g., "777") or four-digit (e.g., "0777") formats.
    /// The notation must be valid, according to Unix file permission rules.
    /// </param>
    /// <returns>
    /// A <see cref="UnixFileMode"/> value that represents the parsed file permissions.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided <paramref name="notation"/> is null, empty, or not a valid Unix numeric notation format.
    /// </exception>
    private static UnixFileMode ParseNumericNotation(string notation)
    {
        ArgumentException.ThrowIfNullOrEmpty(notation);
        
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