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
// ReSharper disable AsyncVoidLambda
// ReSharper disable RedundantJumpStatement

namespace AlastairLundy.DotExtensions.Processes
{
    public static class ProcessWaitForExitAsyncExtensions
    {
        
#if NETSTANDARD2_0 || NETSTANDARD2_1
        /// <summary>
        /// Waits for the specified process to exit, but only waits while the specified token is held.
        /// </summary>
        /// <param name="process">The process to wait for.</param>
        /// <param name="cancellationToken">A cancellation token that determines whether the operation should continue to run or be
        /// cancelled.</param>
        private static async Task WaitForExitAsync(this Process process, CancellationToken cancellationToken = default)
        {
            Task task = new Task(process.WaitForExit);
            
            task.Start();
            
            await task;
        }
#endif
    
        /// <summary>
        /// Waits for the specified process to exit within the given time span.
        /// </summary>
        /// <param name="process">The process to wait for.</param>
        /// <param name="timeout">The maximum amount of time that is spent waiting for the process to exit. If the timeout
        /// expires before the process exits.</param>
        /// <param name="endProcessAtTimeout">Whether the process should be ended when it times out. Default is false (wait).</param>
        /// <param name="cancellationToken">A cancellation token that determines whether the operation should continue to run or be cancelled.</param>
        public static async Task WaitForExitAsync(this Process process,
            TimeSpan timeout,
            bool endProcessAtTimeout = false,
            CancellationToken cancellationToken = default)
        {
            Task processTask = new Task(async () =>
            {
                await process.WaitForExitAsync(cancellationToken);

                return;
            });
            
            processTask.Start();
            
            Task timeoutTask = new Task(() =>
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                stopWatch.Start();

                while (stopWatch.IsRunning && process.IsRunning())
                {
                    if (stopWatch.Elapsed > timeout)
                    {
                        stopWatch.Stop();

                        if (endProcessAtTimeout)
                        {
                            process.Close();
                        }
                        else
                        {
                            cancellationToken = new CancellationToken(true);
                        }
                        
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
        
            timeoutTask.Start();
            
            await timeoutTask;
        }

        /// <summary>
        /// Waits for the specified process to exit.
        /// </summary>
        /// <param name="process">The process to wait for.</param>
        /// <param name="millisecondTimeout">The number of milliseconds to wait before timing out. If 0, there is no timeout.</param>
        /// <param name="endProcessAtTimeout">Whether the process should be ended when it times out. Default is false (wait).</param>
        /// <param name="cancellationToken">A cancellation token that determines whether the operation
        /// should continue to run or be cancelled.</param>
        public static async Task WaitForExitAsync(this Process process, int millisecondTimeout,
            bool endProcessAtTimeout = false,
            CancellationToken cancellationToken = default)
        { 
            await WaitForExitAsync(process, 
                TimeSpan.FromMilliseconds(Convert.ToDouble(millisecondTimeout)),
                endProcessAtTimeout,
                cancellationToken);
        }
    }
}