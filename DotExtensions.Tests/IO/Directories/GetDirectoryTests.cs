using System;
using System.IO;
using DotExtensions.IO;

namespace DotExtensions.Tests.IO.Directories;

public class GetDirectoryTests
{
    [Test]
    public async Task GetDirectory_NestedDuplicateNameDirectory_ReturnsImmediateParent()
    {
        string root = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        // A directory that shares its name with the file it contains, e.g. <root>\test.txt\test.txt.
        string nestedDirectory = Path.Combine(root, "test.txt");
        Directory.CreateDirectory(nestedDirectory);

        string filePath = Path.Combine(nestedDirectory, "test.txt");
        File.WriteAllText(filePath, "content");

        try
        {
            FileInfo fileInfo = new(filePath);

            DirectoryInfo parent = fileInfo.GetDirectory();

            await Assert.That(Path.GetFullPath(parent.FullName))
                .IsEqualTo(Path.GetFullPath(nestedDirectory));
        }
        finally
        {
            if (Directory.Exists(root))
            {
                Directory.Delete(root, recursive: true);
            }
        }
    }

    [Test]
    public async Task GetDirectory_DeeplyNestedDuplicateNames_ReturnsLastSegmentParent()
    {
        string root = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        string firstLevel = Path.Combine(root, "child");
        string secondLevel = Path.Combine(firstLevel, "child");
        Directory.CreateDirectory(secondLevel);

        string filePath = Path.Combine(secondLevel, "child.txt");
        File.WriteAllText(filePath, "content");

        try
        {
            FileInfo fileInfo = new(filePath);

            DirectoryInfo parent = fileInfo.GetDirectory();

            await Assert.That(Path.GetFullPath(parent.FullName))
                .IsEqualTo(Path.GetFullPath(secondLevel));
        }
        finally
        {
            if (Directory.Exists(root))
            {
                Directory.Delete(root, recursive: true);
            }
        }
    }

    [Test]
    public async Task GetDirectory_MissingFileInMissingDirectory_ThrowsArgumentException()
    {
        string missingDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString(), "missing");
        FileInfo fileInfo = new(Path.Combine(missingDirectory, "missing.txt"));

        await Assert.That(() =>
        {
            fileInfo.GetDirectory();
        }).Throws<ArgumentException>();
    }

    [Test]
    public async Task GetDirectory_ParentSegmentIsAFile_ThrowsArgumentException()
    {
        string root = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(root);

        // <root>\test.txt is a *file*, so <root>\test.txt\test.txt can never exist and
        // the parent must not be reported as the file itself.
        string filePath = Path.Combine(root, "test.txt");
        File.WriteAllText(filePath, "content");

        try
        {
            FileInfo fileInfo = new(Path.Combine(filePath, "test.txt"));

            await Assert.That(() =>
            {
                fileInfo.GetDirectory();
            }).Throws<ArgumentException>();
        }
        finally
        {
            if (Directory.Exists(root))
            {
                Directory.Delete(root, recursive: true);
            }
        }
    }
}
