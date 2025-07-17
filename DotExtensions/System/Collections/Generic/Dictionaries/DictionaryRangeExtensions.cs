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
using System.Collections.Generic;

namespace AlastairLundy.DotExtensions.Collections.Generic.Dictionaries;

public static class DictionaryRangeExtensions
{
        
    /// <summary>
    /// Adds an IEnumerable of KeyValuePair items to a Dictionary.
    /// </summary>
    /// <param name="source">The dictionary to add items to.</param>
    /// <param name="pairsToAdd">The items to be added.</param>
    /// <typeparam name="TKey">The type representing the Keys.</typeparam>
    /// <typeparam name="TValue">The type representing the Values.</typeparam>
    /// <exception cref="OverflowException">Thrown if the dictionary is unable to store all the Key Value Pairs to be added.</exception>
    public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> source,
        IEnumerable<KeyValuePair<TKey, TValue>> pairsToAdd)
    {
        foreach (KeyValuePair<TKey, TValue> pair in pairsToAdd)
        {
            if (source.Count == int.MaxValue)
            {
                throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
            }
            
            source.Add(pair.Key, pair.Value);
        }
    }

    /// <summary>
    /// Appends the contents of one Dictionary to the current dictionary.
    /// </summary>
    /// <param name="source">The dictionary to add items to.</param>
    /// <param name="dictionaryToAdd">The dictionary to be added to the existing dictionary.</param>
    /// <typeparam name="TKey">The type representing the Keys.</typeparam>
    /// <typeparam name="TValue">The type representing the Values.</typeparam>
    /// <exception cref="OverflowException">Thrown if the dictionary is unable to store all the dictionary values to be added.</exception>
    public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> source,
        IDictionary<TKey, TValue> dictionaryToAdd)
    {
        if (source.Count == int.MaxValue)
        {
            throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
        }
        else if (dictionaryToAdd.Count == int.MaxValue)
        {
            throw new OverflowException($"{nameof(dictionaryToAdd)} contains the maximum size of {int.MaxValue} and cannot be added to {nameof(source)}.");
        }
         
        foreach (KeyValuePair<TKey, TValue> pair in dictionaryToAdd)
        {
            if (source.Count == int.MaxValue)
            {
                throw new OverflowException($"{nameof(source)} contains the maximum size of {int.MaxValue} and cannot be added to.");
            }
            
            source.Add(pair.Key, pair.Value);
        }
    }
}