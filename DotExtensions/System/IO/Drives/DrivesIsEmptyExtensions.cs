/*
        MIT License

       Copyright (c) 2025 Alastair Lundy

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
using AlastairLundy.DotExtensions.IO.Directories;

namespace AlastairLundy.DotExtensions.IO.Drives;

/// <summary>
/// Provides extension methods for working with drive-related operations.
/// </summary>
public static class DrivesIsEmptyExtensions
{
    /// <param name="driveInfo">The drive to be examined.</param>
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
    }
}