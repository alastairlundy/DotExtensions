/*
        MIT License
       
       Copyright (c) 2025 Alastair Lundy
       
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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;

namespace AlastairLundy.DotExtensions.Processes;

public static class IsProcessRunningExtensions
{
    /// <summary>
    /// Check to see if a specified process is running or not.
    /// </summary>
    /// <param name="process">The process to be checked.</param>
    /// <returns>True if the specified process is running; returns false otherwise.</returns>
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    [SupportedOSPlatform("maccatalyst")]
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    [SupportedOSPlatform("android")]
    public static bool IsRunning(this Process process) => 
        process.HasStarted() && process.HasExited() == false;

    /// <summary>
    /// Detects whether a process is running on a remote device.
    /// </summary>
    /// <param name="process"></param>
    /// <returns>True if the process is running on a remote device, false otherwise.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the process has been disposed of.</exception>
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    [SupportedOSPlatform("maccatalyst")]
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    [SupportedOSPlatform("android")]
    [Obsolete(DeprecationMessages.DeprecationV9)]
    public static bool IsRunningOnRemoteDevice(this Process process)
        => IsProcessOnRemoteDevice(process);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="process"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    [SupportedOSPlatform("maccatalyst")]
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    [SupportedOSPlatform("android")]
    public static bool IsProcessOnRemoteDevice(this Process process)
    {
        if (process.IsDisposed() != false) 
            throw new NotSupportedException("Process is running on remote device");
        
        return Process.GetProcesses().All(x => x.Id != process.Id) && 
               process.MachineName.Equals(Environment.MachineName) == false;
    }
    
    /// <summary>
    /// Check to see if a specified process is running or not.
    /// </summary>
    /// <param name="processName">The name of the process to be checked.</param>
    /// <param name="sanitizeProcessName"></param>
    /// <returns>true if the specified process is running; returns false otherwise.</returns>
    [Obsolete(DeprecationMessages.DeprecationV9)]
    public static bool IsProcessRunning(this string processName, bool sanitizeProcessName = true)
    {
        IEnumerable<string> processes;

        string tempProcessName = processName;
            
        if (sanitizeProcessName)
        {
            tempProcessName = Path.GetFileNameWithoutExtension(processName);
            processes = Process.GetProcesses().SanitizeProcessNames(excludeFileExtensions: true);
        }
        else
        {
            processes = Process.GetProcesses().Select(x => x.ProcessName);
        }
            
        return processes.Any(x => x.ToLower().Equals(tempProcessName.ToLower()));
    }
}