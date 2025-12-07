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

// ReSharper disable ConvertToAutoProperty
namespace AlastairLundy.DotExtensions.Strings;

/// <summary>
/// Provides predefined constants related to characters, specifically escape characters.
/// </summary>
public static class CharacterConstants
{
    private static readonly string[] EscapeChars =
    [
        "\r",
        "\n",
        "\t",
        "\v",
        @"\c",
        @"\e",
        "\f",
        "\a",
        "\b",
        @"\NNN",
        @"\xHH",
        "\\",
    ];

    /// <summary>
    /// Represents an array of predefined escape characters commonly used in strings.
    /// Provides a set of constants that can be referenced for operations involving
    /// the detection or removal of escape characters from strings.
    /// </summary>
    public static string[] EscapeCharacters => EscapeChars;
}
