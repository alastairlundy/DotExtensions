using Bogus;

namespace DotExtensions.Tests.TestData;

public class AlphabeticalCharacterTestData
{
    public static IEnumerable<char> GetChars()
    {
        foreach (char c in Chars.UpperCase)
        {
            yield return c;
        }

        foreach (char c in Chars.LowerCase)
        {
            yield return c;
        }
    }
}
