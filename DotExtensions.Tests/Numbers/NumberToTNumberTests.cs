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
using DotExtensions.Numbers;

namespace DotExtensions.Tests.Numbers;

public class NumberToTNumberTests
{
    // Regression: the old ToString()/Parse round-trip threw FormatException for double 1e21,
    // whose default ToString() is exponent form ("1E+21"), even though decimal covers the value.
    [Test]
    public async Task ToDestinationNumber_Double1E21_ToDecimal_Succeeds()
    {
        decimal actual = NumberToTNumber.ToDestinationNumber<double, decimal>(1e21);

        await Assert.That(actual).IsEqualTo(1_000_000_000_000_000_000_000m);
    }

    [Test]
    public async Task ToDestinationNumber_Double_ToDecimal_PreservesFractionalValues()
    {
        await Assert.That(NumberToTNumber.ToDestinationNumber<double, decimal>(0.5)).IsEqualTo(0.5m);
        await Assert.That(NumberToTNumber.ToDestinationNumber<double, decimal>(10.5)).IsEqualTo(10.5m);
    }

    // Documented semantics: NaN throws for destinations without a NaN representation (the old
    // text round-trip also threw for decimal), while floating destinations preserve NaN.
    [Test]
    public async Task ToDestinationNumber_NaN_ToDecimal_ThrowsOverflowException()
    {
        await Assert.That(() => NumberToTNumber.ToDestinationNumber<double, decimal>(double.NaN))
            .ThrowsExactly<OverflowException>();
    }

    [Test]
    public async Task ToDestinationNumber_NaN_ToDouble_PreservesNaN()
    {
        double actual = NumberToTNumber.ToDestinationNumber<double, double>(double.NaN);

        await Assert.That(double.IsNaN(actual)).IsTrue();
    }

    // Documented semantics: values outside the destination's range throw OverflowException
    // instead of failing to parse (or silently saturating through text).
    [Test]
    public async Task ToDestinationNumber_OutOfRange_ToInt32_ThrowsOverflowException()
    {
        await Assert.That(() => NumberToTNumber.ToDestinationNumber<double, int>(1e30))
            .ThrowsExactly<OverflowException>();
    }

    [Test]
    public async Task ToDestinationNumber_WithinRange_ConvertsExactly()
    {
        await Assert.That(NumberToTNumber.ToDestinationNumber<double, double>(1.0)).IsEqualTo(1.0);
        await Assert.That(NumberToTNumber.ToDestinationNumber<double, float>(0.1)).IsEqualTo(0.1f);
        await Assert.That(NumberToTNumber.ToDestinationNumber<int, double>(5)).IsEqualTo(5.0);
        await Assert.That(NumberToTNumber.ToDestinationNumber<int, decimal>(5)).IsEqualTo(5m);
    }
}

