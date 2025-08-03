/*using System;
using System.Collections.Generic;
using System.Linq;

using AlastairLundy.DotExtensions.Linq.Enumerables;

using AlastairLundy.DotExtensions.Numbers;

using Bogus;
using Bogus.DataSets;

namespace DotExtensions.Tests.Collections.Enumerables;

public class ElementsAtTests
{
    #if NETFRAMEWORK
    private readonly Random _random = new Random();
    #endif
    
    private readonly Faker _faker = new Faker();
    
    [Fact]
    public void Enumerable_ElementsAt_Correct()
    {
        int count =
#if NETFRAMEWORK
            _random.Next(100, 10000);
#else
        Random.Shared.Next(100, 10000);
#endif
        
        IEnumerable<string> data = _faker.Make<string>(count, ()=> _faker.Lorem.Sentence());
        
        IEnumerable<int> indices = 0.RangeAsEnumerable(count);

       IEnumerable<string> expected = ElementsAt(data, indices);
       
       Assert.Equal(expected, data);
    }

    [Fact]
    public void Enumerable_ElementsAt_Incorrect()
    {
        int count =
#if NETFRAMEWORK
            _random.Next(100, 10000);
#else
        Random.Shared.Next(100, 10000);
#endif
        IEnumerable<string> data = _faker.Make<string>(count, ()=> _faker.Lorem.Sentence());
        
        IEnumerable<int> indices = 0.RangeAsEnumerable(count).Take(
#if !NETFRAMEWORK
                Random.Shared.Next(1, count - 50)
#else
                _random.Next(1, count - 50)
#endif
);

        IEnumerable<string> expected = EnumerableElementsAtExtensions.ElementsAt(data, indices);
       
        Assert.NotEqual(expected, data);
    }
}*/