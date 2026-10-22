using System;
using System.IO;
using System.Linq;
using DotExtensions.IO;

namespace DotExtensions.Tests.IO.SafeEnumeration;

public class SafeEnumerationMissingRootTests
{
    private static string CreateNonExistentPath()
    {
        string path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString(), "does-not-exist");

        // Guard against an astronomically unlikely pre-existing collision.
        if (Directory.Exists(path))
        {
            Directory.Delete(path, recursive: true);
        }

        return path;
    }

    [Test]
    public async Task SafelyGetFiles_MissingDirectory_ReturnsEmptyArray()
    {
        string missing = CreateNonExistentPath();

        FileInfo[] files = Directory.SafelyGetFiles(missing);

        await Assert.That(files.Length).IsEqualTo(0);
    }

    [Test]
    public async Task SafelyEnumerateFiles_MissingDirectory_ReturnsEmptyEnumerable()
    {
        string missing = CreateNonExistentPath();

        IEnumerable<FileInfo> files = Directory.SafelyEnumerateFiles(missing);

        await Assert.That(files.Count()).IsEqualTo(0);
    }

    [Test]
    public async Task SafelyGetDirectories_MissingDirectory_ReturnsEmptyArray()
    {
        string missing = CreateNonExistentPath();

        DirectoryInfo[] directories = Directory.SafelyGetDirectories(missing, "*");

        await Assert.That(directories.Length).IsEqualTo(0);
    }

    [Test]
    public async Task SafelyEnumerateDirectories_MissingDirectory_ReturnsEmptyEnumerable()
    {
        string missing = CreateNonExistentPath();

        IEnumerable<DirectoryInfo> directories = Directory.SafelyEnumerateDirectories(missing);

        await Assert.That(directories.Count()).IsEqualTo(0);
    }

    [Test]
    public async Task SafelyGetFiles_DirectoryInfoOverload_MissingDirectory_ReturnsEmptyArray()
    {
        string missing = CreateNonExistentPath();
        DirectoryInfo directoryInfo = new(missing);

        await Assert.That(directoryInfo.SafelyGetFiles().Length).IsEqualTo(0);
        await Assert.That(directoryInfo.SafelyGetDirectories().Length).IsEqualTo(0);
    }

    [Test]
    public async Task SafelyEnumerateFiles_MissingDirectoryWithSearchOption_ReturnsEmptyEnumerable()
    {
        string missing = CreateNonExistentPath();

        IEnumerable<FileInfo> files = Directory.SafelyEnumerateFiles(missing, "*", SearchOption.AllDirectories);

        await Assert.That(files.Count()).IsEqualTo(0);
    }
}
