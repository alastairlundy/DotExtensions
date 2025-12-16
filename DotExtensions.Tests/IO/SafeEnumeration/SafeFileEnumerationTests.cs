using System;
using System.Collections.Generic;
using System.IO;
using AlastairLundy.DotExtensions.IO.Directories;

namespace DotExtensions.Tests.IO.SafeEnumeration;

public class SafeFileEnumerationTests
{
    [Fact]
    public void SafeFileEnumeration_NoExceptions()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory);

        IEnumerable<FileInfo> files = directoryInfo.SafelyEnumerateFiles();
        
        Assert.NotNull(files);
        Assert.NotEmpty(files);
    }

    [Fact]
    public void SafeFileGetting_NoExceptions()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory);

        FileInfo[] files = directoryInfo.SafelyGetFiles();
        
        Assert.NotNull(files);
        Assert.NotEmpty(files);
    }
    
    [Fact]
    public void SafeFileGetting_WithSearchOption_NoExceptions()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory);

        FileInfo[] files = directoryInfo.SafelyGetFiles("*",  SearchOption.AllDirectories);
        
        Assert.NotNull(files);
        Assert.NotEmpty(files);
    }
}