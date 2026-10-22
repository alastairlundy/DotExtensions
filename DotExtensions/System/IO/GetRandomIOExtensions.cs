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

using DotExtensions.IO.Internal;

namespace DotExtensions.IO;

/// <summary>
/// Provides extensions to retrieve a random file.
/// </summary>
/// <remarks>
/// Hidden and system entries never take part in random selection because
/// <see cref="EnumerationOptions.AttributesToSkip"/> keeps its default value
/// (<see cref="FileAttributes.Hidden"/> | <see cref="FileAttributes.System"/>), so only
/// non-hidden, non-system files and directories are ever returned.
/// </remarks>
public static class GetRandomIOExtensions
{
    private const int MaxWalkDepth = 64;

    /// <summary>
    /// Checks whether a directory contains any files, swallowing transient I/O failures
    /// (e.g. a file deleted between enumeration and inspection mid-walk) and treating
    /// such directories as containing no files.
    /// </summary>
    private static bool DirectoryHasFilesSafe(DirectoryInfo directory)
    {
        try
        {
            return directory.HasFiles;
        }
        catch (IOException)
        {
            return false;
        }
        catch (UnauthorizedAccessException)
        {
            return false;
        }
    }

    /// <summary>
    /// Returns <see langword="true"/> when a directory entry can be inspected and is not a reparse point.
    /// Entries that disappeared mid-walk or whose attributes cannot be read are skipped.
    /// </summary>
    private static bool IsNotReparsePoint(DirectoryInfo directory)
    {
        try
        {
            return !directory.Attributes.HasFlag(FileAttributes.ReparsePoint);
        }
        catch (IOException)
        {
            return false;
        }
        catch (UnauthorizedAccessException)
        {
            return false;
        }
    }

    extension(DriveInfo)
    {
        /// <summary>
        /// Retrieves a random drive from the system.
        /// </summary>
        /// <returns>A <see cref="DriveInfo"/> object representing the randomly selected drive.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no logical drives are available.</exception>
        public static DriveInfo GetRandomDrive(bool driveMustContainFiles = false, bool driveMustContainDirectories = true)
        {
            DriveInfo[] drives = DriveEnumerator.GetLogicalDrives();

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
                return drive.IsReady && SafeEnumerator.EnumerateDirectories(drive.RootDirectory, "*", SearchOption.TopDirectoryOnly, true).Any();
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

                if (SafeEnumerator.EnumerateFiles(root, "*", SearchOption.TopDirectoryOnly, false).Any())
                    return true;

                return SafeEnumerator.EnumerateDirectories(root, "*", SearchOption.TopDirectoryOnly, true)
                    .Any(d => SafeEnumerator.EnumerateFiles(d, "*", SearchOption.TopDirectoryOnly, false).Any());
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
        /// <exception cref="InvalidOperationException">Thrown when no random directory could be found.</exception>
        public static DirectoryInfo GetRandomDirectory(bool mustContainFiles = false)
        {
            DriveInfo drive = DriveInfo.GetRandomDrive(driveMustContainFiles: mustContainFiles);

            return FindRandomDirectoryByWalk(drive.RootDirectory, mustContainFiles)
                   ?? throw new InvalidOperationException(Resources.Exceptions_IO_RandomDirectoryNotFound);
        }

        /// <summary>
        /// Walks the directory tree randomly to find a directory, optionally one that contains files.
        /// At each step the current directory is kept with probability 1/(depth+1) (reservoir-style
        /// selection, so shallower directories are more likely) and the walk otherwise descends into
        /// a random subdirectory. When <paramref name="mustContainFiles"/> is <see langword="true"/>,
        /// only directories that actually contain files are ever selected.
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

                    // Reservoir-style single-pass choice: return the current directory with
                    // probability 1/(depth+1); the root itself (depth 0) is never selected
                    // directly - otherwise the walk would always return immediately.
                    if (depth > 0 && Random.Shared.Next(depth + 1) == 0 &&
                        (!mustContainFiles || DirectoryHasFilesSafe(current)))
                        return current;

                    DirectoryInfo[] subDirs = SafeEnumerator
                        .EnumerateDirectories(current, "*", SearchOption.TopDirectoryOnly, true)
                        .Where(IsNotReparsePoint)
                        .ToArray();

                    if (subDirs.Length == 0)
                    {
                        if (!mustContainFiles || DirectoryHasFilesSafe(current))
                            return current;

                        break;
                    }

                    current = subDirs[Random.Shared.Next(0, subDirs.Length)];
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

                    FileInfo[] files = SafeEnumerator.GetFiles(current, "*", SearchOption.TopDirectoryOnly, false);

                    if (files.Length > 0)
                        return files[Random.Shared.Next(0, files.Length)];

                    DirectoryInfo[] subDirs = SafeEnumerator
                        .EnumerateDirectories(current, "*", SearchOption.TopDirectoryOnly, true)
                        .Where(IsNotReparsePoint)
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