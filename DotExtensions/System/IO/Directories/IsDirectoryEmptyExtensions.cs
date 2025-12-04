/*
        MIT License
       
       Copyright (c) 2024-2025 Alastair Lundy
       
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
