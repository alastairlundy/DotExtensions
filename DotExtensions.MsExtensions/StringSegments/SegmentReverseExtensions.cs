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

using System.Text;
using AlastairLundy.DotExtensions.MsExtensions.Internal.Localizations;
using Microsoft.Extensions.Primitives;

namespace AlastairLundy.DotExtensions.MsExtensions.StringSegments;

public static class SegmentReverseExtensions
{
    /// <summary>
    /// Reverses the contents of the StringSegment.
    /// </summary>
    /// <param name="target">The StringSegment to reverse.</param>
    /// <returns>The reversed StringSegment.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the target StringSegment is Empty.</exception>
    public static StringSegment Reverse(this StringSegment target)
    {
        if (target.IsEmpty())
            throw new InvalidOperationException(Resources.Exceptions_Arguments_Segment_Empty);
    
        StringBuilder stringBuilder = new();

        for (int i = 0; i < target.Length; i++)
        {
            if(target.Length - 1 - i >= 0)
                stringBuilder.Append(target[target.Length - 1 - i]);
        }
        
        return new StringSegment(stringBuilder.ToString());
    }
}