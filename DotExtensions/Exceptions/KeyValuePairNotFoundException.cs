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
using AlastairLundy.DotExtensions.Localizations;

namespace AlastairLundy.DotExtensions.Exceptions;

/// <summary>
/// Represents an exception thrown when a key-value pair is not found in a collection.
/// </summary>
/// <remarks>This exception may be thrown when a Key is found but is associated with a different value than expected or vice versa.
/// <para>This exception is typically thrown by methods that access or manipulate collections.</para></remarks>
public class KeyValuePairNotFoundException : Exception
{
    ///<summary>
    /// Initializes a new instance of the <see cref="KeyValuePairNotFoundException"/> class.
    /// </summary>
    /// <param name="collectionName">The name of the collection that was not found.</param>
    public KeyValuePairNotFoundException(string collectionName)
        : base($"{Resources.Exceptions_KeyValuePairNotFound}: {collectionName}") { }
}
