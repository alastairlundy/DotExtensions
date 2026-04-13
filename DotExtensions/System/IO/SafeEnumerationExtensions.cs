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

namespace DotExtensions.IO;

/// <summary>
/// 
/// </summary>
public static class SafeEnumerationExtensions
{
    extension(SearchOption directorySearchOption)
    {
        /// <summary>
        /// Creates and configures an <see cref="EnumerationOptions"/> instance with predefined settings
        /// to support safe file enumeration while handling various directory traversal options.
        /// </summary>
        /// <param name="matchCasing">Specifies whether file and directory names should be matched case-sensitively.</param>
        /// <returns>Returns an <see cref="EnumerationOptions"/> object configured for safe and conditional file enumeration.</returns>
#if NET8_0_OR_GREATER
        public
#else
        internal
#endif
            EnumerationOptions ToEnumerationOptions(bool matchCasing = false)
        {
            return new EnumerationOptions
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = directorySearchOption == SearchOption.AllDirectories,
                MatchCasing = matchCasing ? MatchCasing.CaseSensitive : MatchCasing.CaseInsensitive,
                MatchType = MatchType.Simple,
                ReturnSpecialDirectories = false,
                MaxRecursionDepth = directorySearchOption == SearchOption.AllDirectories ? int.MaxValue : 0
            };
        }
    }
}