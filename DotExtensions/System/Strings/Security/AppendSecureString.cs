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