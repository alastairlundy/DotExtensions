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

// ReSharper disable InconsistentNaming

using DotExtensions.IO.Files;

namespace DotExtensions.IO;

/// <summary>
/// Provides extensions to retrieve a random file.
/// </summary>
public static class GetRandomIOExtensions
{
    private const int MaxWalkDepth = 64;

    extension(DriveInfo)
    {
        /// <summary>
        /// Retrieves a random drive from the system.
        /// </summary>
        /// <returns>A <see cref="DriveInfo"/> object representing the randomly selected drive.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no logical drives are available.</exception>
        public static DriveInfo GetRandomDrive(bool driveMustContainFiles = false, bool driveMustContainDirectories = true)
        {
            DriveInfo[] drives = DriveInfo.SafelyGetLogicalDrives();

            if (drives.Length == 0)
                throw new InvalidOperationException(Resources.Exceptions_Drives_NotFound);

            if (drives.Length == 1)
            {
                DriveInfo single = drives[0];

                if (driveMustContainDirectories && !DriveHasDirectoriesCheap(single))
                    throw new InvalidOperationException(Resources.Exceptions_Drives_NotFound);

                if (driveMustContainFiles && !DriveHasFilesCheap(single))
                    throw new InvalidOperationException(Resources.Exceptions_Drives_NoneContainFiles);

                return single;
            }

            if (driveMustContainDirectories || driveMustContainFiles)
            {
                DriveInfo[] filtered = drives.Where(d =>
                    (!driveMustContainDirectories || DriveHasDirectoriesCheap(d)) &&
                    (!driveMustContainFiles || DriveHasFilesCheap(d))
                ).ToArray();

                if (filtered.Length == 0)
                {
                    throw driveMustContainFiles
                        ? new InvalidOperationException(Resources.Exceptions_Drives_NoneContainFiles)
                        : new InvalidOperationException(Resources.Exceptions_Drives_NotFound);
                }

                return filtered[Random.Shared.Next(0, filtered.Length)];
            }

            return drives[Random.Shared.Next(0, drives.Length)];
        }

        /// <summary>
        /// Cheaply checks whether a drive has any top-level directories without recursive enumeration.
        /// </summary>
        private static bool DriveHasDirectoriesCheap(DriveInfo drive)
        {
            try
            {
                return drive.IsReady && drive.RootDirectory.SafelyEnumerateDirectories().Any();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Cheaply checks whether a drive has any files by checking the root directory
        /// and its immediate subdirectories, avoiding a full recursive enumeration.
        /// </summary>
        private static bool DriveHasFilesCheap(DriveInfo drive)
        {
            try
            {
                if (!drive.IsReady)
                    return false;

                DirectoryInfo root = drive.RootDirectory;

                if (root.SafelyEnumerateFiles().Any())
                    return true;

                return root.SafelyEnumerateDirectories()
                    .Any(d => d.SafelyEnumerateFiles().Any());
            }
            catch
            {
                return false;
            }
        }
    }

    extension(DirectoryInfo)
    {
        /// <summary>
        /// Retrieves a random directory from the system.
        /// </summary>
        /// <param name="mustContainFiles">Whether to filter directories containing files.</param>
        /// <returns>A <see cref="DirectoryInfo"/> object representing the randomly selected directory.</returns>
        /// <exception cref="DirectoryNotFoundException">Thrown when no valid directory is found.</exception>
        public static DirectoryInfo GetRandomDirectory(bool mustContainFiles = false)
        {
            DriveInfo drive = DriveInfo.GetRandomDrive(driveMustContainFiles: mustContainFiles);
            
            DirectoryInfo? result = FindRandomDirectoryByWalk(drive.RootDirectory, mustContainFiles);
            
            if (result is not null)
                return result;
            
            string sysDir = OperatingSystem.IsWindows()
                ? Environment.GetFolderPath(Environment.SpecialFolder.Windows)
                : Environment.SystemDirectory;
            
            return new DirectoryInfo(sysDir).Parent ?? 
#if NET8_0_OR_GREATER
                   throw new DirectoryNotFoundException(Resources.Exceptions_IO_DirectoryNotFound.Replace("{x}",
                       sysDir, StringComparison.OrdinalIgnoreCase));
#else
                   throw new DirectoryNotFoundException(Resources.Exceptions_IO_DirectoryNotFound.Replace("{x}", sysDir));
#endif
        }

        /// <summary>
        /// Walks the directory tree randomly to find a directory, optionally one that contains files.
        /// </summary>
        private static DirectoryInfo? FindRandomDirectoryByWalk(DirectoryInfo dir, bool mustContainFiles)
        {
            const int maxAttempts = 64;

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                DirectoryInfo current = dir;
                HashSet<string> visited = new(StringComparer.Ordinal);
                int depth = 0;

                while (depth < MaxWalkDepth)
                {
                    if (!visited.Add(current.FullName))
                        break;

                    DirectoryInfo[] subDirs = current.SafelyGetDirectories()
                        .Where(d => !d.Attributes.HasFlag(FileAttributes.ReparsePoint))
                        .ToArray();

                    if (subDirs.Length == 0)
                    {
                        if (!mustContainFiles || current.HasFiles)
                            return current;

                        break;
                    }

                    DirectoryInfo next = subDirs[Random.Shared.Next(0, subDirs.Length)];

                    if (mustContainFiles && next.HasFiles)
                        return next;

                    current = next;
                    depth++;
                }
            }

            return null;
        }
    }

    extension(FileInfo)
    {
        /// <summary>
        /// Retrieves a random file from a system.
        /// </summary>
        /// <returns>A <see cref="FileInfo"/> object representing the randomly selected file.</returns>
        public static FileInfo GetRandomFile()
        {
            DriveInfo drive = DriveInfo.GetRandomDrive(driveMustContainFiles: true, driveMustContainDirectories: true);
            
            FileInfo? result = FindRandomFileByWalk(drive.RootDirectory);
            
            if (result is not null)
                return result;

            throw new InvalidOperationException(Resources.Exceptions_Drives_NoneContainFiles);
        }

        /// <summary>
        /// Walks the directory tree randomly, descending into a random subdirectory at each level,
        /// and returns a random file from the first directory that contains files.
        /// </summary>
        private static FileInfo? FindRandomFileByWalk(DirectoryInfo dir)
        {
            const int maxAttempts = 64;

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                DirectoryInfo current = dir;
                HashSet<string> visited = new(StringComparer.Ordinal);
                int depth = 0;

                while (depth < MaxWalkDepth)
                {
                    if (!visited.Add(current.FullName))
                        break;

                    FileInfo[] files = current.SafelyGetFiles();

                    if (files.Length > 0)
                        return files[Random.Shared.Next(0, files.Length)];

                    DirectoryInfo[] subDirs = current.SafelyGetDirectories()
                        .Where(d => !d.Attributes.HasFlag(FileAttributes.ReparsePoint))
                        .ToArray();

                    if (subDirs.Length == 0)
                        break;

                    current = subDirs[Random.Shared.Next(0, subDirs.Length)];
                    depth++;
                }
            }

            return null;
        }
    }
    
    extension(Path)
    {
        /// <summary>
        /// Retrieves the full path of a random file.
        /// </summary>
        public static string GetRandomFilePath()
            => FileInfo.GetRandomFile().FullName;
    }
}