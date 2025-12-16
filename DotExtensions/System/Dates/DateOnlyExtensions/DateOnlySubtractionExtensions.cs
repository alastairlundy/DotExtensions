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

#if NET8_0_OR_GREATER
namespace AlastairLundy.DotExtensions.Dates;

/// <summary>
/// Provides extension methods for subtracting days, months, and years from a DateOnly object.
/// </summary>
public static class DateOnlySubtractionExtensions
{
    /// <param name="dateOnly">The DateOnly object to be subtracted from.</param>
    extension(DateOnly dateOnly)
    {
        /// <summary>
        /// Subtract a specified number of days from a DateOnly object.
        /// </summary>
        /// <param name="days">The number of days to subtract from the specified DateOnly object.</param>
        /// <returns>the modified DateOnly object.</returns>
        public DateOnly SubtractDays(int days)
        {
            DateTime result = DateOnly.ToDateTime(dateOnly).Subtract(TimeSpan.FromDays(days));
            return DateOnly.FromDateTime(result);
        }

        /// <summary>
        /// Subtract a specified number of months from a DateOnly object.
        /// </summary>
        /// <param name="months">The number of months to subtract from the specified DateOnly.</param>
        /// <returns>the modified DateOnly object.</returns>
        public DateOnly SubtractMonths(int months)
        {
            DateTime result = DateOnly.ToDateTime(dateOnly).SubtractMonths(Convert.ToDouble(months));
            return DateOnly.FromDateTime(result);
        }

        /// <summary>
        /// Subtract a specified number of years from a DateOnly object.
        /// </summary>
        /// <param name="years">The number of years to subtract from the specified DateOnly object.</param>
        /// <returns>the modified DateOnly object.</returns>
        public DateOnly SubtractYears(int years)
        {
            DateTime result = DateOnly.ToDateTime(dateOnly).SubtractYears(Convert.ToDouble(years));
            return DateOnly.FromDateTime(result);
        }
    }
}

#endif
