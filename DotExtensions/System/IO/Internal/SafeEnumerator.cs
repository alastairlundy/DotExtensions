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

namespace DotExtensions.IO.Internal;

/// <summary>
/// Internal helper that wraps <see cref="DirectoryInfo"/> enumeration APIs
/// with exception-safe iteration.
/// </summary>
/// <remarks>
/// Inaccessible entries are skipped by the underlying enumerator via
/// <see cref="EnumerationOptions.IgnoreInaccessible"/>. If the enumerator itself faults with an
/// <see cref="UnauthorizedAccessException"/> or <see cref="IOException"/>, enumeration stops.
/// The enumerator is not resumed after a fault, because a faulted
/// <see cref="System.IO.Enumeration.FileSystemEnumerator{TResult}"/> does not advance its internal
/// state and would throw the same exception indefinitely.
/// </remarks>
internal static class SafeEnumerator
{
    /// <summary>
    /// Safely enumerates files, skipping inaccessible paths and stopping if enumeration faults.
    /// </summary>
    public static IEnumerable<FileInfo> EnumerateFiles(DirectoryInfo directoryInfo, string searchPattern,
        SearchOption searchOption, bool ignoreCase)
    {
        EnumerationOptions options = searchOption.ToEnumerationOptions(!ignoreCase);

        using IEnumerator<FileInfo> enumerator = directoryInfo.EnumerateFiles(searchPattern, options).GetEnumerator();

        while (true)
        {
            try
            {
                if (!enumerator.MoveNext())
                    break;
            }
            catch (UnauthorizedAccessException)
            {
                break;
            }
            catch (IOException)
            {
                break;
            }

            yield return enumerator.Current;
        }
    }

    /// <summary>
    /// Safely retrieves files as an array, skipping inaccessible paths.
    /// </summary>
    public static FileInfo[] GetFiles(DirectoryInfo directoryInfo, string searchPattern,
        SearchOption searchOption, bool ignoreCase)
        => EnumerateFiles(directoryInfo, searchPattern, searchOption, ignoreCase).ToArray();

    /// <summary>
    /// Safely enumerates directories, skipping inaccessible paths and stopping if enumeration faults.
    /// </summary>
    public static IEnumerable<DirectoryInfo> EnumerateDirectories(DirectoryInfo directoryInfo, string searchPattern,
        SearchOption searchOption, bool ignoreCase)
    {
        EnumerationOptions options = searchOption.ToEnumerationOptions(!ignoreCase);

        using IEnumerator<DirectoryInfo> enumerator =
            directoryInfo.EnumerateDirectories(searchPattern, options).GetEnumerator();

        while (true)
        {
            try
            {
                if (!enumerator.MoveNext())
                    break;
            }
            catch (UnauthorizedAccessException)
            {
                break;
            }
            catch (IOException)
            {
                break;
            }

            yield return enumerator.Current;
        }
    }

    /// <summary>
    /// Safely retrieves directories as an array, skipping inaccessible paths.
    /// </summary>
    public static DirectoryInfo[] GetDirectories(DirectoryInfo directoryInfo, string searchPattern,
        SearchOption searchOption, bool ignoreCase)
        => EnumerateDirectories(directoryInfo, searchPattern, searchOption, ignoreCase).ToArray();
}
