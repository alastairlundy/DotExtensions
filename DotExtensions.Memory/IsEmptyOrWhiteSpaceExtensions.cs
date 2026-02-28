/*
        MIT License

       Copyright (c) 2026 Alastair Lundy

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
/// Provides extension methods for working with spans.
/// </summary>
public static class IsEmptyOrWhiteSpaceExtensions
{
    /// <param name="source">The span to search.</param>
    /// <typeparam name="T">The type of elements in the Span.</typeparam>
    extension<T>(Span<T> source)
    {
        /// <summary>
        /// Determines if a span is empty.
        /// </summary>
        /// <returns>True if the span is empty, false otherwise.</returns>
        public bool IsEmpty => source.Length == 0;
    }
    
    /// <param name="source">The span to search.</param>
    /// <typeparam name="T">The type of elements in the Span.</typeparam>
    extension<T>(ReadOnlySpan<T> source)
    {
        /// <summary>
        /// Determines if a span is empty.
        /// </summary>
        /// <returns>True if the span is empty, false otherwise.</returns>
        public bool IsEmpty => source.Length == 0;
    }
    
    /// <param name="memory">The memory to search.</param>
    /// <typeparam name="T">The type of elements in the memory.</typeparam>
    extension<T>(Memory<T> memory)
    {
        /// <summary>
        /// Indicates whether the memory is empty.
        /// </summary>
        /// <returns>True if the memory is empty or has a length of zero; otherwise, false.</returns>
        public bool IsEmpty => memory.Length == 0;
    }
    
    /// <param name="memory">The memory to search.</param>
    /// <typeparam name="T">The type of elements in the read-only memory.</typeparam>
    extension<T>(ReadOnlyMemory<T> memory)
    {
        /// <summary>
        /// Indicates whether the specified read-only memory is empty.
        /// </summary>
        /// <returns>True if the read-only memory is empty or has a length of zero; otherwise, false.</returns>
        public bool IsEmpty => memory.Length == 0;
    }

    #region Is Empty Or char WhiteSpace Extensions

    /// <param name="span">The span to search.</param>
    extension(Span<char> span)
    {
        /// <summary>
        /// Determines if a span is empty or whitespace.
        /// </summary>
        /// <param name="source">The span to search.</param>
        /// <returns>True if the span is empty or whitespace, false otherwise.</returns>
        public static bool IsEmptyOrWhiteSpace(Span<char> source) => source.IsEmpty || source.IsWhiteSpace();

        /// <summary>
        /// Determines if a span is empty or whitespace.
        /// </summary>
        /// <returns>True if the span is empty or whitespace, false otherwise.</returns>
        private bool IsWhiteSpace()
        {
            for (int i = 0; i < span.Length; i++)
            {
                bool isWhiteSpace = char.IsWhiteSpace(span[i]);

                if (!isWhiteSpace)
                    return false;
            }

            return true;
        }
    }
    
    /// <param name="span">The span to search.</param>
    extension(ReadOnlySpan<char> span)
    {
        /// <summary>
        /// Determines if a span is empty or whitespace.
        /// </summary>
        /// <param name="source">The span to search.</param>
        /// <returns>True if the span is empty or whitespace, false otherwise.</returns>
        public static bool IsEmptyOrWhiteSpace(ReadOnlySpan<char> source) => source.IsEmpty || source.IsWhiteSpace();

        /// <summary>
        /// Determines if a span is empty or whitespace.
        /// </summary>
        /// <returns>True if the span is empty or whitespace, false otherwise.</returns>
        private bool IsWhiteSpace()
        {
            for (int i = 0; i < span.Length; i++)
            {
                bool isWhiteSpace = char.IsWhiteSpace(span[i]);

                if (!isWhiteSpace)
                    return false;
            }

            return true;
        }
    }

    /// <summary>
    /// Provides extension methods for working with spans of memory.
    /// </summary>
    /// <param name="memory">The memory of characters to evaluate.</param>
    extension(Memory<char> memory)
    {
        /// <summary>
        /// Determines whether a span of memory containing characters is empty or consists only of whitespace.
        /// </summary>
        /// <param name="source">The memory of characters to evaluate.</param>
        /// <returns>True if the memory is empty or contains only whitespace characters; otherwise, false.</returns>
        public static bool IsEmptyOrWhiteSpace(Memory<char> source) => source.IsEmpty || source.IsWhiteSpace();

        /// <summary>
        /// Determines whether a span of memory containing characters is empty or consists only of whitespace.
        /// </summary>
        /// <returns>True if the memory is empty or contains only whitespace characters; otherwise, false.</returns>
        private bool IsWhiteSpace()
        {
            for (int i = 0; i < memory.Length; i++)
            { 
                bool isWhiteSpace = char.IsWhiteSpace(memory.Span[i]);

                if (!isWhiteSpace)
                    return false;
            }

            return true;
        }
    }
    
    /// <param name="memory">The read-only memory to evaluate.</param>
    extension(ReadOnlyMemory<char> memory)
    {
        /// <summary>
        /// Determines whether the provided read-only memory of characters is empty or consists solely of whitespace characters.
        /// </summary>
        /// <param name="source">The read-only memory to evaluate.</param>
        /// <returns>True if the memory is empty or contains only whitespace characters; otherwise, false.</returns>
        public static bool IsEmptyOrWhiteSpace(ReadOnlyMemory<char> source) => source.IsEmpty || source.IsWhiteSpace();

        /// <summary>
        /// Determines whether the provided read-only memory of characters is empty or consists solely of whitespace characters.
        /// </summary>
        /// <returns>True if the memory is empty or contains only whitespace characters; otherwise, false.</returns>
        private bool IsWhiteSpace()
        {
            for (int i = 0; i < memory.Length; i++)
            {
                bool isWhiteSpace = char.IsWhiteSpace(memory.Span[i]);

                if (!isWhiteSpace)
                    return false;
            }

            return true;
        }
    }
    
    #endregion
    #region Is Empty or String WhiteSpace Extensions

    /// <param name="span">The span to search.</param>
    extension(Span<string> span)
    {
        /// <summary>
        /// Determines if a span is empty or whitespace.
        /// </summary>
        /// <param name="source">The span to search.</param>
        /// <returns>True if the span is empty or whitespace, false otherwise.</returns>
        public static bool IsEmptyOrWhiteSpace(Span<string> source) => source.IsEmpty || source.IsWhiteSpace();

        /// <summary>
        /// Determines if a span contains only whitespace characters.
        /// </summary>
        /// <returns>
        /// True if the span contains only whitespace characters, false otherwise.
        /// </returns>
        private bool IsWhiteSpace()
        {
            for (int i = 0; i < span.Length; i++)
            {
                bool isWhiteSpace = span[i].IsWhiteSpace();
                
                if(!isWhiteSpace)
                    return false;
            }

            return true;
        }
    }
    
    /// <param name="span">The span to search.</param>
    extension(ReadOnlySpan<string> span)
    {
        /// <summary>
        /// Determines if a span is empty or whitespace.
        /// </summary>
        /// <param name="source">The span to search.</param>
        /// <returns>True if the span is empty or whitespace, false otherwise.</returns>
        public static bool IsEmptyOrWhiteSpace(ReadOnlySpan<string> source) => source.IsEmpty || source.IsWhiteSpace();

        /// <summary>
        /// Determines if a span is empty or whitespace.
        /// </summary>
        /// <returns>True if the span is empty or whitespace, false otherwise.</returns>
        private bool IsWhiteSpace()
        {
            bool[] isWhiteSpace = new bool[span.Length];
            for (int i = 0; i < span.Length; i++)
            {
                isWhiteSpace[i] = span[i].IsWhiteSpace();
            }

            return isWhiteSpace.All(x => x);
        }
    }
    
    /// <param name="memory">The input span to check for whitespace.</param>
    extension(Memory<string> memory)
    {
        /// <summary>
        /// Determines whether a memory of strings is empty or consists entirely of whitespace elements.
        /// </summary>
        /// <param name="source">The memory of strings to evaluate.</param>
        /// <returns>True if the memory is empty or all its elements are whitespace, false otherwise.</returns>
        public static bool IsEmptyOrWhiteSpace(Memory<string> source) => source.IsEmpty || source.IsWhiteSpace();

        /// <summary>
        /// Determines whether all elements in the provided span are whitespace.
        /// </summary>
        /// <returns>True if every element in the span is a whitespace character, false otherwise.</returns>
        public bool IsWhiteSpace()
        {
            bool[] isWhiteSpace = new bool[memory.Length];
            
            for (int i = 0; i < memory.Length; i++)
            {
                isWhiteSpace[i] = memory.Span[i].IsWhiteSpace();
            }

            return isWhiteSpace.All(x => x);
        }
    }
    
    /// <param name="memory">The ReadOnlyMemory of strings to evaluate.</param>
    extension(ReadOnlyMemory<string> memory)
    {
        /// <summary>
        /// Determines if a ReadOnlyMemory of strings is empty or contains only whitespace elements.
        /// </summary>
        /// <param name="source">The ReadOnlyMemory of strings to evaluate.</param>
        /// <returns>True if the memory is empty or all its elements are whitespace, false otherwise.</returns>
        public static bool IsEmptyOrWhiteSpace(ReadOnlyMemory<string> source) => source.IsEmpty || source.IsWhiteSpace();

        /// <summary>
        /// Determines if a ReadOnlyMemory of strings contains only whitespace elements.
        /// </summary>
        /// <returns>True if the memory contains only whitespace, false otherwise.</returns>
        private bool IsWhiteSpace()
        {
            bool[] isWhiteSpace = new bool[memory.Length];
            
            for (int i = 0; i < memory.Length; i++)
            {
                isWhiteSpace[i] = memory.Span[i].IsWhiteSpace();
            }

            return isWhiteSpace.All(x => x);
        }
    }
    #endregion
}
