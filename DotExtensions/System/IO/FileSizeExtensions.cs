/*
        MIT License

       Copyright (c) 2025 Alastair Lundy

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

using System.IO;
using AlastairLundy.DotExtensions.Numbers;

namespace AlastairLundy.DotExtensions.IO;

public static class FileSizeExtensions
{
    /// <summary>
    /// Returns a string representation of the file size, including the most suitable unit.
    /// </summary>
    /// <param name="file">The FileInfo to get the file size for.</param>
    /// <returns>A string representation of the file size with the most suitable unit.</returns>
    public static string GetFileSizeString(this FileInfo file)
    {
        long quantity = file.Length;
        
        while (quantity > 1000)
        {
            quantity /= 1000;
        }

        return $"{quantity}{GetFileSizeUnitString(file)}";
    }

    /// <summary>
    /// Gets a string representation of the most suitable file size unit.
    /// </summary>
    /// <param name="file">The FileInfo to get the file size unit for.</param>
    /// <returns>A string representation of the most suitable file size unit.</returns>
    public static string GetFileSizeUnitString(this FileInfo file)
    {
        int numberOfDigits = file.Length.CountNumberOfDigits();

        return numberOfDigits switch
        {
            >= 16 => "PB",
            >= 13 and <= 15 => "TB",
            >= 10 and <= 12 => "GB",
            >= 7 and <= 9 => "MB",
            >= 4 and <= 6 => "KB",
            <= 3 => "B"
        };
    }
}