using System;

namespace AlastairLundy.DotExtensions.Numbers.Helpers.Integers;

internal class Int32Integer : Integer<int>, IEquatable<Int32Integer>
{
    // ReSharper disable once MemberCanBePrivate.Global
    
    internal Int32Integer(int value) : base(value, 0)
    {
    }

    internal static Integer<int> New(int value)
    {
        return new Int32Integer(value);
    }

    internal override Integer<int> Add(Integer<int> number)
    {
       return new Int32Integer(Value + number.Value);
    }

    internal override Integer<int> Subtract(Integer<int> number)
    {
        return new Int32Integer(Value - number.Value);
    }

    internal override Integer<int> Multiply(Integer<int> number)
    {
        return new Int32Integer(Value * number.Value);
    }

    internal override Integer<int> Divide(Integer<int> number)
    {
        return new Int32Integer(number.Value / number.Value);
    }

    internal override Integer<int> Modulo(Integer<int> number)
    {
        return new Int32Integer(number.Value % number.Value);
    }

    internal override int ToInt32()
    {
        return Value;
    }

    internal override long ToInt64()
    {
        return Convert.ToInt64(Value);
    }

    public bool Equals(Int32Integer? other)
    {
        if (other is null) return false;

        return other.Value == Value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if(obj is Int32Integer integer)
            return Equals(integer);
        return false;
    }

    public static bool operator ==(Int32Integer left, Int32Integer right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Int32Integer left, Int32Integer right)
    {
        return left.Equals(right) == false;
    }

    public static bool operator >=(Int32Integer left, Int32Integer right)
    {
        return left.Value >= right.Value;
    }

    public static bool operator >(Int32Integer left, Int32Integer right)
    {
        return left.Value > right.Value;
    }

    public static bool operator <=(Int32Integer left, Int32Integer right)
    {
        return left.Value <= right.Value; 
    }

    public static bool operator <(Int32Integer left, Int32Integer right)
    {
        return left.Value < right.Value; 
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}