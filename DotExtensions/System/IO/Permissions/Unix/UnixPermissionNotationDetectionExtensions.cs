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
/// 
/// </summary>
public static partial class UnixPermissionNotationDetectionExtensions
{
    /// <summary>
    /// Validates whether a given string represents a valid Unix file permission symbolic notation (rwx format).
    /// </summary>
    /// <param name="notation">The symbolic notation string to validate. The expected format is a 10-character string.</param>
    /// <returns>A boolean value indicating whether the input notation is valid.</returns>
    public static bool IsValidRwxSymbolNotation(string notation)
    {
        ArgumentException.ThrowIfNullOrEmpty(notation);

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
                // ReSharper disable once StringLiteralTypo
                "-rwxr-----" or
                // ReSharper disable once StringLiteralTypo
                "-rwxrwx---" or
                // ReSharper disable once StringLiteralTypo
                "-rwxrwxrwx" => true,
            _ => false
        };
    }
    
    /// <summary>
    /// Validates if the provided numeric notation string represents a valid Unix permission in numeric format.
    /// </summary>
    /// <param name="notation">The numeric notation string to validate. It can be in three-digit (e.g. "777") or four-digit (e.g. "0777") formats.</param>
    /// <returns>
    /// True if the numeric notation string represents a valid Unix permission; otherwise, false.
    /// </returns>
    public static bool IsValidNumericNotation(string notation)
    {
        ArgumentException.ThrowIfNullOrEmpty(notation);

        if (notation.Length is >= 3 and <= 4 || !int.TryParse(notation, out int result)) 
            return false;

        if (notation.Length == 4 && notation[0] != '0')
            return result is >= 0 and <= 4777 && notation.ToCharArray()
                .All(x => x != '8' && x != '9');
        
        return result is >= 0 and <= 777 && notation.Length is >= 3 and <= 4;
    }
}