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

using System.Globalization;

namespace DotExtensions.Dates;

/// <summary>
/// Provides extension methods for working with day-related functionality for <see cref="DateTime"/> instances.
/// </summary>
public static class DayOfExtensions
{
    private static int CalculateDayOfWeekInt(DayOfWeek dayOfWeek)
    {
        DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        
        int output;
        
        switch (firstDayOfWeek)
        {
            case DayOfWeek.Sunday:
                output = (int)dayOfWeek + 1;
                break;
            case DayOfWeek.Monday:
                switch (dayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        output = 7;
                        break;
                    default:
                        output = (int)dayOfWeek;
                        break;
                }
                break;
            case DayOfWeek.Tuesday:
            case DayOfWeek.Wednesday:
            case DayOfWeek.Thursday:
            case DayOfWeek.Friday:
            case DayOfWeek.Saturday:
            default:
                output = (int)dayOfWeek + 1;
                break;
        }
            
        return output;
    }
    
#if NET8_0_OR_GREATER
    extension(DateOnly date)
    {
        /// <summary>
        /// Returns the day of the week as a number from 1 to 7 using the current culture to determine what day is considered the first day of the week.
        /// </summary>
        /// <returns>The day of the week as a 32-Bit integer.</returns>
        public int CalculateDayOfWeekAsInteger()
            => CalculateDayOfWeekInt(date.DayOfWeek);
    }
#endif
    
    /// <summary>
    ///
    /// </summary>
    /// <param name="date">The date </param>
    extension(DateTime date)
    {
        /// <summary>
        /// Returns the day of the week as a number from 1 to 7 using the current culture to determine what day is considered the first day of the week.
        /// </summary>
        /// <returns>The day of the week as a 32-Bit integer.</returns>
        public int CalculateDayOfWeekAsInteger()
            => CalculateDayOfWeekInt(date.DayOfWeek);
    }
}