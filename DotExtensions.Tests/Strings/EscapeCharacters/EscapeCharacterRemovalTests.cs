using DotExtensions.Strings;
using DotExtensions.Tests.TestData;

namespace DotExtensions.Tests.Strings.EscapeCharacters;

public class EscapeCharacterRemovalTests
{
    [Test]
    [MethodDataSource(nameof(EscapeCharacterTestData.GetEnumerator))]
    public async Task ContainsEscapedCharactersTest(string escapeCharacters)
    {
        bool actual = escapeCharacters.ContainsEscapeCharacters();

        await Assert.That(actual).IsTrue();
    }

    [Test]
    [MethodDataSource(nameof(EscapeCharacterTestData.GetEnumerator))]
    public async Task ContainsEscapeCharacters_string_Test(string escapeCharacters)
    {
        bool actual = string.IsEscapeCharacter(escapeCharacters);

        await Assert.That(actual).IsTrue();
    }

    [Test]
    [MethodDataSource(nameof(LoremWordsTestData.GetEnumerator))]
    public async Task DoesntContainEscapeCharactersTest(string word)
    {
        bool actual = word.ContainsEscapeCharacters();

        await Assert.That(actual).IsFalse();
    }

    [Test]
    [MethodDataSource(nameof(EscapeCharacterTestData.GetEnumerator))]
    public async Task SuccessfullyRemoveEscapeCharsTests(string escapeCharacter)
    {
        string text = $"Hello World {escapeCharacter}";

        string expected = "Hello World";

        string actual = text.RemoveEscapeCharacters();

        await Assert.That(expected)
            .IsEqualTo(actual);
    }
}
