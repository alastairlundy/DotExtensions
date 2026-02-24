using System;
using System.Linq;
using System.Text;
using Bogus;
using DotExtensions.Strings;

namespace DotExtensions.Tests.Strings.Cases;

public class CapitalizationTests
{
    private readonly Faker _faker = new Faker();

    [Test]
    public async Task CapitalizeLowerCase_Char()
    {
        string fakeName = _faker.Name.FirstName().ToLower();

        int index = Random.Shared.Next(0, fakeName.Length - 1);
        
        string expected = fakeName.Insert(index, fakeName[index].ToString().ToUpper());
        expected = expected.Remove(index + 1, 1);

        string actual = fakeName.CapitalizeChar(index);

        await Assert.That(expected)
            .IsEqualTo(actual);
    }

    [Test]
    public async Task CapitalizeLowerCase_Chars()
    {
        string fakeWord = _faker.Name.FullName().ToLower();

        int numberOfCharsToCapitalize = Random.Shared.Next(0, fakeWord.Length - 1);

        IList<int> charsToCapitalize =
            _faker.Make(numberOfCharsToCapitalize, _ => Random.Shared.Next(0, fakeWord.Length - 1));

        StringBuilder sb = new StringBuilder(fakeWord);

        for (int i = 0; i < numberOfCharsToCapitalize; i++)
        {
            sb[charsToCapitalize[i]] = sb[charsToCapitalize[i]].ToString().ToUpper().First();
        }

        string expected = sb.ToString();
        
        string actual = fakeWord.CapitalizeChars(charsToCapitalize);
        
        await Assert.That(expected)
            .IsEqualTo(actual);
    }
}