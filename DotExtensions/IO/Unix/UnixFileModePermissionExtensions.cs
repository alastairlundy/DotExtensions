/*
        MIT License
       
       Copyright (c) 2024-2025 Alastair Lundy
       
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

#if NET8_0_OR_GREATER
using System.IO;
#endif

namespace AlastairLundy.DotExtensions.IO.Unix;

/// <summary>
/// 
/// </summary>
public static class UnixFileModePermissionExtensions
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Determines whether the specified Unix file mode has execute permission.
    /// </summary>
    /// <param name="mode">The Unix file mode to check.</param>
    /// <returns>True if the mode includes execute permission, false otherwise.</returns>
    // TODO: Rename to HasExecutePermission in v8
    public static bool IsExecutePermission(this UnixFileMode mode)
    {
        return mode switch
        {
            UnixFileMode.OtherExecute or UnixFileMode.UserExecute or UnixFileMode.GroupExecute => true,
            _ => false
        };
    }
#endif
}