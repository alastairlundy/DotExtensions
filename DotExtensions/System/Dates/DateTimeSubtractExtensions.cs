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
/// Provides extension methods for subtracting various time intervals or other DateTime values
/// from a DateTime instance.
/// </summary>
public static class DateTimeSubtractExtensions
{
    /// <param name="dateTimeOne">The starting DateTime.</param>
    extension(DateTime dateTimeOne)
    {
        /// <summary>
        /// Subtracts two dates from each other and returns the resulting date and time.
        /// </summary>
        /// <param name="dateTimeTwo">The date and time to subtract from.</param>
        /// <returns>The first DateTime subtracted from the 2nd DateTime.</returns>
        public DateTime Subtract(DateTime dateTimeTwo)
        {
            TimeSpan timeSpan = TimeSpan.FromTicks(dateTimeTwo.Ticks - dateTimeOne.Ticks);
            return dateTimeOne.Subtract(timeSpan);
        }
    }

    /// <param name="dateTime">The DateTime to be subtracted from.</param>
    extension(DateTime dateTime)
    {
        /// <summary>
        /// Subtract a specified number of months from a DateTime.
        /// </summary>
        /// <param name="months">The number of months to subtract from the specified DateTime.</param>
        /// <returns>The modified DateTime object.</returns>
        public DateTime SubtractMonths(double months) =>
            dateTime.AddMonths(-(int)months);

        /// <summary>
        /// Subtract a specified number of years from a DateTime.
        /// </summary>
        /// <param name="years">The number of years to subtract from the specified DateTime.</param>
        /// <returns>The modified DateTime object.</returns>
        public DateTime SubtractYears(double years) =>
            dateTime.AddYears(-(int)years);
    }
}