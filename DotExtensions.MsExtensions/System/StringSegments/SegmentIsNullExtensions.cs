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

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.DotExtensions.MsExtensions.System.StringSegments;

/// <summary>
/// A class to hold StringSegment extension methods to check if a StringSegment is null, empty, whitespace, or a combination thereof.
/// </summary>
public static class SegmentIsNullExtensions
{
    /// <summary>
    /// Returns true if the specified string segment is empty.
    /// </summary>
    /// <param name="segment">The string segment to check.</param>
    /// <returns>True if the string segment is empty; otherwise, false.</returns>
    public static bool IsEmpty(this StringSegment segment)
    {
        return segment.Equals(StringSegment.Empty);
    }

    /// <summary>
    /// Checks whether the specified string segment is null or empty.
    /// </summary>
    /// <param name="segment">The string segment to check.</param>
    /// <returns>True if the string segment is null or empty; otherwise, false.</returns>
    public static bool IsNullOrWhiteSpace(this StringSegment? segment)
    {
        if (segment is null)
            return true;

        if (segment.Value.Equals(" "))
            return true;

        bool[] hasWhitespace = new bool[segment.Value.Length];
        
        for (int i = 0; i < segment.Value.Length; i++)
        {
            hasWhitespace[i] = segment.Value[i] == ' ';
        }

        return hasWhitespace.All(x => x);
    }
}