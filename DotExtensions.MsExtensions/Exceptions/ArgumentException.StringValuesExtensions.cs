/*
        MIT License

       Copyright (c) 2025 Alastair Lundy

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

using AlastairLundy.DotExtensions.MsExtensions.StringValuePlural;


/// <summary>
/// Provides extension methods for handling ArgumentException with string values.
/// These extensions aim to enhance the functionality of ArgumentException by allowing more detailed analysis and reporting of invalid string inputs.
/// </summary>
public static class ArgumentExceptionStringValuesExtensions
{
    /// <summary>
    /// 
    /// </summary>
    extension(ArgumentException)
    {
        /// <summary>
        /// Checks whether a given collection of string values contains any null or whitespace strings.
        /// This method throws an ArgumentException if the provided StringValues collection is null,
        /// or if it is empty. It ensures robust handling of input validation by explicitly checking for these edge cases.
        /// </summary>
        /// <param name="values">The collection of strings to be validated.</param>
        /// <param name="paramName">
        /// The parameter name used in the exception message when an argument is null or whitespace.
        /// This can help identify which specific input caused the error during debugging.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if the values are null and paramName is not empty.</exception>
        /// <exception cref="ArgumentException">Thrown if any of the strings in the collection are null or whitespace.</exception>
        public static void ThrowIfNullOrEmpty(StringValues? values, string paramName = "")
        {
            if (values is null || StringValues.IsNullOrEmpty((StringValues)values))
            {
                if(paramName != string.Empty)
                    throw new ArgumentNullException(paramName);
                
                throw new ArgumentNullException($"{nameof(values)} cannot be null or empty.");
            }
        }

        /// <summary>
        /// Checks whether a given collection of string values contains any null or whitespace strings.
        /// This method throws an ArgumentException if the provided StringValues collection is null,
        /// or if it contains any string that is either null or consists solely of whitespace characters.
        /// It ensures robust handling of input validation by explicitly checking for these edge cases.
        /// </summary>
        /// <param name="values">The collection of strings to be validated.</param>
        /// <param name="paramName">
        /// The parameter name used in the exception message when an argument is null or whitespace.
        /// This can help identify which specific input caused the error during debugging.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if the values are null and paramName is not empty.</exception>
        /// <exception cref="ArgumentException">Thrown if any of the strings in the collection are null or whitespace.</exception>
        public static void ThrowIfNullOrWhiteSpace(StringValues? values, string paramName = "")
        {
            if (values is null)
            {
                if (paramName != string.Empty)
                    throw new ArgumentNullException(paramName);
                
                throw new ArgumentNullException($"{nameof(values)} cannot be null.");
            }

            if (!StringValues.IsNullOrWhiteSpace(values)) 
                return;
            
            if(paramName != string.Empty)
                throw new ArgumentNullException(paramName);
                
            throw new ArgumentException($"{nameof(values)} cannot be null or whitespace.");
        }
    }
}