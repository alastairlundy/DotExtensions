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

namespace AlastairLundy.DotExtensions.IO.Unix;

/// <summary>
/// 
/// </summary>
public static class UnixFilePermissionNotationDetection
{
    
    /// <summary>
    /// Detects whether a Unix Octal file permission notation is valid.
    /// </summary>
    /// <param name="notation">The numeric notation to be compared.</param>
    /// <returns>True if a valid unix file permission octal notation has been provided; false otherwise.</returns>
    public static bool IsNumericNotation(this string notation)
    {
        if (notation.Length is >= 3 and <= 4 || int.TryParse(notation, out int result) == false) 
            return false;
        
        return result is >= 0 and <= 777 && notation.Length is >= 3 and <= 4;
    }
    
    /// <summary>
    /// Detects whether a Unix symbolic file permission is valid.
    /// </summary>
    /// <param name="notation">The symbolic notation to be compared.</param>
    /// <returns>True if a valid unix file permission symbolic notation has been provided; false otherwise.</returns>
    public static bool IsSymbolicNotation(this string notation)
    {
        if (notation.Length != 10) 
            return false;
        
#if NET6_0_OR_GREATER
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
#else
        return notation == "----------" ||
               notation == "---x--x--x" ||
               notation == "--w--w--w-" ||
               notation == "--wx-wx-wx" ||
               notation == "-r--r--r--" ||
               notation == "-r-xr-xr-x" ||
               notation == "-rw-rw-rw-" ||
               notation == "-rwx------" ||
               notation == "-rwxr-----" ||
               notation == "-rwxrwx---" ||
               notation == "-rwxrwxrwx";
#endif
    }
}