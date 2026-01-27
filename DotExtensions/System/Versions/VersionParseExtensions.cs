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

using System.Linq;

namespace DotExtensions.Versions;

/// <summary>
/// Provides extension methods for parsing and manipulating version strings.
/// </summary>
public static class VersionParseExtensions
{
    extension(Version)
    {
        /// <summary>
        /// Gracefully parses a version string into a <see cref="Version"/> object.
        /// </summary>
        /// <param name="versionString">The version string to parse into a <see cref="Version"/> object.</param>
        /// <returns>Returns a gracefully parsed version.</returns>
        /// <exception cref="ArgumentException">Thrown if the provided <paramref name="versionString"/>
        /// string is null or empty or contains no digits.</exception>
        public static Version GracefulParse(string versionString)
        {
            ArgumentException.ThrowIfNullOrEmpty(versionString);

            char separator = '.';

            foreach (char currentChar in versionString)
            {
                if (char.IsSeparator(currentChar) || char.IsPunctuation(currentChar))
                {
                    separator = versionString.First(c => char.IsSeparator(c) || char.IsPunctuation(c));
                    break;
                }
            }
            
            if (versionString.Contains(','))
            {
                versionString = versionString.Replace(",", ".");
                separator = '.';
            }
            
            string[] versionComponents = versionString.Split(separator);

            StringBuilder stringBuilder = new(capacity: versionString.Length);
            int componentsAdded = 0;

            componentsAdded = Version.ParseComponents(versionComponents, componentsAdded, stringBuilder);

            if (componentsAdded == 0)
            {
                if (versionString.Any(c => char.IsDigit(c)))
                {
                    stringBuilder = stringBuilder.Clear();
                    stringBuilder =  stringBuilder.Append(versionString.First(c => char.IsDigit(c)));
                    componentsAdded = 1;
                }
            }
            
            if (componentsAdded == 1)
                stringBuilder.Append(".0");

            if (stringBuilder.Length > 0)
            {
                if(stringBuilder[^1] == '.')
                    stringBuilder = stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }
            
            string result = stringBuilder.ToString();
            
            if (string.IsNullOrEmpty(result))
                throw new ArgumentException(Resources
                    .Exceptions_Versions_GracefulParse_NoDigits, nameof(versionString));
            
            return new Version(result);
        }

        /// <summary>
        /// Parses version components and adds them to a StringBuilder.
        /// </summary>
        /// <param name="versionComponents">The string array containing version components.</param>
        /// <param name="componentsAdded">The number of components already added to the StringBuilder.</param>
        /// <param name="stringBuilder">The StringBuilder used to construct the final version string.</param>
        /// <returns>The updated count of components added to the StringBuilder.</returns>
        private static int ParseComponents(string[] versionComponents, int componentsAdded,
            StringBuilder stringBuilder)
        {
            int output = componentsAdded;

            versionComponents = versionComponents.Where(v =>
                {
                    int firstNumberIndex = v.IndexOf(v.FirstOrDefault(c => char.IsDigit(c)));

                    return firstNumberIndex != -1;
                })
                .Select(v =>
                {
                    int index = v.IndexOf(v.FirstOrDefault(c => char.IsDigit(c)));

                    return v.Substring(index);
                })
                .ToArray();
            
            
            foreach (string component in versionComponents)
            {
                if (output >= 4)
                    break;
                int digitsCount = 0;

                foreach (char currentChar in component)
                {
                    if (char.IsDigit(currentChar))
                    {
                        stringBuilder.Append(currentChar);
                        digitsCount++;
                    }
                }
                
                if (digitsCount > 0)
                {
                    stringBuilder.Append('.');
                    output++;

                    // Check for suffixes that should stop parsing
                    bool containsSuffix = component.Length > digitsCount &&
                                          !string.IsNullOrWhiteSpace(component
                                              .Substring(digitsCount));

                    if (containsSuffix)
                    {
                        break;
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Attempts to gracefully parse a version string into a <see cref="Version"/> object.
        /// </summary>
        /// <param name="versionString">The version string to parse.</param>
        /// <param name="version">
        /// If the method returns <c>true</c>, contains the parsed <see cref="Version"/>
        /// object. Otherwise, it contains null.
        /// </param>
        /// <returns><c>true</c> if the parsing was successful; otherwise, <c>false</c>.</returns>
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