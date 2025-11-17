/*
        MIT License
       
       Copyright (c) 2020-2025 Alastair Lundy
       
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

namespace AlastairLundy.DotExtensions.Dates;

/// <summary>
/// 
/// </summary>
public static class DayInExtensions
{
    internal static int DaysInYear(int year)
    {
        int days = 0;
            
        for (int month = 1; month <= 12; month++)
        {
            days += DateTime.DaysInMonth(year, month);
        }
            
        return days;
    }

    /// <summary>
    /// Provides extension methods for working with DateTime objects.
    /// </summary>
    extension(DateTime)
    {
        /// <summary>
        /// Calculates the total number of days in the year of the specified DateTime object.
        /// </summary>
        /// <param name="dateTime">The DateTime object representing the year to calculate the total number of days for.</param>
        /// <returns>The total number of days in the year of the specified DateTime.</returns>
        public static int DaysInYear(DateTime dateTime)
            => DaysInYear(dateTime.Year);
    }
}