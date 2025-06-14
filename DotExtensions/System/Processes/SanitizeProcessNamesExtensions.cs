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


using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AlastairLundy.DotExtensions.Processes;

public static class SanitizeProcessNamesExtensions
{
    /// <summary>
    /// Sanitizes a Process Name.
    /// </summary>
    /// <param name="process">The process to sanitize the name of.</param>
    /// <param name="excludeFileExtension">Whether to remove the file extension from the Process
    /// when sanitizing the process name.</param>
    /// <returns>The sanitized process names.</returns>
    public static string SanitizeProcessName(this Process process, bool excludeFileExtension = true)
    {
#if NET8_0_OR_GREATER
            return SanitizeProcessNames([process], excludeFileExtension).First();
#else
        return SanitizeProcessNames(new Process[]{process}, excludeFileExtension).First();
#endif
    }

    /// <summary>
    /// Sanitizes Process Names from a list of Process objects.
    /// </summary>
    /// <param name="processNames">The list of Processes to sanitize the names of.</param>
    /// <param name="excludeFileExtensions">Whether to remove the file extension from the Process
    /// when sanitizing the process name.</param>
    /// <returns>The sanitized process names.</returns>
    public static IEnumerable<string> SanitizeProcessNames(this IEnumerable<Process> processNames, bool excludeFileExtensions = true)
    {
        if (excludeFileExtensions)
        {
            return processNames.Select(x => x.ProcessName.Replace(Path.GetExtension(x.ProcessName), string.Empty))
                .Select(x => x.Replace("System.Diagnostics.Process (", string.Empty)
                    .Replace(")", string.Empty));
        }
        else
        {
            return processNames.Select(x => x.ProcessName.Replace("System.Diagnostics.Process (", string.Empty)
                .Replace(")", string.Empty));
        }
    }
}