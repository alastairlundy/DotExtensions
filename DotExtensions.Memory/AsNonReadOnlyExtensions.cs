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

namespace AlastairLundy.DotExtensions.Memory;

/// <summary>
/// Provides extension methods for converting <see cref="ReadOnlyMemory{T}"/> or <see cref="ReadOnlySpan{T}"/> instances
/// to their writable counterparts.
/// </summary>
public static class AsNonReadOnlyExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    extension<T>(ReadOnlySpan<T> source)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Span<T> AsSpan()
        {
            T[] array = ArrayPool<T>.Shared.Rent(source.Length);

            Span<T> output = new Span<T>(array);
            
            source.CopyTo(output);
            
            output = output.Slice(0, source.Length);
            
            ArrayPool<T>.Shared.Return(array);
            return output;
        }
    }
    
    /// <param name="source">The <see cref="ReadOnlyMemory{T}"/> instance to convert.</param>
    /// <typeparam name="T">The type of elements stored in the memory.</typeparam>
    extension<T>(ReadOnlyMemory<T> source)
    {
        /// <summary>
        /// Converts a <see cref="ReadOnlyMemory{T}"/> instance to a writable <see cref="Memory{T}"/>instance.
        /// The method creates a copy of the data to ensure writability without altering the original source.
        /// </summary>
        /// <returns>A <see cref="Memory{T}"/> instance containing a copied version of the data from the source.</returns>
        public Memory<T> AsMemory()
        {
            T[] array = ArrayPool<T>.Shared.Rent(source.Length);

            Memory<T> output = new Memory<T>(array);
            
            source.CopyTo(output);
            
            output = output.Slice(0, source.Length); 
            
            ArrayPool<T>.Shared.Return(array);
            return output;
        }
    }
}