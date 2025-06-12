using System;

namespace AlastairLundy.DotExtensions.Numbers.Helpers.Integers;

internal class Int64Integer : Integer<long>, IEquatable<Int64Integer>
{

    internal Int64Integer(long value) : base(value, 0)
    {
        
    }
    
    internal override Integer<long> Add(Integer<long> number)
    {
        return new Int64Integer(Value + number.Value);
    }

    internal override Integer<long> Subtract(Integer<long> number)
    {
        return new Int64Integer(Value - number.Value);
    }

    internal override Integer<long> Multiply(Integer<long> number)
    {
        return new Int64Integer(Value - number.Value);
    }

    internal override Integer<long> Divide(Integer<long> number)
    {
        return new Int64Integer(Value / number.Value);
    }

    internal override Integer<long> Modulo(Integer<long> number)
    {
        return new Int64Integer(Value % number.Value);
    }

    internal override int ToInt32()
    {
        return Convert.ToInt32(Value);
    }

    internal override long ToInt64()
    {
        return Value;
    }

    public bool Equals(Int64Integer? other)
    {
        if (other is null) return false;
        
        return other.Value == Value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (obj is Int64Integer integer)
            return Equals(integer);
        return false;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    
    
    public static bool operator ==(Int64Integer left, Int64Integer right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Int64Integer left, Int64Integer right)
    {
        return left.Equals(right) == false;
    }

    public static bool operator >=(Int64Integer left, Int64Integer right)
    {
        return left.Value >= right.Value;
    }

    public static bool operator >(Int64Integer left, Int64Integer right)
    {
        return left.Value > right.Value;
    }

    public static bool operator <=(Int64Integer left, Int64Integer right)
    {
        return left.Value <= right.Value; 
    }

    public static bool operator <(Int64Integer left, Int64Integer right)
    {
        return left.Value < right.Value; 
    }

}