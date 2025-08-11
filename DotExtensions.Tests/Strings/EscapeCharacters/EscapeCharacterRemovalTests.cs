﻿using AlastairLundy.DotExtensions.Strings;
using DotExtensions.Tests.TestData;

namespace DotExtensions.Tests.Strings.EscapeCharacters;

public class EscapeCharacterRemovalTests
{
    [Theory]
    [ClassData(typeof(EscapeCharacterTestData))]
    public void ContainsEscapedCharactersTest(string escapeCharacters)
    {
        bool actual = escapeCharacters.ContainsEscapeCharacters();
        
        Assert.True(actual);
    }

    [Theory]
    [ClassData(typeof(LoremWordsTestData))]
    public void DoesntContainEscapeCharactersTest(string word)
    {
        bool actual = word.ContainsEscapeCharacters();
        
        Assert.False(actual);
    }
    
    [Theory]
    [ClassData(typeof(EscapeCharacterTestData))]
    public void SuccessfullyRemoveEscapeCharsTests(string escapeCharacter)
    {
        string text = $"Hello World {escapeCharacter}";

        string expected = "Hello World";
        
        string actual = text.RemoveEscapeCharacters();
        
        Assert.Equal(expected, actual);
    }
}