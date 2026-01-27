using DotExtensions.Strings;
using DotExtensions.Tests.TestData;

namespace DotExtensions.Tests.Strings.SpecialCharacters;

public class SpecialCharacterDetectorTests
{
    [Test]
    [MethodDataSource(nameof(AlphabeticalCharacterTestData))]
    public async Task NotASpecialCharacter(char character)
    {
        bool actual = char.IsSpecialCharacter(character);

        await Assert.That(actual)
            .IsFalse();
    }

    [Test]
    [MethodDataSource(nameof(SpecialCharacterTestData))]
    public async Task IsASpecialCharacter(char character)
    {
        bool actual = char.IsSpecialCharacter(character);

        await Assert.That(actual).IsTrue();
    }
}
