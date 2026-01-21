using System;
using DotExtensions.Versions;

namespace DotExtensions.Tests;

public class VersionParseTests
{
    [Theory]
    [InlineData("1.2.3.4", 1, 2, 3, 4)]
    [InlineData("1.2.3", 1, 2, 3)]
    [InlineData("1.2", 1, 2)]
    [InlineData("10.20.30.40", 10, 20, 30, 40)]
    [InlineData("1000.2000", 1000, 2000)]
    public void GracefulParse_StandardVersions_ReturnsCorrectVersion(string input, int major, int minor, 
        int build = -1, int revision = -1)
    {
        Version expected = build == -1 ? new Version(major, minor) : 
            revision == -1 ? new Version(major, minor, build) : 
            new Version(major, minor, build, revision);
        
        Version actual = Version.GracefulParse(input);
        
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("v1.2.3", 1, 2, 3)]
    [InlineData("ver1.2.3.4", 1, 2, 3, 4)]
    [InlineData("version 1.2.3", 1, 2, 3)]
    [InlineData("version 10.20.30.40", 10, 20, 30, 40)]
    public void GracefulParse_WithPrefixes_ReturnsCorrectVersion(string input, int major, int minor, 
        int build = -1, int revision = -1)
    {
        Version expected = build == -1 ? new Version(major, minor) : 
            revision == -1 ? new Version(major, minor, build) : 
            new Version(major, minor, build, revision);
        
        Version actual = Version.GracefulParse(input);
        
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("1 . 2 . 3", 1, 2, 3)]
    [InlineData(" 1.2.3 ", 1, 2, 3)]
    [InlineData("v 1 . 2 . 3", 1, 2, 3)]
    public void GracefulParse_WithSpaces_ReturnsCorrectVersion(string input, int major, int minor, 
        int build = -1, int revision = -1)
    {
        Version expected = build == -1 ? new Version(major, minor) : 
            revision == -1 ? new Version(major, minor, build) : 
            new Version(major, minor, build, revision);
        
        Version actual = Version.GracefulParse(input);
        
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("1.2.3-beta.1", 1, 2, 3)]
    [InlineData("1.2.3.4-alpha", 1, 2, 3, 4)]
    [InlineData("1.2.3.4.5", 1, 2, 3, 4)]
    [InlineData("10.20.300-beta.3", 10, 20, 300)]
    public void GracefulParse_WithSuffixesOrExtraComponents_ReturnsCorrectVersion(string input, int major, int minor, 
        int build = -1, int revision = -1)
    {
        Version expected = build == -1 ? new Version(major, minor) : 
            revision == -1 ? new Version(major, minor, build) : 
            new Version(major, minor, build, revision);
        
        Version actual = Version.GracefulParse(input);
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GracefulParse_SingleComponent_ReturnsVersionWithZeroMinor()
    {
        Version actual = Version.GracefulParse("1");
        Assert.Equal(new Version(1, 0), actual);
    }

    [Fact]
    public void GracefulParse_Null_ThrowsArgumentNullException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.Throws<ArgumentNullException>(() => Version.GracefulParse(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [Fact]
    public void GracefulParse_Empty_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Version.GracefulParse(""));
    }

    [Theory]
    [InlineData("v1", 1, 0)]
    [InlineData("1.0.0.0.0", 1, 0, 0, 0)]
    public void GracefulParse_EdgeCases_ReturnsCorrectVersion(string input, int major, int minor, int build = -1, int revision = -1)
    {
        Version expected = build == -1 ? new Version(major, minor) : 
            revision == -1 ? new Version(major, minor, build) : 
            new Version(major, minor, build, revision);
        
        Version actual = Version.GracefulParse(input);
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GracefulParse_NoDigits_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Version.GracefulParse("abc.def"));
    }
}