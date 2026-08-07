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
/// Provides extension properties for checking whether a directory or drive is empty.
/// </summary>
public static class IsEmptyExtensions
{
    private static void ThrowIfDirectoryNotFound(DirectoryInfo directory)
    {
        ArgumentNullException.ThrowIfNull(directory);

        if (!directory.Exists)
            throw new DirectoryNotFoundException(
                Resources.Exceptions_IO_DirectoryNotFound.Replace("{x}", directory.FullName,
                    StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Provides extension properties for checking whether a directory is empty.
    /// </summary>
    extension(DirectoryInfo directory)
    {
        /// <summary>
        /// Checks if a Directory is empty or not.
        /// </summary>
        /// <returns>True if the directory is empty; false otherwise.</returns>
        /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist.</exception>
        public bool IsEmpty
        {
            get
            {
                ThrowIfDirectoryNotFound(directory);

                return !SafeEnumerator.EnumerateFiles(directory, "*", SearchOption.TopDirectoryOnly, false).Any() &&
                       !SafeEnumerator.EnumerateDirectories(directory, "*", SearchOption.TopDirectoryOnly, true).Any();
            }
        }

        /// <summary>
        /// Determines if the directory contains any files.
        /// </summary>
        /// <value><see langword="true"/> if the directory has at least one file; otherwise, <see langword="false"/>.</value>
        /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist.</exception>
        public bool HasFiles
        {
            get
            {
                ThrowIfDirectoryNotFound(directory);

                return SafeEnumerator.EnumerateFiles(directory, "*", SearchOption.TopDirectoryOnly, false).Any();
            }
        }
    }

    /// <summary>
    /// Provides extension properties for checking whether a drive is empty.
    /// </summary>
    extension(DriveInfo driveInfo)
    {
        /// <summary>
        /// Whether a Drive is empty or not.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                ArgumentNullException.ThrowIfNull(driveInfo);

                if (!driveInfo.IsReady)
                    return false;

                return driveInfo.TotalFreeSpace == driveInfo.TotalSize
                       && driveInfo.RootDirectory.IsEmpty;
            }
        }

        /// <summary>
        /// Determines if the <see cref="DriveInfo"/> contains any files.
        /// </summary>
        /// <value><see langword="true"/> if the <see cref="DriveInfo"/> has at least one file; otherwise, <see langword="false"/>.</value>
        public bool HasFiles
        {
            get
            {
                ArgumentNullException.ThrowIfNull(driveInfo);

                return SafeEnumerator.EnumerateFiles(driveInfo.RootDirectory, "*", SearchOption.AllDirectories, false).Any();
            }
        }

        /// <summary>
        /// Whether the drive contains any directories (recursively).
        /// </summary>
        /// <value><see langword="true"/> if the <see cref="DriveInfo"/> has at least one directory; otherwise, <see langword="false"/>.</value>
        public bool HasDirectories
        {
            get
            {
                ArgumentNullException.ThrowIfNull(driveInfo);

                return SafeEnumerator.EnumerateDirectories(driveInfo.RootDirectory, "*", SearchOption.AllDirectories, true).Any();
            }
        }
    }
}
