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

using System.Linq;
using DotExtensions.IO.Directories;
using DotExtensions.IO.Drives;

namespace DotExtensions.IO.Files;

/// <summary>
/// Provides extension methods for working with directory extensions.
/// </summary>
public static class GetDirectoryExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileInfo"></param>
    extension(FileInfo fileInfo)
    {
        /// <summary>
        /// Gets the parent directory of a file.
        /// </summary>
        /// <returns>The parent directory of the <paramref name="fileInfo"/> if found.</returns>
        /// <exception cref="ArgumentException">Thrown if the file does not exist.</exception>
        public DirectoryInfo GetDirectory()
        {
            if(fileInfo.Directory is not null)
                return fileInfo.Directory;
            
            int lastDirSeparatorIndex = fileInfo.FullName
                .Substring(0, fileInfo.FullName.IndexOf(fileInfo.Name, StringComparison.Ordinal) - 1)
                .LastIndexOfAny(
                    [Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar]);
            
            if (lastDirSeparatorIndex != -1)
            {
                string newPath = fileInfo.FullName.Substring(0, lastDirSeparatorIndex);

                if (Directory.Exists(newPath))
                {
                    return new DirectoryInfo(newPath);
                }
            }

            if (!fileInfo.Exists)
                throw new ArgumentException("File to get the directory of does not exist.");

            DirectoryInfo? directory = DriveInfo.SafelyEnumerateLogicalDrives()
                .Select(d => d.RootDirectory)
                .SelectMany(d => d.SafelyEnumerateDirectories())
                .FirstOrDefault(d => d.SafelyEnumerateFiles().Any(f => f.Name.Equals(fileInfo.Name)));
           
            return directory ??
                   new DirectoryInfo(Directory.GetDirectoryRoot(fileInfo.FullName));
        }
    }
}