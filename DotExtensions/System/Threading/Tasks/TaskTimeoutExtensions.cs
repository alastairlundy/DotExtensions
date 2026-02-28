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

using System.Threading;
using System.Threading.Tasks;

namespace DotExtensions.Threading.Tasks;

/// <summary>
/// Provides extensions for waiting for a task to wait for completion or be cancelled if the timeout is reached.
/// </summary>
public static class TaskTimeoutExtensions
{
    extension(Task task)
    {
        /// <summary>
        /// Asynchronously waits for the Task to end or for the Timeout, cancels the task if it has not ended by the timeout <see cref="TimeSpan"/>,
        /// and allows for suppressing the <exception cref="OperationCanceledException"/> if cancelled.
        /// </summary>
        /// <param name="timeout">The <see cref="TimeSpan"/> to wait for timeout.</param>
        /// <param name="suppressTaskCancellationException">Whether to suppress the exception thrown upon cancellation or not.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use to cancel the task.</param>
        /// <exception cref="OperationCanceledException">Thrown if the task cancellation exception is not suppressed AND if the task is cancelled.</exception>
        public async Task WaitAsync(TimeSpan timeout, bool suppressTaskCancellationException, CancellationToken cancellationToken)
        {
            CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(timeout);
            
            try
            {
                await task.WaitAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                if (!suppressTaskCancellationException)
                    throw;
            }
        }
    }
    
    extension<TResult>(Task<TResult> task)
    {
        /// <summary>
        /// Asynchronously waits for the Task to end or for the Timeout, cancels the task if it has not ended by the timeout <see cref="TimeSpan"/>,
        /// and allows for suppressing the <exception cref="OperationCanceledException"/> if cancelled.
        /// </summary>
        /// <param name="timeout">The <see cref="TimeSpan"/> to wait for timeout.</param>
        /// <param name="suppressTaskCancellationException">Whether to suppress the exception thrown upon cancellation or not.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use to cancel the task.</param>
        /// <exception cref="OperationCanceledException">Thrown if the task cancellation exception is not suppressed AND if the task is cancelled.</exception>
        public async Task<TResult> WaitAsync(TimeSpan timeout, bool suppressTaskCancellationException, CancellationToken cancellationToken)
        {
            CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(timeout);
            
            try
            {
                return await task.WaitAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                if (!suppressTaskCancellationException)
                    throw;

                return task.Result;
            }
        }
    }
}