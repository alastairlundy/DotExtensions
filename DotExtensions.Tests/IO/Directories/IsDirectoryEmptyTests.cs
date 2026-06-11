using System;
using System.IO;
using DotExtensions.IO.Directories;

namespace DotExtensions.Tests.IO.Directories;

public class IsDirectoryEmptyTests
{
    public record DirectoryTestCase(string Label, Action<DirectoryInfo> Setup, bool ExpectedIsEmpty, bool ExpectedHasFiles);

    public static IEnumerable<DirectoryTestCase> DirectoryStates()
    {
        yield return new("Empty directory", _ => { }, true, false);
        yield return new("Directory with files", dir =>
        {
            File.Create(Path.Combine(dir.FullName, "test.txt")).Dispose();
        }, false, true);
        yield return new("Directory with subdirectories only", dir =>
        {
            dir.CreateSubdirectory("sub");
        }, false, false);
    }

    [Test]
    [MethodDataSource(nameof(DirectoryStates))]
    public async Task IsEmpty_And_HasFiles_ReturnCorrectValues(DirectoryTestCase testCase)
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        DirectoryInfo dir = Directory.CreateDirectory(tempPath);
        try
        {
            testCase.Setup(dir);

            await Assert.That(dir.IsEmpty).IsEqualTo(testCase.ExpectedIsEmpty);
            await Assert.That(dir.HasFiles).IsEqualTo(testCase.ExpectedHasFiles);
        }
        finally
        {
            if (dir.Exists)
            {
                dir.Delete(true);
            }
        }
    }

    [Test]
    public async Task IsEmpty_ThrowsDirectoryNotFoundException()
    {
        DirectoryInfo dir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));

        await Assert.That(() => dir.IsEmpty).Throws<DirectoryNotFoundException>();
    }

    [Test]
    public async Task HasFiles_ThrowsDirectoryNotFoundException()
    {
        DirectoryInfo dir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));

        await Assert.That(() => dir.HasFiles).Throws<DirectoryNotFoundException>();
    }
}
