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

namespace DotExtensions.Dates;

/// <summary>
/// Provides extension methods for calculating the number of Days in a period of time.
/// </summary>
public static class DayInExtensions
{
    private static int DaysInYear(int year)
    {
        int days = 0;
            
        for (int month = 1; month <= 12; month++)
        {
            days += DateTime.DaysInMonth(year, month);
        }
            
        return days;
    }

#if NET8_0_OR_GREATER
    /// <summary>
    /// Extensions to calculate the number of days in a <see cref="DateOnly"/> object within a time interval.
    /// </summary>
    /// <param name="dateOnly">The <see cref="DateOnly"/> object to calculate the total number of days in the year for.</param>
    extension(DateOnly dateOnly)
    {
        /// <summary>
        /// Calculates the total number of days in the year of the specified <see cref="DateOnly"/> object.
        /// </summary>
        /// <returns>The total number of days in the year of the specified <see cref="DateOnly"/>.</returns>
        public int DaysInYear()
            => DaysInYear(dateOnly.Year);
    }
#endif
    
    /// <summary>
    /// Extensions to calculate the number of days in a <see cref="DateTime"/> object within a time interval.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/> object representing the year to calculate the total number of days for.</param>
    extension(DateTime dateTime)
    {
        /// <summary>
        /// Calculates the total number of days in the year of the specified <see cref="DateTime"/> object.
        /// </summary>
        /// <returns>The total number of days in the year of the specified <see cref="DateTime"/>.</returns>
        public int DaysInYear()
            => DaysInYear(dateTime.Year);
    }
}