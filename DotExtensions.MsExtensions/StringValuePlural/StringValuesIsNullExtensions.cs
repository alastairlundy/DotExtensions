using System.Linq;
using Microsoft.Extensions.Primitives;
// ReSharper disable ConvertClosureToMethodGroup

namespace AlastairLundy.DotExtensions.MsExtensions.StringValuePlural;

public static class StringValuesIsNullExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stringValues"></param>
    /// <returns></returns>
    public static bool IsEmpty(this StringValues stringValues) 
        => stringValues.Equals(StringValues.Empty);

    /// <summary>
    /// Indicates whether a <see cref="StringValues"/> contains any strings that are null or whitespace./>
    /// </summary>
    /// <param name="strValues">The <see cref="StringValues"/> to search.</param>
    /// <returns>True if any of the strings is WhiteSpace or null.</returns>
    public static bool IsNullOrWhiteSpace(this StringValues? strValues)
    {
        if (strValues is null)
            return true;

        bool[] vals = new bool[strValues.Value.Count];
        
        for(int index = 0; index < strValues.Value.Count; index++)
        {
            string? val = strValues.Value[index];
            
            if(val is null)
                return true;
            
            vals[index] = string.IsNullOrWhiteSpace(val);
        }

        return vals.Any(x => x == false);
    }
}