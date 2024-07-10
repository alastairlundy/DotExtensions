﻿/*
        MIT License
       
       Copyright (c) 2024 Alastair Lundy
       
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

namespace AlastairLundy.Extensions.System;

    public static class DateTimeSubtractExtensions
    {
#if NET8_0_OR_GREATER
        /// <summary>
        /// Subtract a specified amount of microseconds from a DateTime.
        /// </summary>
        /// <param name="dateTime">The DateTime to be subtracted from.</param>
        /// <param name="microseconds">The amount of microseconds to subtract from the specified DateTime.</param>
        /// <returns>the modified DateTime object.</returns>
        public static DateTime SubtractMicroseconds(this DateTime dateTime, double microseconds)
        {
            return dateTime.Subtract(TimeSpan.FromMicroseconds(microseconds));
        }
#endif
        
        /// <summary>
        /// Subtract a specified amount of milliseconds from a DateTime.
        /// </summary>
        /// <param name="dateTime">The DateTime to be subtracted from.</param>
        /// <param name="milliseconds">The amount of milliseconds to subtract from the specified DateTime.</param>
        /// <returns>the modified DateTime object.</returns>
        public static DateTime SubtractMilliseconds(this DateTime dateTime, double milliseconds)
        {
            return dateTime.Subtract(TimeSpan.FromMilliseconds(milliseconds));
        }

        /// <summary>
        /// Subtract a specified amount of seconds from a DateTime.
        /// </summary>
        /// <param name="dateTime">The DateTime to be subtracted from.</param>
        /// <param name="seconds">The amount of seconds to subtract from the specified DateTime.</param>
        /// <returns>the modified DateTime object.</returns>
        public static DateTime SubtractSeconds(this DateTime dateTime, double seconds)
        {
            return dateTime.Subtract(TimeSpan.FromSeconds(seconds));
        }
        
        /// <summary>
        /// Subtract a specified amount of minutes from a DateTime.
        /// </summary>
        /// <param name="dateTime">The DateTime to be subtracted from.</param>
        /// <param name="minutes">The amount of minutes to subtract from the specified DateTime.</param>
        /// <returns>the modified DateTime object.</returns>
        public static DateTime SubtractMinutes(this DateTime dateTime, double minutes)
        {
            return dateTime.Subtract(TimeSpan.FromMinutes(minutes));
        }
        
        /// <summary>
        /// Subtract a specified amount of hours from a DateTime.
        /// </summary>
        /// <param name="dateTime">The DateTime to be subtracted from.</param>
        /// <param name="hours">The amount of hours to subtract from the specified DateTime.</param>
        /// <returns>the modified DateTime object.</returns>
        public static DateTime SubtractHours(this DateTime dateTime, double hours)
        {
            return dateTime.Subtract(TimeSpan.FromHours(hours));
        }
        
        /// <summary>
        /// Subtract a specified amount of days from a DateTime.
        /// </summary>
        /// <param name="dateTime">The DateTime to be subtracted from.</param>
        /// <param name="days">The amount of days to subtract from the specified DateTime.</param>
        /// <returns>the modified DateTime object.</returns>
        public static DateTime SubtractDays(this DateTime dateTime, double days)
        {
           return dateTime.Subtract(TimeSpan.FromDays(days));
        }
        
        /// <summary>
        /// Subtract a specified amount of months from a DateTime.
        /// </summary>
        /// <param name="dateTime">The DateTime to be subtracted from.</param>
        /// <param name="months">The amount of months to subtract from the specified DateTime.</param>
        /// <returns>the modified DateTime object.</returns>
        public static DateTime SubtractMonths(this DateTime dateTime, double months)
        {
            DateTime result = dateTime;
            int daysToRemove = 0;

            if(months > 12)
            {
                double years = Math.Round(12 % months);
                result = dateTime.SubtractYears(years);

                double remainder = years - Math.Round(12 % months);
                daysToRemove += Convert.ToInt32(remainder * Convert.ToDouble(DateTime.DaysInMonth(result.Year, result.Month)));
            }

            if (months <= 12)
            {
                for (double index = 1.0; index <= months; index += 1.0)
                {

                    int currentMonth = dateTime.Month - Convert.ToInt32(index);
                    daysToRemove += DateTime.DaysInMonth(dateTime.Year, currentMonth);
                }
            }

            return result.Subtract(TimeSpan.FromDays(Convert.ToDouble(daysToRemove)));
        }

        /// <summary>
        /// Subtract a specified amount of years from a DateTime.
        /// </summary>
        /// <param name="dateTime">The DateTime to be subtracted from.</param>
        /// <param name="years">The amount of years to subtract from the specified DateTime.</param>
        /// <returns>the modified DateTime object.</returns>
        public static DateTime SubtractYears(this DateTime dateTime, double years)
        {
            int daysToRemove = 0;
            
            for (double index = 0.0; index < years; index += 1.0)
            {
                for (int monthCount = 1; monthCount <= 12; monthCount++)
                {
                    daysToRemove += DateTime.DaysInMonth(dateTime.Year - Convert.ToInt32(index), monthCount);
                }
            }

            return dateTime.Subtract(TimeSpan.FromDays(daysToRemove));
        }
    }