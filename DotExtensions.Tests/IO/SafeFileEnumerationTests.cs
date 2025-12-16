using System;
using System.Collections.Generic;
using System.IO;
using AlastairLundy.DotExtensions.IO.Directories;

namespace DotExtensions.Tests.IO;

public class SafeFileEnumerationTests
{
    [Fact]
    public void SafeFileEnumeration_Net8Plus_NoExceptions()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory);

        IEnumerable<FileInfo> files = directoryInfo.SafelyEnumerateFiles("*",
            SearchOption.TopDirectoryOnly, true);
        
        Assert.NotNull(files);
        Assert.NotEmpty(files);
    }
    
    [Fact]
    public void SafeFileEnumeration_NetStandard20_NoExceptions()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory);

        IEnumerable<FileInfo> files = directoryInfo.SafeFileEnumeration_NetStandard20("*",
            SearchOption.TopDirectoryOnly, true);
        
        Assert.NotNull(files);
        Assert.NotEmpty(files);
    }
}