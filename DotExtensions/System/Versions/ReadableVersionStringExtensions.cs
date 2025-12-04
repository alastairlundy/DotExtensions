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
