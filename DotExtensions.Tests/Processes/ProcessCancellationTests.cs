using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;

using AlastairLundy.DotExtensions.Processes;
using DotExtensions.Tests.Constants;

namespace DotExtensions.Tests.Processes;

public class ProcessCancellationTests
{
    
    [Fact]
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    public async Task ProcessCancelled_Immediately_Success()
    {
        //Arrange 
        string filePath = "";

        if (OperatingSystem.IsWindows())
        {
            filePath = TargetFilePaths.CmdFilePath;
        }
        else if(OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD())
        {
            filePath = TargetFilePaths.LinuxEchoFilePath;
        }
        
        //Act 
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = filePath,
            Arguments = "",
            RedirectStandardInput = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
        };
        
       Process process = new Process()
       {
           StartInfo = startInfo,
       };
       
       process.Start();
       
       int processId = process.Id;
       
       await process.RequestCancellationAsync();

      await Task.Delay(1000, TestContext.Current.CancellationToken);

       bool actual = Process.GetProcesses().Any(x => x.Id == processId);

        //Assert
        Assert.False(actual);
    }
}