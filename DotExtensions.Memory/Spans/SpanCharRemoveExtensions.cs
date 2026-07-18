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

namespace DotExtensions.Memory.Spans;

/// <summary>
/// Provides extension methods for removing substrings from <see cref="Span{T}"/> of characters
/// in place, mirroring the documented contract of <c>DotExtensions.Strings.StringRemoveExtensions</c>.
/// </summary>
public static class SpanCharRemoveExtensions
{
    /// <param name="source">The <see cref="Span{T}"/> of characters to update in place.</param>
    extension(ref Span<char> source)
    {
        /// <summary>
        /// Removes all occurrences of <paramref name="value"/> from <paramref name="source"/> in place,
        /// using the specified <see cref="StringComparison"/> rules, and returns the new length
        /// of the span after removal.
        /// </summary>
        /// <param name="value">The substring to remove. Must not be empty.</param>
        /// <param name="stringComparison">The rules to use when comparing substrings.</param>
        /// <returns>
        /// The new length of <paramref name="source"/> after all occurrences of
        /// <paramref name="value"/> have been removed. Callers typically slice the span with
        /// <c>source = source[..newLength];</c> to trim trailing characters.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="value"/> is empty.
        /// </exception>
        public int RemoveAll(
            ReadOnlySpan<char> value,
            StringComparison stringComparison = StringComparison.CurrentCulture)
        {
            if (value.IsEmpty)
                throw new ArgumentException(
                    "The substring to remove must not be empty.",
                    nameof(value));

            int sourceLength = source.Length;
            int valueLength = value.Length;

            if (valueLength > sourceLength)
                return sourceLength;

            if (stringComparison == StringComparison.Ordinal)
                return RemoveAllOrdinal(ref source, value, sourceLength, valueLength);

            return RemoveAllWithComparison(ref source, value, stringComparison, sourceLength, valueLength);
        }
    }

    private static int RemoveAllOrdinal(
        ref Span<char> source,
        ReadOnlySpan<char> value,
        int sourceLength,
        int valueLength)
    {
        int searchStart = 0;
        int writePos = 0;

        while (searchStart <= sourceLength - valueLength)
        {
            int relative = source.Slice(searchStart).IndexOf(value);

            if (relative == -1)
            {
                AppendTail(ref source, sourceLength, searchStart, ref writePos);
                return writePos;
            }

            int index = searchStart + relative;

            if (index > searchStart)
            {
                source.Slice(searchStart, index - searchStart)
                    .CopyTo(source.Slice(writePos));
                writePos += index - searchStart;
            }

            searchStart = index + valueLength;
        }

        AppendTail(ref source, sourceLength, searchStart, ref writePos);
        return writePos;
    }

    private static int RemoveAllWithComparison(
        ref Span<char> source,
        ReadOnlySpan<char> value,
        StringComparison stringComparison,
        int sourceLength,
        int valueLength)
    {
        // For culture-aware comparisons, hoist a single string copy of the source
        // and value outside the loop so each iteration does not allocate.
        string sourceString = source.ToString();
        string valueString = value.ToString();

        int searchStart = 0;
        int writePos = 0;

        while (searchStart <= sourceLength - valueLength)
        {
            int index = sourceString.IndexOf(valueString, searchStart, stringComparison);

            if (index == -1)
            {
                AppendTail(ref source, sourceLength, searchStart, ref writePos);
                return writePos;
            }

            if (index > searchStart)
            {
                source.Slice(searchStart, index - searchStart)
                    .CopyTo(source.Slice(writePos));
                writePos += index - searchStart;
            }

            searchStart = index + valueLength;
        }

        AppendTail(ref source, sourceLength, searchStart, ref writePos);
        return writePos;
    }

    private static void AppendTail(
        ref Span<char> source,
        int sourceLength,
        int searchStart,
        ref int writePos)
    {
        if (searchStart >= sourceLength)
            return;

        int remaining = sourceLength - searchStart;
        source.Slice(searchStart, remaining).CopyTo(source.Slice(writePos));
        writePos += remaining;
    }
}
