/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

// ReSharper disable ConvertToAutoProperty

namespace DotExtensions.Tests.TestData;

/// <summary>
///
/// </summary>
internal static class CharacterConstants
{
    private static readonly char[] SpecialChars =
    [
        ',',
        '.',
        '\\',
        '/',
        '^',
        '*',
        '&',
        '?',
        '!',
        '#',
        '~',
        '_',
        '+',
        '-',
        '@',
        '<',
        '>',
        '=',
        '(',
        ')',
        '%',
        '$',
        '£',
        '"',
        ';',
        ':',
        '{',
        '}',
        '[',
        ']',
    ];

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
    internal static char[] SpecialCharacters => SpecialChars;

    /// <summary>
    ///
    /// </summary>
    internal static string[] EscapeCharacters => EscapeChars;
}
