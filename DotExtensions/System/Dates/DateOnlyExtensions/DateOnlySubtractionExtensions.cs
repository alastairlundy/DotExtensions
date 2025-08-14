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
public static class DateOnlySubtractionExtensions
{
    /// <summary>
    /// Subtract a specified number of days from a DateOnly object.
    /// </summary>
    /// <param name="dateOnly">The DateOnly object to be subtracted from.</param>
    /// <param name="days">The number of days to subtract from the specified DateOnly object.</param>
    /// <returns>the modified DateOnly object.</returns>
    public static DateOnly SubtractDays(this DateOnly dateOnly, int days)
    {
        TimeSpan timeSpan = TimeSpan.FromDays(days);
        DateTime result = dateOnly.ToDateTime().Subtract(timeSpan);
        return DateOnly.FromDateTime(result);
    }

    /// <summary>
    /// Subtract a specified number of months from a DateOnly object.
    /// </summary>
    /// <param name="dateOnly">The DateOnly object to be subtracted from.</param>
    /// <param name="months">The number of months to subtract from the specified DateOnly.</param>
    /// <returns>the modified DateOnly object.</returns>
    public static DateOnly SubtractMonths(this DateOnly dateOnly, int months)
    {
        DateTime result = dateOnly.ToDateTime().SubtractMonths(Convert.ToDouble(months));
        return DateOnly.FromDateTime(result);
    }

    /// <summary>
    /// Subtract a specified number of years from a DateOnly object.
    /// </summary>
    /// <param name="dateOnly">The DateOnly object to be subtracted from.</param>
    /// <param name="years">The number of years to subtract from the specified DateOnly object.</param>
    /// <returns>the modified DateOnly object.</returns>
    public static DateOnly SubtractYears(this DateOnly dateOnly, int years)
    {
        DateTime result = dateOnly.ToDateTime().SubtractYears(Convert.ToDouble(years));
        return DateOnly.FromDateTime(result);
    } 
}