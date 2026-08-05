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

namespace DotExtensions.MsExtensions.Primitives;

/// <summary>
/// Provides extension methods for working with <see cref="StringValues"/> to determine
/// if they are null, empty, or contain only null or whitespace values.
/// </summary>
public static class StringValuesExtensions
{
    /// <summary>
    /// Provides extension methods for null checks on <see cref="StringValues"/>.
    /// </summary>
    extension(StringValues strValues)
    {
        /// <summary>
        /// Whether this <see cref="StringValues"/> is empty.
        /// </summary>
        /// <returns>True if it is empty, false otherwise.</returns>
        public bool IsEmpty => strValues.Equals(StringValues.Empty);
    }
    
    extension(StringValues)
    {
        /// <summary>
        /// Determines whether a <see cref="StringValues"/> contains any strings that are null or whitespace./>
        /// </summary>
        /// <param name="other"></param>
        /// <returns>True if any of the strings is WhiteSpace or null.</returns>
        public static bool IsNullOrWhiteSpace(StringValues? other)
        {
            if (other is null)
                return true;
            
            return StringValues.IsWhiteSpace((StringValues)other);
        }

        /// <summary>
        /// Determines whether a <see cref="StringValues"/> contains only strings that consist entirely of whitespace characters.
        /// </summary>
        /// <param name="other">The <see cref="StringValues"/> to check for whitespace characters.</param>
        /// <returns>True if all strings in the <see cref="StringValues"/> consist entirely of whitespace characters; otherwise, false.</returns>
        private static bool IsWhiteSpace(StringValues other)
        {
            if (other.Count == 0)
                return false;
            
            bool[] vals = new bool[other.Count];

            for (int index = 0; index < other.Count; index++)
            {
                string? val = other[index];
                
                vals[index] = string.IsNullOrWhiteSpace(val);
            }

            return vals.Any(x => x);
        }
    }
    
    #region Length

    /// <param name="stringValues">The <see cref="StringValues"/> object to search.</param>
    extension(StringValues stringValues)
    {
        /// <summary>
        /// The total length of all strings in a <see cref="StringValues"/> object combined.
        /// </summary>
        public int TotalLength
        {
            get
            {
                int length = 0;

                foreach (string? value in stringValues)
                {
                    if(value is not null)
                        length += value.Length;
                }

                return length;
            }  
        }
    }
    
    #endregion
    
    #region ToString

    /// <summary>
    /// Provides extension methods for converting <see cref="StringValues"/> to string representations with various separators.
    /// </summary>
    extension(StringValues strValues)
    {
        /// <summary>
        /// Converts the StringValues instance to its string representation
        /// using the specified character as a separator.
        /// </summary>
        /// <param name="separator">
        /// The character used to separate the values in the StringValues instance.
        /// </param>
        /// <returns>
        /// A string that concatenates the values in the StringValues instance,
        /// separated by the specified character.
        /// </returns>
        public string ToString(char separator)
        {
            StringBuilder stringBuilder = new(strValues.TotalLength);

            foreach (string? str in strValues)
            {
                if (str is not null)
                {
                    stringBuilder.Append(str);
                    stringBuilder.Append($"{separator}");
                }
            }

            string output = stringBuilder.ToString();
            
            if(output.EndsWith($"{separator}", StringComparison.Ordinal))
                output = output.Remove(output.Length - 1, 1);
            
            return output;
        }

        /// <summary>
        /// Converts the StringValues instance to its string representation
        /// using a default separator (' ').
        /// </summary>
        /// <param name="separator">
        /// The string used to separate the values in the StringValues instance.
        /// </param>
        /// <returns>
        /// A string that concatenates the values in the StringValues instance,
        /// separated by the default character (' ').
        /// </returns>
        public string ToString(string separator)
        {
            StringBuilder stringBuilder = new(strValues.TotalLength);

            foreach (string? str in strValues)
            {
                if (str is not null)
                {
                    stringBuilder.Append(str);
                    stringBuilder.Append($"{separator}");
                }
            }

            string output = stringBuilder.ToString();
            
            if(output.EndsWith($"{separator}", StringComparison.Ordinal))
                output = output.Remove(output.Length - separator.Length,
                    separator.Length);
            
            return output;
        }
    }
    
    #endregion
}