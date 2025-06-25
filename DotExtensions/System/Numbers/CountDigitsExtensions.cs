/*
        MIT License

       Copyright (c) 2025 Alastair Lundy

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