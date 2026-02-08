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

using DotExtensions.Numbers;

namespace DotExtensions.IO;

/// <summary>
/// Provides extension methods for working with file sizes in a more human-readable format.
/// </summary>
public static class FileSizeExtensions
{
    /// <param name="file"></param>
    extension(FileInfo file)
    {
        /// <summary>
        /// Returns a string representation of the file size, including the most suitable unit.
        /// </summary>
        /// <returns>A string representation of the file size with the most suitable unit.</returns>
        public string GetFileSizeString()
        {
            long quantity = file.Length;

            while (quantity > 1000)
            {
                quantity /= 1000;
            }

            return $"{quantity}{file.GetFileSizeUnitString()}";
        }

        /// <summary>
        /// Gets a string representation of the most suitable file size unit.
        /// </summary>
        /// <returns>A string representation of the most suitable file size unit.</returns>
        public string GetFileSizeUnitString()
        {
            int numberOfDigits = file.Length.CountNumberOfDigits();

            return numberOfDigits switch
            {
                >= 16 => "PB",
                >= 13 => "TB",
                >= 10 => "GB",
                >= 7 => "MB",
                >= 4 => "KB",
                <= 3 => "B",
            };
        }
    }
}