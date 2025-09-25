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

using System.Linq;
using System.Text;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.DotExtensions.MsExtensions.StringSegments;

public static class SegmentTitleCaseExtensions
{
    /// <summary>
    /// Converts a <see cref="StringSegment"/> to Title Case.
    /// </summary>
    /// <param name="segment">The <see cref="StringSegment"/> to be converted.</param>
    /// <returns>The title case version of the input <see cref="StringSegment"/>.</returns>
    public static StringSegment ToTitleCase(this StringSegment segment)
    {
        StringTokenizer wordsTokenizer = segment.Split([' ']);
        
        StringBuilder stringBuilder = new StringBuilder();
        
        foreach (StringSegment word in wordsTokenizer)
        {
#if NET8_0_OR_GREATER
            stringBuilder.Append(word.IsTitleCase() ? word.AsSpan() :
                word.CapitalizeChar(1).AsSpan());
#else
            stringBuilder.Append(word.IsTitleCase() ? word.ToCharArray() :
                word.CapitalizeChar(1).ToCharArray());
#endif
        }
        
        return stringBuilder.ToString();
    }
        
    /// <summary>
    /// Returns whether the specified <see cref="StringSegment"/> to be checked is in Title Case or not.
    /// </summary>
    /// <param name="segment">The <see cref="StringSegment"/> to be checked.</param>
    /// <returns>True if the specified <see cref="StringSegment"/> is in Title Case, false otherwise.</returns>
    public static bool IsTitleCase(this StringSegment segment)
    {
        StringTokenizer wordsTokenizer = segment.Split([' ']);
            
        return wordsTokenizer.All(x => char.IsUpper(x[0]) && 
                                       x.Subsegment(1, x.Length - 1)
                                           .IsLowerCase());
    }
}