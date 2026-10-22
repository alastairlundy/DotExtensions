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

using System.Linq;
using DotExtensions.Numbers;

namespace DotExtensions.Tests.Numbers;

/// <summary>
/// Tests for <see cref="IncrementedNumberRange"/> pinning that every empty sequence shape
/// (list, array, iterator) returns true rather than throwing, alongside known-true and
/// known-false non-empty cases.
/// </summary>
public class IncrementedNumberRangeTests
{
    [Test]
    public async Task IsIncrementedNumberRange_EmptyList_ReturnsTrue()
    {
        List<int> empty = [];

        await Assert.That(empty.IsIncrementedNumberRange(1)).IsTrue();
    }

    [Test]
    public async Task IsIncrementedNumberRange_EmptyArray_ReturnsTrue()
    {
        int[] empty = [];

        await Assert.That(empty.IsIncrementedNumberRange(1)).IsTrue();
    }

    [Test]
    public async Task IsIncrementedNumberRange_EmptyIterator_ReturnsTrue()
    {
        await Assert.That(EmptyIterator().IsIncrementedNumberRange(1)).IsTrue();

        static IEnumerable<int> EmptyIterator()
        {
            yield break;
        }
    }

    [Test]
    public async Task IsIncrementedNumberRange_EnumerableEmpty_ReturnsTrue()
    {
        await Assert.That(Enumerable.Empty<int>().IsIncrementedNumberRange(1)).IsTrue();
    }

    [Test]
    public async Task IsIncrementedNumberRange_SequentialListWithExpectedIncrement_ReturnsTrue()
    {
        List<int> source = [1, 2, 3, 4];

        await Assert.That(source.IsIncrementedNumberRange(1)).IsTrue();
    }

    [Test]
    public async Task IsIncrementedNumberRange_RepeatedValueList_ReturnsFalse()
    {
        List<int> source = [1, 2, 2];

        await Assert.That(source.IsIncrementedNumberRange(1)).IsFalse();
    }
}
