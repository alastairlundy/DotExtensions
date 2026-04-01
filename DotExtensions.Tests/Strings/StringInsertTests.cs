using Bogus;
using DotExtensions.Strings;

namespace DotExtensions.Tests.Strings;

public class StringInsertTests
{
    private readonly Faker _faker = new Faker();
    
    [Test]
    [Arguments("Hello")]
    [Arguments("World")]
    [Arguments("Avocado")]
    [Arguments("Banana")]
    public async Task StringInsert_Char_Works(string origin)
    {
        char c = _faker.Random.Char();
        
        int index = _faker.Random.Int(0, origin.Length - 1);
        
        string expected = $"{origin.Insert(index, c.ToString())}";
        
        string actual = origin.Insert(index, c);
        
        await Assert.That(actual)
            .IsEqualTo(expected);
    }
}