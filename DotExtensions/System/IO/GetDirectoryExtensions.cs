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

namespace DotExtensions.IO;

/// <summary>
/// Provides extension methods for working with directory extensions.
/// </summary>
public static class GetDirectoryExtensions
{
    /// <param name="fileInfo">The file to get the directory of.</param>
    extension(FileInfo fileInfo)
    {
        /// <summary>
        /// Gets the parent directory of a file.
        /// </summary>
        /// <returns>The parent directory of the file, if found.</returns>
        /// <exception cref="ArgumentException">Thrown if the file does not exist.</exception>
        public DirectoryInfo GetDirectory()
        {
            string? parentPath = Path.GetDirectoryName(fileInfo.FullName);

            if (parentPath is not null && parentPath.Length > 0 &&
                (Directory.Exists(parentPath) || fileInfo.Exists))
                return new DirectoryInfo(parentPath);

            throw new ArgumentException(
                Resources.Exceptions_Directory_FileArgumentNotFound.Replace("{0}", fileInfo.Name,
                    StringComparison.OrdinalIgnoreCase), nameof(fileInfo));
        }
    }
}