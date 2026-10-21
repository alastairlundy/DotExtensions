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
using DotExtensions.Memory.Spans;

namespace DotExtensions.Tests.Memory;

/// <summary>
/// Tests for <see cref="SpanCopyExtensions"/> covering <c>TryCopyTo</c>, <c>CopyTo</c> and <c>OptimisticCopy</c>.
/// Array buffers are used as span backing stores so that buffer contents (and aliasing) can be observed.
/// </summary>
public class SpanCopyExtensionsTests
{
    [Test]
    public async Task TryCopyTo_DestinationTooSmall_ReturnsFalseAndLeavesDestinationUnmodified()
    {
        int[] sourceBuffer = new[] { 1, 2, 3, 4, 5 };
        int[] destinationBuffer = new[] { 10, 20, 30 };

        (bool Result, int[] DestinationAfterCopy, int DestinationFirstAfterWrite, int[] SourceAfterWrite) observed = CopyAndObserve();

        await Assert.That(observed.Result).IsFalse();

        // The caller's buffer must be left untouched.
        await Assert.That(observed.DestinationAfterCopy[0]).IsEqualTo(10);
        await Assert.That(observed.DestinationAfterCopy[1]).IsEqualTo(20);
        await Assert.That(observed.DestinationAfterCopy[2]).IsEqualTo(30);

        // Writing through the returned destination must still target the caller's original
        // buffer (destination was not replaced) and must not mutate the source (aliasing regression).
        await Assert.That(observed.DestinationFirstAfterWrite).IsEqualTo(999);
        await Assert.That(destinationBuffer[0]).IsEqualTo(999);
        await Assert.That(observed.SourceAfterWrite[0]).IsEqualTo(1);
        await Assert.That(observed.SourceAfterWrite[1]).IsEqualTo(2);

        // Spans cannot cross await boundaries (CS4007), so all span work happens synchronously here.
        (bool Result, int[] DestinationAfterCopy, int DestinationFirstAfterWrite, int[] SourceAfterWrite) CopyAndObserve()
        {
            Span<int> source = sourceBuffer;
            Span<int> destination = destinationBuffer;

            bool result = source.TryCopyTo(ref destination, 0);

            int[] destinationAfterCopy = (int[])destinationBuffer.Clone();

            destination[0] = 999;

            return (result, destinationAfterCopy, destination[0], (int[])sourceBuffer.Clone());
        }
    }

    [Test]
    public async Task TryCopyTo_WithLengthDestinationTooSmall_ReturnsFalseAndLeavesDestinationUnmodified()
    {
        int[] sourceBuffer = new[] { 1, 2, 3, 4, 5 };
        int[] destinationBuffer = new[] { 10, 20 };

        (bool Result, int[] DestinationAfterCopy, int DestinationFirstAfterWrite, int[] SourceAfterWrite) observed = CopyAndObserve();

        await Assert.That(observed.Result).IsFalse();

        await Assert.That(observed.DestinationAfterCopy[0]).IsEqualTo(10);
        await Assert.That(observed.DestinationAfterCopy[1]).IsEqualTo(20);

        // Writing through the returned destination must not mutate the source (aliasing regression).
        await Assert.That(observed.DestinationFirstAfterWrite).IsEqualTo(999);
        await Assert.That(destinationBuffer[0]).IsEqualTo(999);
        await Assert.That(observed.SourceAfterWrite[0]).IsEqualTo(1);

        // Spans cannot cross await boundaries (CS4007), so all span work happens synchronously here.
        (bool Result, int[] DestinationAfterCopy, int DestinationFirstAfterWrite, int[] SourceAfterWrite) CopyAndObserve()
        {
            Span<int> source = sourceBuffer;
            Span<int> destination = destinationBuffer;

            bool result = source.TryCopyTo(ref destination, 0, 4);

            int[] destinationAfterCopy = (int[])destinationBuffer.Clone();

            destination[0] = 999;

            return (result, destinationAfterCopy, destination[0], (int[])sourceBuffer.Clone());
        }
    }

    [Test]
    public async Task TryCopyTo_DestinationLargeEnough_CopiesAndReturnsTrue()
    {
        int[] sourceBuffer = new[] { 1, 2, 3 };
        int[] destinationBuffer = new[] { 0, 0, 0, 0, 0 };

        Span<int> source = sourceBuffer;
        Span<int> destination = destinationBuffer;

        bool result = source.TryCopyTo(ref destination, 1);

        await Assert.That(result).IsTrue();

        await Assert.That(destinationBuffer[0]).IsEqualTo(2);
        await Assert.That(destinationBuffer[1]).IsEqualTo(3);
        await Assert.That(destinationBuffer[2]).IsEqualTo(0);

        await Assert.That(sourceBuffer[0]).IsEqualTo(1);
        await Assert.That(sourceBuffer[1]).IsEqualTo(2);
        await Assert.That(sourceBuffer[2]).IsEqualTo(3);
    }

    [Test]
    public async Task TryCopyTo_WithLengthDestinationLargeEnough_CopiesAndReturnsTrue()
    {
        int[] sourceBuffer = new[] { 1, 2, 3, 4, 5 };
        int[] destinationBuffer = new[] { 0, 0, 0, 0, 0 };

        Span<int> source = sourceBuffer;
        Span<int> destination = destinationBuffer;

        bool result = source.TryCopyTo(ref destination, 2, 3);

        await Assert.That(result).IsTrue();

        await Assert.That(destinationBuffer[0]).IsEqualTo(3);
        await Assert.That(destinationBuffer[1]).IsEqualTo(4);
        await Assert.That(destinationBuffer[2]).IsEqualTo(5);
        await Assert.That(destinationBuffer[3]).IsEqualTo(0);
        await Assert.That(destinationBuffer[4]).IsEqualTo(0);
    }

    [Test]
    public async Task TryCopyTo_StartIndexOutOfRange_ReturnsFalse()
    {
        int[] sourceBuffer = new[] { 1, 2, 3 };
        int[] destinationBuffer = new[] { 0, 0, 0, 0, 0 };

        Span<int> source = sourceBuffer;
        Span<int> destination = destinationBuffer;

        bool result = source.TryCopyTo(ref destination, 99);

        await Assert.That(result).IsFalse();

        await Assert.That(destinationBuffer[0]).IsEqualTo(0);
        await Assert.That(destinationBuffer[1]).IsEqualTo(0);
        await Assert.That(destinationBuffer[2]).IsEqualTo(0);
    }

    [Test]
    public async Task CopyTo_StartIndexOutOfRange_ThrowsNamingStartIndexParameter()
    {
        int[] sourceBuffer = new[] { 1, 2, 3, 4, 5 };
        int[] destinationBuffer = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        Span<int> source = sourceBuffer;
        Span<int> destination = destinationBuffer;

        ArgumentOutOfRangeException? thrown = null;

        try
        {
            source.CopyTo(ref destination, 99);
        }
        catch (ArgumentOutOfRangeException exception)
        {
            thrown = exception;
        }

        await Assert.That(thrown is not null).IsTrue();
        await Assert.That(thrown!.ParamName).IsEqualTo("startIndex");
    }

    [Test]
    public async Task CopyTo_NonZeroStartIndex_CopiesRemainingElements()
    {
        int[] sourceBuffer = new[] { 1, 2, 3, 4, 5 };
        int[] destinationBuffer = new[] { 0, 0, 0, 0, 0 };

        await Assert.That(() =>
        {
            Span<int> source = sourceBuffer;
            Span<int> destination = destinationBuffer;

            source.CopyTo(ref destination, 2);
        }).ThrowsNothing();

        await Assert.That(destinationBuffer[0]).IsEqualTo(3);
        await Assert.That(destinationBuffer[1]).IsEqualTo(4);
        await Assert.That(destinationBuffer[2]).IsEqualTo(5);
        await Assert.That(destinationBuffer[3]).IsEqualTo(0);
        await Assert.That(destinationBuffer[4]).IsEqualTo(0);

        await Assert.That(sourceBuffer[2]).IsEqualTo(3);
    }

    [Test]
    public async Task OptimisticCopy_DestinationSmallerThanSource_CopiesAsMuchAsFitsWithoutThrowing()
    {
        int[] sourceBuffer = new[] { 1, 2, 3, 4, 5 };
        int[] destinationBuffer = new[] { 10, 20, 30 };

        await Assert.That(() =>
        {
            Span<int> source = sourceBuffer;
            Span<int> destination = destinationBuffer;

            source.OptimisticCopy(ref destination, 0);
        }).ThrowsNothing();

        await Assert.That(destinationBuffer[0]).IsEqualTo(1);
        await Assert.That(destinationBuffer[1]).IsEqualTo(2);
        await Assert.That(destinationBuffer[2]).IsEqualTo(3);

        await Assert.That(sourceBuffer[0]).IsEqualTo(1);
        await Assert.That(sourceBuffer[3]).IsEqualTo(4);
        await Assert.That(sourceBuffer[4]).IsEqualTo(5);
    }

    [Test]
    public async Task OptimisticCopy_DestinationLargerThanSource_CopiesWithoutThrowing()
    {
        int[] sourceBuffer = new[] { 1, 2, 3 };
        int[] destinationBuffer = new[] { 10, 20, 30, 40, 50 };

        await Assert.That(() =>
        {
            Span<int> source = sourceBuffer;
            Span<int> destination = destinationBuffer;

            source.OptimisticCopy(ref destination, 0);
        }).ThrowsNothing();

        await Assert.That(destinationBuffer[0]).IsEqualTo(1);
        await Assert.That(destinationBuffer[1]).IsEqualTo(2);
        await Assert.That(destinationBuffer[2]).IsEqualTo(3);
        await Assert.That(destinationBuffer[3]).IsEqualTo(40);
        await Assert.That(destinationBuffer[4]).IsEqualTo(50);

        await Assert.That(sourceBuffer[0]).IsEqualTo(1);
        await Assert.That(sourceBuffer[2]).IsEqualTo(3);
    }

    [Test]
    public async Task OptimisticCopy_ReadOnlySpanSourceDestinationLargerThanSource_CopiesWithoutThrowing()
    {
        int[] sourceBuffer = new[] { 1, 2, 3 };
        int[] destinationBuffer = new[] { 10, 20, 30, 40, 50 };

        await Assert.That(() =>
        {
            ReadOnlySpan<int> source = sourceBuffer;
            Span<int> destination = destinationBuffer;

            source.OptimisticCopy(ref destination, 0);
        }).ThrowsNothing();

        await Assert.That(destinationBuffer[0]).IsEqualTo(1);
        await Assert.That(destinationBuffer[1]).IsEqualTo(2);
        await Assert.That(destinationBuffer[2]).IsEqualTo(3);
        await Assert.That(destinationBuffer[3]).IsEqualTo(40);
        await Assert.That(destinationBuffer[4]).IsEqualTo(50);
    }

    [Test]
    public async Task OptimisticCopy_LengthExceedingDestination_ReplacesDestinationLeavingSourceAndOriginalBufferUnchanged()
    {
        int[] sourceBuffer = new[] { 1, 2, 3, 4, 5 };
        int[] destinationBuffer = new[] { 10, 20, 30 };

        (int Length, int[] Contents, int SourceFirstAfterWrite, int DestinationBufferFirstAfterWrite) observed = Observe();

        await Assert.That(observed.Length).IsEqualTo(5);

        await Assert.That(observed.Contents[0]).IsEqualTo(1);
        await Assert.That(observed.Contents[1]).IsEqualTo(2);
        await Assert.That(observed.Contents[2]).IsEqualTo(3);
        await Assert.That(observed.Contents[3]).IsEqualTo(4);
        await Assert.That(observed.Contents[4]).IsEqualTo(5);

        // The new destination must not alias the source, and the original buffer must be untouched.
        await Assert.That(observed.SourceFirstAfterWrite).IsEqualTo(1);
        await Assert.That(observed.DestinationBufferFirstAfterWrite).IsEqualTo(10);

        // Spans cannot cross await boundaries (CS4007), so all span work happens synchronously here.
        (int Length, int[] Contents, int SourceFirstAfterWrite, int DestinationBufferFirstAfterWrite) Observe()
        {
            Span<int> source = sourceBuffer;
            Span<int> destination = destinationBuffer;

            source.OptimisticCopy(ref destination, 0, 5);

            int length = destination.Length;
            int[] contents = new int[destination.Length];
            destination.CopyTo(contents);

            destination[0] = 999;

            return (length, contents, sourceBuffer[0], destinationBuffer[0]);
        }
    }

    [Test]
    public async Task OptimisticCopy_NonZeroStartIndex_CopiesFromStartIndex()
    {
        int[] sourceBuffer = new[] { 1, 2, 3, 4, 5 };
        int[] destinationBuffer = new[] { 0, 0, 0 };

        Span<int> source = sourceBuffer;
        Span<int> destination = destinationBuffer;

        source.OptimisticCopy(ref destination, 2);

        await Assert.That(destinationBuffer[0]).IsEqualTo(3);
        await Assert.That(destinationBuffer[1]).IsEqualTo(4);
        await Assert.That(destinationBuffer[2]).IsEqualTo(5);

        await Assert.That(sourceBuffer[2]).IsEqualTo(3);
    }
}
