﻿/* BSD 3-Clause License
    
    Copyright (c) 2020-2021, AluminiumTech DevKit
    All rights reserved.
    
    Redistribution and use in source and binary forms, with or without
    modification, are permitted provided that the following conditions are met:
    
    1. Redistributions of source code must retain the above copyright notice, this
       list of conditions and the following disclaimer.
    
    2. Redistributions in binary form must reproduce the above copyright notice,
       this list of conditions and the following disclaimer in the documentation
       and/or other materials provided with the distribution.
    
    3. Neither the name of the copyright holder nor the names of its
       contributors may be used to endorse or promote products derived from
       this software without specific prior written permission.
    
    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
    AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
    IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
    DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
    FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
    DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
    SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
    CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
    OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
    OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
    */

using System;

namespace AlastairLundy.DeveloperKit.Maths{
    public class AverageHelper{

        public double Root(double value, double power)
        {
           return Math.Pow(value, (1.0 / power));
        }

        /// <summary>
        /// Calculate the geometric mean of a given set of numbers.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public double CalculateGeometricMean(double[] values)
        {
            double sum = 1;
            
            for(int index = 0; index < values.Length; index++)
            {
                sum *= values[index];
            }

            return Root(sum, values.Length);
        }
        /// <summary>
        /// Calculate the geometric mean of a given set of numbers.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public long CalculateGeometricMean(long[] values)
        {
            long sum = 1;
            
            for(int index = 0; index < values.Length; index++)
            {
                sum *= values[index];
            }

            return Convert.ToInt64(Root(Convert.ToDouble(sum), Convert.ToDouble(values.Length)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public double CalculateArithmeticMean(double[] values)
        {
            double sum = 0;

            for (int index = 0; index < values.Length; index++)
            {
                sum += index;
            }

            return sum / values.Length;
        }
    }
}
