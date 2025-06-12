namespace AlastairLundy.DotExtensions.Numbers.Helpers.FloatingPointNumbers;

internal abstract class FloatingPointNumber<TNumber>
{
    internal FloatingPointNumber(TNumber number, TNumber zero)
    {
        Value = number;
        Zero = zero;
    }
    
    public bool Equals(FloatingPointNumber<TNumber>? other)
    {
        if (other is null)
            return false;

        if (other.Value is null)
            return false;

        return other.Value.Equals(Value);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj is FloatingPointNumber<TNumber> floatingPointNumber)
            return Equals(floatingPointNumber);
        else
            return false;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(FloatingPointNumber<TNumber>? left, FloatingPointNumber<TNumber>? right)
    { 
        if (left is null || right is null)
            return false;
        
        return left.Equals(right);
    }

    public static bool operator !=(FloatingPointNumber<TNumber>? left, FloatingPointNumber<TNumber>? right)
    {
        if (left is null || right is null)
            return false;
        
        return left.Equals(right) == false;
    }

    internal TNumber Value { get; }
    
    internal abstract FloatingPointNumber<TNumber> Add(FloatingPointNumber<TNumber> number);
    internal abstract FloatingPointNumber<TNumber> Subtract(FloatingPointNumber<TNumber> number);
    internal abstract FloatingPointNumber<TNumber> Multiply(FloatingPointNumber<TNumber> number);
    internal abstract FloatingPointNumber<TNumber> Divide(FloatingPointNumber<TNumber> number);

    internal abstract FloatingPointNumber<TNumber> Modulo(FloatingPointNumber<TNumber> number);

    internal abstract int ToInt32();

    internal abstract long ToInt64();

    internal TNumber Zero { get; }
}