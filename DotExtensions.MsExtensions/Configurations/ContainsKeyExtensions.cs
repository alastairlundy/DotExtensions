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

using Microsoft.Extensions.Configuration;

namespace AlastairLundy.DotExtensions.MsExtensions.Configurations;

public static class ContainsKeyExtensions
{
    /// <summary>
    /// Determines if a configuration contains a specified key.
    /// </summary>
    /// <param name="configuration">The configuration to search.</param>
    /// <param name="keyName">The key name to search for.</param>
    /// <returns>True if the key is found within the configuration, false otherwise.</returns>
    public static bool ContainsKey(this IConfiguration configuration, string keyName)
    {
        IConfigurationSection config = configuration.GetSection(keyName);

        return config.Exists();
    }
}