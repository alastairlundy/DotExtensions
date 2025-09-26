using System;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using AlastairLundy.DotExtensions.Processes;

using DotExtensions.Tests.Constants;

namespace DotExtensions.Tests.Processes;

public class ProcessRemoteDeviceDetectionTests
{
    
    [Fact]
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    public async Task LocalProcess_Detected_Successfully()
    {
        string filePath = "";

        if (OperatingSystem.IsWindows())
        {
            filePath = TargetFilePaths.CmdFilePath;
        }
        else if(OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD())
        {
            filePath = TargetFilePaths.LinuxEchoFilePath;
        }
        else if (OperatingSystem.IsMacOS())
        {
            
        }
        else
        {
            
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
        
        bool actual = process.IsRunningOnRemoteDevice();
        
        await process.WaitForExitAsync(TestContext.Current.CancellationToken);
        
        process.Dispose();
        
        //Assert
        Assert.False(actual);
    }
}