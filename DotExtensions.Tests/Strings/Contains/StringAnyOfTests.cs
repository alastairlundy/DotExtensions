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

using Bogus;
using Bogus.DataSets;

namespace SystemExtensions.Tests.Strings.Contains;

public class StringAnyOfTests
{
    private Lorem _lorem = new();
    
    #if NETFRAMEWORK
    private readonly Random _random = new();
    #endif
    
    [Fact]
    public void AnyOfChars()
    {
        char[] chars = Chars.UpperCase.Take(5).ToArray();
       
#if NETFRAMEWORK
        _random.Shuffle(chars);
#else
        Random.Shared.Shuffle(chars);
#endif
        
        string str = String.Join("", chars);

        bool actual = str.ContainsAnyOf(Chars.UpperCase.ToCharArray());
        
        Assert.True(actual);
    }

    [Fact]
    public void NotAnyOfChars()
    {
        string str = _lorem.Word();

        bool actual = str.ContainsAnyOf(Chars.Numbers.ToCharArray());
        
        Assert.False(actual);
    }

}