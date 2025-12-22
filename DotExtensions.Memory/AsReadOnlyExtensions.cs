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

namespace DotExtensions.Memory;

/// <summary>
/// Provides a set of extension methods to facilitate the conversion of mutable memory structures to their read-only equivalents.
/// </summary>
public static class AsReadOnlyExtensions
{
    /// <summary>
    /// Provides extension methods for converting writable spans to read-only spans.
    /// </summary>
    extension<T>(ref Span<T> span)
    {
        /// <summary>
        /// Converts the provided writable span to a read-only span representation.
        /// </summary>
        /// <returns>A read-only span representation of the original writable span.</returns>
        public ReadOnlySpan<T> AsReadOnlySpan()
            => span;
    }

    /// <summary>
    /// Provides extension methods for converting writable memory to read-only memory.
    /// </summary>
    extension<T>(ref Memory<T> memory)
    {
        /// <summary>
        /// Converts the provided writable memory to a read-only memory representation.
        /// </summary>
        /// <returns>A read-only memory representation of the original writable memory.</returns>
        public ReadOnlyMemory<T> AsReadOnlyMemory()
            => memory;
    }
}