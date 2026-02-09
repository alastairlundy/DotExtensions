using System;
using System.IO;
using DotExtensions.IO;

namespace DotExtensions.Tests.IO;

public class FileSizeExtensionTests
{
    private readonly FileInfo _testFile;

    public FileSizeExtensionTests()
    {
        _testFile = FileInfo.GetRandomFile();
    }
    
    [Test]
    public async Task FileSize_CalculatedCorrectly()
    {
        string actualFileSizeString = _testFile.GetFileSizeString();

        int end = actualFileSizeString
            .LastIndexOfAny(['0', '1', '2', '3', '4', '5', '6', '7', '8', '9']);
        
        long actualFileSize = long.Parse(actualFileSizeString.Substring(0, Math.Abs(0 - end)));
        
        await Assert.That(actualFileSizeString)
            .IsNotEmpty();

        await Assert.That(actualFileSize)
            .IsGreaterThan(0);
    } 
    
    [Test]
    public async Task FileSizeWithUnit_HasCorrectUnit()
    {
        string fileSizeString = _testFile.GetFileSizeString();

        int lastNumberIndex = fileSizeString.LastIndexOfAny(['0', '1', '2', '3', '4', '5', '6', '7', '8', '9']);
        
        int endIndex = Math.Abs(fileSizeString.Length -1 - lastNumberIndex);
        
        string actual = fileSizeString.Substring(lastNumberIndex + 1, endIndex);

        string expected = _testFile.GetFileSizeUnitString();

        await Assert.That(actual)
            .IsEqualTo(expected)
            .And
            .IsNotEmpty();
    }
}