using System;

namespace AlastairLundy.DotExtensions.Numbers.Helpers.FloatingPointNumbers;

internal class DoubleFloatingPointNumber : FloatingPointNumber<double>
{
    internal DoubleFloatingPointNumber(double number) : base(number, 0.0)
    {
        
    }
    
    internal override FloatingPointNumber<double> Add(FloatingPointNumber<double> number)
    {
        return new DoubleFloatingPointNumber(Value + number.Value);
    }

    internal override FloatingPointNumber<double> Subtract(FloatingPointNumber<double> number)
    {
        return new DoubleFloatingPointNumber(Value - number.Value);
    }

    internal override FloatingPointNumber<double> Multiply(FloatingPointNumber<double> number)
    {
        return new DoubleFloatingPointNumber(Value * number.Value);
    }

    internal override FloatingPointNumber<double> Divide(FloatingPointNumber<double> number)
    {
        return new DoubleFloatingPointNumber(Value / number.Value);
    }

    internal override FloatingPointNumber<double> Modulo(FloatingPointNumber<double> number)
    {
        return new DoubleFloatingPointNumber(Value % number.Value);
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