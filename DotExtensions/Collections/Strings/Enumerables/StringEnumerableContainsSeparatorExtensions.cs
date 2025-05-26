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
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global

namespace AlastairLundy.DotExtensions.Collections.Strings.Enumerables
{
    public static class StringEnumerableContainsSeparatorExtensions
    {
        /// <summary>
        /// Check to see if an IEnumerable contains a separator character.
        /// </summary>
        /// <param name="args">The IEnumerable to be searched.</param>
        /// <param name="separator">The separator to look for.</param>
        /// <returns>True, if the separator character is found in the IEnumerable, returns false otherwise.</returns>
        public static bool ContainsSeparator(this IEnumerable<string> args, char separator)
        {
            return args.Any(x => x.Contains(separator) && x.Split(separator).Length > 1);
        }
    
        /// <summary>
        /// Check to see if an IEnumerable contains a separator character string.
        /// </summary>
        /// <param name="args">The IEnumerable to be searched.</param>
        /// <param name="separator">The separator to look for.</param>
        /// <returns>True, if the separator character string is found in the IEnumerable,
        /// returns false otherwise.</returns>
        public static bool ContainsSeparator(this IEnumerable<string> args, string separator)
        {
            //string[] enumerable = args as string[] ?? args.ToArray();

            return args.Any(x => x.Contains(separator) && x.Split([separator], StringSplitOptions.None).Length > 1);
        }
    }
}