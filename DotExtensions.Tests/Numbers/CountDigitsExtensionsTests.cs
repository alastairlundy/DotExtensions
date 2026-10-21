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

/// <summary>
/// Invariant tests for <see cref="CountDigitsExtensions"/>.
/// These tests pin integer-digit counting semantics for the absolute value
/// (matching the int/long overloads) plus the documented -1 contract for
/// NaN and +/-infinity. Culture-invariant numeric literals are used
/// throughout so machine culture cannot affect the results.
/// </summary>
public class CountDigitsExtensionsTests
{
    // -----------------------------------------------------------------------
    // double
    // -----------------------------------------------------------------------

    [Test]
    [Arguments(0.0, 1)]
    [Arguments(0.5, 1)]
    [Arguments(-0.5, 1)]
    [Arguments(7.0, 1)]
    [Arguments(3.14, 1)]
    [Arguments(9.99, 1)]
    [Arguments(10.0, 2)]
    [Arguments(100.0, 3)]
    [Arguments(-100.0, 3)]
    [Arguments(123456.0, 6)]
    [Arguments(-123456.0, 6)]
    [Arguments(1000000.0, 7)]
    [Arguments(double.PositiveInfinity, -1)]
    [Arguments(double.NegativeInfinity, -1)]
    [Arguments(double.NaN, -1)]
    public async Task CountNumberOfDigits_Double_CountsIntegerDigitsOfAbsoluteValue(double number, int expected)
    {
        await Assert.That(number.CountNumberOfDigits()).IsEqualTo(expected);
    }

    // -----------------------------------------------------------------------
    // float
    // -----------------------------------------------------------------------

    [Test]
    [Arguments(0.0f, 1)]
    [Arguments(7.5f, 1)]
    [Arguments(100f, 3)]
    [Arguments(-100f, 3)]
    [Arguments(123456f, 6)]
    [Arguments(float.PositiveInfinity, -1)]
    [Arguments(float.NegativeInfinity, -1)]
    [Arguments(float.NaN, -1)]
    public async Task CountNumberOfDigits_Single_CountsIntegerDigitsOfAbsoluteValue(float number, int expected)
    {
        await Assert.That(number.CountNumberOfDigits()).IsEqualTo(expected);
    }

    // -----------------------------------------------------------------------
    // decimal
    // -----------------------------------------------------------------------

    [Test]
    public async Task CountNumberOfDigits_Decimal_CountsIntegerDigitsOfAbsoluteValue()
    {
        //decimal is not a legal [Arguments] attribute parameter type, so the matrix is inlined here.
        (decimal Number, int Expected)[] cases =
        [
            (0.0m, 1),
            (0.5m, 1),
            (1.0m, 1),
            (1.5m, 1),
            (100.0m, 3),
            (-100.0m, 3),
            (123456.789m, 6),
            (1000000.0m, 7)
        ];

        foreach ((decimal number, int expected) in cases)
        {
            await Assert.That(number.CountNumberOfDigits()).IsEqualTo(expected);
        }
    }

    // -----------------------------------------------------------------------
    // int
    // -----------------------------------------------------------------------

    [Test]
    [Arguments(0, 1)]
    [Arguments(7, 1)]
    [Arguments(100, 3)]
    [Arguments(-100, 3)]
    [Arguments(123456, 6)]
    [Arguments(1000000, 7)]
    [Arguments(int.MinValue, 10)]
    [Arguments(int.MaxValue, 10)]
    public async Task CountNumberOfDigits_Int32_CountsDigitsOfAbsoluteValue(int number, int expected)
    {
        await Assert.That(number.CountNumberOfDigits()).IsEqualTo(expected);
    }

    // -----------------------------------------------------------------------
    // long
    // -----------------------------------------------------------------------

    [Test]
    [Arguments(0L, 1)]
    [Arguments(7L, 1)]
    [Arguments(100L, 3)]
    [Arguments(-100L, 3)]
    [Arguments(123456L, 6)]
    [Arguments(1000000L, 7)]
    [Arguments(long.MinValue, 19)]
    [Arguments(long.MaxValue, 19)]
    public async Task CountNumberOfDigits_Int64_CountsDigitsOfAbsoluteValue(long number, int expected)
    {
        await Assert.That(number.CountNumberOfDigits()).IsEqualTo(expected);
    }

    // -----------------------------------------------------------------------
    // Half (exercises the generic INumber{T} fallback path, including the
    // infinity guard that previously caused an infinite loop).
    // -----------------------------------------------------------------------

    [Test]
    public async Task CountNumberOfDigits_Half_CountsIntegerDigitsAndReturnsMinusOneForSpecialValues()
    {
        await Assert.That(((Half)0).CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(((Half)0.5).CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(((Half)3.5).CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(((Half)7).CountNumberOfDigits()).IsEqualTo(1);
        await Assert.That(((Half)100).CountNumberOfDigits()).IsEqualTo(3);
        await Assert.That(((Half)(-100)).CountNumberOfDigits()).IsEqualTo(3);

        await Assert.That(Half.NaN.CountNumberOfDigits()).IsEqualTo(-1);
        await Assert.That(Half.PositiveInfinity.CountNumberOfDigits()).IsEqualTo(-1);
        await Assert.That(Half.NegativeInfinity.CountNumberOfDigits()).IsEqualTo(-1);
    }
}
