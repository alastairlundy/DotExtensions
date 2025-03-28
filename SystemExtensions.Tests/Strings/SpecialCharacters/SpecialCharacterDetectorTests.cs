
using AlastairLundy.DotExtensions.Strings;
using Bogus;
using SystemExtensions.Tests.TestData;

namespace SystemExtensions.Tests.Strings.SpecialCharacters;

public class SpecialCharacterDetectorTests
{
    [Theory]
    [ClassData(typeof(AlphabeticalCharacterTestData))]   
    public void NotASpecialCharacter(char character)
    {
        bool actual = character.IsSpecialCharacter();
        
        Assert.False(actual);
    }

    [Theory]
    [ClassData(typeof(SpecialCharacterTestData))]
    public void IsASpecialCharacter(char character)
    {
        bool actual = character.IsSpecialCharacter();
        
        Assert.True(actual);
    }
}