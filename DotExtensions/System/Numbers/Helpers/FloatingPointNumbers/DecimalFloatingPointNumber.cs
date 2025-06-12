using System;

namespace AlastairLundy.DotExtensions.Numbers.Helpers.FloatingPointNumbers;

internal class DecimalFloatingPointNumber : FloatingPointNumber<decimal>
{
    public DecimalFloatingPointNumber(decimal number) : base(number, decimal.Zero)
    {
    }

    internal override FloatingPointNumber<decimal> Add(FloatingPointNumber<decimal> number)
    {
        return new DecimalFloatingPointNumber(decimal.Add(Value, number.Value));
    }

    internal override FloatingPointNumber<decimal> Subtract(FloatingPointNumber<decimal> number)
    {
        return new DecimalFloatingPointNumber(decimal.Subtract(Value, number.Value));
    }

    internal override FloatingPointNumber<decimal> Multiply(FloatingPointNumber<decimal> number)
    {
        return new DecimalFloatingPointNumber(decimal.Multiply(Value, number.Value));
    }

    internal override FloatingPointNumber<decimal> Divide(FloatingPointNumber<decimal> number)
    {
        return new DecimalFloatingPointNumber(decimal.Divide(Value, number.Value));
    }

    internal override FloatingPointNumber<decimal> Modulo(FloatingPointNumber<decimal> number)
    {
        return new DecimalFloatingPointNumber(Value % number.Value);
    }

    internal override int ToInt32()
    {
        return Convert.ToInt32(Value);
    }

    internal override long ToInt64()
    {
        return Convert.ToInt64(Value);
    }
}