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
using System.Linq;
// ReSharper disable CheckNamespace

// ReSharper disable RedundantBoolCompare
// ReSharper disable ConvertClosureToMethodGroup

namespace AlastairLundy.DotExtensions.Strings;

/// <summary>
/// 
/// </summary>
public static class StringContainsOfExtensions
{
    /// <summary>
    /// Returns whether a string contains all the specified chars.
    /// </summary>
    /// <param name="source">The string to be searched.</param>
    /// <param name="values">The chars to search for.</param>
    /// <returns>True if all the values are found in the string; false otherwise.</returns>
    [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
    public static bool ContainsAllOf(this string source, IEnumerable<char> values)
    {
        return values.Select(t => source.Contains(t)).All(containsSource => containsSource == true);
    }
        
    /// <summary>
    /// Returns whether a string contains any of the specified possible chars.
    /// </summary>
    /// <param name="source">The string to be searched.</param>
    /// <param name="possibleValues">The possible values to search for.</param>
    /// <returns>True if any of the possible values is found, false otherwise.</returns>
    [Obsolete(Deprecations.DeprecationMessages.DeprecationV8)]
    public static bool ContainsAnyOf(this string source, IEnumerable<char> possibleValues)
    {
        return possibleValues.Select(c => source.Contains(c)).Any(containsValue => containsValue == true);
    }
}