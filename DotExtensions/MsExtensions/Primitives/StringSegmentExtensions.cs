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

namespace DotExtensions.MsExtensions.Primitives;

/// <summary>
/// A class to hold StringSegment extension methods to check if a StringSegment is null, empty,
/// whitespace, or a combination thereof.
/// </summary>
public static class StringSegmentExtensions
{
    /// <summary>
    /// Provides extension methods for null and empty checks on <see cref="StringSegment"/>.
    /// </summary>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Returns true if this string segment is empty.
        /// </summary>
        /// <returns>True if the string segment is empty; otherwise, false.</returns>
        public bool IsEmpty => segment.Length == 0;
    }

    extension(StringSegment)
    {
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
        private static bool IsWhiteSpace(StringSegment other)
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
    
    #region CharConversion

    /// <param name="segment"></param>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Returns the <see cref="StringSegment"/> as a Char Array.
        /// </summary>
        /// <returns>The string segment as a char array.</returns>
        /// <exception cref="ArgumentException">Thrown if the StringSegment is null or empty.</exception>
        public char[] ToCharArray()
        { 
            ArgumentException.ThrowIfNullOrEmpty(segment);
            
            char[] charArray = new char[segment.Length];

            for (int i = 0; i < segment.Length; i++)
            {
                charArray[i] = segment[i];
            }

            return charArray;
        }

        /// <summary>
        /// Returns the <see cref="StringSegment"/> as a List of type <see cref="char"/>.
        /// </summary>
        /// <returns>A list of characters from the StringSegment if any characters are in the StringSegment.</returns>
        /// <exception cref="ArgumentException">Thrown if the StringSegment is null or empty.</exception>
#pragma warning disable MA0016
        public List<char> ToList()
#pragma warning restore MA0016
        {
            ArgumentException.ThrowIfNullOrEmpty(segment);

            List<char> list = new(capacity:segment.Length);

            for (int i = 0; i < segment.Length; i++)
            {
                list.Add(segment[i]);
            }

            return list;
        }
    }
    
    #endregion
    
    #region Reverse

    /// <param name="target">The StringSegment to reverse.</param>
    extension(StringSegment target)
    {
        /// <summary>
        /// Reverses the contents of the StringSegment.
        /// </summary>
        /// <returns>The reversed StringSegment.</returns>
        /// <exception cref="ArgumentException">Thrown if the target is null or empty.</exception>
        public StringSegment Reverse()
        {
            ArgumentException.ThrowIfNullOrEmpty(target);
            
            StringBuilder stringBuilder = new(capacity: target.Length);

            for (int i = 0; i < target.Length; i++)
            {
                if (target.Length - 1 - i >= 0)
                    stringBuilder.Append(target[target.Length - 1 - i]);
            }

            return new StringSegment(stringBuilder.ToString());
        }
    }
    
    #endregion
    
    #region CaseManipulation

    /// <param name="segment">The StringSegment to be modified.</param>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Capitalizes the char at the specified index in the specified <see cref="StringSegment"/>.
        /// </summary>
        /// <param name="index">The index of the char to be made upper case.</param>
        /// <returns>The specified <see cref="StringSegment"/> with the specified char made upper case.</returns>
        public StringSegment CapitalizeChar(int index)
        {
            ArgumentException.ThrowIfNullOrEmpty(segment);
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, segment.Length);
            
            char c = segment[index];

            if (char.IsUpper(c))
                return segment;

            return new StringSegment(
                $"{segment.Substring(0, index)}{char.ToUpper(c, CultureInfo.CurrentCulture)}{segment.Substring(index + 1)}"
            );
        }

        /// <summary>
        /// Capitalizes the chars at the specified indices in the specified <see cref="StringSegment"/>.
        /// </summary>
        /// <param name="indices">The indices of the chars to be made upper case.</param>
        /// <returns>The specified <see cref="StringSegment"/> with the specified chars made upper case.</returns>
        public StringSegment CapitalizeChars(IEnumerable<int> indices)
        {
            ArgumentException.ThrowIfNullOrEmpty(segment);
            ArgumentNullException.ThrowIfNull(indices);
            
            StringBuilder stringBuilder = new(capacity: segment.Length);

            for (int i = 0; i < segment.Length; i++)
            {
                stringBuilder.Append(segment[i]);
            }

            foreach (int index in indices)
            {
                if (index == -1)
                    throw new ArgumentException(
                        Resources.Exceptions_Indices_IndexOutOfRange.Replace("{0}", index.ToString("N", CultureInfo.CurrentCulture)
                            , StringComparison.Ordinal
                        ), nameof(indices));
                
                if(index >= segment.Length)
                    throw new ArgumentException(Resources.Exceptions_Indices_LargerIndexThanExpected, nameof(indices));

                
                stringBuilder[index] = char.ToUpper(stringBuilder[index], CultureInfo.CurrentCulture);
            }

            return new StringSegment(stringBuilder.ToString());
        }
    }
    
    /// <summary>
    /// Provides extension methods for determining the case of <see cref="StringSegment"/> instances.
    /// </summary>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Returns whether this <see cref="StringSegment"/> is upper case or not.
        /// </summary>
        public bool IsUpperCase()
        {
            for (int i = 0; i < segment.Length; i++)
            {
                if (char.IsLower(segment[i]) || !char.IsLetter(segment[i]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns whether a <see cref="StringSegment"/> is lower case or not.
        /// </summary>
        public bool IsLowerCase()
        {
            for (int i = 0; i < segment.Length; i++)
            {
                if (char.IsUpper(segment[i]) || !char.IsLetter(segment[i]))
                    return false;
            }

            return true;
        }
    }
    
    #endregion
    
    #region CollectionOperations

    /// <summary>
    /// Provides string manipulation extensions for sequences of StringSegments.
    /// </summary>
    extension(IEnumerable<StringSegment> segments)
    {
        /// <summary>
        /// Converts this sequence of StringSegments into a single string.
        /// </summary>
        /// <param name="separator">Optional separator string between segments (default: space).</param>
        /// <returns>The concatenated string representation of the input segments.</returns>
        public string ToString(StringSegment separator)
        {
            ArgumentNullException.ThrowIfNull(segments);
            ArgumentException.ThrowIfNullOrEmpty(separator);
            
            StringBuilder stringBuilder = new();

            foreach (StringSegment segment in segments)
            {
                for (int i = 0; i < segment.Length; i++)
                {
                    stringBuilder.Append(segment[i]);
                }

                for (int i2 = 0; i2 < separator.Length; i2++)
                {
                    stringBuilder.Append(separator[i2]);
                }
            }

            stringBuilder.Remove(
                stringBuilder.Length - separator.Length,
                length: separator.Length
            );

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts a sequence of StringSegments into a single string.
        /// </summary>
        /// <param name="separator">Optional separator character between segments (default: space).</param>
        /// <returns>The concatenated string representation of the input segments.</returns>
        public string ToString(char separator)
        {
            ArgumentNullException.ThrowIfNull(segments);

            StringBuilder stringBuilder = new();

            foreach (StringSegment segment in segments)
            {
                for (int i = 0; i < segment.Length; i++)
                {
                    stringBuilder.Append(segment[i]);
                }

                stringBuilder.Append(separator);
            }

            stringBuilder.Remove(stringBuilder.Length - 1, length: 1);
            return stringBuilder.ToString();
        }
    }
    
    #endregion
    
    #region Containment

    /// <summary>
    /// A class to hold extension methods for checking if a StringSegment Contains an item.
    /// </summary>
    extension(StringSegment source)
    {
        /// <summary>
        /// Returns whether the String Segment contains a character.
        /// </summary>
        /// <param name="character">The char to search for.</param>
        /// <returns>True if the character is found in the StringSegment, false otherwise.</returns>
        public bool Contains(char character)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == character)
                    return true;
            }

            return false;
        }
        
        /// <summary>
        /// Returns whether the String Segment contains another String Segment.
        /// </summary>
        /// <returns>True if the string segment contains the specified string segment; false otherwise.</returns>
        public bool Contains(StringSegment segment)
        {
            if ((source.IsEmpty && !segment.IsEmpty) || (segment.IsEmpty && !source.IsEmpty))
                return false;
            
            if (source.Length == segment.Length)
                return source.Equals(segment, StringComparison.CurrentCulture);

            if (segment.Length > source.Length || segment.IsEmpty)
                return false;

            int start = -1;
            int index = 0;

            while (index != -1)
            {
                index = source.IndexOf(segment[0], start + 1);
                start = index;

                if (index != -1)
                {
                    StringSegment comparison = source.Subsegment(index, segment.Length);

                    if (segment.Equals(comparison, StringComparison.CurrentCulture))
                        return true;
                }
            }

            return false;
        }
    }
    
    /// <summary>
    /// Provides extension methods for performing operations related to spaces within <see cref="StringSegment"/> instances.
    /// </summary>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Determines whether the specified string segment contains delimited subsegments.
        /// </summary>
        /// <param name="delimiter">The delimiter character to check for.</param>
        /// <returns>True if the string segment contains delimited subsegments; false otherwise.</returns>
        public bool ContainsDelimitedSubSegments(char delimiter)
        {
            if (segment.IsEmpty || StringSegment.IsNullOrWhiteSpace(segment))
                return false;
            
            StringTokenizer tokenizer = segment.Split([delimiter]);
            
            int count = 0;
            foreach (StringSegment unused in tokenizer)
            {
                count++;

                if (count > 1)
                    break;
            }
            
            return segment.Contains(delimiter) && count > 1;
        }
    }
    
    /// <param name="source"></param>
    extension(string source)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public StringSegment[] AsStringSegments(char separator)
        {
            ArgumentException.ThrowIfNullOrEmpty(source);
            IEnumerable<StringSegment> segments = new StringTokenizer(source, [separator]);

            return segments.ToArray();
        }
    }
    
    #endregion
    
    #region Removal

    /// <param name="segment">The segment to remove characters from.</param>
    extension(StringSegment segment)
    {
        /// <summary>
        /// Removes characters from a <see cref="StringSegment"/> starting at a specified index.
        /// </summary>
        /// <param name="startIndex">The index to start removing characters at in the <see cref="StringSegment"/>.</param>
        /// <returns>A <see cref="StringSegment"/> where all the characters occurring after the specified index
        /// have been removed.</returns>
        /// <exception cref="NullReferenceException">Thrown if the segment is null or whitespace.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the segment is empty.</exception>
        /// <exception cref="ArgumentException">Thrown if the index is less than 0 or greater than or equal to the length of the <see cref="StringSegment"/>.</exception>
        public StringSegment Remove(int startIndex)
        {
            ArgumentException.ThrowIfNullOrEmpty(segment);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(startIndex, segment.Length);
            
            return segment.Subsegment(0, startIndex == 0 ? 0 : startIndex - 1);
        }

        /// <summary>
        /// Removes characters from a <see cref="StringSegment"/> starting at a specified index for <paramref name="count"/> number of characters.
        /// </summary>
        /// <param name="startIndex">The index to start removing characters at in the <see cref="StringSegment"/>.</param>
        /// <param name="count">The number of characters to remove from the StringSegment from the start index.</param>
        /// <returns>A <see cref="StringSegment"/> where the characters occurring within the <paramref name="count"/> number of characters after the specified index are removed.</returns>
        /// <exception cref="NullReferenceException">Thrown if the segment is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Thrown if the index is less than 0 or greater than or equal to the length of the <see cref="StringSegment"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the count is than 0 or greater than the length of the <see cref="StringSegment"/>.</exception>
        public StringSegment Remove(int startIndex, int count)
        {
            ArgumentException.ThrowIfNullOrEmpty(segment);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(startIndex, segment.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, segment.Length);

            if (startIndex + count == segment.Length - 1)
                return segment.Remove(startIndex);

            int firstSegmentEnd = startIndex - 1;

            int secondSegmentStart = startIndex + count + 1;
            int secondSegmentEnd = segment.Length - secondSegmentStart;

            StringSegment firstSegment = segment.Subsegment(0, firstSegmentEnd);
            StringSegment secondSegment = segment.Subsegment(secondSegmentStart, secondSegmentEnd);

            return new StringSegment($"{firstSegment}{secondSegment}");
        }
        
        /// <summary>
        /// Removes a subsegment from the <see cref="StringSegment"/>, defined by the specified start and end indices.
        /// </summary>
        /// <param name="startIndex">The zero-based start index of the subsegment to remove.</param>
        /// <param name="endIndex">The zero-based end index of the subsegment to remove. This index is inclusive.</param>
        /// <returns>A new <see cref="StringSegment"/> with the specified range removed.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the startIndex or endIndex is negative, greater than or equal to the <see cref="StringSegment"/> length, or if the <paramref name="startIndex"/> is greater than the <paramref name="endIndex"/>.
        /// </exception>
        /// <exception cref="ArgumentException">Thrown if the calculated length of the subsegment to remove is negative.</exception>
        public StringSegment Remove(Index startIndex, Index endIndex)
            => segment.Remove(startIndex.Value, endIndex.Value);

        /// <summary>
        /// Removes a subsegment from the string, defined by the specified start and end indices.
        /// </summary>
        /// <param name="range">The <see cref="Range"/> of indices of the subsegment to remove.</param>
        /// <returns>A new <see cref="StringSegment"/> with the specified range removed.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <see cref="Range"/>'s Start <see cref="Index"/> or End <see cref="Index"/> is negative, greater than or equal to the <see cref="StringSegment"/> length,
        /// or if the Start <see cref="Index"/> is greater than or equal to the End <see cref="Index"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the calculated length of the subsegment to remove is negative.
        /// </exception>
        public StringSegment Remove(Range range)
            => segment.Remove(range.Start, range.End);
    }
    
    #endregion
}