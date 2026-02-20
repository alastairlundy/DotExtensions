using System;
using Bogus;
using DotExtensions.Strings;

namespace DotExtensions.Tests.Strings;

public class LineEndingTests
{
    private readonly Faker _faker = new Faker();
    
    [Test]
    public async Task LineEnding_Detected()
    {
        string expected = OperatingSystem.IsWindows() ? "\r\n" : "\n";

        string testString = Environment.NewLine;

        string actual = testString.GetLineEnding();
        
        await Assert.That(expected)
            .IsEqualTo(actual);
    }
    
        
    [Test]
    public async Task LineEnding_Detected_FromString()
    {
        string expected = OperatingSystem.IsWindows() ? "\r\n" : "\n";

        string testString = string.Join(' ', _faker.Lorem.Words(Random.Shared.Next(1, 5)));

        string actual = testString.GetLineEnding();
        
        await Assert.That(expected)
            .IsEqualTo(actual);
    }

    [Test]
    public async Task LineEnding_Detected_AmongSentences()
    {
        string expected = OperatingSystem.IsWindows() ? "\r\n" : "\n";

        string testString = $"{_faker.Lorem.Sentence()}{Environment.NewLine}{_faker.Lorem.Sentence()}";

        string actual = testString.GetLineEnding();
        
        await Assert.That(expected)
            .IsEqualTo(actual);
    }
}