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

using DotExtensions.Strings;
using DotExtensions.Tests.TestData;

namespace DotExtensions.Tests.Strings.EscapeCharacters;

public class EscapeCharacterRemovalTests
{
    public static IEnumerable<string> GetStrings()
        => EscapeCharacterTestData.GetStrings();
    
    public static IEnumerable<string> GetLoremWords()
        => LoremWordsTestData.GetLoremWords();
    
    [Test]
    [MethodDataSource(nameof(GetStrings))]
    public async Task ContainsEscapedCharactersTest(string escapeCharacters)
    {
        bool actual = escapeCharacters.ContainsEscapeCharacters();

        await Assert.That(actual).IsTrue();
    }

    [Test]
    [MethodDataSource(nameof(GetStrings))]
    public async Task ContainsEscapeCharacters_string_Test(string escapeCharacters)
    {
        bool actual = string.IsEscapeCharacter(escapeCharacters);

        await Assert.That(actual).IsTrue();
    }

    [Test]
    [MethodDataSource(nameof(GetLoremWords))]
    public async Task DoesntContainEscapeCharactersTest(string word)
    {
        bool actual = word.ContainsEscapeCharacters();

        await Assert.That(actual).IsFalse();
    }

    [Test]
    [MethodDataSource(nameof(GetStrings))]
    public async Task SuccessfullyRemoveEscapeCharsTests(string escapeCharacter)
    {
        string text = $"Hello World {escapeCharacter}";

        const string expected = "Hello World";

        string actual = text.RemoveEscapeCharacters();

        await Assert.That(actual)
            .IsEqualTo(expected);
    }
}