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

namespace AlastairLundy.DotExtensions.Memory;

/// <summary>
/// Provides extension methods for working with spans.
/// </summary>
public static class IsEmptyExtensions
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    extension<T>(Span<T> source)
    {
        /// <summary>
        /// Determines if a span is empty.
        /// </summary>
        /// <typeparam name="T">The type of elements in the Span.</typeparam>
        /// <returns>True if the span is empty, false otherwise.</returns>
        public bool IsEmpty
        {
            get
            {
                if (source.Length == 0 || source == Span<T>.Empty)
                    return true;

                return false;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    extension<T>(ReadOnlySpan<T> source)
    {
        /// <summary>
        /// Determines if a span is empty.
        /// </summary>
        /// <typeparam name="T">The type of elements in the Span.</typeparam>
        /// <returns>True if the span is empty, false otherwise.</returns>
        public bool IsEmpty
        {
            get
            {
                if (source.Length == 0 || source == Span<T>.Empty)
                    return true;

                return false;
            }
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="memory"></param>
    /// <typeparam name="T"></typeparam>
    extension<T>(Memory<T> memory)
    {
        /// <summary>
        /// Indicates whether the memory is empty.
        /// </summary>
        /// <typeparam name="T">The type of elements in the memory.</typeparam>
        /// <returns>True if the memory is empty or has a length of zero; otherwise, false.</returns>
        public bool IsEmpty => memory.Equals(Memory<T>.Empty) || memory.Length == 0;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="memory"></param>
    /// <typeparam name="T"></typeparam>
    extension<T>(ReadOnlyMemory<T> memory)
    {
        /// <summary>
        /// Indicates whether the specified read-only memory is empty.
        /// </summary>
        /// <typeparam name="T">The type of elements in the read-only memory.</typeparam>
        /// <returns>True if the read-only memory is empty or has a length of zero; otherwise, false.</returns>
        public bool IsEmpty => memory.Equals(ReadOnlyMemory<T>.Empty) || memory.Length == 0;
    }

    #region Is Empty Or char WhiteSpace Extensions
    /// <summary>
    /// 
    /// </summary>
    extension(Span<char>)
    {
        /// <summary>
        /// Determines if a span is empty or whitespace.
        /// </summary>
        /// <param name="source">The span to search.</param>
        /// <returns>True if the span is empty or whitespace, false otherwise.</returns>
        public static bool IsEmptyOrWhiteSpace(Span<char> source)
        {
            if (source.IsEmpty)
                return true;

            bool[] isWhiteSpace = new bool[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                isWhiteSpace[i] = char.IsWhiteSpace(source[i]);
            }

            return isWhiteSpace.All(x => x);
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="???"></param>
    extension(ReadOnlySpan<char>)
    {
        /// <summary>
        /// Determines if a span is empty or whitespace.
        /// </summary>
        /// <param name="source">The span to search.</param>
        /// <returns>True if the span is empty or whitespace, false otherwise.</returns>
        public static bool IsEmptyOrWhiteSpace(ReadOnlySpan<char> source)
        {
            if (source.IsEmpty)
                return true;

            bool[] isWhiteSpace = new bool[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                isWhiteSpace[i] = char.IsWhiteSpace(source[i]);
            }

            return isWhiteSpace.All(x => x);
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="???"></param>
    extension(Memory<char>)
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsEmptyOrWhiteSpace(Memory<char> source)
        {
            if (source.IsEmpty)
                return true;

            bool[] isWhiteSpace = new bool[source.Length];
            
            for (int i = 0; i < source.Length; i++)
            {
                isWhiteSpace[i] = char.IsWhiteSpace(source.Span[i]);
            }

            return isWhiteSpace.All(x => x);
        }
    }
    
    extension(ReadOnlyMemory<char>)
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsEmptyOrWhiteSpace(ReadOnlyMemory<char> source)
        {
            if (source.IsEmpty)
                return true;

            bool[] isWhiteSpace = new bool[source.Length];
            
            for (int i = 0; i < source.Length; i++)
            {
                isWhiteSpace[i] = char.IsWhiteSpace(source.Span[i]);
            }

            return isWhiteSpace.All(x => x);
        }
    }
    
    #endregion
    #region Is Empty or String WhiteSpace Extensions
    /// <summary>
    /// 
    /// </summary>
    /// <param name="???"></param>
        extension(Span<string>)
    {
        /// <summary>
        /// Determines if a span is empty or whitespace.
        /// </summary>
        /// <param name="source">The span to search.</param>
        /// <returns>True if the span is empty or whitespace, false otherwise.</returns>
        public static bool IsEmptyOrWhiteSpace(Span<string> source)
        {
            if (source.IsEmpty)
                return true;

            bool[] isWhiteSpace = new bool[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                isWhiteSpace[i] = source[i].IsWhiteSpace();
            }

            return isWhiteSpace.All(x => x);
        }
    }
    
    extension(ReadOnlySpan<string>)
    {
        /// <summary>
        /// Determines if a span is empty or whitespace.
        /// </summary>
        /// <param name="source">The span to search.</param>
        /// <returns>True if the span is empty or whitespace, false otherwise.</returns>
        public static bool IsEmptyOrWhiteSpace(ReadOnlySpan<string> source)
        {
            if (source.IsEmpty)
                return true;

            bool[] isWhiteSpace = new bool[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                isWhiteSpace[i] = source[i].IsWhiteSpace();
            }

            return isWhiteSpace.All(x => x);
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="???"></param>
    extension(Memory<string>)
    {
        /// <summary>
        /// Determines whether a memory of strings is empty or consists entirely of whitespace elements.
        /// </summary>
        /// <param name="source">The memory of strings to evaluate.</param>
        /// <returns>True if the memory is empty or all its elements are whitespace, false otherwise.</returns>
        public static bool IsEmptyOrWhiteSpace(Memory<string> source)
        {
            if (source.IsEmpty)
                return true;

            bool[] isWhiteSpace = new bool[source.Length];
            
            for (int i = 0; i < source.Length; i++)
            {
                isWhiteSpace[i] = source.Span[i].IsWhiteSpace();
            }

            return isWhiteSpace.All(x => x);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="???"></param>
    extension(ReadOnlyMemory<string>)
    {
        /// <summary>
        /// Determines if a ReadOnlyMemory of strings is empty or contains only whitespace elements.
        /// </summary>
        /// <param name="source">The ReadOnlyMemory of strings to evaluate.</param>
        /// <returns>True if the memory is empty or all its elements are whitespace, false otherwise.</returns>
        public static bool IsEmptyOrWhiteSpace(ReadOnlyMemory<string> source)
        {
            if (source.IsEmpty)
                return true;

            bool[] isWhiteSpace = new bool[source.Length];
            
            for (int i = 0; i < source.Length; i++)
            {
                isWhiteSpace[i] = source.Span[i].IsWhiteSpace();
            }

            return isWhiteSpace.All(x => x);
        }
    }
    #endregion
}
