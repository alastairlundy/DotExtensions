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

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AlastairLundy.Extensions.System.Processes
{
    public static class IsProcessRunningExtensions
    {
        /// <summary>
        /// Check to see if a specified process is running or not.
        /// </summary>
        /// <param name="process">The process to be checked.</param>
        /// <returns>True if the specified process is running; returns false otherwise.</returns>
        [Obsolete("This method is deprecated and will be removed in a future version.")]
        public static bool IsRunning(this Process process)
        {
            if (process.HasStarted())
            {
                try
                {
                    return process.HasExited == false;
                }
                catch
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// Check to see if a specified process is running or not.
        /// </summary>
        /// <param name="processName">The name of the process to be checked.</param>
        /// <param name="sanitizeProcessName"></param>
        /// <returns>true if the specified process is running; returns false otherwise.</returns>
        public static bool IsProcessRunning(this string processName, bool sanitizeProcessName = true)
        {
            string[] processes;

            string tempProcessName = processName;
            
            if (sanitizeProcessName)
            {
                tempProcessName = Path.GetFileNameWithoutExtension(processName);
                processes = Process.GetProcesses().SanitizeProcessNames(excludeFileExtensions: true).ToArray();
            }
            else
            {
                processes = Process.GetProcesses().Select(x => x.ProcessName).ToArray();
            }
            
            processes = processes.Where(x => x.Contains(tempProcessName)).ToArray();
            
            return processes.Any(x => x.ToLower().Equals(tempProcessName.ToLower()));
        }
    }
}