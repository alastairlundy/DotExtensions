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
using System.Linq;
using System.Text;

// ReSharper disable RedundantBoolCompare
// ReSharper disable RedundantToStringCallForValueType

// ReSharper disable SuggestVarOrType_SimpleTypes

namespace AlastairLundy.DotExtensions.Collections.Generic.Enumerables
{
    public static class EnumerableToStringObjectExtensions
    {
        /// <summary>
        /// Converts an IEnumerable of Type T to a string separated by a separator string.
        /// </summary>
        /// <param name="source">The enumerable to be turned into a string.</param>
        /// <param name="separator">The string to separate the items in the source enumerable.</param>
        /// <typeparam name="T">The type of objects to be enumerated.</typeparam>
        /// <returns>The string containing all the strings in the source enumerable separated by the separator.</returns>
        public static string ToString<T>(this IEnumerable<T> source, string separator)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (T item in source)
            {
                if (item is null)
                {
                    throw new NullReferenceException($"Item {nameof(item)} in {nameof(source)} was null");
                }

                if (typeof(T) == typeof(string))
                {
                    stringBuilder.Append(item);
                }
                else
                {
                    bool overridesToString = typeof(T).GetMethods().Any(x =>
                        x.Name.Equals(nameof(ToString), StringComparison.Ordinal) && x.IsVirtual == false);

                    if (overridesToString == true)
                    {
                        stringBuilder.Append(item.ToString()); 
                    }
                    else
                    {
                        stringBuilder.Append(item);
                    }
                }

                if (separator == Environment.NewLine)
                {
                    stringBuilder.AppendLine();
                }
                else
                {
                    stringBuilder.Append(separator);
                }
            }

            stringBuilder = stringBuilder.Remove(stringBuilder.Length, stringBuilder.Length - separator.Length);
        
            return stringBuilder.ToString();
        }
    }
}