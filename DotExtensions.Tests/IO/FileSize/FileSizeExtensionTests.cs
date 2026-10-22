/*
        MIT License

       Copyright (c) 2026 Alastair Lundy

       Permission is hereby granted, free of charge, to any person obtaining a copy
       of this software and associated documentation files (the "Software"), to deal
       in the Software without restriction, including without limitation the rights
       to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
       copies of the Software, and to permit persons to whom the Software is
       furnished to do so, subject to the following conditions:

       The above copyright notice and this permission notice shall be included in all
       copies or substantial portions of the Software.

       THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
       IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
       FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
       AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
       LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
       OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
       SOFTWARE.
   */

using System;
using System.IO;
using DotExtensions.IO;

namespace DotExtensions.Tests.IO.FileSize;

public class FileSizeExtensionTests : IDisposable
{
    private static readonly (long Length, long ExpectedQuantity, string Expected)[] Cases =
    [
        (0, 0, "0B"),
        (999, 999, "999B"),
        (1000, 1, "1KB"),
        (1_000_000, 1, "1MB"),
        (1_999_999, 1, "1MB"),
    ];

    private readonly DirectoryInfo _tempDirectory;
    private readonly Dictionary<long, FileInfo> _filesByLength = [];

    public FileSizeExtensionTests()
    {
        //Deterministic fixture: temp files of known lengths instead of sampling the live filesystem
        //via FileInfo.GetRandomFile(), which could land on a 0-byte file or a restricted drive and fail.
        _tempDirectory = Directory.CreateDirectory(
            Path.Combine(Path.GetTempPath(), $"DotExtensions_FileSizeTests_{Guid.NewGuid():N}"));

        foreach ((long length, _, _) in Cases)
        {
            string path = Path.Combine(_tempDirectory.FullName, $"file_{length}.bin");

            using (FileStream stream = File.Create(path))
            {
                stream.SetLength(length);
            }

            _filesByLength[length] = new FileInfo(path);
        }
    }

    public void Dispose()
    {
        try
        {
            _tempDirectory.Delete(recursive: true);
        }
        catch (IOException)
        {
            //Best-effort temp cleanup; a leaked temp file must not fail the test run.
        }
        catch (UnauthorizedAccessException)
        {
        }
    }

    [Test]
    public async Task GetFileSizeString_FormatsKnownLengthsCorrectly()
    {
        foreach ((long length, long expectedQuantity, string expected) in Cases)
        {
            FileInfo file = _filesByLength[length];

            //The unit suffix is derived via the public unit API because Resources.Labels_Size_* is
            //internal to DotExtensions (InternalsVisibleTo only covers DotExtensions.Benchmarking),
            //then cross-checked against the known default (English) resource strings.
            string expectedUsingApiUnit = string.Concat(expectedQuantity.ToString(),
                file.GetFileSizeUnitString());

            await Assert.That(file.GetFileSizeString())
                .IsEqualTo(expectedUsingApiUnit);

            await Assert.That(file.GetFileSizeString())
                .IsEqualTo(expected);
        }
    }

    [Test]
    public async Task GetFileSizeUnitString_ScalesWithLength()
    {
        string bytesUnit = _filesByLength[999].GetFileSizeUnitString();
        string kilobytesUnit = _filesByLength[1000].GetFileSizeUnitString();
        string megabytesUnit = _filesByLength[1_000_000].GetFileSizeUnitString();

        await Assert.That(kilobytesUnit)
            .IsNotEqualTo(bytesUnit);
        await Assert.That(megabytesUnit)
            .IsNotEqualTo(kilobytesUnit);
        await Assert.That(_filesByLength[1_999_999].GetFileSizeUnitString())
            .IsEqualTo(megabytesUnit);
    }
}
