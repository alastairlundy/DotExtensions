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

using DotExtensions.IO.Internal;

namespace DotExtensions.IO;

/// <summary>
/// Contains extension methods for performing safe file and directory enumerations
/// to avoid common exceptions caused by inaccessible or locked file system entries.
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
        public
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
            => SafeEnumerator.EnumerateFiles(directoryInfo, searchPattern, searchOption, ignoreCase);

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
            => SafeEnumerator.GetFiles(directoryInfo, searchPattern, searchOption, ignoreCase);

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
            => directoryInfo.SafelyEnumerateDirectories("*");

        /// <summary>
        /// Safely enumerates all directories in the specified directory, handling potential
        /// exceptions and ignoring inaccessible directories.
        /// </summary>
        /// <param name="searchPattern">
        /// The search string to match against the names of directories. This parameter can contain
        /// a combination of valid literal path and wildcard (* and ?) characters, but it doesn't
        /// support regular expressions.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="DirectoryInfo"/> objects representing the directories
        /// found in the specified directory.
        /// </returns>
        public IEnumerable<DirectoryInfo> SafelyEnumerateDirectories(string searchPattern)
            => directoryInfo.SafelyEnumerateDirectories(searchPattern, SearchOption.TopDirectoryOnly);

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
        public IEnumerable<DirectoryInfo> SafelyEnumerateDirectories(string searchPattern, SearchOption searchOption,
            bool ignoreCase = true)
            => SafeEnumerator.EnumerateDirectories(directoryInfo, searchPattern, searchOption, ignoreCase);

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
            => directoryInfo.SafelyGetDirectories("*");

        /// <summary>
        /// Safely retrieves an array of directories in the current directory using the specified search pattern,
        /// ensuring that inaccessible directories or exceptions during directory traversal are skipped gracefully.
        /// </summary>
        /// <returns>
        /// An array of <see cref="DirectoryInfo"/> objects representing the directories found in the current directory
        /// based on the default pattern "*".
        /// </returns>
        public DirectoryInfo[] SafelyGetDirectories(string searchPattern)
            => directoryInfo.SafelyGetDirectories(searchPattern, SearchOption.TopDirectoryOnly);

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
            => SafeEnumerator.GetDirectories(directoryInfo, searchPattern, searchOption, ignoreCase);
    }

    extension(Directory)
    {
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
        /// <returns>An enumerable collection of FileInfo objects representing the files in the directory that match the specified criteria.
        /// If no files are found or if an error occurs, an empty enumerable is returned.
        /// </returns>
        public static IEnumerable<FileInfo> SafelyEnumerateFiles(string path, string searchPattern,
            SearchOption directorySearchOption, bool ignoreCase = true)
        {
            DirectoryInfo directoryInfo = new(path);

            return directoryInfo.SafelyEnumerateFiles(searchPattern, directorySearchOption, ignoreCase);
        }

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
            => Directory.SafelyEnumerateFiles(path, searchPattern, directorySearchOptions, ignoreCase)
                .ToArray();

        /// <summary>
        /// Safely enumerates directories in the specified path, ignoring inaccessible directories and handling exceptions that may occur
        /// during directory traversal.
        /// </summary>
        /// <param name="path"> The path to enumerate directories from. </param>
        /// <returns> A sequence of <see cref="DirectoryInfo"/> objects representing the directories found at the specified
        /// path based on the default search pattern "*".</returns>
        public static IEnumerable<DirectoryInfo> SafelyEnumerateDirectories(string path)
            => Directory.SafelyEnumerateDirectories(path, "*");

        /// <summary>
        /// Safely enumerates directories in the specified path, ignoring inaccessible directories and handling exceptions that may occur during directory traversal.
        /// </summary>
        /// <param name="path">
        /// The path to enumerate directories from.
        /// </param>
        /// <param name="searchPattern"></param>
        /// <returns>
        /// A sequence of <see cref="DirectoryInfo"/> objects representing the directories found at the specified path based
        /// on the default search pattern "*".
        /// </returns>
        public static IEnumerable<DirectoryInfo> SafelyEnumerateDirectories(string path, string searchPattern)
            => Directory.SafelyEnumerateDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Safely enumerates directories within a given path, ensuring that inaccessible directories are ignored and exceptions during enumeration are handled gracefully.
        /// </summary>
        /// <param name="path">
        /// The path of the directory where the enumeration will be performed.
        /// </param>
        /// <param name="searchPattern"> An optional search pattern for enumerating specific directories based on their names.
        /// If not provided, all directories are enumerated. </param>
        /// <param name="directorySearchOption"> Specifies whether to enumerate only the top-level directory or recursively through all subdirectories.
        /// Defaults to <see cref="SearchOption.TopDirectoryOnly"/>.
        /// </param>
        /// <param name="ignoreCase">
        /// A boolean indicating whether the search should be case-insensitive. Defaults to true, meaning it will ignore the case during enumeration.
        /// </param>
        /// <returns>
        /// An enumerable collection of <see cref="DirectoryInfo"/> objects representing the directories found in the specified path based on the provided parameters.
        /// </returns>
        public static IEnumerable<DirectoryInfo> SafelyEnumerateDirectories(string path, string searchPattern,
            SearchOption directorySearchOption, bool ignoreCase = true)
        {
            DirectoryInfo directoryInfo = new(path);

            return directoryInfo.SafelyEnumerateDirectories(searchPattern, directorySearchOption, ignoreCase);
        }

        /// <summary>
        /// Safely retrieves a list of directories from the specified path.
        /// </summary>
        /// <param name="path">The path to retrieve directories from.</param>
        /// <returns>An array of <see cref="DirectoryInfo"/> objects representing the directories found at the specified
        /// path based on the default search pattern "*".</returns>
        public static DirectoryInfo[] SafelyGetDirectories(string path)
            => Directory.SafelyGetDirectories(path, "*");

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
        /// An array of <see cref="DirectoryInfo"/> objects representing the directories found at the specified path based on the default search pattern "*".
        /// If no directories are found, an empty array is returned.
        /// </returns>
        /// <exception cref="DirectoryNotFoundException">
        /// Thrown if the specified path does not exist or cannot be accessed.
        /// </exception>
        public static DirectoryInfo[] SafelyGetDirectories(string path, string searchPattern)
            => Directory.SafelyGetDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Safely retrieves a collection of <see cref="DirectoryInfo"/> objects representing directories found at the given path,
        /// taking into account potential accessibility issues and exceptions during enumeration.
        /// </summary>
        /// <param name="path">The path to retrieve directories from.</param>
        /// <param name="searchPattern">
        /// The search pattern to filter directories. Defaults to "*", which matches all directories.
        /// </param>
        /// <param name="directorySearchOptions">
        /// Specifies whether the enumeration includes only the current directory (TopDirectoryOnly) or also subdirectories.
        /// </param>
        /// <param name="ignoreCase">Indicates if the search should be case-insensitive. Defaults to true.</param>
        /// <returns>
        /// An array of <see cref="DirectoryInfo"/> objects representing the directories found in the specified path based on the provided parameters.
        /// </returns>
        public static DirectoryInfo[] SafelyGetDirectories(string path, string searchPattern,
            SearchOption directorySearchOptions, bool ignoreCase = true)
            => Directory.SafelyEnumerateDirectories(path, searchPattern, directorySearchOptions, ignoreCase).ToArray();
    }
}