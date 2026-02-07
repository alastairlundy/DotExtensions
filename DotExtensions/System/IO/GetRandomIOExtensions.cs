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
using System.Linq;

// ReSharper disable InconsistentNaming

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
        public static DriveInfo GetRandomDrive()
        {
            DriveInfo[] drives = DriveInfo.SafelyEnumerateLogicalDrives()
                .Where(d => d.IsReady && !d.RootDirectory.IsEmpty)
                .ToArray();

            DriveInfo drive;
            
            switch (drives.Length)
            {
                case 0:
                    throw new InvalidOperationException("No logical drives available.");
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

            return drive;
        }
    }

    extension(DirectoryInfo)
    {
        /// <summary>
        /// Retrieves a random directory from the system.
        /// </summary>
        /// <param name="mustContainFiles">Indicates whether to filter directories containing files.</param>
        /// <returns>A <see cref="DirectoryInfo"/> object representing the randomly selected directory.</returns>
        /// <exception cref="DirectoryNotFoundException">Thrown when no valid directory is found.</exception>
        public static DirectoryInfo GetRandomDirectory(bool mustContainFiles = false)
        {
            IEnumerable<DirectoryInfo> dirs = DriveInfo.GetRandomDrive().RootDirectory
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
                string winDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
                
                output = new DirectoryInfo(winDir)
                    .Parent ?? throw new DirectoryNotFoundException(Resources.Exceptions_IO_DirectoryNotFound.Replace("{x}", winDir));
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
            DirectoryInfo startDir = DriveInfo.GetRandomDrive().RootDirectory;
            
            DirectoryInfo[] dirs = startDir.SafelyEnumerateDirectories("*", SearchOption.TopDirectoryOnly)
                .ToArray();
            
            while(true)
            {
                DirectoryInfo dir = dirs[Random.Shared.Next(0, dirs.Length - 1)];

                DirectoryInfo[] subDirectories = dir.SafelyGetDirectories();

                if (subDirectories.Length == 0)
                    continue;

                FileInfo[] files = dir.SafelyGetFiles("*", SearchOption.AllDirectories);

                if (!files.Any())
                    break;

                return files[Random.Shared.Next(0, files.Length - 1)];
            }

            return Random.Shared.GetItems(dirs, 1)
                .First()
                .SafelyEnumerateFiles()
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