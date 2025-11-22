namespace AlastairLundy.DotExtensions.Memory;

public static class ReverseExtensions
{
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    extension<T>(ref ReadOnlySpan<T> span)
    {
        /// <summary>
        /// Reverses the contents of the specified read-only span,
        /// returning a new <see cref="ReadOnlySpan{T}"/> with the elements in reverse order.
        /// </summary>
        public void Reverse()
        {
            T[] array = new T[span.Length];

            for(int index = span.Length - 1; index > 0; index--)
            {
                array[(span.Length - 1) - index] = span[index];
            }
            
            span = array;
        }
    }
    
    extension<T>(ref Memory<T> memory)
    {
        /// <summary>
        /// Reverses the contents of the specified memory,
        /// returning a new <see cref="Memory{T}"/> with the elements in reverse order.
        /// </summary>
        public void Reverse()
        {
            T[] array = new T[memory.Length];

            for(int index = memory.Length - 1; index > 0; index--)
            {
                array[(memory.Length - 1) - index] = memory.Span[index];
            }
            
            memory = array;
        }
    }
    
    /// <typeparam name="T">The type of elements in the memory.</typeparam>
    /// <param name="memory">The read-only memory span to reverse.</param>
    extension<T>(ref ReadOnlyMemory<T> memory)
    {
        /// <summary>
        /// Reverses the contents of the specified read-only memory,
        /// returning a new <see cref="ReadOnlyMemory{T}"/> with the elements in reverse order.
        /// </summary>
        public void Reverse()
        {
            T[] array = new T[memory.Length];

            for(int index = memory.Length - 1; index > 0; index--)
            {
                array[(memory.Length - 1) - index] = memory.Span[index];
            }
            
            memory = array;
        }
    }
}