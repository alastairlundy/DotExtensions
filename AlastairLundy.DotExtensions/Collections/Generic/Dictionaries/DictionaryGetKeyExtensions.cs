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


using System.Collections.Generic;

using AlastairLundy.DotExtensions.Exceptions;

namespace AlastairLundy.DotExtensions.Collections.Generic.Dictionaries
{
    public static class DictionaryGetKeyExtensions
    {
        /// <summary>
        /// Gets the Key associated with the specified value in the Dictionary.
        /// </summary>
        /// <remarks>
        /// This method assumes there is only ONE Key associated with a specific Value in a Dictionary.
        /// If multiple Keys have the same Value, use the GetKeys method instead.</remarks>
        /// <param name="dictionary">The Dictionary to be searched.</param>
        /// <param name="value">The value to search for.</param>
        /// <typeparam name="TKey">The type of Key in the Dictionary.</typeparam>
        /// <typeparam name="TValue">The type of Value in the Dictionary.</typeparam>
        /// <returns>The key associated with the specified value in a Dictionary.</returns>
        /// <exception cref="ValueNotFoundException">Thrown if the Dictionary does not contain the specified value.</exception>
        public static TKey GetKeyByValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TValue value) where TKey : notnull
        {
            if (dictionary.ContainsValue(value))
            {
                foreach (KeyValuePair<TKey, TValue> pair in dictionary)
                {
                    if (pair.Value is not null && pair.Value.Equals(value))
                    {
                        return pair.Key;
                    }
                }
            }

            throw new ValueNotFoundException(nameof(dictionary), nameof(value));
        }

        /// <summary>
        /// Returns all keys associated with a specified value in a Dictionary.
        /// </summary>
        /// <param name="dictionary">The Dictionary to be searched.</param>
        /// <param name="value">The value to search for.</param>
        /// <typeparam name="TKey">The type of Key in the Dictionary.</typeparam>
        /// <typeparam name="TValue">The type of Value in the Dictionary.</typeparam>
        /// <returns>The keys associated with the specified value in a Dictionary.</returns>
        /// <exception cref="ValueNotFoundException">Thrown if the specified value is not found within the Dictionary.</exception>
        public static IEnumerable<TKey> GetKeysByValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
        {
            List<TKey> list = new List<TKey>();
            
            if (dictionary.Values.Contains(value))
            {
                foreach (KeyValuePair<TKey, TValue> pair in dictionary)
                {
                    if (pair.Value is not null && pair.Value.Equals(value))
                    {
                        list.Add(pair.Key);
                    }
                }

                return list.ToArray();
            }

            throw new ValueNotFoundException(nameof(dictionary), nameof(value));
        }
    }
}