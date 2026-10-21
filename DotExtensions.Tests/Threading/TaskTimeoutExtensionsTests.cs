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

using System;
using System.Threading;
using DotExtensions.Threading.Tasks;
#pragma warning disable CS0618 // Type or member is obsolete

namespace DotExtensions.Tests.Threading;

/// <summary>
/// Invariant tests for the deprecated <see cref="TaskTimeoutExtensions.WaitAsync(TimeSpan, bool, CancellationToken)"/>
/// overloads. Uses short timeouts and never-completing <see cref="TaskCompletionSource{TResult}"/> instances so that
/// every test completes quickly (a regression to synchronous blocking on <c>Task.Result</c> would hang the test).
/// </summary>
public class TaskTimeoutExtensionsTests
{
    [Test]
    public async Task WaitAsync_Generic_NeverCompletingTask_WithSuppression_ReturnsDefault()
    {
        TaskCompletionSource<int> tcs = new(TaskCreationOptions.RunContinuationsAsynchronously);

        int result = await tcs.Task.WaitAsync(TimeSpan.FromMilliseconds(50), true, CancellationToken.None);

        await Assert.That(result).IsEqualTo(default(int));
    }

    [Test]
    public async Task WaitAsync_Generic_NeverCompletingTask_WithoutSuppression_Throws()
    {
        TaskCompletionSource<int> tcs = new(TaskCreationOptions.RunContinuationsAsynchronously);

        OperationCanceledException? exception = null;

        try
        {
            await tcs.Task.WaitAsync(TimeSpan.FromMilliseconds(50), false, CancellationToken.None);
        }
        catch (OperationCanceledException caught)
        {
            exception = caught;
        }

        await Assert.That(exception).IsNotNull();
    }

    [Test]
    public async Task WaitAsync_Generic_TaskCompletesFirst_ReturnsRealResult()
    {
        TaskCompletionSource<int> tcs = new(TaskCreationOptions.RunContinuationsAsynchronously);
        tcs.SetResult(42);

        int result = await tcs.Task.WaitAsync(TimeSpan.FromSeconds(30), true, CancellationToken.None);

        await Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public async Task WaitAsync_Generic_SelfCancelledTask_WithSuppression_ReturnsDefault()
    {
        TaskCompletionSource<int> tcs = new(TaskCreationOptions.RunContinuationsAsynchronously);
        tcs.SetCanceled();

        int result = await tcs.Task.WaitAsync(TimeSpan.FromSeconds(30), true, CancellationToken.None);

        await Assert.That(result).IsEqualTo(default(int));
    }

    [Test]
    public async Task WaitAsync_NonGeneric_NeverCompletingTask_WithSuppression_StopsWaitingWithoutBlocking()
    {
        TaskCompletionSource<int> tcs = new(TaskCreationOptions.RunContinuationsAsynchronously);

        await ((Task)tcs.Task).WaitAsync(TimeSpan.FromMilliseconds(50), true, CancellationToken.None);

        await Assert.That(tcs.Task.IsCompleted).IsFalse();
    }
}
#pragma warning restore CS0618 // Type or member is obsolete
