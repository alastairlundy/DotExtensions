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
using System.Runtime.InteropServices;

#if NETSTANDARD2_0 || NETSTANDARD2_1
using OperatingSystem = Polyfills.OperatingSystemPolyfill;
#endif

using AlastairLundy.DotExtensions.Processes;

using AlastairLundy.DotExtensions.Resyslib.Localizations;

using AlastairLundy.ProcessInvoke.Primitives.Policies;

namespace AlastairLundy.DotExtensions.Resyslib.Processes
{
    public static class ProcessSetResourcePolicyExtensions
    {

        /// <summary>
        /// Applies a ProcessResourcePolicy to a Process.
        /// </summary>
        /// <param name="process">The process to apply the policy to.</param>
        /// <param name="resourcePolicy">The process resource policy to be applied.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void SetResourcePolicy(this Process process, ProcessResourcePolicy? resourcePolicy)
        {
            if (process.HasStarted() && resourcePolicy != null)
            {
#if NET5_0_OR_GREATER
            if (OperatingSystem.IsWindows() || OperatingSystem.IsLinux())
#else
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                    RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
#endif
                {
                    if (resourcePolicy.ProcessorAffinity is not null)
                    {
                        process.ProcessorAffinity = (IntPtr)resourcePolicy.ProcessorAffinity;
                    }
                }

                if (OperatingSystem.IsMacOS() ||
                    OperatingSystem.IsMacCatalyst() ||
                    OperatingSystem.IsFreeBSD() ||
                    OperatingSystem.IsWindows())
                {
                    if (resourcePolicy.MinWorkingSet != null)
                    {
                        process.MinWorkingSet = (nint)resourcePolicy.MinWorkingSet;
                    }

                    if (resourcePolicy.MaxWorkingSet != null)
                    {
                        process.MaxWorkingSet = (nint)resourcePolicy.MaxWorkingSet;
                    }
                }
        
                process.PriorityClass = resourcePolicy.PriorityClass;
                process.PriorityBoostEnabled = resourcePolicy.EnablePriorityBoost;
            }
            else
            {
                throw new InvalidOperationException(Resources.Exceptions_ResourcePolicy_CannotSetToNotStartedProcess);
            }
        }
    }
}