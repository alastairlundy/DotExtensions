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
    private const int MaxSafeRecursionDepth = 64;

    extension(SearchOption directorySearchOption)
    {
        /// <summary>
        /// Creates and configures an <see cref="EnumerationOptions"/> instance with predefined settings
        /// to support safe file enumeration while handling various directory traversal options.
        /// </summary>
        /// <param name="matchCasing">Specifies whether file and directory names should be matched case-sensitively.</param>
        /// <returns>Returns an <see cref="EnumerationOptions"/> object configured for safe and conditional file enumeration.</returns>
        /// <remarks>
        /// <para>
        /// <see cref="EnumerationOptions.AttributesToSkip"/> is set to <c>FileAttributes.None</c>, so hidden,
        /// system, and Unix dot-prefixed entries are included in results instead of being filtered out as the BCL
        /// default (<c>FileAttributes.Hidden | FileAttributes.System</c>) would do. Only inaccessible items are
        /// skipped, via <see cref="EnumerationOptions.IgnoreInaccessible"/>.
        /// </para>
        /// <para>
        /// When recursing (<see cref="SearchOption.AllDirectories"/>),
        /// <see cref="EnumerationOptions.MaxRecursionDepth"/> is capped at <c>64</c>
        /// (<c>MaxSafeRecursionDepth</c>) rather than <c>int.MaxValue</c>, so cyclic symbolic links, junctions,
        /// and other reparse points within the enumerated tree terminate instead of enumerating forever.
        /// </para>
        /// </remarks>
#if NET8_0_OR_GREATER
        public
#else
        internal
#endif
            EnumerationOptions ToEnumerationOptions(bool matchCasing = false)
        {
            EnumerationOptions options = new EnumerationOptions
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = directorySearchOption == SearchOption.AllDirectories,
                MatchCasing = matchCasing ? MatchCasing.CaseSensitive : MatchCasing.CaseInsensitive,
                MatchType = MatchType.Simple,
                ReturnSpecialDirectories = false,
                MaxRecursionDepth = directorySearchOption == SearchOption.AllDirectories ? MaxSafeRecursionDepth : 0
            };

#if NET5_0_OR_GREATER
            options.AttributesToSkip = FileAttributes.None;
#else
#pragma warning disable MA0099 // FileAttributes.None does not exist on netstandard2.0; 0 means "skip nothing".
            options.AttributesToSkip = 0;
#pragma warning restore MA0099
#endif

            return options;
        }
    }
}