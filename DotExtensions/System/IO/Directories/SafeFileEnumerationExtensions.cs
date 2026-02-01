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

using System.Collections.Generic;
using DotPrimitives.IO.Directories;

// ReSharper disable InconsistentNaming

namespace DotExtensions.IO.Directories;

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
            => directoryInfo.SafelyEnumerateFiles("*");

        /// <summary>
        /// Safely enumerates files in the specified directory, handling inaccessible or special directories
        /// based on the provided search pattern.
        /// </summary>
        /// <param name="searchPattern">The search string to match against the names of files in the directory.</param>
        /// <returns>Returns a sequence of <see cref="FileInfo"/> objects representing the files in the directory.</returns>
        public IEnumerable<FileInfo> SafelyEnumerateFiles(string searchPattern)
            => directoryInfo.SafelyEnumerateFiles(searchPattern, SearchOption.TopDirectoryOnly);

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
#if NET8_0_OR_GREATER
            EnumerationOptions enumerationOptions = new()
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = searchOption == SearchOption.AllDirectories,
                ReturnSpecialDirectories = true,
                MatchType = MatchType.Simple,
                MatchCasing = ignoreCase ? MatchCasing.CaseInsensitive : MatchCasing.CaseSensitive
            };

            return directoryInfo.EnumerateFiles(searchPattern, enumerationOptions);
#else
            
#endif
        }
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
            => directoryInfo.SafelyGetFiles("*");

        /// <summary>
        /// Safely retrieves an array of files in the specified directory, optionally using a search pattern,
        /// while handling inaccessible or locked files gracefully.
        /// </summary>
        /// <param name="searchPattern">The search string used to match file names. The default is "*".</param>
        /// <returns>Returns an array of <see cref="FileInfo"/> objects representing the files in the directory.</returns>
        public FileInfo[] SafelyGetFiles(string searchPattern)
            => directoryInfo.SafelyGetFiles(searchPattern, SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Safely retrieves an array of files from the specified directory, using the provided
        /// search pattern, search option, and case sensitivity, while handling inaccessible files gracefully.
        /// </summary>
        /// <param name="searchPattern">The search pattern to match against the file names in the directory.</param>
        /// <param name="searchOption">Specifies whether to search only the current directory or all subdirectories.</param>
        /// <param name="ignoreCase">Indicates whether the search pattern will be treated as case-insensitive.</param>
        /// <returns>Returns an array of <see cref="FileInfo"/> objects representing the files in the directory.</returns>
        public FileInfo[] SafelyGetFiles(string searchPattern, SearchOption searchOption,
            bool ignoreCase = false)
            => SafeDirectoryEnumeration.Shared.SafelyGetFiles(directoryInfo, searchPattern, searchOption, ignoreCase);
    }

    #endregion

    /// <summary>
    /// Contains extension methods for performing safe file and directory enumerations
    /// to avoid common exceptions caused by inaccessible or locked file system entries.
    /// </summary>
    extension(Directory)
    {
        #region Safe File Enumeration (Static Directory C# 14 extensions)

        /// <summary>
        /// Safely enumerates files in the specified directory.
        /// </summary>
        /// <param name="path">
        /// The path to the directory from which to enumerate files. This can be a relative or absolute path.
        /// </param>
        /// <returns>
        /// An enumerable collection of <see cref="FileInfo"/> objects representing the files in the directory.
        /// If there is an issue accessing the directory, this method will return an empty enumerable.
        /// </returns>
        public static IEnumerable<FileInfo> SafelyEnumerateFiles(string path)
            => Directory.SafelyEnumerateFiles(path, "*");

        /// <summary>
        /// Safely enumerates files in the specified directory, handling inaccessible or special directories gracefully.
        /// </summary>
        /// <param name="path">The path of the directory to search for files.</param>
        /// <param name="searchPattern">The search string to match against file names in the directory.</param>
        /// <returns>Returns an enumerable collection of <see cref="FileInfo"/> objects representing the files in the directory.</returns>
        public static IEnumerable<FileInfo> SafelyEnumerateFiles(string path, string searchPattern)
            => Directory.SafelyEnumerateFiles(path, searchPattern, SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Safely enumerates files in the specified directory.
        /// </summary>
        /// <param name="path">
        /// The path to the directory from which to enumerate files.
        /// </param>
        /// <param name="searchPattern">
        /// The search pattern for the files. Defaults to "*".
        /// </param>
        /// <param name="directorySearchOption">
        /// Specifies whether the enumeration includes only the current directory or all subdirectories as well.
        /// </param>
        /// <param name="ignoreCase">
        /// Indicates whether the search pattern should be case-sensitive. Defaults to false (case-insensitive).
        /// </param>
        /// <returns>An array of FileInfo objects representing the files in the directory that match the specified criteria.
        /// If no files are found or if an error occurs, null is returned instead.
        /// </returns>
        public static IEnumerable<FileInfo> SafelyEnumerateFiles(string path, string searchPattern,
            SearchOption directorySearchOption, bool ignoreCase = true)
            => SafeDirectoryEnumeration.Shared.SafelyEnumerateFiles(new DirectoryInfo(path), searchPattern, directorySearchOption, ignoreCase);

        #endregion
        #region Safe File Getting (Static Directory C# 14 extensions)

        /// <summary>
        /// Safely retrieves an array of files in the specified directory, handling potential errors
        /// such as inaccessible files or directories gracefully.
        /// </summary>
        /// <param name="path">
        /// The path to the directory from which to retrieve files. This can be a relative or absolute path.
        /// </param>
        /// <returns>
        /// An array of <see cref="FileInfo"/> objects representing the files in the directory.
        /// If there is an issue accessing the directory, this method will return an empty array.
        /// </returns>
        public static FileInfo[] SafelyGetFiles(string path)
            => Directory.SafelyGetFiles(path, "*");

        /// <summary>
        /// Retrieves an array of <see cref="FileInfo"/> objects that represent the files in the specified directory,
        /// handling inaccessible or locked files gracefully.
        /// </summary>
        /// <param name="path">The directory path to search for files.</param>
        /// <param name="searchPattern">The search string to match against file names. Wildcards can be used.</param>
        /// <returns>Returns an array of <see cref="FileInfo"/> objects representing the files in the specified directory.</returns>
        public static FileInfo[] SafelyGetFiles(string path, string searchPattern)
            => Directory.SafelyGetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Safely retrieves an array of <see cref="FileInfo"/> objects representing the files in the specified directory,
        /// handling exceptions caused by locked or inaccessible files gracefully.
        /// </summary>
        /// <param name="path">The directory path to search for files.</param>
        /// <param name="searchPattern">The search string to match against file names. Wildcards can be used.</param>
        /// <param name="directorySearchOptions">Specifies whether to search all subdirectories or only the current directory.</param>
        /// <param name="ignoreCase">Indicates whether the search should be case-insensitive. Defaults to false.</param>
        /// <returns>Returns an array of <see cref="FileInfo"/> objects representing the files found in the directory.</returns>
        public static FileInfo[] SafelyGetFiles(string path, string searchPattern, SearchOption directorySearchOptions,
            bool ignoreCase = true)
            => SafeDirectoryEnumeration.Shared.SafelyGetFiles(new DirectoryInfo(path), searchPattern, directorySearchOptions, ignoreCase);
        #endregion
    }
}