using System;
using System.Collections.Generic;
using System.Linq;
using AlastairLundy.DotExtensions.Collections.Generic.Enumerables;
using AlastairLundy.DotExtensions.Numbers;
using Bogus;
using Bogus.DataSets;

namespace DotExtensions.Tests.Collections.Enumerables;

public class ElementsAtTests
{
    private readonly Faker _faker = new Faker();
    
    [Fact]
    public void Enumerable_ElementsAt_Correct()
    {
        int count = Random.Shared.Next(100, 10000);
        IEnumerable<string> data = _faker.Make<string>(count, ()=> _faker.Lorem.Sentence());
        
        IEnumerable<int> indices = 0.RangeAsEnumerable(count);

       IEnumerable<string> expected = EnumerableElementsAtExtensions.ElementsAt(data, indices);
       
       Assert.Equal(expected, data);
    }

    [Fact]
    public void Enumerable_ElementsAt_Incorrect()
    {
        int count = Random.Shared.Next(100, 10000);
        IEnumerable<string> data = _faker.Make<string>(count, ()=> _faker.Lorem.Sentence());
        
        IEnumerable<int> indices = 0.RangeAsEnumerable(count).Take(Random.Shared.Next(1, count - 50));

        IEnumerable<string> expected = EnumerableElementsAtExtensions.ElementsAt(data, indices);
       
        Assert.NotEqual(expected, data);
    }
}