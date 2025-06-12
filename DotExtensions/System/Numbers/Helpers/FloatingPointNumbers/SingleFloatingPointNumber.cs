using System;

namespace AlastairLundy.DotExtensions.Numbers.Helpers.FloatingPointNumbers;

internal class SingleFloatingPointNumber : FloatingPointNumber<float>
{
    internal SingleFloatingPointNumber(float number) : base(number, 0.0F)
    {
    }

    internal override FloatingPointNumber<float> Add(FloatingPointNumber<float> number)
    {
        return new SingleFloatingPointNumber(Value + number.Value);
    }

    internal override FloatingPointNumber<float> Subtract(FloatingPointNumber<float> number)
    {
        return new SingleFloatingPointNumber(Value - number.Value);
    }

    internal override FloatingPointNumber<float> Multiply(FloatingPointNumber<float> number)
    {
        return new SingleFloatingPointNumber(Value * number.Value);
    }

    internal override FloatingPointNumber<float> Divide(FloatingPointNumber<float> number)
    {
        return new SingleFloatingPointNumber(Value / number.Value);
    }

    internal override FloatingPointNumber<float> Modulo(FloatingPointNumber<float> number)
    {
        return new SingleFloatingPointNumber(Value % number.Value);
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