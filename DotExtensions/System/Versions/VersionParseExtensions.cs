/*
        MIT License
       
       Copyright (c) 2020-2026 Alastair Lundy
       
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
/// Provides extension methods for parsing and manipulating version strings.
/// </summary>
public static class VersionParseExtensions
{
    #region Version Parsing Helpers
    private static bool IsSeparator(char currentChar)
    {
        return currentChar == '.' || char.IsWhiteSpace(currentChar);
    }

    private static void ReadSeparatorRun(string versionString, int start, out bool containsDot, out int end)
    {
        containsDot = false;
        int index = start;

        while (index < versionString.Length && IsSeparator(versionString[index]))
        {
            if (versionString[index] == '.')
                containsDot = true;

            index++;
        }

        end = index;
    }

    private static int FindVersionStart(string versionString)
    {
        int index = 0;
        int firstDigitRun = -1;

        while (index < versionString.Length)
        {
            if (!char.IsDigit(versionString[index]))
            {
                index++;
                continue;
            }

            int digitRunEnd = index;

            while (digitRunEnd < versionString.Length && char.IsDigit(versionString[digitRunEnd]))
                digitRunEnd++;

            if (firstDigitRun == -1)
                firstDigitRun = index;

            ReadSeparatorRun(versionString, digitRunEnd, out bool containsDot, out int runEnd);

            if (containsDot && runEnd < versionString.Length && char.IsDigit(versionString[runEnd]))
                return index;

            index = digitRunEnd;
        }

        return firstDigitRun;
    }

    private static int ParseComponent(string versionString, ref int index)
    {
        int start = index;

        while (index < versionString.Length && char.IsDigit(versionString[index]))
            index++;

        if (!int.TryParse(versionString.Substring(start, index - start), NumberStyles.Integer, CultureInfo.InvariantCulture, out int component))
            component = int.MaxValue;

        return component;
    }
    #endregion

    extension(Version)
    {
        /// <summary>
        /// Gracefully parses a version string into a <see cref="Version"/> object.
        /// </summary>
        /// <remarks>
        /// Parsing scans left-to-right from the first usable digit run and collects at most four
        /// components separated by runs of dots and whitespace. Once the version ends, everything
        /// else is treated as a suffix and discarded: prerelease tags (<c>1.2.3-beta.1</c>),
        /// release groups (<c>5.2.15(1)-release</c>), and architecture tags (<c>3.10.11 (x64)</c>)
        /// all terminate parsing, including any digits they contain.
        /// </remarks>
        /// <param name="versionString">The version string to parse into a <see cref="Version"/> object.</param>
        /// <returns>Returns a gracefully parsed version.</returns>
        /// <exception cref="ArgumentException">Thrown if the provided <paramref name="versionString"/>
        /// string is null or empty or contains no digits.</exception>
        public static Version GracefulParse(string versionString)
        {
            ArgumentException.ThrowIfNullOrEmpty(versionString);
            ArgumentException.ThrowIfNullOrWhiteSpace(versionString);

            int index = FindVersionStart(versionString);

            if (index == -1)
            {
                throw new ArgumentException(string.Format(Resources.Exceptions_VersionParsing_InvalidVersionString, versionString), nameof(versionString));
            }

            int[] components = new int[4];
            int componentCount = 0;

            while (componentCount < 4)
            {
                components[componentCount] = ParseComponent(versionString, ref index);
                componentCount++;

                ReadSeparatorRun(versionString, index, out bool containsDot, out int runEnd);

                if (!containsDot || runEnd >= versionString.Length || !char.IsDigit(versionString[runEnd]))
                    break;

                index = runEnd;
            }

            return componentCount switch
            {
                1 => new Version(components[0], 0),
                2 => new Version(components[0], components[1]),
                3 => new Version(components[0], components[1], components[2]),
                _ => new Version(components[0], components[1], components[2], components[3])
            };
        }

        /// <summary>
        /// Attempts to gracefully parse a version string into a <see cref="Version"/> object.
        /// </summary>
        /// <param name="versionString">The version string to parse.</param>
        /// <param name="version">
        /// If the method returns <see langword="true"/>, contains the parsed <see cref="Version"/>
        /// object. Otherwise, it contains null.
        /// </param>
        /// <returns><see langword="true"/> if the parsing was successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGracefulParse(string versionString, out Version? version)
        {
            try
            {
                version = Version.GracefulParse(versionString);
                return true;
            }
            catch
            {
                version = null;
                return false;
            }
        }
    }
}