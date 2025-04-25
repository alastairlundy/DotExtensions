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
// ReSharper disable SuggestVarOrType_BuiltInTypes

// ReSharper disable RedundantBoolCompare
// ReSharper disable RedundantIfElseBlock

namespace AlastairLundy.DotExtensions.Collections.Generic.Dictionaries
{
    public static class DictionaryGetValueExtensions
    {
        /// <summary>
        /// Returns the value associated with a Key if found or a default value if not found.
        /// </summary>
        /// <param name="dictionary">The dictionary to be searched.</param>
        /// <param name="key">The key to search for.</param>
        /// <param name="defaultValue">The value to be returned if the key is not found.</param>
        /// <typeparam name="TKey">The type of Key stored in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of Value stored in the dictionary.</typeparam>
        /// <returns>the value associated with the specified Key if found; the specified default value otherwise.</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            TValue defaultValue)
        {
            try
            {
                bool containsKey = dictionary.TryGetValue(key, out TValue? value);

                if (containsKey == true && value is not null)
                {
                    return value;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}