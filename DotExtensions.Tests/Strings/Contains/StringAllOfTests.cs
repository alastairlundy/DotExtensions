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
using System.Linq;
using AlastairLundy.DotExtensions.Strings;
using Bogus.DataSets;

namespace DotExtensions.Tests.Strings.Contains;

public class StringAllOfTests
{
    private Lorem _lorem = new Lorem();
    
    [Fact]
    public void AllOfChars()
    {
        char[] chars = ['a', 'b', 'c', 'd', 'e', 'f'];

        string str = String.Join("", chars);

        bool actual = str.ContainsAllOf(chars);
        
        Assert.True(actual);
    }

    [Fact]
    public void NotAllOfChars()
    {
        char[] chars = ['a', 'b', 'c', 'd', 'e', 'f'];

        string str = chars.Take(3).ToString()!;

        bool actual = str.ContainsAllOf(chars);
        
        Assert.False(actual);
    }
}