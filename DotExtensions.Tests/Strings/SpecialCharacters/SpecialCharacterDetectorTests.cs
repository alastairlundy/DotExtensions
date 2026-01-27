using DotExtensions.Strings;
using DotExtensions.Tests.TestData;

namespace DotExtensions.Tests.Strings.SpecialCharacters;

public class SpecialCharacterDetectorTests
{
    public static IEnumerable<char> AlphabetChars()
        => AlphabeticalCharacterTestData.GetChars();

    public static IEnumerable<char> GetSpecialChars()
        => SpecialCharacterTestData.GetSpecialCharacters();
    
    [Test]
    [MethodDataSource(nameof(AlphabetChars))]
    public async Task NotASpecialCharacter(char character)
    {
        bool actual = char.IsSpecialCharacter(character);

        await Assert.That(actual)
            .IsFalse();
    }

    [Test]
    [MethodDataSource(nameof(GetSpecialChars))]
    public async Task IsASpecialCharacter(char character)
    {
        bool actual = char.IsSpecialCharacter(character);

        await Assert.That(actual).IsTrue();
    }
}
