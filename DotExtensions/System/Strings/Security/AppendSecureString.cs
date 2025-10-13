/*
        MIT License

       Copyright (c) 2020-2025 Alastair Lundy

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
using System.Collections.Generic;
using System.Security;

namespace AlastairLundy.DotExtensions.Strings.Security;

public static class AppendSecureStringExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="secureString"></param>
    /// <param name="value"></param>
    public static void AppendString(this SecureString secureString, string value)
    {
        if (secureString.IsReadOnly())
            throw new InvalidOperationException();
        
        foreach (char c in value)
        {
            secureString.AppendChar(c);
        } 
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="secureString"></param>
    /// <param name="chars"></param>
    public static void AppendChars(this SecureString secureString, params IEnumerable<char> chars)
    {
        if (secureString.IsReadOnly())
            throw new InvalidOperationException();
        
        foreach (char c in chars)
        {
            secureString.AppendChar(c);
        } 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="secureString"></param>
    /// <param name="chars"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void SetChars(this SecureString secureString, params IEnumerable<char> chars)
    {
        if (secureString.IsReadOnly())
            throw new InvalidOperationException();
        
        secureString.Clear();
        
        AppendChars(secureString, chars);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="secureString"></param>
    /// <param name="value"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void SetString(this SecureString secureString, string value)
    {
        if (secureString.IsReadOnly())
            throw new InvalidOperationException();
        
        secureString.Clear();
        
        AppendString(secureString, value);
    }
}