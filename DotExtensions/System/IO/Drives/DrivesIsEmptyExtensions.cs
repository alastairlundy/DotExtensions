/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
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