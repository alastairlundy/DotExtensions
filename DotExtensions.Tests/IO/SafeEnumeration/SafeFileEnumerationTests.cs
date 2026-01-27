using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DotExtensions.IO.Directories;

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

        FileInfo[] files = directoryInfo.SafelyGetFiles("*",  SearchOption.AllDirectories);
        
        Assert.NotNull(files);
        await Assert.That(files.Length)
            .IsGreaterThan(0);
    }
}