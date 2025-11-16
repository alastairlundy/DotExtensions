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

#if NET8_0_OR_GREATER

using System;
using System.IO;
using AlastairLundy.DotExtensions.Localizations;

namespace AlastairLundy.DotExtensions.IO.Permissions.Unix;


/// <summary>
/// 
/// </summary>
public static partial class UnixPermissionsExtensions
{
    /// <summary>
    /// 
    /// </summary>
    extension(UnixFileMode)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static UnixFileMode Parse(string input)
        {
            #if NET8_0_OR_GREATER
            ArgumentException.ThrowIfNullOrEmpty(input);
            #else
            input = Ensure.NotNullOrEmpty(input);
            #endif
            
            if(IsValidNumericNotation(input))
                return ParseNumericNotation(input);
            
            if(IsValidRwxSymbolNotation(input))
                return ParseRwxSymbolNotation(input);

            throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidSymbolicNotation.Replace("{x}",  input));
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
                result = Parse(input);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
    }
}
#endif
