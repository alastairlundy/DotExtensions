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

using System.Diagnostics;
using DotExtensions.Processes;

namespace DotExtensions.Exceptions;

public static partial class PlatformNotSupportedExceptionExtensions
{
    extension(PlatformNotSupportedException)
    {
        /// <summary>
        /// Throws a <see cref="PlatformNotSupportedException"/> if the current platform does not support
        /// operations using the <see cref="System.Diagnostics.Process"/> class. Unsupported platforms
        /// include iOS, watchOS, tvOS, browser, and WASI.
        /// </summary>
        /// <exception cref="PlatformNotSupportedException">
        /// Thrown if the platform is not supported for process-related operations.
        /// </exception>
        public static void ThrowIfProcessClassNotSupported()
        {
            if (!Process.IsSupportedPlatform())
            {
                throw new PlatformNotSupportedException(
                    "The process class does not support starting Processes on this Operating System.");
            }
        }
    }
}