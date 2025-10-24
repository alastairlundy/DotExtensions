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
using System.Globalization;

namespace AlastairLundy.DotExtensions.Dates;

/// <summary>
/// Provides extension methods for working with day-related functionality for <see cref="DateTime"/> instances.
/// </summary>
public static class DayOfExtensions
{
        
    /// <summary>
    /// Returns the day of the week as a number from 1 to 7 using the current culture to determine what day is considered the first day of the week.
    /// </summary>
    /// <param name="date">The date </param>
    /// <returns>The day of the week as a 32-Bit integer.</returns>
    public static int DayOfWeekInt(this DateTime date)
    {
        DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        
        int dayOfWeek;
        
        if (DayOfWeek.Sunday == firstDayOfWeek)
        {
            dayOfWeek = (int)date.DayOfWeek + 1;
        }
        else if (DayOfWeek.Monday == firstDayOfWeek)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    dayOfWeek = 7;
                    break;
                default:
                    dayOfWeek = (int)date.DayOfWeek;
                    break;
            }
        }
        else
        {
            dayOfWeek = (int)date.DayOfWeek + 1;
        }
        
        return dayOfWeek;
    }
}