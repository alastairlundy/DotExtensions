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

            if (input.IsValidNumericNotation())
                return ParseNumericNotation(input);

            if (input.IsValidRwxSymbolNotation())
                return ParseRwxSymbolNotation(input);

            throw new ArgumentException(Resources.
                Exceptions_Permissions_Unix_InvalidSymbolicNotation.Replace("{x}", input), nameof(input));
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

        if (notation.Length == 10)
            notation = notation.Remove(0, 1);

        string userPermissionStr = notation[..3];
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
    /// For four-digit notations the first digit is the set-user-ID/set-group-ID/sticky bit
    /// (1 = set-user-ID, 2 = set-group-ID, 4 = sticky bit, or a sum of those), and the last three digits
    /// are the user, group, and other permission triples respectively.
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

        // Normalize to exactly four digits: an optional leading setuid/setgid/sticky digit
        // followed by the user, group and other permission digits. Never unpad - a three-digit
        // input such as "077" must keep its leading zero as part of the user digit.
        string padded = notation.PadLeft(4, '0');

        int special = padded[0] - '0';
        int user = padded[1] - '0';
        int group = padded[2] - '0';
        int others = padded[3] - '0';

        UnixFileMode specialPermissions = special switch
        {
            0 => UnixFileMode.None,
            1 => UnixFileMode.SetUser,
            2 => UnixFileMode.SetGroup,
            3 => UnixFileMode.SetUser | UnixFileMode.SetGroup,
            4 => UnixFileMode.StickyBit,
            5 => UnixFileMode.SetUser | UnixFileMode.StickyBit,
            6 => UnixFileMode.SetGroup | UnixFileMode.StickyBit,
            7 => UnixFileMode.SetUser | UnixFileMode.SetGroup | UnixFileMode.StickyBit,
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation, nameof(notation))
        };

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
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation, nameof(notation))
        };

        UnixFileMode groupPermissions = group switch
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
            5 => UnixFileMode.OtherRead | UnixFileMode.OtherExecute,
            6 => UnixFileMode.OtherRead | UnixFileMode.OtherWrite,
            7 => UnixFileMode.OtherRead | UnixFileMode.OtherWrite | UnixFileMode.OtherExecute,
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation, nameof(notation))
        };

        return othersPermissions | groupPermissions | userPermissions | specialPermissions;
    }
    #endregion
}