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

using DotExtensions.Numbers;

namespace DotExtensions.Tests.Numbers;

/// <summary>
/// Tests for <see cref="NumberToTNumber"/> pinning that conversions to a lower-precision
/// destination type truncate toward zero (matching C# cast semantics) instead of performing
/// a culture-sensitive string round-trip that throws <see cref="FormatException"/>.
/// </summary>
public class NumberToTNumberTests
{
    [Test]
    public async Task ToDestinationNumber_DoubleWithFractionalPart_ToInt_TruncatesTowardZero()
    {
        double source = 10.5;

        // Extension-block members require the receiver's type parameter as the first explicit
        // type argument, mirroring the library's own ToDestinationNumber<TSource, TDestination> call shape.
        int result = source.ToDestinationNumber<double, int>();

        await Assert.That(result).IsEqualTo(10);
    }

    [Test]
    public async Task ToDestinationNumber_NegativeDoubleWithFractionalPart_ToInt_TruncatesTowardZero()
    {
        double source = -10.5;

        int result = source.ToDestinationNumber<double, int>();

        await Assert.That(result).IsEqualTo(-10);
    }

    [Test]
    public async Task ToDestinationNumber_Int_ToDouble_IsExact()
    {
        int source = 10;

        double result = source.ToDestinationNumber<int, double>();

        await Assert.That(result).IsEqualTo(10.0);
    }
}
