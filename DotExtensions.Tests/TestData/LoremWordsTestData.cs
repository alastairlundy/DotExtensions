using System;
using System.Collections;
using System.Collections.Generic;
using Bogus.DataSets;

namespace DotExtensions.Tests.TestData;

public class LoremWordsTestData : IEnumerable<object[]>
{
    private Lorem _lorem = new();

    public IEnumerator<object[]> GetEnumerator()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new object[] { string.Join(" ", _lorem.Words(Random.Shared.Next(2, 10))) };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
