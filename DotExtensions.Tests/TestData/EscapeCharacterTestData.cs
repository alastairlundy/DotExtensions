using System.Collections;
using System.Collections.Generic;
using AlastairLundy.DotExtensions.Strings;

namespace DotExtensions.Tests.TestData;

public class EscapeCharacterTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (string s in CharacterConstants.EscapeCharacters)
        {
            yield return new object[] { s };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}