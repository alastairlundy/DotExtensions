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

using System;

namespace AlastairLundy.DotExtensions.Dates;

/// <summary>
/// A static class to Add to DateTime values.
/// </summary>
public static class DateTimeAddExtensions
{
    /// <summary>
    /// Adds two dates together and returns the resulting DateTime.
    /// </summary>
    /// <param name="dateTimeOne">The starting DateTime.</param>
    /// <param name="dateTimeTwo">The date and time to add.</param>
    /// <returns>The sum of the two input dates and times.</returns>
    public static DateTime Add(this DateTime dateTimeOne, DateTime dateTimeTwo)
    {
        TimeSpan timeSpan = dateTimeTwo.Subtract(dateTimeOne);
        return dateTimeOne.Add(timeSpan);
    }
    
    /// <summary>
    /// Adds a specified number of days to a given date and time.
    /// </summary>
    /// <param name="dateTime">The initial date and time to add days to.</param>
    /// <param name="days">The number of days to add.</param>
    /// <returns>The resulting date and time after adding the specified number of days.</returns>
    public static DateTime AddDays(this DateTime dateTime, int days)
    {
        return dateTime.AddDays(Convert.ToDouble(days));
    }
}