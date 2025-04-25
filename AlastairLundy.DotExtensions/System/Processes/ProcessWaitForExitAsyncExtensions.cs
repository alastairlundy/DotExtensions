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

using System.Threading;
using System.Threading.Tasks;

namespace AlastairLundy.DotExtensions.Processes
{
    public static class ProcessWaitForExitAsyncExtensions
    {
#if NETSTANDARD2_0 || NETSTANDARD2_1
    /// <summary>
    /// 
    /// </summary>
    /// <param name="process"></param>
    /// <param name="cancellationToken"></param>
    private static async Task WaitForExitAsync(this Process process, CancellationToken cancellationToken = default)
    {
        Task task = new Task(process.WaitForExit);
        
        task.Start();
        
        await task;
    }
#endif
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        public static async Task WaitForExitAsync(this Process process, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            Task task = new Task(() =>
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                stopWatch.Start();

                while (stopWatch.IsRunning)
                {
                    if (stopWatch.Elapsed > timeout)
                    {
                        return;
                    }

                    if (timeout.TotalMilliseconds >= 100)
                    {
                        Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                    }
                    else
                    {
                        Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                    }
                }
            });
        
            task.Start();
        
            Task.WaitAny([task, process.WaitForExitAsync(cancellationToken)], cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        /// <param name="millisecondTimeout"></param>
        /// <param name="cancellationToken"></param>
        public static async Task WaitForExitAsync(this Process process, int millisecondTimeout,
            CancellationToken cancellationToken = default)
        { 
            await WaitForExitAsync(process, TimeSpan.FromMilliseconds(Convert.ToDouble(millisecondTimeout)),
                cancellationToken);
        }
    }
}