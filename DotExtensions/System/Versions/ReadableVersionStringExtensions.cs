/*
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

namespace AlastairLundy.DotExtensions.Versions;

/// <summary>
/// Provides extension methods for working with
/// version information in a human-readable format.
/// </summary>
public static class ReadableVersionStringExtensions
{
    /// <summary>
    /// Converts a Version object to a human-readable friendly string.
    /// </summary>
    /// <param name="version">The Version object to convert.</param>
    /// <returns>A string representing the version in a friendly format, e.g. "1.2.3", "1.2.3.4", etc.</returns>
    public static string ToReadableString(this Version version)
    {
        bool showMinor = version.Minor != 0;
        bool showBuild = version.Build != 0;
        bool showRevision = version.Revision != 0;

        switch (showRevision)
        {
            case true:
                return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
            case false:
            {
                if (showBuild)
                    return $"{version.Major}.{version.Minor}.{version.Build}";
                if (showMinor)
                    return $"{version.Major}.{version.Minor}";
                
                return version.Major.ToString();
            }
        }
    }
}
