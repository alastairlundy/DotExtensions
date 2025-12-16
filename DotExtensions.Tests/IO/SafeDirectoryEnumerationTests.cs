using System;
using System.Collections.Generic;
using System.IO;
using AlastairLundy.DotExtensions.IO.Directories;

namespace DotExtensions.Tests.IO;

public class SafeDirectoryEnumerationTests
{
    [Fact]
    public void SafeDirectoryEnumeration_Net8Plus_NoExceptions()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory);

        IEnumerable<DirectoryInfo> files = directoryInfo.SafelyEnumerateDirectories("*",
            SearchOption.TopDirectoryOnly, true);
        
        Assert.NotNull(files);
        Assert.NotEmpty(files);
    }
    
    [Fact]
    public void SafeDirectoryEnumeration_NetStandard20_NoExceptions()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory);

        IEnumerable<DirectoryInfo> files = directoryInfo.SafeDirectoryEnumeration_NetStandard20("*",
            SearchOption.TopDirectoryOnly, true);
        
        Assert.NotNull(files);
        Assert.NotEmpty(files);
    }
}