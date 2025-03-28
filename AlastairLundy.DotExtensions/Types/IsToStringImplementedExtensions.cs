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

using System.Linq;

namespace AlastairLundy.DotExtensions.Types;

public static class IsToStringImplementedExtensions
{
    /// <summary>
    /// Returns whether a Type implements ToString such that a concrete non-virtual implementation is available.
    /// </summary>
    /// <param name="this">The instance of the Type to be checked.</param>
    /// <typeparam name="T">The type to be checked.</typeparam>
    /// <returns>True if ToString is implemented, false otherwise.</returns>
    public static bool IsToStringImplemented<T>(this T @this)
    {
        return typeof(T).GetMethods().Any(m => m.Name == "ToString" &&
                                               m.ReturnType == typeof(string) &&
                                               m.GetParameters().Length == 0 &&
                                               m is { IsPublic: true, IsStatic: false});
    }
}