using System;
using System.Collections.Generic;

namespace AlastairLundy.DotExtensions.Collections.Generic.ILists;

/// <summary>
/// 
/// </summary>
public static class DistinctListExtensions
{
    /// <summary>
    /// Creates a new <see cref="List{T}"/> non-distinct elements from the source list.
    /// </summary>
    /// <param name="source">The list to de-duplicate.</param>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <returns>The new list with distinct elements from the source list.</returns>
    public static List<T> DistinctList<T>(this List<T> source)
    {
        HashSet<T> hash = new();
        List<T> output = new();
        
        for (int index = 0; index < source.Count; index++)
        {
            T item = source[index];
            bool result = hash.Add(item);

            if (result == false)
                output.Add(item);
        }

        return output;
    }

    /// <summary>
    /// Creates a new array with distinct elements from the source array.
    /// </summary>
    /// <param name="source">The array to de-duplicate.</param>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <returns>The new array with distinct elements from the source array.</returns>
    public static T[] DistinctArray<T>(this T[] source)
    {
        HashSet<T> hash = new();
        T[] output = new T[source.Length];

        int count = 0;

        for (int index = 0; index < source.Length; index++)
        {
            T item = source[index];
            
            bool result = hash.Add(item);

            if (result == false)
            {
                output[count] = source[index];
                count++;
            }
        }
        
        Array.Resize(ref output, count);
        
        return output;
    }
}