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
public static class DateTimeAbsoluteExtensions
{
    /// <summary>
    /// Determines the absolute difference of two <see cref="DateTime"/> objects.
    /// </summary>
    /// <param name="dateTimeOne">The first date to be subtracted.</param>
    /// <param name="dateTimeTwo">The second date to be subtracted.</param>
    /// <returns>The absolute difference as a <see cref="TimeSpan"/>.</returns>
    public static TimeSpan Abs(this DateTime dateTimeOne, DateTime dateTimeTwo)
    {
        long absoluteTicks = Math.Abs(dateTimeOne.Ticks - dateTimeTwo.Ticks);

        return TimeSpan.FromTicks(absoluteTicks);
    }

    /// <summary>
    /// Determines the absolute difference of two <see cref="TimeOnly"/> objects.
    /// </summary>
    /// <param name="timeOnlyOne">The first time to be subtracted.</param>
    /// <param name="timeOnlyTwo">The second time to be subtracted.</param>
    /// <returns>The absolute difference as a <see cref="TimeSpan"/>.</returns>
    public static TimeSpan Abs(this TimeOnly timeOnlyOne, TimeOnly timeOnlyTwo)
    {
        long absoluteTicks = Math.Abs(timeOnlyOne.Ticks - timeOnlyTwo.Ticks);
        
        return TimeSpan.FromTicks(absoluteTicks);
    }

    /// <summary>
    /// Determines the absolute difference of two <see cref="DateOnly"/> objects.
    /// </summary>
    /// <param name="dateOnlyOne">The first date to be subtracted.</param>
    /// <param name="dateOnlyTwo">The second date to be subtracted.</param>
    /// <returns>The absolute difference as a <see cref="TimeSpan"/>.</returns>
    public static TimeSpan Abs(this DateOnly dateOnlyOne, DateOnly dateOnlyTwo)
    {
        int yearDifference = Math.Abs(dateOnlyOne.Year - dateOnlyTwo.Year);
        
        if(yearDifference == 0)
            return TimeSpan.FromDays(Math.Abs(dateOnlyOne.DayOfYear - dateOnlyTwo.DayOfYear));
        
        int days = 0;
        
        int startYear, endYear;
        
        if(dateOnlyOne.Year <  dateOnlyTwo.Year)
            startYear = dateOnlyOne.Year;
        else
            startYear = dateOnlyTwo.Year;
        
        if(startYear == dateOnlyOne.Year)
            endYear = dateOnlyTwo.Year;
        else
            endYear = dateOnlyOne.Year;

        for (int year = startYear; year <= endYear; year++)
        {
            if (year == endYear)
            {
                int startMonth, endMonth;

                if (dateOnlyOne.Month > dateOnlyTwo.Month)
                {
                    startMonth = dateOnlyTwo.Month;
                    endMonth = dateOnlyOne.Month;
                }
                else
                {
                    startMonth = dateOnlyOne.Month;
                    endMonth = dateOnlyTwo.Month;
                }
                
                for (int month = startMonth; month <= endMonth; month++)
                {
                    days += DateTime.DaysInMonth(year, month);
                }
            }
            else
            {
                for (int month = 1; month <= 12; month++)
                {
                    days += DateTime.DaysInMonth(year, month);
                }
            }

        }
        days += Math.Abs(dateOnlyOne.Day - dateOnlyTwo.Day);
        
        return TimeSpan.FromDays(days);
    }
}