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

using System;
using AlastairLundy.DotExtensions.Memory.Internal;

namespace AlastairLundy.DotExtensions.Memory;

/// <summary>
/// 
/// </summary>
public static class MemoryFirstAndLast
{
    /// <summary>
    /// Returns the first element of a Memory sequence.
    /// </summary>
    /// <param name="source">The source Memory sequence.</param>
    /// <typeparam name="T">The type of elements in the Memory sequence.</typeparam>
    /// <returns>The first element of the Memory sequence.</returns>
    [Obsolete(DeprecationMessages.DeprecationV9)]
    public static T First<T>(this Memory<T> source)
    {
        if (source.IsEmpty)
            throw new InvalidOperationException("The source Memory is empty.");
        
        return source.ElementAt(0);
    }

    /// <summary>
    /// Returns the first element of a Memory sequence or default if it is empty.
    /// </summary>
    /// <param name="source">The source Memory sequence.</param>
    /// <typeparam name="T">The type of elements in the Memory sequence.</typeparam>
    /// <returns>The first element of the Memory or default if no elements were found.</returns>
    [Obsolete(DeprecationMessages.DeprecationV9)]
    public static T? FirstOrDefault<T>(this Memory<T> source)
    {
        if(source.IsEmpty)
            return default;
        
        return source.ElementAt(0);
    }
    
    /// <summary>
    /// Returns the last element of a Memory sequence.
    /// </summary>
    /// <param name="source">The source Memory sequence.</param>
    /// <typeparam name="T">The type of elements in the Memory sequence.</typeparam>
    /// <returns>The last element of the Memory sequence.</returns>
    [Obsolete(DeprecationMessages.DeprecationV9)]
    public static T Last<T>(this Memory<T> source)
    {
        if(source.IsEmpty)
            throw new InvalidOperationException("The source Memory is empty.");

        return source.ElementAt(source.Length - 1);
    }
    
    /// <summary>
    /// Returns the last element of a Memory sequence or default if it is empty.
    /// </summary>
    /// <param name="source">The source Memory sequence.</param>
    /// <typeparam name="T">The type of elements in the Memory sequence.</typeparam>
    /// <returns>The last element of the Memory or default if no elements were found.</returns>
    [Obsolete(DeprecationMessages.DeprecationV9)]
    public static T? LastOrDefault<T>(this Memory<T> source)
    {
        if(source.IsEmpty)
            return default;

        return source.ElementAt(source.Length - 1);
    }
}