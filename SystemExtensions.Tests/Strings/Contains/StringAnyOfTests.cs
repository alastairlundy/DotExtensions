﻿/*
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

using AlastairLundy.Extensions.System.Generics;
using AlastairLundy.Extensions.System.Strings;

using Bogus;
using Bogus.DataSets;

namespace SystemExtensions.Tests.Strings.Contains;

public class StringAnyOfTests
{
    private Lorem _lorem = new();
    
    [Fact]
    public void AnyOfChars()
    {
        char[] chars = Chars.UpperCase.Take(5).ToArray();
        
        Random.Shared.Shuffle(chars);
        
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

    [Fact]
    public void AnyOfStrings()
    {
        string[] words = _lorem.Words();
        string text = _lorem.Word();
        
        text += words.First();

        bool actual = text.ContainsAnyOf(words);
        
        Assert.True(actual);
    }

    [Fact]
    public void NotAnyOfStrings()
    {
        string[] words = _lorem.Words().ToArray();
        string text = words.First();
        
        words = words.Skip(1).ToArray();

        bool actual = text.ContainsAnyOf(words);
        
        Assert.False(actual);
    }

}