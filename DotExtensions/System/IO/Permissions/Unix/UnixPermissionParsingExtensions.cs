/*
        MIT License

       Copyright (c) 2026 Alastair Lundy

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

#if NET8_0_OR_GREATER
namespace DotExtensions.IO.Permissions.Unix;

/// <summary>
/// Contains extension methods for handling Unix file permission operations.
/// </summary>
public static class UnixPermissionParsingExtensions
{
    /// <summary>
    /// Provides extension methods for working with Unix file permissions represented as <see cref="UnixFileMode"/>.
    /// </summary>
    extension(UnixFileMode)
    {
        /// <summary>
        /// Parses the provided string representation of a Unix file mode into a corresponding <see cref="UnixFileMode"/> object.
        /// </summary>
        /// <param name="input">The input string representing the Unix file mode in numeric or symbolic format.</param>
        /// <returns>
        /// The parsed <see cref="UnixFileMode"/> object if the input string is in a valid format.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the input string is null, empty, or does not represent a valid Unix file mode in either numeric or symbolic notation.
        /// </exception>
        public static UnixFileMode Parse(string input)
        {
            ArgumentException.ThrowIfNullOrEmpty(input);
            
            if(input.IsValidNumericNotation())
                return ParseNumericNotation(input);
            
            if(input.IsValidRwxSymbolNotation())
                return ParseRwxSymbolNotation(input);

            throw new ArgumentException(Resources.
                Exceptions_Permissions_Unix_InvalidSymbolicNotation.Replace("{x}",  input), nameof(input));
        }

        /// <summary>
        /// Attempts to parse the provided string into a <see cref="UnixFileMode"/> object.
        /// </summary>
        /// <param name="input">The input string representing the Unix file mode in either numeric or symbolic notation.</param>
        /// <param name="result">
        /// Upon returning, contains the parsed <see cref="UnixFileMode"/> object if the parsing was successful; otherwise, null.
        /// </param>
        /// <returns>
        /// True if the input string was successfully parsed into a <see cref="UnixFileMode"/> object; otherwise, false.
        /// </returns>
        public static bool TryParse(string input, out UnixFileMode? result)
        {
            try
            {
                result = UnixFileMode.Parse(input);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
    }
    
    #region Parsing Logic
        
    /// <summary>
    /// Parses a Unix file permission symbolic notation string (rwx format) into a corresponding <see cref="UnixFileMode"/> value.
    /// </summary>
    /// <param name="notation">The symbolic notation string representing Unix file permissions. The expected format is a 10-character string starting with a file type indicator (e.g. "-", "d"), followed by three permission segments for user, group, and others.</param>
    /// <returns>A <see cref="UnixFileMode"/> value representing the permissions described by the notation.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided <paramref name="notation"/> is not a valid symbolic notation format.</exception>
    private static UnixFileMode ParseRwxSymbolNotation(string notation)
    {
        ArgumentException.ThrowIfNullOrEmpty(notation);

        if (!notation.IsValidRwxSymbolNotation())
            throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidSymbolicNotation, nameof(notation));

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
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidSymbolicNotation, nameof(notation))
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
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidSymbolicNotation, nameof(notation))
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
    
    /// <summary>
    /// Parses a numeric notation string representing Unix file permissions into a corresponding <see cref="UnixFileMode"/> value.
    /// </summary>
    /// <param name="notation">
    /// The numeric notation string to parse. It can be in three-digit (e.g. "777") or four-digit (e.g. "0777") formats.
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
        
        if (!notation.IsValidNumericNotation())
            throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation, nameof(notation));

        if(!notation.StartsWith("0", StringComparison.OrdinalIgnoreCase))
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
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation, notation)
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
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation, nameof(notation))
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
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation, nameof(notation))
        };
        
        return othersPermissions | groupPermissions |  userPermissions;
    }
    #endregion
}
#endif