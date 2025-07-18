using System;
using System.Collections;
using System.Collections.Generic;
using Bogus.DataSets;

namespace SystemExtensions.Tests.TestData;

public class LoremWordsTestData : IEnumerable<object[]>
{
#if NETFRAMEWORK
    private readonly Random _random = new Random();
#endif
    
    private Lorem _lorem = new();
    public IEnumerator<object[]> GetEnumerator()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new object[] { string.Join(" ", _lorem.Words(
#if NETFRAMEWORK
                _random.Next(2, 20)
#else
                    Random.Shared.Next(2, 10)
#endif
                )) };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}