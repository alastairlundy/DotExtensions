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
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;

namespace AlastairLundy.DotExtensions.Processes;

public static class ProcessCancellationExtensions
{
    
    /// <summary>
    /// Requests the immediate cancellation of a process.
    /// </summary>
    /// <param name="process">The process to be cancelled.</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException">Thrown if run on a remote computer or device.</exception>
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    [SupportedOSPlatform("maccatalyst")]
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    [SupportedOSPlatform("android")]
    public static async Task RequestCancellationAsync(this Process process)
        => await WaitForExitAsync(process, TimeSpan.Zero);
    
    /// <summary>
    /// Asynchronously waits for the process to exit or for the <param name="timeoutThreshold"/> to be exceeded, whichever is sooner.
    /// </summary>
    /// <param name="process">The process to cancel.</param>
    /// <param name="timeoutThreshold">The delay to wait before requesting cancellation.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the timeout threshold is less than 0.</exception>
    /// <exception cref="NotSupportedException">Thrown if run on a remote computer or device.</exception>
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    [SupportedOSPlatform("maccatalyst")]
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    [SupportedOSPlatform("android")]
    public static async Task WaitForExitAsync(this Process process,TimeSpan timeoutThreshold)
    {
        if (timeoutThreshold < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException();
        
        if (process.IsRunningOnRemoteDevice())
            throw new NotSupportedException();
        
        CancellationTokenSource cts = new CancellationTokenSource();
        
        cts.CancelAfter(timeoutThreshold);
        
        await process.WaitForExitAsync(cts.Token);
    }
}