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

namespace DotExtensions.Tests.IO.SafeEnumeration;

public class SafeEnumerationIntegrationTests
{
    [Test]
    public async Task SafelyEnumerateFiles_PathOverload_FindsFiles()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempPath);
        try
        {
            File.Create(Path.Combine(tempPath, "one.txt")).Dispose();
            File.Create(Path.Combine(tempPath, "two.txt")).Dispose();

            IEnumerable<FileInfo> files = Directory.SafelyEnumerateFiles(tempPath);

            await Assert.That(files.Count()).IsEqualTo(2);
        }
        finally
        {
            if (Directory.Exists(tempPath)) Directory.Delete(tempPath, true);
        }
    }

    [Test]
    public async Task SafelyGetDirectories_DirectoryInfoOverload_FindsSubdirectories()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        DirectoryInfo dir = Directory.CreateDirectory(tempPath);
        try
        {
            dir.CreateSubdirectory("alpha");
            dir.CreateSubdirectory("beta");

            DirectoryInfo[] dirs = dir.SafelyGetDirectories();

            await Assert.That(dirs.Length).IsEqualTo(2);
            string[] names = dirs.Select(d => d.Name).ToArray();
            await Assert.That(names).Contains("alpha");
            await Assert.That(names).Contains("beta");
        }
        finally
        {
            if (dir.Exists) dir.Delete(true);
        }
    }

    [Test]
    public async Task SafelyEnumerateLogicalDrives_ReturnsReadyDrives()
    {
        DriveInfo[] readyDrives = DriveInfo.GetDrives()
            .Where(d => d.IsReady && d.TotalSize > 0)
            .ToArray();

        if (readyDrives.Length == 0)
        {
            return;
        }

        DriveInfo[] drives = DriveInfo.SafelyEnumerateLogicalDrives().ToArray();

        await Assert.That(drives.Length).IsGreaterThan(0);

        foreach (DriveInfo drive in drives)
        {
            await Assert.That(drive.IsReady).IsTrue();
            await Assert.That(drive.TotalSize).IsGreaterThan(0);
        }
    }

    [Test]
    public async Task IsEmpty_EmptyDirectory_ReturnsTrue()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        DirectoryInfo dir = Directory.CreateDirectory(tempPath);
        try
        {
            await Assert.That(dir.IsEmpty).IsTrue();
        }
        finally
        {
            if (dir.Exists) dir.Delete(true);
        }
    }

    [Test]
    public async Task IsEmpty_DirectoryWithFiles_ReturnsFalse()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        DirectoryInfo dir = Directory.CreateDirectory(tempPath);
        try
        {
            File.Create(Path.Combine(dir.FullName, "test.txt")).Dispose();

            await Assert.That(dir.IsEmpty).IsFalse();
        }
        finally
        {
            if (dir.Exists) dir.Delete(true);
        }
    }

    [Test]
    public async Task HasFiles_DirectoryWithFiles_ReturnsTrue()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        DirectoryInfo dir = Directory.CreateDirectory(tempPath);
        try
        {
            File.Create(Path.Combine(dir.FullName, "test.txt")).Dispose();

            await Assert.That(dir.HasFiles).IsTrue();
        }
        finally
        {
            if (dir.Exists) dir.Delete(true);
        }
    }

    [Test]
    public async Task HasFiles_EmptyDirectory_ReturnsFalse()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        DirectoryInfo dir = Directory.CreateDirectory(tempPath);
        try
        {
            await Assert.That(dir.HasFiles).IsFalse();
        }
        finally
        {
            if (dir.Exists) dir.Delete(true);
        }
    }
}
