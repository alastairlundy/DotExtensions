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


using Bogus;
using DotExtensions.Strings;

namespace DotExtensions.Tests.Strings;

public class StringInsertTests
{
    private readonly Faker _faker = new();
    
    [Test]
    [Arguments("Hello")]
    [Arguments("World")]
    [Arguments("Avocado")]
    [Arguments("Banana")]
    public async Task StringInsert_Char_Works(string origin)
    {
        char c = _faker.Random.Char();
        
        int index = _faker.Random.Int(0, origin.Length - 1);
        
        string expected = $"{origin.Insert(index, c.ToString())}";
        
        string actual = origin.Insert(index, c);
        
        await Assert.That(actual)
            .IsEqualTo(expected);
    }
}