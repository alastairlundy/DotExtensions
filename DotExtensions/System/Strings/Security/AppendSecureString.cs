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
    /// Appends a string to this <see cref="SecureString"/>.
    /// </summary>
    /// <param name="secureString">The <see cref="SecureString"/> to append a value to.</param>
    /// <param name="value">The string to append to this <see cref="SecureString"/>.</param>
    /// <exception cref="InvalidOperationException">Thrown if this <see cref="SecureString"/> is read only.</exception>
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
    /// Appends a sequence of characters to this <see cref="SecureString"/>.
    /// </summary>
    /// <param name="secureString">The <see cref="SecureString"/> to append chars to.</param>
    /// <param name="chars">The sequence of characters to append to this <see cref="SecureString"/>.</param>
    /// <exception cref="InvalidOperationException">Thrown if this <see cref="SecureString"/> is read only.</exception>
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
    /// Sets this <see cref="SecureString"/> to the specified characters.
    /// </summary>
    /// <param name="secureString">The <see cref="SecureString"/> to set a value to.</param>
    /// <param name="chars">The characters to set to this <see cref="SecureString"/>.</param>
    /// <exception cref="InvalidOperationException">Thrown if the <see cref="SecureString"/> is read only and the number of characters to set is greater than the length of this <see cref="SecureString"/>.</exception>
    public static void SetChars(this SecureString secureString, params ICollection<char> chars)
    {
        if (secureString.IsReadOnly() && chars.Count > secureString.Length)
            throw new InvalidOperationException();

        if (secureString.IsReadOnly() && chars.Count == secureString.Length)
        {
            int i = 0;
            foreach(char c in chars)
            {
                secureString.SetAt(i, c);
                i++;
            }
        }
        else if (secureString.IsReadOnly())
        {
            secureString.Clear();
            secureString = new SecureString();
            secureString.AppendChars(chars);
            secureString.MakeReadOnly();
        }
        else
        {
            secureString.Clear();
        
            secureString.AppendChars(chars);
        }
        
        foreach (char c in chars)
        {
            secureString.AppendChar(c);
        } 
    }

    /// <summary>
    /// Sets this <see cref="SecureString"/> to the specified value.
    /// </summary>
    /// <param name="secureString">The <see cref="SecureString"/> to set a value to.</param>
    /// <param name="value">The value to set to the <see cref="SecureString"/>.</param>
    /// <exception cref="InvalidOperationException">Thrown if the <see cref="SecureString"/> is read only and the new value is greater than the length of this <see cref="SecureString"/>.</exception>
    public static void SetString(this SecureString secureString, string value)
    {
        if (secureString.IsReadOnly() && value.Length > secureString.Length)
            throw new InvalidOperationException();

        if (secureString.IsReadOnly() && value.Length == secureString.Length)
        {
            for (int i = 0; i < value.Length; i++)
            {
                secureString.SetAt(i, value[i]);
            }
        }
        else if (secureString.IsReadOnly())
        {
            secureString.Clear();
            secureString = new SecureString();
            secureString.AppendString(value);
            secureString.MakeReadOnly();
        }
        else
        {
            secureString.Clear();
        
            secureString.AppendString(value);
        }
    }
}