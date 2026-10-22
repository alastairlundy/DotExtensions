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
using System.Linq;
using DotExtensions.IO;
using DotExtensions.IO.Directories;
using DotExtensions.IO.Files;

namespace DotExtensions.Tests.IO.SafeEnumeration;

public class SafeFileEnumerationTests
{
    [Test]
    public async Task SafeFileEnumeration_NoExceptions()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory);

        IEnumerable<FileInfo> files = directoryInfo.SafelyEnumerateFiles();

        await Assert.That(files.Any())
            .IsTrue();
    }

    [Test]
    public async Task SafeFileGetting_NoExceptions()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory);

        FileInfo[] files = directoryInfo.SafelyGetFiles();

        Assert.NotNull(files);
        await Assert.That(files.Length)
            .IsGreaterThan(0);
    }

    [Test]
    public async Task SafeFileGetting_WithSearchOption_NoExceptions()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory);

        FileInfo[] files = directoryInfo.SafelyGetFiles("*", SearchOption.AllDirectories);

        Assert.NotNull(files);
        await Assert.That(files.Length)
            .IsGreaterThan(0);
    }

    [Test]
    public async Task ToEnumerationOptions_DoesNotSkipHiddenOrSystemEntries()
    {
        EnumerationOptions options = SearchOption.TopDirectoryOnly.ToEnumerationOptions();

        await Assert.That(options.AttributesToSkip)
            .IsEqualTo(FileAttributes.None);
    }

    [Test]
    public async Task ToEnumerationOptions_RecursionDepthIsCappedWhenRecursing()
    {
        EnumerationOptions allDirectoriesOptions = SearchOption.AllDirectories.ToEnumerationOptions();

        await Assert.That(allDirectoriesOptions.RecurseSubdirectories)
            .IsTrue();
        await Assert.That(allDirectoriesOptions.MaxRecursionDepth)
            .IsEqualTo(64);

        EnumerationOptions topDirectoryOnlyOptions = SearchOption.TopDirectoryOnly.ToEnumerationOptions();

        await Assert.That(topDirectoryOnlyOptions.RecurseSubdirectories)
            .IsFalse();
        await Assert.That(topDirectoryOnlyOptions.MaxRecursionDepth)
            .IsEqualTo(0);
    }

    [Test]
    public async Task SafelyGetFiles_DefaultsToCaseInsensitiveMatching()
    {
        string tempDir = Path.Combine(Path.GetTempPath(),
            $"DotExtensions_SafeFileEnumeration_{Guid.NewGuid():N}");
        DirectoryInfo directoryInfo = Directory.CreateDirectory(tempDir);

        try
        {
            File.WriteAllText(Path.Combine(tempDir, "file.txt"), "test");

            //ignoreCase defaults to true, so an uppercase pattern must still match "file.txt"
            //on every platform, including case-sensitive Linux filesystems.
            FileInfo[] files = directoryInfo.SafelyGetFiles("*.TXT");

            await Assert.That(files.Length)
                .IsEqualTo(1);
            await Assert.That(files[0].Name)
                .IsEqualTo("file.txt");
        }
        finally
        {
            directoryInfo.Delete(recursive: true);
        }
    }

    [Test]
    public async Task SafelyEnumerateFiles_MissingDirectory_ReturnsEmptySequence()
    {
        string missingPath = Path.Combine(Path.GetTempPath(),
            $"DotExtensions_MissingDirectory_{Guid.NewGuid():N}");

        //Must not throw DirectoryNotFoundException; the "Safely" contract yields an empty sequence.
        IEnumerable<FileInfo> files = Directory.SafelyEnumerateFiles(missingPath, "*",
            SearchOption.TopDirectoryOnly);

        await Assert.That(files.Any())
            .IsFalse();
    }

    [Test]
    public async Task SafelyEnumerateDirectories_MissingDirectory_ReturnsEmptySequence()
    {
        string missingPath = Path.Combine(Path.GetTempPath(),
            $"DotExtensions_MissingDirectory_{Guid.NewGuid():N}");

        //Must not throw DirectoryNotFoundException; the "Safely" contract yields an empty sequence.
        IEnumerable<DirectoryInfo> directories = Directory.SafelyEnumerateDirectories(missingPath, "*",
            SearchOption.TopDirectoryOnly);

        await Assert.That(directories.Any())
            .IsFalse();
    }
}