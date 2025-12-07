/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using Bogus.DataSets;
using DotExtensions.Tests.TestData;

namespace DotExtensions.Tests.Strings.Contains;

public class ContainsSpacesTests
{
    private readonly Lorem _lorem = new();

    [Theory]
    [ClassData(typeof(LoremWordsTestData))]
    public void UnSpacedWordDetection(string words)
    {
        string text = words.Replace(" ", string.Empty);

        bool actual = text.ContainsSpaceSeparatedSubStrings();

        Assert.False(actual);
    }

    [Theory]
    [ClassData(typeof(LoremWordsTestData))]
    public void SpacedWordsDetection(string words)
    {
        bool actual = words.ContainsSpaceSeparatedSubStrings();

        Assert.Equal(words.Contains(" "), actual);
    }

    [Fact]
    public void WordWithEmptyStringDetection()
    {
        string text = _lorem.Word() + " ";

        bool actual = text.ContainsSpaceSeparatedSubStrings();

        Assert.True(actual);
    }
}
