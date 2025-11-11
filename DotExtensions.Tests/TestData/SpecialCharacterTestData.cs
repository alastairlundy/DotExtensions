using System.Collections;
using System.Collections.Generic;

namespace DotExtensions.Tests.TestData;

public class SpecialCharacterTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (char c in CharacterConstants.SpecialCharacters)
        {
            yield return new object[] { c };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}