using System.Collections;
using System.Collections.Generic;
using Bogus;

namespace DotExtensions.Tests.TestData;

public class AlphabeticalCharacterTestData : IEnumerable<object[]>
{
    
    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (char c in Chars.UpperCase)
        {
            yield return new object[] { c };
        }

        foreach (char c in Chars.LowerCase)
        {
            yield return new object[] { c };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}