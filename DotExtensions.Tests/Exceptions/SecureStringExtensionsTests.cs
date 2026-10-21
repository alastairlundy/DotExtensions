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
using System.Security;
using DotExtensions.Exceptions;
using DotExtensions.Strings.Security;

namespace DotExtensions.Tests.Exceptions;

public class SecureStringExtensionsTests
{
    private static string ReadSecureString(SecureString secureString)
    {
        IntPtr ptr = Marshal.SecureStringToGlobalAllocUnicode(secureString);

        try
        {
            return Marshal.PtrToStringUni(ptr) ?? string.Empty;
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(ptr);
        }
    }

    [Test]
    public async Task AppendChars_EmptySecureString_Succeeds()
    {
        SecureString secureString = new();

        secureString.AppendChars('a', 'b', 'c');

        await Assert.That(secureString.Length).IsEqualTo(3);
        await Assert.That(ReadSecureString(secureString)).IsEqualTo("abc");
    }

    [Test]
    public async Task SetChars_WritableSecureString_SetsContent()
    {
        SecureString secureString = new();
        secureString.AppendChar('o');
        secureString.AppendChar('l');
        secureString.AppendChar('d');

        secureString.SetChars('n', 'e', 'w');

        await Assert.That(secureString.Length).IsEqualTo(3);
        await Assert.That(ReadSecureString(secureString)).IsEqualTo("new");
    }

    [Test]
    public async Task SetChars_NewContentLongerThanOriginal_Succeeds()
    {
        SecureString secureString = new();
        secureString.AppendChar('x');

        secureString.SetChars('a', 'b', 'c');

        await Assert.That(ReadSecureString(secureString)).IsEqualTo("abc");
    }

    [Test]
    public async Task SetChars_ReadOnlySecureString_ThrowsAndPreservesContent()
    {
        SecureString secureString = new();
        secureString.AppendChar('s');
        secureString.AppendChar('e');
        secureString.AppendChar('c');
        secureString.MakeReadOnly();

        await Assert.That(() => secureString.SetChars('x'))
            .Throws<InvalidOperationException>();

        await Assert.That(ReadSecureString(secureString)).IsEqualTo("sec");
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_Null_ThrowsArgumentNullException()
    {
        await Assert.That(() => ArgumentException.ThrowIfNullOrWhiteSpace((SecureString?)null))
            .Throws<ArgumentNullException>();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_Empty_ThrowsArgumentException()
    {
        using SecureString secureString = new();

        await Assert.That(() => ArgumentException.ThrowIfNullOrWhiteSpace(secureString))
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_WhitespaceOnly_ThrowsArgumentException()
    {
        using SecureString secureString = new();
        secureString.AppendChar(' ');
        secureString.AppendChar(' ');
        secureString.AppendChar(' ');

        await Assert.That(() => ArgumentException.ThrowIfNullOrWhiteSpace(secureString))
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task ThrowIfNullOrWhiteSpace_NormalContent_DoesNotThrow()
    {
        using SecureString secureString = new();
        secureString.AppendChar('s');
        secureString.AppendChar('3');
        secureString.AppendChar('c');

        await Assert.That(() => ArgumentException.ThrowIfNullOrWhiteSpace(secureString))
            .ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfNullOrEmpty_NullWithoutName_DefaultsParamNameToSecureString()
    {
        ArgumentNullException? exception = null;

        try
        {
            ArgumentException.ThrowIfNullOrEmpty((SecureString?)null);
        }
        catch (ArgumentNullException caught)
        {
            exception = caught;
        }

        await Assert.That(exception).IsNotNull();
        await Assert.That(exception!.ParamName).IsEqualTo("secureString");
    }
}
