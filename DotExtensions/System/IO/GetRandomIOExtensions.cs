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
    extension(DriveInfo)
    {
        /// <summary>
        /// Retrieves a random drive from the system.
        /// </summary>
        /// <returns>A <see cref="DriveInfo"/> object representing the randomly selected drive.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no logical drives are available.</exception>
        public static DriveInfo GetRandomDrive(bool driveMustContainFiles = false)
        {
            DriveInfo[] drives = DriveInfo.SafelyGetLogicalDrives();

            DriveInfo drive;

            bool driveHasFiles;

            do
            {
                switch (drives.Length)
                {
                    case 0:
                        throw new InvalidOperationException(Resources.Exceptions_Drives_NotFound);
                    case > 1:
                    {
                        int randomDriveNumber = Random.Shared.Next(0, drives.Length - 1);

                        drive = drives[randomDriveNumber];
                        break;
                    }
                    default:
                        drive = drives[0];
                        break;
                }

                driveHasFiles = drive.HasFiles;

                if (!(drives.Length > 1) && !driveHasFiles && driveMustContainFiles)
                    throw new InvalidOperationException(Resources.Exceptions_Drives_NoneContainFiles);
            }
            while (!driveHasFiles && driveMustContainFiles);

            return drive;
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
            IEnumerable<DirectoryInfo> dirs = DriveInfo.GetRandomDrive(driveMustContainFiles: mustContainFiles).RootDirectory
                .SafelyEnumerateDirectories("*", SearchOption.AllDirectories)
                .Where(d => d.Exists);

            if (mustContainFiles)
            {
                dirs = dirs.Where(d => d.HasFiles);
            }
            
            DirectoryInfo[] array = dirs.ToArray();
            
            DirectoryInfo? output = Random.Shared.GetItems(array, 1).FirstOrDefault();
            
            if (output is null)
            {
                string sysDir = OperatingSystem.IsWindows()
                    ? Environment.GetFolderPath(Environment.SpecialFolder.Windows)
                    : Environment.SystemDirectory;
                
                output = new DirectoryInfo(sysDir)
                             .Parent ?? 
#if NET8_0_OR_GREATER
                         throw new DirectoryNotFoundException(Resources.Exceptions_IO_DirectoryNotFound.Replace("{x}", sysDir, StringComparison.OrdinalIgnoreCase));
#else
                         throw new DirectoryNotFoundException(Resources.Exceptions_IO_DirectoryNotFound.Replace("{x}", sysDir));
#endif
            }
            
            return output;
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
            DirectoryInfo startDir = DriveInfo.GetRandomDrive(driveMustContainFiles: true).RootDirectory;
            
            DirectoryInfo[] dirs = startDir.SafelyEnumerateDirectories()
                .Where(d => d.HasFiles)
                .ToArray();
            
            while(true)
            {
                DirectoryInfo dir = dirs[Random.Shared.Next(0, dirs.Length - 1)];

                DirectoryInfo[] subDirectories = dir.SafelyEnumerateDirectories()
                    .Where(d => d.HasFiles).ToArray();

                if (subDirectories.Length == 0)
                    continue;

                FileInfo[] files = dir.SafelyGetFiles("*", SearchOption.AllDirectories);

                if (!files.Any())
                    break;

                return files[Random.Shared.Next(0, files.Length - 1)];
            }

            return Random.Shared.GetItems(dirs, 1)
                [0].SafelyEnumerateFiles()
                .First();
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