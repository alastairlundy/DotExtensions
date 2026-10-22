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
using DotExtensions.Strings.Security;

namespace DotExtensions.Tests.Strings.Security;

public class AppendSecureStringExtensionsTests
{
    private static string ToInsecureString(SecureString secureString)
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
    public async Task AppendChars_EmptySecureString_AppendsSuccessfully()
    {
        using SecureString secureString = new();

        secureString.AppendChars(new[] { 'a', 'b', 'c' });

        await Assert.That(secureString.Length).IsEqualTo(3);
        await Assert.That(ToInsecureString(secureString)).IsEqualTo("abc");
    }

    [Test]
    public async Task AppendChars_MutableSecureString_AppendsToExistingContent()
    {
        using SecureString secureString = new();

        secureString.AppendChars(new[] { 'a', 'b' });
        secureString.AppendChars(new[] { 'c' });

        await Assert.That(ToInsecureString(secureString)).IsEqualTo("abc");
    }

    [Test]
    public async Task AppendChars_ReadOnlySecureString_ThrowsInvalidOperationException()
    {
        using SecureString secureString = new();
        secureString.MakeReadOnly();

        await Assert.That(() => secureString.AppendChars(new[] { 'a' }))
            .Throws<InvalidOperationException>();
    }

    [Test]
    public async Task SetChars_MutableSecureString_RoundTrips()
    {
        using SecureString secureString = new();

        secureString.SetChars(new[] { 'a', 'b', 'c' });

        await Assert.That(secureString.Length).IsEqualTo(3);
        await Assert.That(ToInsecureString(secureString)).IsEqualTo("abc");

        // Setting again overwrites the previous value, including when the new value is shorter.
        secureString.SetChars(new[] { 'x', 'y' });

        await Assert.That(secureString.Length).IsEqualTo(2);
        await Assert.That(ToInsecureString(secureString)).IsEqualTo("xy");
    }

    [Test]
    public async Task SetChars_ReadOnlySecureString_ThrowsInvalidOperationExceptionWithClearMessage()
    {
        using SecureString secureString = new();
        secureString.MakeReadOnly();

        InvalidOperationException? exception = null;

        try
        {
            secureString.SetChars(new[] { 'x' });
        }
        catch (InvalidOperationException ex)
        {
            exception = ex;
        }

        await Assert.That(exception).IsNotNull();
        await Assert.That(exception!.Message).Contains("read-only");
    }
}
