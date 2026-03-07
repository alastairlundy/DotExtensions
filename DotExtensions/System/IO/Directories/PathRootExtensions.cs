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

namespace DotExtensions.IO.Directories;

/// <summary>
/// Provides extension methods for manipulating the root path of a directory.
/// </summary>
public static class PathRootExtensions
{
    extension(Path)
    {
        /// <summary>
        /// Gets the root directory of a given path as a non-nullable string.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns>The full path of the root directory.</returns>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="path"/> is null or empty./></exception>
        public static string GetNonNullPathRoot(string path)
        {
            ArgumentException.ThrowIfNullOrEmpty(path);

            if (Path.HasExtension(path))
            {
                FileInfo fileInfo = new FileInfo(path);

                return fileInfo.GetDirectory().Root.FullName;
            }
            
            DirectoryInfo directoryInfo = new(path);

            return Directory.GetDirectoryRoot(directoryInfo.FullName);
        }
    }
}