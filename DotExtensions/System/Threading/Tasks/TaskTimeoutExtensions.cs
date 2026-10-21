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
/// Provides extensions for waiting for a task to complete or for a timeout to elapse first.
/// </summary>
public static class TaskTimeoutExtensions
{
    extension(Task task)
    {
        /// <summary>
        /// Asynchronously waits for the Task to end or for the timeout <see cref="TimeSpan"/> to elapse first.
        /// Reaching the timeout does NOT cancel the task itself; it only stops waiting for it (the task keeps running in the background).
        /// Allows for suppressing the <see cref="OperationCanceledException"/> thrown when the wait is cancelled or times out.
        /// </summary>
        /// <param name="timeout">The <see cref="TimeSpan"/> to wait for timeout.</param>
        /// <param name="suppressTaskCancellationException">Whether to suppress the exception thrown upon cancellation or not.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the wait for the task (it does not cancel the task itself).</param>
        /// <remarks>The linked cancellation token cancels only the wait, never the underlying task.</remarks>
        /// <exception cref="OperationCanceledException">Thrown if the wait is cancelled or times out AND exception suppression is not enabled.</exception>
        [Obsolete(DeprecationMessages.DeprecationV11)]
        public async Task WaitAsync(TimeSpan timeout, bool suppressTaskCancellationException, CancellationToken cancellationToken)
        {
            using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(timeout);

            try
            {
                await task.WaitAsync(cts.Token).ConfigureAwait(true);
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
        /// Asynchronously waits for the Task to end or for the timeout <see cref="TimeSpan"/> to elapse first.
        /// Reaching the timeout does NOT cancel the task itself; it only stops waiting for it (the task keeps running in the background).
        /// Allows for suppressing the <see cref="OperationCanceledException"/> thrown when the wait is cancelled or times out.
        /// When suppressed, returns <c>default(TResult)</c> because the task's result is not available (the task may still be running).
        /// </summary>
        /// <param name="timeout">The <see cref="TimeSpan"/> to wait for timeout.</param>
        /// <param name="suppressTaskCancellationException">Whether to suppress the exception thrown upon cancellation or not.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the wait for the task (it does not cancel the task itself).</param>
        /// <remarks>The linked cancellation token cancels only the wait, never the underlying task. The task's result is never read synchronously.</remarks>
        /// <exception cref="OperationCanceledException">Thrown if the wait is cancelled or times out AND exception suppression is not enabled.</exception>
        [Obsolete(DeprecationMessages.DeprecationV11)]
        public async Task<TResult> WaitAsync(TimeSpan timeout, bool suppressTaskCancellationException, CancellationToken cancellationToken)
        {
            using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(timeout);

            try
            {
                return await task.WaitAsync(cts.Token).ConfigureAwait(true);
            }
            catch (OperationCanceledException)
            {
                if (!suppressTaskCancellationException)
                    throw;

                return default(TResult)!;
            }
        }
    }
}