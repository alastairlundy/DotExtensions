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

namespace AlastairLundy.DotExtensions.Exceptions
{
    
    /// <summary>
    /// Represents an exception thrown when a value is not found in a collection.
    /// </summary>
    /// <remarks>This exception is typically thrown by methods that access or manipulate collections.</remarks>
    public class ValueNotFoundException : Exception
    {
        
        /// <summary>
        /// Represents an exception thrown when a value cannot be found in a collection.
        /// </summary>
        /// <param name="collectionName">The name of the collection that was searched.</param>
        /// <param name="valueName">The name of the value that was expected.</param>
        public ValueNotFoundException(string collectionName, string valueName) : base(
            $"{Resources.Exceptions_ValueNotFound.Replace("{x}", valueName)
                .Replace("{y}", collectionName)}")
        {
            
        }
    }
}