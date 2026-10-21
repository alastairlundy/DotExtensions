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
        /// Returns twice the first DateTime minus the second DateTime; that is, dateTimeTwo reflected across dateTimeOne.
        /// </summary>
        /// <param name="dateTimeTwo">The date and time reflected across <paramref name="dateTimeOne"/>.</param>
        /// <returns>2 * dateTimeOne - dateTimeTwo; equivalently dateTimeOne shifted by the signed difference dateTimeTwo - dateTimeOne.</returns>
        /// <remarks>
        /// The BCL's instance method <c>DateTime.Subtract(DateTime)</c> returns a <see cref="TimeSpan"/> and is defined on
        /// every target this library supports, including netstandard2.0 and .NET 8+, so instance-style calls such as
        /// <c>a.Subtract(b)</c> bind to that BCL overload and yield a TimeSpan rather than the DateTime documented here.
        /// This extension is therefore only reachable through static invocation, for example
        /// <c>DateTimeSubtractExtensions.Subtract(a, b)</c>.
        /// </remarks>
        [Obsolete(DeprecationMessages.DeprecationV11)]
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
        /// Subtract a specified number of milliseconds from a DateTime.
        /// </summary>
        /// <param name="milliseconds">The number of milliseconds to subtract from the specified DateTime.</param>
        /// <returns>The modified DateTime object.</returns>
        [Obsolete(DeprecationMessages.DeprecationV11)]
        public DateTime SubtractMilliseconds(double milliseconds) =>
            dateTime.Subtract(TimeSpan.FromMilliseconds(milliseconds));

        /// <summary>
        /// Subtract a specified number of seconds from a DateTime.
        /// </summary>
        /// <param name="seconds">The number of seconds to subtract from the specified DateTime.</param>
        /// <returns>The modified DateTime object.</returns>
        [Obsolete(DeprecationMessages.DeprecationV11)]
        public DateTime SubtractSeconds(double seconds) =>
            dateTime.Subtract(TimeSpan.FromSeconds(seconds));

        /// <summary>
        /// Subtract a specified number of minutes from a DateTime.
        /// </summary>
        /// <param name="minutes">The number of minutes to subtract from the specified DateTime.</param>
        /// <returns>The modified DateTime object.</returns>
        [Obsolete(DeprecationMessages.DeprecationV11)]
        public DateTime SubtractMinutes(double minutes) =>
            dateTime.Subtract(TimeSpan.FromMinutes(minutes));

        /// <summary>
        /// Subtract a specified number of hours from a DateTime.
        /// </summary>
        /// <param name="hours">The number of hours to subtract from the specified DateTime.</param>
        /// <returns>The modified DateTime object.</returns>
        [Obsolete(DeprecationMessages.DeprecationV11)]
        public DateTime SubtractHours(double hours) =>
            dateTime.Subtract(TimeSpan.FromHours(hours));

        /// <summary>
        /// Subtract a specified number of days from a DateTime.
        /// </summary>
        /// <param name="days">The number of days to subtract from the specified DateTime.</param>
        /// <returns>The modified DateTime object.</returns>
        [Obsolete(DeprecationMessages.DeprecationV11)]
        public DateTime SubtractDays(double days) =>
            dateTime.Subtract(TimeSpan.FromDays(days));

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

#if NET8_0_OR_GREATER
        /// <summary>
        /// Subtract a specified number of microseconds from a DateTime.
        /// </summary>
        /// <param name="microseconds">The number of microseconds to subtract from the specified DateTime.</param>
        /// <returns>The modified DateTime object.</returns>
        [Obsolete(DeprecationMessages.DeprecationV11)]
        public DateTime SubtractMicroseconds(double microseconds) =>
            dateTime.Subtract(TimeSpan.FromMicroseconds(microseconds));
#endif
    }
}