using System;
using System.Globalization;
using System.Linq;

namespace AlastairLundy.DotExtensions.Numbers;

public static class CountDigitsExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public static int NumberOfDigits(this int i)
    {
        int tempI = 0;

        if (i < 0)
        {
            tempI = i * -1;
        }
        
        // String Implementation
        return tempI.ToString()
            .Count(char.IsDigit);
    }

    /*
    public static int NumberOfDigits(this long i)
    {
        
    }
    */

    public static int NumberOfDigits(this double d)
    {
        double temp = double.Parse(string.Join("", d.ToString(CultureInfo.InvariantCulture)
            .Where(x => x != '.' && x != ',' && x != '-')));
        
        // String Implementation
        return temp.ToString(CultureInfo.InvariantCulture)
            .Count(char.IsDigit);
    }
    
    
}