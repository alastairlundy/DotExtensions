using System;
using System.IO;
using DotExtensions.IO;

namespace DotExtensions.Tests.IO.FileSize;

public class FileSizeExactPowerTests : IDisposable
{
    private readonly string _tempDirectory;

    public FileSizeExactPowerTests()
    {
        _tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        Directory.CreateDirectory(_tempDirectory);
    }

    public void Dispose()
    {
        try
        {
            Directory.Delete(_tempDirectory, recursive: true);
        }
        catch (Exception)
        {
            // Ignore cleanup failures so test teardown doesn't mask test results.
        }
    }

    private FileInfo CreateFileWithLength(string name, int byteCount)
    {
        FileInfo file = new(Path.Combine(_tempDirectory, name));

        using (FileStream stream = file.Create())
        {
            stream.Write(new byte[byteCount]);
        }

        file.Refresh();

        return file;
    }

    [Test]
    public async Task ExactThousandBytes_FormatsAsOneKilobyte()
    {
        FileInfo file = CreateFileWithLength("exact-1000.dat", 1000);

        string sizeString = file.GetFileSizeString();

        // Regression: 1000 used to be reported as "1000KB" because the loop condition was `> 1000`.
        await Assert.That(sizeString)
            .IsEqualTo("1KB");
    }

    [Test]
    public async Task ExactMillionBytes_FormatsAsOneMegabyte()
    {
        FileInfo file = CreateFileWithLength("exact-1000000.dat", 1_000_000);

        string sizeString = file.GetFileSizeString();

        // Regression: 1000000 used to be reported as "1000MB".
        await Assert.That(sizeString)
            .IsEqualTo("1MB");
    }

    [Test]
    public async Task NineHundredNinetyNineBytes_KeepsByteUnit()
    {
        FileInfo file = CreateFileWithLength("exact-999.dat", 999);

        string sizeString = file.GetFileSizeString();

        await Assert.That(sizeString)
            .IsEqualTo("999B");
    }

    [Test]
    public async Task OneThousandAndOneBytes_FormatsAsOneKilobyte()
    {
        FileInfo file = CreateFileWithLength("exact-1001.dat", 1001);

        string sizeString = file.GetFileSizeString();

        await Assert.That(sizeString)
            .IsEqualTo("1KB");
    }
}
