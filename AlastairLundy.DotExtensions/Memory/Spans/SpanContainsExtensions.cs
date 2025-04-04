/*
        MIT License
       
       Copyright (c) 2024-2025 Alastair Lundy
       
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

namespace AlastairLundy.DotExtensions.Memory.Spans;

public static class SpanContainsExtensions
{
    #if NETSTANDARD2_0 || NETSTANDARD2_1
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">The span to check through.</param>
    /// <param name="item">The item to search for in the span.</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>True if the item is found in the span; false otherwise.</returns>
    public static bool Contains<T>(this Span<T> target, T item) where T : IEquatable<T>
    {
        foreach (T t in target)
        {
            if (t is not null && t.Equals(item))
            {
                return true;
            }
        }
        
        return false;
    }
    
    #endif
}