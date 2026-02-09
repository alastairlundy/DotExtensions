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
using System.IO;
using DotExtensions.IO;

namespace DotExtensions.Tests.IO;

public class FileSizeExtensionTests
{
    private readonly FileInfo _testFile;

    public FileSizeExtensionTests()
    {
        _testFile = FileInfo.GetRandomFile();
    }
    
    [Test]
    public async Task FileSize_CalculatedCorrectly()
    {
        string actualFileSizeString = _testFile.GetFileSizeString();

        int end = actualFileSizeString
            .LastIndexOfAny(['0', '1', '2', '3', '4', '5', '6', '7', '8', '9']);
        
        long actualFileSize = long.Parse(actualFileSizeString.Substring(0, Math.Abs(0 - end)));
        
        await Assert.That(actualFileSizeString)
            .IsNotEmpty();

        await Assert.That(actualFileSize)
            .IsGreaterThan(0);
    } 
    
    [Test]
    public async Task FileSizeWithUnit_HasCorrectUnit()
    {
        string fileSizeString = _testFile.GetFileSizeString();

        int lastNumberIndex = fileSizeString.LastIndexOfAny(['0', '1', '2', '3', '4', '5', '6', '7', '8', '9']);
        
        int endIndex = Math.Abs(fileSizeString.Length -1 - lastNumberIndex);
        
        string actual = fileSizeString.Substring(lastNumberIndex + 1, endIndex);

        string expected = _testFile.GetFileSizeUnitString();

        await Assert.That(actual)
            .IsEqualTo(expected)
            .And
            .IsNotEmpty();
    }
}