using System;
using System.IO;
using DotExtensions.IO;

namespace DotExtensions.Tests.IO.FileSize;

public class FileSizeExtensionTests : IDisposable
{
    private readonly FileInfo _testFile;

    public FileSizeExtensionTests()
    {
        //Controlled fixture: a real file of known size (1024 bytes) instead of sampling the live
        //filesystem via FileInfo.GetRandomFile(), which made this test fail whenever the walk
        //landed on a 0-byte file or threw on a restricted drive.
        string path = Path.Combine(Path.GetTempPath(), $"DotExtensions_FileSizeTests_{Guid.NewGuid():N}.bin");
        File.WriteAllBytes(path, new byte[1024]);
        _testFile = new FileInfo(path);
    }

    public void Dispose()
    {
        try
        {
            File.Delete(_testFile.FullName);
        }
        catch (IOException)
        {
            //Best-effort temp-file cleanup; a leaked temp file must not fail the test run.
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
