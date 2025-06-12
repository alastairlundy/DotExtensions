using System;
using System.Collections.Generic;

namespace AlastairLundy.DotExtensions.Numbers.Helpers.Integers;

internal abstract class Integer<TNumber> : IEquatable<Integer<TNumber>>
{
    internal Integer(TNumber number, TNumber zero)
    {
        Value = number;
        Zero = zero;
    }
    
    public bool Equals(Integer<TNumber>? other)
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

        if (obj is Integer<TNumber> integer)
            return Equals(integer);
        else
            return false;
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TNumber>.Default.GetHashCode(Value);
    }

    public static bool operator ==(Integer<TNumber>? left, Integer<TNumber>? right)
    { 
        if (left is null || right is null)
            return false;
        
        return left.Equals(right);
    }

    public static bool operator !=(Integer<TNumber>? left, Integer<TNumber>? right)
    {
        if (left is null || right is null)
            return false;
        
        return left.Equals(right) == false;
    }

    internal TNumber Value { get; }
    internal abstract Integer<TNumber> Add(Integer<TNumber> number);
    internal abstract Integer<TNumber> Subtract(Integer<TNumber> number);
    internal abstract Integer<TNumber> Multiply(Integer<TNumber> number);
    internal abstract Integer<TNumber> Divide(Integer<TNumber> number);

    internal abstract Integer<TNumber> Modulo(Integer<TNumber> number);

    internal abstract int ToInt32();

    internal abstract long ToInt64();

    internal TNumber Zero { get; }
}