using System;
using System.Linq;
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

        string testString = string.Join(' ', _faker.Lorem.Words(Random.Shared.Next(1, 10))
            .Append(Environment.NewLine));

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

    [Test]
    public async Task LineEnding_AdjacentCrLf_Detected()
    {
        string actual = "a\r\nb".GetLineEnding();

        await Assert.That(actual).IsEqualTo("\r\n");
    }

    [Test]
    public async Task LineEnding_BareLfOnly_Detected()
    {
        string actual = "a\nb".GetLineEnding();

        await Assert.That(actual).IsEqualTo("\n");
    }

    [Test]
    public async Task LineEnding_BareCrOnly_Detected()
    {
        string actual = "a\rb".GetLineEnding();

        await Assert.That(actual).IsEqualTo("\r");
    }

    [Test]
    public async Task LineEnding_BareLfAndCrWithoutPair_NotClassifiedAsCrLf()
    {
        string actual = "a\nb\rc".GetLineEnding();

        await Assert.That(actual).IsEqualTo("\n");
    }

    [Test]
    public async Task LineEnding_NoLineEndings_FallsBackToPlatformDefault()
    {
        string expected = OperatingSystem.IsWindows() ? "\r" : "\n";

        string actual = "Hello World".GetLineEnding();

        await Assert.That(actual).IsEqualTo(expected);
    }
}