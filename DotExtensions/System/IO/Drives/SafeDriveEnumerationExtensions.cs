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

namespace DotExtensions.IO.Drives;

/// <summary>
/// Extension methods for the <see cref="DriveInfo"/> class related to safe drive enumeration.
/// </summary>
public static class SafeDriveEnumerationExtensions
{
    extension(DriveInfo)
    {
        /// <summary>
        /// Retrieves an array of all logical drives available on the system.
        /// </summary>
        /// <returns>
        /// An array of <see cref="System.IO.DriveInfo"/> objects representing the logical drives
        /// available on the system. Returns an empty array if no logical drives are found.
        /// </returns>
        public static DriveInfo[] SafelyGetLogicalDrives()
            => DriveInfo.SafelyEnumerateLogicalDrives().ToArray();
        
        /// <summary>
        /// Enumerates all logical drives available on the current platform.
        /// </summary>
        /// <returns>
        /// A sequence of <see cref="System.IO.DriveInfo"/> objects representing the logical drives
        /// accessible on the system.
        /// </returns>
        public static IEnumerable<DriveInfo> SafelyEnumerateLogicalDrives()
        {
            return DriveInfo.GetDrives()
                .Where(d => d.IsReady)
                .Where(d =>
                {
                    try
                    {
                        return d.TotalSize > 0;
                    }
                    catch
                    {
                        return false;
                    }
                });
        }
    }
}