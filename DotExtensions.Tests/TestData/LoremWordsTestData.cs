using System;
using System.Collections;
using System.Collections.Generic;
using Bogus.DataSets;

namespace SystemExtensions.Tests.TestData;

public class LoremWordsTestData : IEnumerable<object[]>
{
    private Lorem _lorem = new();
    
    #if NETFRAMEWORK
    private readonly Random _random;
    #endif

    public LoremWordsTestData()
    {
        #if NETFRAMEWORK
        _random = new Random();
#endif
    }
    
    public IEnumerator<object[]> GetEnumerator()
    {
        for (int i = 0; i < 20; i++)
        {
            #if NET5_0_OR_GREATER
            yield return new object[] { string.Join(" ", _lorem.Words(Random.Shared.Next(1, 10))) };
            #else
            yield return new object[] { string.Join(" ", _lorem.Words(_random.Next(1, 10))) };

            #endif
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}