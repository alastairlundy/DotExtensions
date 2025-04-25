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


using System;
using System.Diagnostics;

using AlastairLundy.DotExtensions.Localizations;

namespace AlastairLundy.DotExtensions.Processes;

public static class ProcessHasStartedOrExitedExtensions
{
    /// <summary>
    /// Determines if a process has started.
    /// </summary>
    /// <param name="process">The process to be checked.</param>
    /// <returns>True if it has started; false otherwise.</returns>
    public static bool HasStarted(this Process process)
    {
        try
        {
            return process.StartTime.ToUniversalTime() <= DateTime.UtcNow;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Determines if a process has exited.
    /// </summary>
    /// <remarks>This extension method exists because accessing the Exited property on a Process can cause an exception to be thrown.</remarks>
    /// <param name="process">The process to be checked.</param>
    /// <returns>True if it has exited; false if it is still running.</returns>
    /// <exception cref="NotSupportedException">Thrown if checking whether a Process has exited on a remote device.</exception>
    public static bool HasExited(this Process process)
    {
        if (process.MachineName.Equals(Environment.MachineName))
        {
            try
            {
                int exitCode = process.ExitCode;
                return process.ExitTime.ToUniversalTime() <= DateTime.UtcNow;
            }
            catch
            {
                return false;
            }
        }
        else
        {
            throw new NotSupportedException(Resources.Exceptions_Processes_NotSupportedOnRemoteProcesss);
        }
    }
}