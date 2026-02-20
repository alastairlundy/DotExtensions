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

namespace DotExtensions.MsExtensions.StringSegments;

/// <summary>
/// A class to hold StringSegment extension methods to check if a StringSegment is null, empty,
/// whitespace, or a combination thereof.
/// </summary>
public static class SegmentIsNullExtensions
{
    /// <summary>
    /// A static class that provides extension methods for working with StringSegment objects to determine
    /// whether they are null, empty, or consist only of whitespace characters.
    /// </summary>
    /// <paramref name="segment"></paramref>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Returns true if this string segment is empty.
        /// </summary>
        /// <returns>True if the string segment is empty; otherwise, false.</returns>
        public bool IsEmpty => segment.Length == 0;
        
        /// <summary>
        /// Checks whether the specified string segment is null or whitespace.
        /// </summary>
        /// <paramref name="other"></paramref>
        /// <returns>True if the string segment is null or empty; otherwise, false.</returns>
        public static bool IsNullOrEmpty(StringSegment? other)
        {
            if (!other.HasValue)
                return true;

            return other.Value.IsEmpty;
        }
        
        /// <summary>
        /// Checks whether the specified string segment is null or whitespace.
        /// </summary>
        /// <paramref name="other"></paramref>
        /// <returns>True if the string segment is null or empty; otherwise, false.</returns>
        public static bool IsNullOrWhiteSpace(StringSegment? other)
        {
            if (other is null)
                return true;

            return StringSegment.IsWhiteSpace((StringSegment)other);
        }

        /// <summary>
        /// Determines whether the specified string segment consists entirely of whitespace characters.
        /// </summary>
        /// <param name="other">The string segment to evaluate.</param>
        /// <returns>True if the string segment consists only of whitespace characters; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the provided <see cref="StringSegment"/> is null.</exception>
        public static bool IsWhiteSpace(StringSegment other)
        {
            if (other.IsEmpty)
                return false;
            
            for (int index = 0; index < other.Length; index++)
            {
                char c = other[index];

                if (!char.IsWhiteSpace(c))
                    return false;
            }

            return true;
        }
    }
}