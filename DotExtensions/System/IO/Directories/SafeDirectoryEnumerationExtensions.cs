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

#if NETSTANDARD2_0
using System.Security;
// ReSharper disable InconsistentNaming
#endif

namespace AlastairLundy.DotExtensions.IO.Directories;

public static partial class SafeIOEnumerationExtensions
{
    /// <summary>
    /// Provides extension methods for safely enumerating directories. This ensures that
    /// inaccessible directories or exceptions during directory traversal are handled without
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
        /// <returns>
        /// A sequence of <see cref="DirectoryInfo"/> objects representing the directories
        /// found in the specified directory that match the search pattern and search option.
        /// </returns>
        public IEnumerable<DirectoryInfo> SafelyEnumerateDirectories(string searchPattern, SearchOption searchOption)
        {
#if NETSTANDARD2_1 || NET8_0_OR_GREATER
            return directoryInfo.SafeDirectoryEnumeration_Net8Plus(searchPattern, searchOption);
#else
            return directoryInfo.SafeDirectoryEnumeration_NetStandard20(searchPattern, searchOption);
#endif
        }
        
#if NET8_0_OR_GREATER || NETSTANDARD2_1
        private IEnumerable<DirectoryInfo> SafeDirectoryEnumeration_Net8Plus(string searchPattern,
            SearchOption searchOption)
        {
            EnumerationOptions enumerationOptions = new()
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = searchOption == SearchOption.AllDirectories,
                ReturnSpecialDirectories = true,
                MatchType = MatchType.Simple,
                MatchCasing = MatchCasing.PlatformDefault
            };

            return directoryInfo.EnumerateDirectories(searchPattern, enumerationOptions);
        }
#else
        private IEnumerable<DirectoryInfo> SafeDirectoryEnumeration_NetStandard20(string searchPattern, SearchOption searchOption)
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
                DirectoryInfo? directory = SafelyEnumerateDirectory(directoryInfo, fileSystemInfo);
                    
                if(directory is null)
                    continue;

                yield return directory;
            }
        }

        private DirectoryInfo? SafelyEnumerateDirectory(FileSystemInfo fileSystemInfo)
        {
            try
            {
                if (fileSystemInfo is DirectoryInfo dirInfo)
                {
                    if (!dirInfo.Exists)
                        return null;
                    
                    string baseDirectory = Path.GetDirectoryName(directoryInfo.FullName) ?? directoryInfo.Name;

                    return dirInfo.FullName.StartsWith(baseDirectory) || dirInfo.FullName.Contains(baseDirectory)
                        ? dirInfo
                        : null;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
#endif
        #endregion
    }
}