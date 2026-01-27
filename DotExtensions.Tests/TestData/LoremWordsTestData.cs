using System;
using Bogus.DataSets;

namespace DotExtensions.Tests.TestData;

public class LoremWordsTestData
{
    private static readonly Lorem Lorem = new();

    public static IEnumerable<string> GetLoremWords()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return string.Join(" ", Lorem.Words(Random.Shared.Next(2, 10)));
        }
    }
}
