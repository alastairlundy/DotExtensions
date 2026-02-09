/*
        MIT License
       
       Copyright (c) 2026 Alastair Lundy
       
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
using System.Runtime.InteropServices;
using DotExtensions.Exceptions;

namespace DotExtensions.Tests.Exceptions;

public class PlatformNotSupportExceptionExtensions
{
    [Test]
    public async Task ThrowIfNotOSPlatform_ThrowsCorrectly()
    {
        await Assert.ThrowsAsync(() =>
        {
            PlatformNotSupportedException.ThrowIfNotOSPlatform(!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? OSPlatform.Windows
                : OSPlatform.Linux);

            return Task.CompletedTask;
        });
    }

    [Test]
    public async Task ThrowIfNotOSPlatform_DoNotThrowIfOnSupportedOs()
    {
        OSPlatform osPlatform = OSPlatform.Windows;
        
        if(OperatingSystem.IsWindows())
            osPlatform = OSPlatform.Windows;
        if(OperatingSystem.IsLinux())
            osPlatform = OSPlatform.Linux;
        if(OperatingSystem.IsMacOS())
            osPlatform = OSPlatform.OSX;
        if(OperatingSystem.IsFreeBSD())
            osPlatform = OSPlatform.FreeBSD;

        await Assert.That(() => PlatformNotSupportedException.ThrowIfNotOSPlatform(osPlatform))
            .ThrowsNothing();
    }
}