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
using System.Security;

// ReSharper disable InconsistentNaming

#if NETSTANDARD2_0
using System.Security;
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
//#else
#endif
        internal IEnumerable<DirectoryInfo> SafeDirectoryEnumeration_NetStandard20(string searchPattern,
            SearchOption searchOption,
            bool ignoreCase)
        {
            if (!directoryInfo.Exists)
                throw new DirectoryNotFoundException();

            IEnumerable<DirectoryInfo> fileSystemInfos = directoryInfo.EnumerateDirectories(searchPattern, searchOption)
                .Where(f =>
                {
                    try
                    {
                        return f.Name != string.Empty;
                    }
                    catch
                    {
                        return false;
                    }
                });

            foreach (DirectoryInfo dirInfo in fileSystemInfos)
            {
                DirectoryInfo? directory = SafelyEnumerateDirectory(directoryInfo, dirInfo, ignoreCase);
                    
                if(directory is null)
                    continue;

                yield return directory;
            }
        }

        private DirectoryInfo? SafelyEnumerateDirectory(DirectoryInfo dirInfo, bool ignoreCase)
        {
            try
            {
                if (!dirInfo.Exists)
                    return null;
                    
                string baseDirectory = Path.GetDirectoryName(dirInfo.FullName) ?? dirInfo.Name;

                StringComparison comparison = ignoreCase
                    ? StringComparison.InvariantCultureIgnoreCase
                    : StringComparison.InvariantCulture;
                
                return dirInfo.FullName.StartsWith(baseDirectory, comparison) ||
                       dirInfo.FullName.Contains(baseDirectory, comparison)
                    ? dirInfo
                    : null;
            }
            catch
            {
                return null;
            }
        }
//#endif
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

    extension(Directory)
    {
        #region Safe File Enumeration (Static Directory C# 14 extensions)

        /// <summary>
        /// Safely enumerates directories in the specified path, ignoring inaccessible directories and handling exceptions that may occur
        /// during directory traversal. </summary>
        /// <param name="path"> The path to enumerate directories from. </param>
        /// <returns> A sequence of <see cref="DirectoryInfo"/> objects representing the directories found at the specified
        /// path based on default search pattern "*".</returns>
        public static IEnumerable<DirectoryInfo> SafelyEnumerateDirectories(string path)
            => SafelyEnumerateDirectories(path, "*");

        /// <summary>
        /// Safely enumerates directories in the specified path, ignoring inaccessible directories and handling exceptions that may occur during directory traversal.
        /// </summary>
        /// <param name="path">
        /// The path to enumerate directories from.
        /// </param>
        /// <param name="searchPattern"></param> <returns>
        /// A sequence of <see cref="DirectoryInfo"/> objects representing the directories found at the specified path based
        /// on default search pattern "*".
        /// </returns>
        public static IEnumerable<DirectoryInfo> SafelyEnumerateDirectories(string path, string searchPattern)
            => SafelyEnumerateDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Safely enumerates directories in the specified path.
        /// This method safely enumerates directories within a given path, ensuring that inaccessible directories are ignored and exceptions during enumeration are handled gracefully. It allows for specifying a search pattern to filter the enumerated directories and provides options to enumerate only the top-level directory or recursively through all subdirectories.
        /// </summary>
        /// <param name="path">
        /// The path of the directory where the enumeration will be performed.
        /// </param>
        /// <param name="searchPattern"> An optional search pattern for enumerating specific directories based on their names.
        /// If not provided, all directories are enumerated. </param>
        /// <param name="directorySearchOption"> Specifies whether to enumerate only the top-level directory or recursively through all subdirectories. Defaults to <see cref="SearchOption.TopDirectoryOnly"/>.
        /// </param>
        /// <param name="ignoreCase">
        /// A boolean indicating whether the search should be case-insensitive. Defaults to true, meaning it will ignore case during enumeration.
        /// </param>
        /// <returns>
        /// An enumerable collection of <see cref="DirectoryInfo"/> objects representing the directories found in the specified path based on the provided parameters.
        /// </returns>
        public static IEnumerable<DirectoryInfo> SafelyEnumerateDirectories(string path, string searchPattern,
            SearchOption directorySearchOption, bool ignoreCase = true)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            return directoryInfo.SafelyEnumerateDirectories(searchPattern, directorySearchOption, ignoreCase);
        }

        #endregion
        #region Safe File Getting (Static Directory C# 14 extensions)

        /// <summary>
        /// Safely retrieves a list of directories from the specified path.
        /// </summary>
        /// <param name="path">The path to enumerate directories from.</param>
        /// <returns>An array of <see cref="DirectoryInfo"/> objects representing the directories found at the specified path based on default search pattern "*".</returns>
        public static DirectoryInfo[] SafelyGetDirectories(string path)
            => SafelyGetDirectories(path, "*");

        /// <summary>
        /// Safely retrieves directories from the specified path, ignoring inaccessible directories and handling exceptions that may occur during enumeration.
        /// </summary>
        /// <param name="path">
        /// The path from which to retrieve directories. This can be a relative or absolute path.
        /// </param>
        /// <param name="searchPattern">
        /// The search pattern to filter directories. Defaults to "*", which matches all directories.
        /// </param>
        /// <returns>
        /// An array of <see cref="DirectoryInfo"/> objects representing the directories found at the specified path based on default search pattern "*".
        /// If no directories are found, an empty array is returned.
        /// </returns>
        /// <exception cref="DirectoryNotFoundException">
        /// Thrown if the specified path does not exist or cannot be accessed.
        /// </exception>
        public static DirectoryInfo[] SafelyGetDirectories(string path, string searchPattern)
            => SafelyGetDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Safely enumerates directories in the specified path based on the provided search pattern and directory search options.
        /// This method safely retrieves a collection of <see cref="DirectoryInfo"/> objects representing directories found at the given path,
        /// taking into account potential accessibility issues and exceptions during enumeration.
        /// </summary>
        /// <param name="path">The path to enumerate directories from.</param>
        /// <param name="searchPattern">
        /// The search pattern to filter directories. Defaults to "*", which matches all directories.
        /// </param>
        /// <param name="directorySearchOptions">
        /// Specifies whether the enumeration includes only the current directory (TopDirectoryOnly) or also subdirectories.
        /// Defaults to SearchOption.TopDirectoryOnly.
        /// </param>
        /// <param name="ignoreCase">Indicates if the search should be case-insensitive. Defaults to true.</param>
        /// <returns>
        /// An array of <see cref="DirectoryInfo"/> objects representing the directories found in the specified path based on the provided parameters.
        /// </returns>
        public static DirectoryInfo[] SafelyGetDirectories(string path, string searchPattern,
            SearchOption directorySearchOptions, bool ignoreCase = true)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            return directoryInfo.SafelyGetDirectories(searchPattern, directorySearchOptions, ignoreCase);
        }
        #endregion
    }
}