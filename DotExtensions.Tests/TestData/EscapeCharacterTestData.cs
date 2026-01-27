namespace DotExtensions.Tests.TestData;

public class EscapeCharacterTestData
{
    public static IEnumerable<string> GetStrings()
    {
        foreach (string s in CharacterConstants.EscapeCharacters)
        {
            yield return s;
        }
    }
}
