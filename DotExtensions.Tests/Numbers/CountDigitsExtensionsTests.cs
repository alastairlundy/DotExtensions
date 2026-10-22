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

// Contract under test: CountNumberOfDigits counts the digits of the INTEGER PART of the absolute
// value. Values with magnitude < 1 (including 0) count as 1 digit - the signed integer ladders
// treat 0 as the lower bound of the 1-digit case, so no finite value returns 0. NaN and +/-Infinity
// return -1. The int/long ladders keep their original behavior.
public class CountDigitsExtensionsTests
{
    [Test]
    public async Task CountNumberOfDigits_Double_CountsIntegerPartDigits()
    {
        await Assert.That(0.0.CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(0.5.CountNumberOfDigits()).IsEqualTo(1); // integer part is 0 -> 1, per the int-ladder convention
        await Assert.That(1.0.CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(2.0.CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(9.99.CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(10.0.CountNumberOfDigits()).IsEqualTo(2);
        await Assert.That(10.5.CountNumberOfDigits()).IsEqualTo(2);
        await Assert.That(100.0.CountNumberOfDigits()).IsEqualTo(3);
        await Assert.That(1e21.CountNumberOfDigits()).IsEqualTo(22);
    }

    [Test]
    public async Task CountNumberOfDigits_Double_NegativeValuesCountAbsoluteValue()
    {
        await Assert.That((-2.0).CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That((-10.5).CountNumberOfDigits()).IsEqualTo(2);
        await Assert.That((-100.0).CountNumberOfDigits()).IsEqualTo(3);

        // int.MinValue-scale value: |x| = 2147483648 has 10 digits.
        await Assert.That((-2147483648.0).CountNumberOfDigits()).IsEqualTo(10);
    }

    [Test]
    public async Task CountNumberOfDigits_Double_NegativeZero_CountsAsOne()
    {
        double negativeZero = BitConverter.Int64BitsToDouble(unchecked((long)0x8000_0000_0000_0000));

        await Assert.That(1.0 / negativeZero < 0.0).IsTrue(); // guards the fixture: this really is -0.0
        await Assert.That(negativeZero.CountNumberOfDigits()).IsEqualTo(1);
    }

    [Test]
    public async Task CountNumberOfDigits_Double_NaNAndInfinities_ReturnMinusOne()
    {
        await Assert.That(double.NaN.CountNumberOfDigits()).IsEqualTo(-1);
        await Assert.That(double.PositiveInfinity.CountNumberOfDigits()).IsEqualTo(-1);
        await Assert.That(double.NegativeInfinity.CountNumberOfDigits()).IsEqualTo(-1);
    }

    [Test]
    public async Task CountNumberOfDigits_Double_Extremes()
    {
        // double.MaxValue = 1.797...E+308 -> 309 digits.
        await Assert.That(double.MaxValue.CountNumberOfDigits()).IsEqualTo(309);

        // The smallest positive subnormal is still a magnitude < 1 -> 1 by convention.
        await Assert.That(double.Epsilon.CountNumberOfDigits()).IsEqualTo(1);
    }

    [Test]
    public async Task CountNumberOfDigits_Float_CountsIntegerPartDigits()
    {
        await Assert.That(0.0f.CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(0.5f.CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(2.0f.CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(10.0f.CountNumberOfDigits()).IsEqualTo(2);
        await Assert.That(10.5f.CountNumberOfDigits()).IsEqualTo(2);
        await Assert.That(100.0f.CountNumberOfDigits()).IsEqualTo(3);
        await Assert.That((-100.5f).CountNumberOfDigits()).IsEqualTo(3);

        // float.MaxValue = 3.402...E+38 -> 39 digits (counted through the exact float->double path).
        await Assert.That(float.MaxValue.CountNumberOfDigits()).IsEqualTo(39);
    }

    [Test]
    public async Task CountNumberOfDigits_Float_NaNAndInfinities_ReturnMinusOne()
    {
        await Assert.That(float.NaN.CountNumberOfDigits()).IsEqualTo(-1);
        await Assert.That(float.PositiveInfinity.CountNumberOfDigits()).IsEqualTo(-1);
        await Assert.That(float.NegativeInfinity.CountNumberOfDigits()).IsEqualTo(-1);
    }

    [Test]
    public async Task CountNumberOfDigits_Decimal_CountsIntegerPartDigits()
    {
        // decimal has no NaN/Infinity, so this path never returns -1.
        await Assert.That(0m.CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(0.5m.CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(2.0m.CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(7.5m.CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(9.99m.CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(10.0m.CountNumberOfDigits()).IsEqualTo(2);
        await Assert.That(10.5m.CountNumberOfDigits()).IsEqualTo(2);
        await Assert.That(100.0m.CountNumberOfDigits()).IsEqualTo(3);
        await Assert.That((-10.5m).CountNumberOfDigits()).IsEqualTo(2);

        // decimal.MaxValue = 79228162514264337593543950335 -> 29 digits.
        await Assert.That(decimal.MaxValue.CountNumberOfDigits()).IsEqualTo(29);
        await Assert.That(decimal.MinValue.CountNumberOfDigits()).IsEqualTo(29);
    }

    [Test]
    public async Task CountNumberOfDigits_Int32_LadderBehaviorUnchanged()
    {
        await Assert.That(0.CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(7.CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That((-5).CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(10.CountNumberOfDigits()).IsEqualTo(2);
        await Assert.That(999_999_999.CountNumberOfDigits()).IsEqualTo(9);
        await Assert.That(1_000_000_000.CountNumberOfDigits()).IsEqualTo(10);
        await Assert.That(int.MinValue.CountNumberOfDigits()).IsEqualTo(10);
        await Assert.That(int.MaxValue.CountNumberOfDigits()).IsEqualTo(10);
    }

    [Test]
    public async Task CountNumberOfDigits_Int64_LadderBehaviorUnchanged()
    {
        await Assert.That(0L.CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(10L.CountNumberOfDigits()).IsEqualTo(2);
        await Assert.That(123_456_789_012_345_678L.CountNumberOfDigits()).IsEqualTo(18);
        await Assert.That(long.MinValue.CountNumberOfDigits()).IsEqualTo(19);
        await Assert.That(long.MaxValue.CountNumberOfDigits()).IsEqualTo(19);
    }

    [Test]
    public async Task CountNumberOfDigits_Half_GenericPath_CountsIntegerPartDigits()
    {
        Half two = (Half)2;
        Half tenPointFive = (Half)10.5;
        Half zeroPointFive = (Half)0.5;
        Half hundredPointFive = (Half)100.5;
        Half minusHundredPointFive = (Half)(-100.5);

        await Assert.That(two.CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(tenPointFive.CountNumberOfDigits()).IsEqualTo(2);
        await Assert.That(zeroPointFive.CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(hundredPointFive.CountNumberOfDigits()).IsEqualTo(3);
        await Assert.That(minusHundredPointFive.CountNumberOfDigits()).IsEqualTo(3);

        // Half.MaxValue = 65504 -> 5 digits.
        await Assert.That(Half.MaxValue.CountNumberOfDigits()).IsEqualTo(5);
    }

    // The generic fallback used to loop forever on infinities: +infinity / 10 == +infinity never
    // reaches zero, so the old `!= zero` condition never terminated. These assertions are the
    // no-hang guard - a regression hangs the test run instead of failing an assert.
    [Test]
    public async Task CountNumberOfDigits_Half_GenericPath_InfinitiesAndNaN_ReturnMinusOneWithoutHanging()
    {
        await Assert.That(Half.PositiveInfinity.CountNumberOfDigits()).IsEqualTo(-1);
        await Assert.That(Half.NegativeInfinity.CountNumberOfDigits()).IsEqualTo(-1);
        await Assert.That(Half.NaN.CountNumberOfDigits()).IsEqualTo(-1);
    }
}
