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
using System.Collections.Generic;

using AlastairLundy.DotExtensions.Collections.Generic.Enumerables;

namespace AlastairLundy.DotExtensions.Collections.Generic.ICollections;

/// <summary>
/// 
/// </summary>
public static class GenericCollectionReplaceExtensions
{
    /// <summary>
    /// Replaces all occurrences of the specified old value with the new value in the given <see cref="ICollection{T}"/>.
    /// </summary>
    /// <param name="collection">The collection to modify.</param>
    /// <param name="oldValue">The value to replace.</param>
    /// <param name="newValue">The replacement value.</param>
    /// <typeparam name="T">The type of elements in the <see cref="ICollection{T}"/>.</typeparam>
    [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
    public static void Replace<T>(this ICollection<T> collection, T oldValue, T newValue)
    {
        int index = collection.IndexOf(oldValue);
            
        collection.RemoveAt(index);
        collection.Insert(index, newValue);
    }
}