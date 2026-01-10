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

namespace DotExtensions.MsExtensions.StringValuePlural;

public static class StringValuesToStringExtensions
{
    /// <summary>
    /// Provides extension methods for converting StringValues to string representations with various separators.
    /// </summary>
    extension(StringValues strValues)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToString()
            => strValues.ToString(' ');
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public string ToString(char separator)
        {
            StringBuilder stringBuilder = new();

            foreach (string? str in strValues)
            {
                if (str is not null)
                {
                    stringBuilder.Append(str);
                    stringBuilder.Append($"{separator}");
                }
            }

            string output = stringBuilder.ToString();
            
            if(output.EndsWith($"{separator}"))
                output = output.Remove(output.Length - 1, 1);
            
            return output;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public string ToString(string separator)
        {
            StringBuilder stringBuilder = new();

            foreach (string? str in strValues)
            {
                if (str is not null)
                {
                    stringBuilder.Append(str);
                    stringBuilder.Append($"{separator}");
                }
            }

            string output = stringBuilder.ToString();
            
            if(output.EndsWith($"{separator}"))
                output = output.Remove(output.Length - separator.Length, separator.Length);
            
            return output;
        }
    }
}