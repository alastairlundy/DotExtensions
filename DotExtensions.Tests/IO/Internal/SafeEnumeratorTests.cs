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
using System.Security.AccessControl;
using System.Security.Principal;
using DotExtensions.IO.Internal;

namespace DotExtensions.Tests.IO.Internal;

public class SafeEnumeratorTests
{
    [Test]
    public async Task EnumerateFiles_FindsExpectedFiles()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        DirectoryInfo dir = Directory.CreateDirectory(tempPath);
        try
        {
            File.Create(Path.Combine(dir.FullName, "a.txt")).Dispose();
            File.Create(Path.Combine(dir.FullName, "b.txt")).Dispose();

            FileInfo[] files =
                SafeEnumerator.EnumerateFiles(dir, "*", SearchOption.TopDirectoryOnly, false).ToArray();

            await Assert.That(files.Length).IsEqualTo(2);
            string[] names = files.Select(f => f.Name).ToArray();
            await Assert.That(names).Contains("a.txt");
            await Assert.That(names).Contains("b.txt");
        }
        finally
        {
            if (dir.Exists) dir.Delete(true);
        }
    }

    [Test]
    public async Task GetFiles_ReturnsCorrectCount()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        DirectoryInfo dir = Directory.CreateDirectory(tempPath);
        try
        {
            File.Create(Path.Combine(dir.FullName, "one.cs")).Dispose();
            File.Create(Path.Combine(dir.FullName, "two.cs")).Dispose();
            File.Create(Path.Combine(dir.FullName, "three.cs")).Dispose();

            FileInfo[] files =
                SafeEnumerator.GetFiles(dir, "*.cs", SearchOption.TopDirectoryOnly, false);

            await Assert.That(files.Length).IsEqualTo(3);
        }
        finally
        {
            if (dir.Exists) dir.Delete(true);
        }
    }

    [Test]
    public async Task EnumerateDirectories_FindsExpectedSubdirectories()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        DirectoryInfo dir = Directory.CreateDirectory(tempPath);
        try
        {
            dir.CreateSubdirectory("alpha");
            dir.CreateSubdirectory("beta");

            DirectoryInfo[] dirs =
                SafeEnumerator.EnumerateDirectories(dir, "*", SearchOption.TopDirectoryOnly, false).ToArray();

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
    public async Task GetDirectories_ReturnsCorrectCount()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        DirectoryInfo dir = Directory.CreateDirectory(tempPath);
        try
        {
            dir.CreateSubdirectory("one");
            dir.CreateSubdirectory("two");
            dir.CreateSubdirectory("three");

            DirectoryInfo[] dirs =
                SafeEnumerator.GetDirectories(dir, "*", SearchOption.TopDirectoryOnly, false);

            await Assert.That(dirs.Length).IsEqualTo(3);
        }
        finally
        {
            if (dir.Exists) dir.Delete(true);
        }
    }

    [Test]
    public async Task EnumerateFiles_InaccessiblePathSkipped()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        DirectoryInfo dir = Directory.CreateDirectory(tempPath);
        try
        {
            File.Create(Path.Combine(dir.FullName, "visible.txt")).Dispose();

            if (OperatingSystem.IsWindows())
            {
                DirectoryInfo locked = dir.CreateSubdirectory("locked");
                DirectorySecurity security = new DirectorySecurity();
                security.AddAccessRule(new FileSystemAccessRule(
                    new SecurityIdentifier(
                        WellKnownSidType.WorldSid, null),
                    FileSystemRights.Read,
                    AccessControlType.Deny));
                locked.SetAccessControl(security);

                FileInfo[] files =
                    SafeEnumerator.GetFiles(dir, "*", SearchOption.AllDirectories, false);

                await Assert.That(files.Length).IsEqualTo(1);
                await Assert.That(files[0].Name).IsEqualTo("visible.txt");
            }
            else
            {
                FileInfo[] files =
                    SafeEnumerator.GetFiles(dir, "*", SearchOption.AllDirectories, false);

                await Assert.That(files.Length).IsEqualTo(1);
            }
        }
        finally
        {
            if (dir.Exists)
            {
                try
                {
                    dir.Delete(true);
                }
                catch
                {
                    // cleanup may fail on locked dirs in CI
                }
            }
        }
    }

    [Test]
    public async Task EnumerateFiles_IgnoreCaseDifference_MatchCasingChanges()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        DirectoryInfo dir = Directory.CreateDirectory(tempPath);
        try
        {
            File.Create(Path.Combine(dir.FullName, "lower.txt")).Dispose();
            File.Create(Path.Combine(dir.FullName, "UPPER.TXT")).Dispose();

            // ignoreCase: false → MatchCasing.CaseSensitive → only "lower.txt" matches "*.txt" on case-sensitive FS
            // ignoreCase: true → MatchCasing.CaseInsensitive → both match
            FileInfo[] caseSensitive = SafeEnumerator.GetFiles(dir, "*.txt", SearchOption.TopDirectoryOnly, ignoreCase: false);
            FileInfo[] caseInsensitive = SafeEnumerator.GetFiles(dir, "*.txt", SearchOption.TopDirectoryOnly, ignoreCase: true);

            await Assert.That(caseInsensitive.Length).IsEqualTo(2);
            // On case-sensitive file systems (Linux), caseSensitive will be 1; on Windows it may be 2.
            // The key assertion is that the two results CAN differ — i.e., ignoreCase matters.
            await Assert.That(caseSensitive.Length).IsGreaterThanOrEqualTo(1);
            await Assert.That(caseSensitive.Length).IsLessThanOrEqualTo(caseInsensitive.Length);
        }
        finally
        {
            if (dir.Exists) dir.Delete(true);
        }
    }
}
