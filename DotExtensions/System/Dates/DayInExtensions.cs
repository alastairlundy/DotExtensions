using System;

namespace AlastairLundy.DotExtensions.Dates;

public static class DayInExtensions
{
    internal static int DaysInYear(int year)
    {
        int days = 0;
            
        for (int month = 1; month <= 12; month++)
        {
            days += DateTime.DaysInMonth(year, month);
        }
            
        return days;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    extension(DateTime)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int DaysInYear(DateTime dateTime)
         => DaysInYear(dateTime.Year);
    }
}