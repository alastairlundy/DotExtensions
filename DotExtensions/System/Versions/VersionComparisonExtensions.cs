/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.Versions;

/// <summary>
/// Provides extension methods for comparing <see cref="Version"/> objects.
/// </summary>
public static class VersionComparisonExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="version">The current version object.</param>
    extension(Version version)
    {
        /// <summary>
        /// Returns whether this version is newer than or equal to the current version object.
        /// </summary>
        /// <param name="versionToBeCompared">The version to be compared.</param>
        /// <returns>True, if the specified compared version is newer than or equal to this version, returns false otherwise.</returns>
        public bool IsAtLeast(Version versionToBeCompared) => versionToBeCompared >= version;

        /// <summary>
        /// Returns whether this version is older than the current Version object.
        /// </summary>
        /// <param name="versionToBeCompared">The version to be compared.</param>
        /// <returns>True, if the specified compared version is older than this version, returns false otherwise.</returns>
        public bool IsOlderThan(Version versionToBeCompared) => versionToBeCompared < version;

        /// <summary>
        /// Returns whether this version is newer than the current version object.
        /// </summary>
        /// <param name="versionToBeCompared">The version to be compared.</param>
        /// <returns>True, if the specified compared version is newer than this version, returns false otherwise.</returns>
        public bool IsNewerThan(Version versionToBeCompared) => version > versionToBeCompared;
    }

    /// <summary>
    /// Provides extension methods for performing version comparison operations on <see cref="Version"/> objects.
    /// </summary>
    extension(Version)
    {
        /// <summary>
        /// Compares two versions and returns the newer version.
        /// </summary>
        /// <param name="versionA">The first version to compare.</param>
        /// <param name="versionB">The second version to compare.</param>
        /// <returns>The newer of the two specified versions.</returns>
        public static Version GetNewerVersion(Version versionA, Version versionB)
        {
            if (versionA > versionB)
                return versionA;
            return versionB;
        }

        /// <summary>
        /// Compares two versions and returns the older version.
        /// </summary>
        /// <param name="versionA">The first version to compare.</param>
        /// <param name="versionB">The second version to compare.</param>
        /// <returns>The older of the two specified versions.</returns>
        public static Version GetOlderVersion(Version versionA, Version versionB)
        {
            if (versionA < versionB)
                return versionA;
            return versionB;
        }
    }
}
