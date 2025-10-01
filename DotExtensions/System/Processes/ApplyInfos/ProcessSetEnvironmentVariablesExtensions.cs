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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AlastairLundy.DotExtensions.Processes;

public static class ProcessSetEnvironmentVariablesExtensions
{
    /// <summary>
    /// Sets environment variables for a specified ProcessStartInfo object.
    /// </summary>
    /// <param name="processStartInfo">The ProcessStartInfo object to set environment variables for.</param>
    /// <param name="environmentVariables">A dictionary of environment variable names and their corresponding values.</param>
    [Obsolete(DeprecationMessages.DeprecationV9)]
    public static void SetEnvironmentVariables(this ProcessStartInfo processStartInfo,
        IDictionary<string, string> environmentVariables)
    {
        if (environmentVariables.Any() == false)
            return;
        
        foreach (KeyValuePair<string, string> variable in environmentVariables)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (variable.Value is not null)
            {
                processStartInfo.Environment[variable.Key] = variable.Value;
            }
        }
    }
}