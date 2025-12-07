/*
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using System.IO;
using System.Linq;

namespace AlastairLundy.DotExtensions.IO.Directories;

/// <summary>
/// Provides extension methods for directory operations.
/// </summary>
public static class IsDirectoryEmptyExtensions
{
    /// <summary>
    /// Provides extension methods for checking if a directory is empty.
    /// </summary>
    extension(DirectoryInfo directory)
    {
        ///<summary>
        /// Checks if a Directory is empty or not.
        /// </summary>
        /// <returns>True if the directory is empty; false otherwise.</returns>
        /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist.</exception>
        public bool IsEmpty
        {
            get
            {
                ArgumentNullException.ThrowIfNull(directory);

                if (!Directory.Exists(directory.FullName))
                    throw new DirectoryNotFoundException(
                        Resources.Exceptions_IO_DirectoryNotFound.Replace("{x}", directory.FullName)
                    );

                try
                {
                    return !directory.EnumerateFiles().Any()
                           && !directory.EnumerateDirectories().Any();
                }
                catch(UnauthorizedAccessException)
                {
                    return false;
                }
            }
        }
    }
}
