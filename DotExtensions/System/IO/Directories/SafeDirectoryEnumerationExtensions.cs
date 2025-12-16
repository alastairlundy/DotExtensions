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

using System.Collections.Generic;
using System.IO;
using System.Linq;

// ReSharper disable InconsistentNaming

#if NETSTANDARD2_0
using System.Linq;
#endif

namespace AlastairLundy.DotExtensions.IO.Directories;

public static partial class SafeIOEnumerationExtensions
{
    /// <summary>
    /// Provides extension methods for safely enumerating directories. This ensures that
    /// inaccessible directories or exceptions during directory traversal are skipped without
    /// disrupting the enumeration process.
    /// </summary>
    extension(DirectoryInfo directoryInfo)
    {
        #region Safe Directory Enumeration

        /// <summary>
        /// Safely enumerates directories in the current directory, ignoring
        /// inaccessible directories and handling exceptions that may occur during
        /// directory traversal.
        /// </summary>
        /// <returns>
        /// A sequence of <see cref="DirectoryInfo"/> objects representing the
        /// directories found in the current directory based on the default pattern "*".
        /// </returns>
        public IEnumerable<DirectoryInfo> SafelyEnumerateDirectories()
            => SafelyEnumerateDirectories(directoryInfo, "*");
        
        /// <summary>
        /// Safely enumerates all directories in the specified directory, handling potential
        /// exceptions and ignoring inaccessible directories.
        /// </summary>
        /// <param name="searchPattern">
        /// The search string to match against the names of directories. This parameter can contain
        /// a combination of valid literal path and wildcard (* and ?) characters, but it doesn't
        /// support regular expressions. </param>
        /// <returns>
        /// A sequence of <see cref="DirectoryInfo"/> objects representing the directories
        /// found in the specified directory.
        /// </returns>
        public IEnumerable<DirectoryInfo> SafelyEnumerateDirectories(string searchPattern)
            => SafelyEnumerateDirectories(directoryInfo, searchPattern, SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Safely enumerates directories in the specified directory, ignoring inaccessible
        /// directories and handling exceptions that may occur during directory traversal.
        /// </summary>
        /// <param name="searchPattern">
        /// The search string to match against the names of directories. This parameter can contain
        /// a combination of valid literal path and wildcard (* and ?) characters, but it doesn't
        /// support regular expressions.
        /// </param>
        /// <param name="searchOption">
        /// Specifies whether the search operation should include all subdirectories (AllDirectories)
        /// or only the current directory (TopDirectoryOnly).
        /// </param>
        /// <param name="ignoreCase">Whether to ignore the case of the directories, true by default.</param>
        /// <returns>
        /// A sequence of <see cref="DirectoryInfo"/> objects representing the directories
        /// found in the specified directory that match the search pattern and search option.
        /// </returns>
        public IEnumerable<DirectoryInfo> SafelyEnumerateDirectories(string searchPattern, SearchOption searchOption, bool ignoreCase = true)
        {
#if NETSTANDARD2_1 || NET8_0_OR_GREATER
            return directoryInfo.SafeDirectoryEnumeration_Net8Plus(searchPattern, searchOption, ignoreCase);
#else
            return directoryInfo.SafeDirectoryEnumeration_NetStandard20(searchPattern, searchOption, ignoreCase);
#endif
        }
        
#if NET8_0_OR_GREATER || NETSTANDARD2_1
        private IEnumerable<DirectoryInfo> SafeDirectoryEnumeration_Net8Plus(string searchPattern,
            SearchOption searchOption, bool ignoreCase)
        {
            EnumerationOptions enumerationOptions = new()
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = searchOption == SearchOption.AllDirectories,
                ReturnSpecialDirectories = true,
                MatchType = MatchType.Simple,
                MatchCasing = ignoreCase ? MatchCasing.CaseSensitive : MatchCasing.CaseInsensitive,
                MaxRecursionDepth = searchOption == SearchOption.AllDirectories ? int.MaxValue : 0
            };

            return directoryInfo.EnumerateDirectories(searchPattern, enumerationOptions);
        }
#endif
        internal IEnumerable<DirectoryInfo> SafeDirectoryEnumeration_NetStandard20(string searchPattern,
            SearchOption searchOption,
            bool ignoreCase)
        {
            if (!directoryInfo.Exists)
                throw new DirectoryNotFoundException();

            IEnumerable<DirectoryInfo> directories = directoryInfo.
                EnumerateDirectories(searchPattern, searchOption)
                .Where(f =>
                {
                    try
                    {
                        return f.Exists;
                    }
                    catch
                    {
                        return false;
                    }
                });

            foreach (DirectoryInfo directory in directories)
            {
                if (searchPattern != "*" && searchPattern != "?")
                {
                    if (!directory.Exists)
                        continue;
                        
                    string baseDirectory = Path.GetDirectoryName(directoryInfo.FullName) ?? directoryInfo.Name;

                    StringComparison comparison = ignoreCase
                        ? StringComparison.InvariantCultureIgnoreCase
                        : StringComparison.InvariantCulture;

                    bool result = directory.FullName.StartsWith(baseDirectory, comparison) ||
                                  directory.FullName.Contains(baseDirectory, comparison);

                    if (result)
                        yield return directory;
                }
                else if(searchPattern.Contains('*') || searchPattern.Contains('?'))
                {
                    yield return directory;
                }
            }
        }
        #endregion

        #region Safe Directory Getting

        /// <summary>
        /// Safely retrieves directories in the current directory, ignoring
        /// inaccessible directories and handling exceptions that may occur during
        /// the directory traversal process.
        /// </summary>
        /// <returns>
        /// An array of <see cref="DirectoryInfo"/> objects representing the
        /// directories found in the current directory based on the default pattern "*".
        /// </returns>
        public DirectoryInfo[] SafelyGetDirectories()
            => SafelyGetDirectories(directoryInfo, "*");


        /// <summary>
        /// Safely retrieves an array of directories in the current directory using the specified search pattern,
        /// ensuring that inaccessible directories or exceptions during directory traversal are skipped gracefully.
        /// </summary>
        /// <returns>
        /// An array of <see cref="DirectoryInfo"/> objects representing the directories found in the current directory
        /// based on the default pattern "*".
        /// </returns>
        public DirectoryInfo[] SafelyGetDirectories(string searchPattern)
            => SafelyGetDirectories(directoryInfo, searchPattern, SearchOption.TopDirectoryOnly);


        /// <summary>
        /// Safely retrieves directories from the specified directory, handling exceptions
        /// and ignoring inaccessible directories during the directory traversal process.
        /// </summary>
        /// <param name="searchPattern">
        /// The search string to match against directory names in the directory.
        /// </param>
        /// <param name="searchOption">
        /// Specifies whether the search operation should include only the current directory
        /// or all subdirectories. Use <see cref="SearchOption.TopDirectoryOnly"/> to include
        /// only the current directory, or <see cref="SearchOption.AllDirectories"/> to include
        /// all subdirectories.
        /// </param>
        /// <param name="ignoreCase">
        /// A boolean value indicating whether the search pattern matching should ignore case sensitivity.
        /// </param>
        /// <returns>
        /// An array of <see cref="DirectoryInfo"/> objects representing the directories found
        /// based on the specified search parameters.
        /// </returns>
        public DirectoryInfo[] SafelyGetDirectories(string searchPattern, SearchOption searchOption,
            bool ignoreCase = true)
        {
#if NETSTANDARD2_1 || NET8_0_OR_GREATER
            return directoryInfo.SafeDirectoryGetting_Net8Plus(searchPattern, searchOption, ignoreCase);
#else
            return directoryInfo.SafelyEnumerateDirectories(searchPattern, searchOption, ignoreCase)
                .ToArray();
#endif
        }

#if NET8_0_OR_GREATER || NETSTANDARD2_1
        private DirectoryInfo[] SafeDirectoryGetting_Net8Plus(string searchPattern, SearchOption searchOption, bool ignoreCase)
        {
            EnumerationOptions enumerationOptions = new()
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories =  searchOption == SearchOption.AllDirectories,
                ReturnSpecialDirectories = true,
                MatchCasing = ignoreCase ? MatchCasing.CaseSensitive : MatchCasing.CaseInsensitive,
                MatchType = MatchType.Simple,
                MaxRecursionDepth = searchOption == SearchOption.AllDirectories ? int.MaxValue : 0
            };
            
            return directoryInfo.GetDirectories(searchPattern, enumerationOptions);
        }
#endif

        #endregion
    }
}