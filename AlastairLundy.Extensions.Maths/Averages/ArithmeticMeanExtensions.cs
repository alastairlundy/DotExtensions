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

namespace AlastairLundy.Extensions.Maths.Averages
{
    /// <summary>
    /// A class to assist in getting the Arithmetic mean of an array of values.
    /// </summary>
    public static class ArithmeticMeanExtensions
    {
        /// <summary>
        /// Returns the arithmetic mean of a double array.
        /// </summary>
        /// <param name="values">The array of values to get the arithmetic mean of.</param>
        /// <returns></returns>
        public static double ArithmeticMean(this IEnumerable<double> values)
        {
            double[] enumerable = values as double[] ?? values.ToArray();
            
            return enumerable.Sum() / enumerable.Length;
        }

        /// <summary>
        /// Returns the arithmetic mean of a decimal array.
        /// </summary>
        /// <param name="values">The array of values to get the arithmetic mean of.</param>
        /// <returns></returns>
        public static decimal ArithmeticMean(this IEnumerable<decimal> values)
        {
            decimal[] enumerable = values as decimal[] ?? values.ToArray();
            
            return Decimal.Divide(enumerable.Sum(), Convert.ToDecimal(enumerable.Length));
        }

        /// <summary>
        /// Returns the arithmetic mean of a 64-Bit integer array.
        /// </summary>
        /// <param name="values">The array of values to get the arithmetic mean of.</param>
        /// <returns></returns>
        public static long ArtihmeticMean(this IEnumerable<long> values)
        {
            long[] enumerable = values as long[] ?? values.ToArray();
            
            return enumerable.Sum() / Convert.ToInt64(enumerable.Length);
        }

        /// <summary>
        ///  Returns the arithmetic mean of a 32-Bit integer array.
        /// </summary>
        /// <param name="values">The array of values to get the arithmetic mean of.</param>
        /// <returns></returns>
        public static int ArithmeticMean(this IEnumerable<int> values)
        {
            int[] enumerable = values as int[] ?? values.ToArray();
            
            return enumerable.Sum() / enumerable.Length;
        }
    
        /// <summary>
        ///  Returns the arithmetic mean of a float array.
        /// </summary>
        /// <param name="values">The array of values to get the arithmetic mean of.</param>
        /// <returns></returns>
        public static float ArithmeticMean(this IEnumerable<float> values)
        {
            float[] enumerable = values as float[] ?? values.ToArray();
            
            return enumerable.Sum() / Convert.ToSingle(enumerable.Length);
        }
    }
}