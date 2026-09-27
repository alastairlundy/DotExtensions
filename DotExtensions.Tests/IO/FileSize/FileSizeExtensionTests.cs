using System;
using System.IO;
using DotExtensions.IO;

namespace DotExtensions.Tests.IO.FileSize;

public class FileSizeExtensionTests : IDisposable
{
    private const int TestFileBufferSize = 2048;

    private readonly FileInfo _testFile;

    public FileSizeExtensionTests()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        Directory.CreateDirectory(tempPath);

        _testFile = new FileInfo(Path.Combine(tempPath, "file-size-test.dat"));

        using (FileStream stream = _testFile.Create())
        {
            stream.Write(new byte[TestFileBufferSize]);
        }

        _testFile.Refresh();
    }

    public void Dispose()
    {
        try
        {
            _testFile.Directory?.Delete(recursive: true);
        }
        catch (Exception)
        {
            // Ignore cleanup failures so test teardown doesn't mask test results.
        }
    }

    [Test]
    public async Task FileSize_CalculatedCorrectly()
    {
        string actualFileSizeString = _testFile.GetFileSizeString();

        string fileSizeUnitString = _testFile.GetFileSizeUnitString();

        int length = actualFileSizeString.Length - fileSizeUnitString.Length;

        long actualFileSize = long.Parse(actualFileSizeString[..length]);

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

        int endIndex = Math.Abs(fileSizeString.Length - 1 - lastNumberIndex);

        string actual = fileSizeString.Substring(lastNumberIndex + 1, endIndex);

        string expected = _testFile.GetFileSizeUnitString();

        await Assert.That(actual)
            .IsEqualTo(expected)
            .And
            .IsNotEmpty();
    }
}
