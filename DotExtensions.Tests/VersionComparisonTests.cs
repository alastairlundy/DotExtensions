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
using DotExtensions.Versions;

namespace DotExtensions.Tests;

public class VersionComparisonTests
{
    [Test]
    [Arguments("1.2.3.4", "1.2.3.5")]
    [Arguments("1.0.0", "2.0.0")]
    [Arguments("1.0.0", "1.0.0.1")]
    [Arguments("0.1.0", "0.10.0")]
    public async Task OldVersion_IsOlderThanNewVersion_ShouldBeTrue(string oldVersionInput, string newVersionInput)
    {
        Version oldVersion = Version.Parse(oldVersionInput);
        Version newVersion = Version.Parse(newVersionInput);

        bool expected = oldVersion.IsOlderThan(newVersion);
       
        await Assert.That(expected).IsTrue();
    }
    
    [Test]
    [Arguments("1.2.3.4", "1.2.3.0")]
    [Arguments("1.0.0", "0.99.0")]
    [Arguments("1.0.1", "1.0.0")]
    [Arguments("0.10.0", "0.10.0")]
    public async Task NewVersion_IsAtLeast_OldVersion_ShouldBeTrue(string newVersionInput, string oldVersionInput)
    {
        Version oldVersion = Version.Parse(oldVersionInput);
        Version newVersion = Version.Parse(newVersionInput);
        
        bool expected = newVersion.IsAtLeast(oldVersion);
        
        await Assert.That(expected).IsTrue();
    }
}