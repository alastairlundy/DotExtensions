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

public class SpanCopyExtensionsTests
{
    [Test]
    public async Task OptimisticCopy_Span_SufficientDestination_CopiesInPlace()
    {
        int[] sourceArray = [1, 2, 3];
        int[] destinationArray = [0, 0, 0];
        Span<int> source = sourceArray;
        Span<int> destination = destinationArray;

        source.OptimisticCopy(ref destination, 1, 2);

        int firstCopied = destination[0];
        int secondCopied = destination[1];

        await Assert.That(firstCopied).IsEqualTo(2);
        await Assert.That(secondCopied).IsEqualTo(3);
    }

    [Test]
    public async Task OptimisticCopy_Span_TooSmallDestination_ReassignsToFreshCopyNotSourceSlice()
    {
        int[] sourceArray = [1, 2, 3];
        Span<int> source = sourceArray;
        Span<int> destination = new int[1];

        source.OptimisticCopy(ref destination, 0, 3);

        int destinationLength = destination.Length;
        int first = destination[0];

        // Writing through the reassigned destination must not mutate the source.
        destination[0] = 99;
        int sourceFirst = sourceArray[0];

        await Assert.That(destinationLength).IsEqualTo(3);
        await Assert.That(first).IsEqualTo(1);
        await Assert.That(sourceFirst).IsEqualTo(1);
    }

    [Test]
    public async Task TryCopyTo_Span_TooSmallDestination_ReturnsTrueWithFreshCopy()
    {
        int[] sourceArray = [1, 2, 3];
        Span<int> source = sourceArray;
        Span<int> destination = new int[1];

        bool result = source.TryCopyTo(ref destination, 0, 3);

        int destinationLength = destination.Length;
        int first = destination[0];

        destination[0] = 99;
        int sourceFirst = sourceArray[0];

        await Assert.That(result).IsTrue();
        await Assert.That(destinationLength).IsEqualTo(3);
        await Assert.That(first).IsEqualTo(1);
        await Assert.That(sourceFirst).IsEqualTo(1);
    }

    [Test]
    public async Task TryCopyTo_Span_InvalidRange_ReturnsFalse()
    {
        int[] sourceArray = [1, 2, 3];
        Span<int> source = sourceArray;
        Span<int> destination = new int[3];

        bool result = source.TryCopyTo(ref destination, 0, 5);

        await Assert.That(result).IsFalse();
    }
}
