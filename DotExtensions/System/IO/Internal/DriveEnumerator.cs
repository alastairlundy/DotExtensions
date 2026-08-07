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

namespace DotExtensions.IO.Internal;

/// <summary>
/// Internal helper for safe logical-drive enumeration, filtering out
/// unready drives and those with zero total size.
/// </summary>
internal static class DriveEnumerator
{
    /// <summary>
    /// Enumerates all logical drives that are ready and have a positive total size,
    /// swallowing exceptions from inaccessible drives.
    /// </summary>
    public static IEnumerable<DriveInfo> EnumerateLogicalDrives()
    {
        return DriveInfo.GetDrives()
            .Where(d =>
            {
                try
                {
                    return d.IsReady && d.TotalSize > 0;
                }
                catch
                {
                    return false;
                }
            });
    }

    /// <summary>
    /// Returns an array of all logical drives that are ready and have a positive total size.
    /// </summary>
    public static DriveInfo[] GetLogicalDrives()
        => EnumerateLogicalDrives().ToArray();
}
