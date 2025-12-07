/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

// ReSharper disable ConvertToAutoProperty
// ReSharper disable CheckNamespace
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
    ///
    /// </summary>
    public static string[] EscapeCharacters => EscapeChars;
}
