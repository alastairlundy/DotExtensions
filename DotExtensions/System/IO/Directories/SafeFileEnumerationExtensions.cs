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
using System.Security;
#endif

namespace AlastairLundy.DotExtensions.IO.Directories;

/// <summary>
/// Contains extension methods for performing safe file and directory enumerations
/// to avoid common exceptions caused by inaccessible or locked file system entries.
/// </summary>
public static partial class SafeIOEnumerationExtensions
{
    #region Safe File Enumeration.

    /// <summary>
    /// Provides extension methods for safe enumeration of files in directories.
    /// </summary>
    extension(DirectoryInfo directoryInfo)
    {
        /// <summary>
        /// Safely enumerates files in the specified directory, handling inaccessible or special directories gracefully.
        /// </summary>
        /// <returns>Returns an enumerable collection of <see cref="FileInfo"/> objects representing the files in the directory.</returns>
        public IEnumerable<FileInfo> SafelyEnumerateFiles()
            => SafelyEnumerateFiles(directoryInfo, "*");

        /// <summary>
        /// Safely enumerates files in the specified directory, handling inaccessible or special directories
        /// based on the provided search pattern.
        /// </summary>
        /// <param name="searchPattern">The search string to match against the names of files in the directory.</param>
        /// <returns>Returns a sequence of <see cref="FileInfo"/> objects representing the files in the directory.</returns>
        public IEnumerable<FileInfo> SafelyEnumerateFiles(string searchPattern)
            => SafelyEnumerateFiles(directoryInfo, searchPattern, SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Safely enumerates files in the specified directory, handling inaccessible or special directories
        /// based on the provided search pattern, search option, and case sensitivity preference.
        /// </summary>
        /// <param name="searchPattern">The search string to match against the names of files in the directory.</param>
        /// <param name="searchOption">Specifies whether to search only the current directory or all subdirectories.</param>
        /// <param name="ignoreCase">Specifies whether the search pattern should be case-insensitive.</param>
        /// <returns>Returns a sequence of <see cref="FileInfo"/> objects representing the files in the directory.</returns>
        public IEnumerable<FileInfo> SafelyEnumerateFiles(string searchPattern, SearchOption searchOption,
            bool ignoreCase = false)
        {
#if NETSTANDARD2_1 || NET8_0_OR_GREATER
            return directoryInfo.SafeFileEnumeration_Net8Plus(searchPattern, searchOption, ignoreCase);
#else
            return directoryInfo.SafeFileEnumeration_NetStandard20(searchPattern, searchOption, ignoreCase);
#endif
        }

        private FileInfo? SafelyEnumerateFile(FileSystemInfo info, string searchPattern, bool ignoreCase)
        {
            try
            {
                if (info is FileInfo fileInfo)
                {
                    if (ignoreCase)
                    {
                        if (fileInfo.Name.ToLower().Contains(searchPattern.ToLower()))
                            return fileInfo;
                    }
                    else
                    {
                        if (fileInfo.Name.Contains(searchPattern))
                            return fileInfo;
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

#if NETSTANDARD2_1 || NET8_0_OR_GREATER
        private IEnumerable<FileInfo> SafeFileEnumeration_Net8Plus(string searchPattern, SearchOption searchOption, bool ignoreCase)
        {
            EnumerationOptions enumerationOptions = new()
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = searchOption == SearchOption.AllDirectories,
                ReturnSpecialDirectories = true,
                MatchType = MatchType.Simple,
                MatchCasing = ignoreCase ? MatchCasing.CaseInsensitive : MatchCasing.CaseSensitive
            };

            return directoryInfo.EnumerateFiles(searchPattern, enumerationOptions);
        }
#else
        private IEnumerable<FileInfo> SafeFileEnumeration_NetStandard20(string searchPattern, SearchOption searchOption,
            bool ignoreCase)
        {
            if (!directoryInfo.Exists)
                throw new DirectoryNotFoundException();

            IEnumerable<FileSystemInfo>? fileSystemInfos;

            try
            {
                fileSystemInfos = directoryInfo.EnumerateFileSystemInfos(searchPattern, searchOption);
            }
            catch (UnauthorizedAccessException)
            {
                fileSystemInfos = null;
            }
            catch (SecurityException)
            {
                fileSystemInfos = null;
            }

            if (fileSystemInfos is null)
                yield break;

            foreach (FileSystemInfo fileSystemInfo in fileSystemInfos)
            {
                FileInfo? file = SafelyEnumerateFile(directoryInfo, fileSystemInfo, searchPattern, ignoreCase);

                if (file is null)
                    continue;

                yield return file;
            }
        }
#endif
    }

    #endregion

    #region Safely Get Files

    /// <summary>
    /// Provides extension methods for safer file enumeration operations on directories while handling
    /// inaccessible or locked files gracefully.
    /// </summary>
    extension(DirectoryInfo directoryInfo)
    {
        /// <summary>
        /// Safely retrieves an array of files in the specified directory, using a default
        /// search pattern of "*", while handling inaccessible or locked files gracefully.
        /// </summary>
        /// <returns>Returns an array of <see cref="FileInfo"/> objects representing the files in the directory.</returns>
        public FileInfo[] SafelyGetFiles()
            => SafelyGetFiles(directoryInfo, "*");

        /// <summary>
        /// Safely retrieves an array of files in the specified directory, optionally using a search pattern,
        /// while handling inaccessible or locked files gracefully.
        /// </summary>
        /// <param name="searchPattern">The search string used to match file names. The default is "*".</param>
        /// <returns>Returns an array of <see cref="FileInfo"/> objects representing the files in the directory.</returns>
        public FileInfo[] SafelyGetFiles(string searchPattern)
            => SafelyGetFiles(directoryInfo, searchPattern, SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Safely retrieves an array of files from the specified directory, using the provided
        /// search pattern, search option, and case sensitivity, while handling inaccessible or locked files gracefully.
        /// </summary>
        /// <param name="searchPattern">The search pattern to match against the file names in the directory.</param>
        /// <param name="searchOption">Specifies whether to search only the current directory or all subdirectories.</param>
        /// <param name="ignoreCase">Indicates whether the search pattern should be treated as case-insensitive.</param>
        /// <returns>Returns an array of <see cref="FileInfo"/> objects representing the files in the directory.</returns>
        public FileInfo[] SafelyGetFiles(string searchPattern, SearchOption searchOption,
            bool ignoreCase = false)
        {
#if NETSTANDARD2_1 || NET8_0_OR_GREATER
            return directoryInfo.SafeFileGetting_Net8Plus(searchPattern, searchOption, ignoreCase);
#else
            return directoryInfo.SafelyEnumerateFiles(searchPattern, searchOption, ignoreCase)
                .ToArray();
#endif
        }

#if NETSTANDARD2_1 || NET8_0_OR_GREATER
        private FileInfo[] SafeFileGetting_Net8Plus(string searchPattern, SearchOption searchOption, bool ignoreCase)
        {
            EnumerationOptions enumerationOptions = new()
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = searchOption == SearchOption.AllDirectories,
                ReturnSpecialDirectories = true,
                MatchType = MatchType.Simple,
                MatchCasing = ignoreCase ? MatchCasing.CaseInsensitive : MatchCasing.CaseSensitive
            };

            return directoryInfo.GetFiles(searchPattern, enumerationOptions);
        }
#endif
    }

    #endregion
}