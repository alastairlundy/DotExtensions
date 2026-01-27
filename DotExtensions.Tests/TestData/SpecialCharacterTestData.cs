namespace DotExtensions.Tests.TestData;

public class SpecialCharacterTestData
{
    public static IEnumerable<char> GetSpecialCharacters()
    {
        foreach (char c in CharacterConstants.SpecialCharacters)
        {
            yield return c;
        }
    }
}
