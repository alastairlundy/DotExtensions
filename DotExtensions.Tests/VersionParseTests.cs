using System;
using DotExtensions.Versions;

namespace DotExtensions.Tests;

public class VersionParseTests
{
    [Test]
    [Arguments("1.2.3.4", 1, 2, 3, 4)]
    [Arguments("1.2.3", 1, 2, 3)]
    [Arguments("1.2", 1, 2)]
    [Arguments("10.20.30.40", 10, 20, 30, 40)]
    [Arguments("1000.2000", 1000, 2000)]
    public async Task GracefulParse_StandardVersions_ReturnsCorrectVersion(string input, int major, int minor, 
        int build = -1, int revision = -1)
    {
        Version expected = build == -1 ? new Version(major, minor) : 
            revision == -1 ? new Version(major, minor, build) : 
            new Version(major, minor, build, revision);
        
        Version actual = Version.GracefulParse(input);
        
        await Assert.That(expected).IsEqualTo(actual);
    }

    [Test]
    [Arguments("v1.2.3", 1, 2, 3)]
    [Arguments("ver1.2.3.4", 1, 2, 3, 4)]
    [Arguments("version 1.2.3", 1, 2, 3)]
    [Arguments("version 10.20.30.40", 10, 20, 30, 40)]
    public async Task GracefulParse_WithPrefixes_ReturnsCorrectVersion(string input, int major, int minor, 
        int build = 0, int revision = 0)
    {
        Version expected = new Version(major, minor, build, revision);
        
        Version actual = Version.GracefulParse(input);
        
        await Assert.That(expected).IsEqualTo(actual);
    }

    [Test]
    [Arguments("1 . 2 . 3", 1, 2, 3)]
    [Arguments(" 1.2.3 ", 1, 2, 3)]
    [Arguments("v 1 . 2 . 3", 1, 2, 3)]
    public async Task GracefulParse_WithSpaces_ReturnsCorrectVersion(string input, int major, int minor, 
        int build = 0)
    {
        Version expected = new Version(major, minor, build);
        
        Version actual = Version.GracefulParse(input);
        
        await Assert.That(expected).IsEqualTo(actual);
    }

    [Test]
    [Arguments("1.2.3-beta.1", 1, 2, 3)]
    [Arguments("1.2.3.4-alpha", 1, 2, 3, 4)]
    [Arguments("1.2.3.4.5", 1, 2, 3, 4)]
    [Arguments("10.20.300-beta.3", 10, 20, 300)]
    public async Task GracefulParse_WithSuffixesOrExtraComponents_ReturnsCorrectVersion(string input, int major, int minor, 
        int build = 0, int revision = 0)
    {
        Version expected = new Version(major, minor, build, revision);
        
        Version actual = Version.GracefulParse(input);
        
        await Assert.That(expected).IsEqualTo(actual);
    }

    [Test]
    public async Task GracefulParse_SingleComponent_ReturnsVersionWithZeroMinor()
    {
        Version actual = Version.GracefulParse("1");
        
        await Assert.That(new Version(1, 0))
            .IsEqualTo(actual);
    }

    [Test]
    public async Task GracefulParse_Null_ThrowsArgumentNullException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        await Assert.ThrowsAsync<ArgumentNullException>(() => Task.FromResult(Version.GracefulParse(null)));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [Test]
    public async Task GracefulParse_Empty_ThrowsArgumentException()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => Task.FromResult(Version.GracefulParse("")));
    }

    [Test]
    [Arguments("v1", 1, 0)]
    [Arguments("1.0.0.0.0", 1, 0, 0, 0)]
    public async Task GracefulParse_EdgeCases_ReturnsCorrectVersion(string input, int major, int minor, int build = 0, int revision = 0)
    {
        Version expected = new Version(major, minor, build, revision);
        
        Version actual = Version.GracefulParse(input);
        
        await Assert.That(expected).IsEqualTo(actual);
    }

    [Test]
    public async Task GracefulParse_NoDigits_ThrowsArgumentException()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => Task.FromResult(Version.GracefulParse("abc.def")));
    }
}