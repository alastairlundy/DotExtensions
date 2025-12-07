/*
 * Copyright (c) 2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

#if NET8_0_OR_GREATER

using System.IO;

namespace AlastairLundy.DotExtensions.IO.Permissions.Unix;

/// <summary>
/// Contains extension methods for handling Unix file permission operations.
/// </summary>
public static partial class UnixPermissionsExtensions
{
    /// <summary>
    /// Provides extension methods for working with Unix file permissions represented as <see cref="UnixFileMode"/>.
    /// </summary>
    extension(UnixFileMode)
    {
        /// <summary>
        /// Parses the provided string representation of a Unix file mode into a corresponding <see cref="UnixFileMode"/> object.
        /// </summary>
        /// <param name="input">The input string representing the Unix file mode in numeric or symbolic format.</param>
        /// <returns>
        /// The parsed <see cref="UnixFileMode"/> object if the input string is in a valid format.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the input string is null, empty, or does not represent a valid Unix file mode in either numeric or symbolic notation.
        /// </exception>
        public static UnixFileMode Parse(string input)
        {
            ArgumentException.ThrowIfNullOrEmpty(input);
            
            if(IsValidNumericNotation(input))
                return ParseNumericNotation(input);
            
            if(IsValidRwxSymbolNotation(input))
                return ParseRwxSymbolNotation(input);

            throw new ArgumentException(Resources.
                Exceptions_Permissions_Unix_InvalidSymbolicNotation.Replace("{x}",  input));
        }

        /// <summary>
        /// Attempts to parse the provided string into a <see cref="UnixFileMode"/> object.
        /// </summary>
        /// <param name="input">The input string representing the Unix file mode in either numeric or symbolic notation.</param>
        /// <param name="result">
        /// Upon returning, contains the parsed <see cref="UnixFileMode"/> object if the parsing was successful; otherwise, null.
        /// </param>
        /// <returns>
        /// True if the input string was successfully parsed into a <see cref="UnixFileMode"/> object; otherwise, false.
        /// </returns>
        public static bool TryParse(string input, out UnixFileMode? result)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(input);
                
                result = Parse(input);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
    }
}
#endif
