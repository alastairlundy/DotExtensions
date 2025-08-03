using System;

namespace AlastairLundy.DotExtensions.Memory.Spans;

/// <summary>
/// 
/// </summary>
public static class SpanResizeExtensions
{
    /// <summary>
    /// Resizes the span to the specified new size and optimistically copies the elements of the old span to the new Span.
    /// </summary>
    /// <param name="target">The span to be resized.</param>
    /// <param name="newSize">The desired new size of the span.</param>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the new size is less than or equal to zero.</exception>
    public static void Resize<T>(this ref Span<T> target, int newSize)
    {
        if (newSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(newSize));

        T[] newTargetArray = new T[newSize];
        
        Span<T> destination = new  Span<T>(newTargetArray);
        
        int endCopy = target.Length < newSize ? target.Length : newSize;
        
        target.OptimisticCopy(ref destination, 0, endCopy);
        
        target = destination;
    }
}