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
/// Provides extension methods for <see cref="DateTime"/> to compute absolute differences.
/// </summary>
public static class TimeSpanDifferenceExtensions
{
    /// <param name="dateTimeOne">The first date to be subtracted.</param>
    extension(DateTime dateTimeOne)
    {
        /// <summary>
        /// Determines the absolute difference of two <see cref="DateTime"/> objects.
        /// </summary>
        /// <param name="dateTimeTwo">The second date to be subtracted.</param>
        /// <returns>The absolute difference as a <see cref="TimeSpan"/>.</returns>
        public TimeSpan TimeSpanDifference(DateTime dateTimeTwo)
        {
            long absoluteTicks = Math.Abs(dateTimeOne.Ticks - dateTimeTwo.Ticks);

            return TimeSpan.FromTicks(absoluteTicks);
        }
    }

#if NET8_0_OR_GREATER
    /// <param name="timeOnlyOne">The first time to be subtracted.</param>
    extension(TimeOnly timeOnlyOne)
    {
        /// <summary>
        /// Determines the absolute difference of two <see cref="TimeOnly"/> objects.
        /// </summary>
        /// <param name="timeOnlyTwo">The second time to be subtracted.</param>
        /// <returns>The absolute difference as a <see cref="TimeSpan"/>.</returns>
        public TimeSpan TimeSpanDifference(TimeOnly timeOnlyTwo)
        {
            long absoluteTicks = Math.Abs(timeOnlyOne.Ticks - timeOnlyTwo.Ticks);

            return TimeSpan.FromTicks(absoluteTicks);
        }
    }

    /// <param name="dateOnlyOne">The first date to be subtracted.</param>
    extension(DateOnly dateOnlyOne)
    {
        /// <summary>
        /// Determines the absolute difference of two <see cref="DateOnly"/> objects.
        /// </summary>
        /// <param name="dateOnlyTwo">The second date to be subtracted.</param>
        /// <returns>The absolute difference as a <see cref="TimeSpan"/>.</returns>
        public TimeSpan TimeSpanDifference(DateOnly dateOnlyTwo) =>
            TimeSpanDifference(DateOnly.ToDateTime(dateOnlyOne).Date, DateOnly.ToDateTime(dateOnlyTwo).Date);
    }
#endif
}
