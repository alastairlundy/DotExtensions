/*
        MIT License
       
       Copyright (c) 2020-2025 Alastair Lundy
       
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

namespace DotExtensions.Versions;

/// <summary>
/// Provides extension methods for comparing <see cref="Version"/> objects.
/// </summary>
public static class VersionComparisonExtensions
{
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
        [Obsolete(DeprecationMessages.DeprecationV10 + " . Use Version.Max() instead.")]
        public static Version GetNewerVersion(Version versionA, Version versionB)
            => Max(versionA, versionB);

        /// <summary>
        /// Compares two version objects and returns the higher version number.
        /// </summary>
        /// <param name="versionA">The first version to compare.</param>
        /// <param name="versionB">The second version to compare.</param>
        /// <returns>The newer of the two specified versions.</returns>
        public static Version Max(Version versionA, Version versionB)
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
        [Obsolete(DeprecationMessages.DeprecationV10 + " . Use Version.Min() instead.")]
        public static Version GetOlderVersion(Version versionA, Version versionB)
            => Min(versionA, versionB);

        /// <summary>
        /// Compares two versions and returns the older version.
        /// </summary>
        /// <param name="versionA">The first version to compare.</param>
        /// <param name="versionB">The second version to compare.</param>
        /// <returns>The older of the two specified versions.</returns>
        public static Version Min(Version versionA, Version versionB)
        {
            if (versionA < versionB)
                return versionA;
            return versionB;
        }
    }
}
