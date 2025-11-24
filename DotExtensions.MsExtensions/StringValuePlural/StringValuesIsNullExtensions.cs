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



// ReSharper disable ConvertClosureToMethodGroup
// ReSharper disable RedundantBoolCompare

using System.Diagnostics.CodeAnalysis;

namespace AlastairLundy.DotExtensions.MsExtensions.StringValuePlural;

/// <summary>
/// Provides extension methods for working with <see cref="StringValues"/> to determine
/// if they are null, empty, or contain only null or whitespace values.
/// </summary>
public static class StringValuesIsNullExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strValues"></param>
    extension(StringValues strValues)
    {
        /// <summary>
        /// Whether this <see cref="StringValues"/> is empty.
        /// </summary>
        /// <returns>True if it is empty, false otherwise.</returns>
        public bool IsEmpty => strValues.Equals(StringValues.Empty);
    }

    /// <summary>
    /// 
    /// </summary>
    extension(StringValues)
    {
        /// <summary>
        /// Determines whether a <see cref="StringValues"/> contains any strings that are null or whitespace./>
        /// </summary>
        /// <param name="other"></param>
        /// <returns>True if any of the strings is WhiteSpace or null.</returns>
        public static bool IsNullOrWhiteSpace(StringValues? other)
        {
            if (other is null)
                return true;

            return StringValues.IsWhiteSpace((StringValues)other);
        }

        /// <summary>
        /// Determines whether a <see cref="StringValues"/> contains only strings that consist entirely of whitespace characters.
        /// </summary>
        /// <param name="other">The <see cref="StringValues"/> to check for whitespace characters.</param>
        /// <returns>True if all strings in the <see cref="StringValues"/> consist entirely of whitespace characters; otherwise, false.</returns>
        public static bool IsWhiteSpace(StringValues other)
        {
            ArgumentNullException.ThrowIfNull(other);
            
            bool[] vals = new bool[other.Count];

            for (int index = 0; index < other.Count; index++)
            {
                string? val = other[index];

                ArgumentNullException.ThrowIfNull(val);
                
                vals[index] = string.IsNullOrWhiteSpace(val);
            }

            return vals.Any(x => x == true);
        }
    }
}
