using System;

namespace AlastairLundy.DotExtensions.Numbers.Helpers.Integers;

internal class UInt32Integer : Integer<UInt32>, IEquatable<UInt32Integer>
{
    internal UInt32Integer(uint number) : base(number, 0)
    {
        
    }
    
    internal override Integer<uint> Add(Integer<uint> number)
    {
        return new UInt32Integer(Value + number.Value);
    }

    internal override Integer<uint> Subtract(Integer<uint> number)
    {
        return new UInt32Integer(Value - number.Value);
    }

    internal override Integer<uint> Multiply(Integer<uint> number)
    {
        return new UInt32Integer(Value * number.Value);
    }

    internal override Integer<uint> Divide(Integer<uint> number)
    {
        return new UInt32Integer(Value / number.Value);
    }

    internal override Integer<uint> Modulo(Integer<uint> number)
    {
        return new UInt32Integer(Value % number.Value);
    }

    internal override int ToInt32()
    {
        return Convert.ToInt32(Value);
    }

    internal override long ToInt64()
    {
        return Convert.ToInt64(Value);
    }

    public bool Equals(UInt32Integer? other)
    {
        if (other is null)
            return false;
        
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        
        if(obj is UInt32Integer integer)
            return Equals(integer);
        return false;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    
    
    public static bool operator ==(UInt32Integer left, UInt32Integer right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(UInt32Integer left, UInt32Integer right)
    {
        return left.Equals(right) == false;
    }

    public static bool operator >=(UInt32Integer left, UInt32Integer right)
    {
        return left.Value >= right.Value;
    }

    public static bool operator >(UInt32Integer left, UInt32Integer right)
    {
        return left.Value > right.Value;
    }

    public static bool operator <=(UInt32Integer left, UInt32Integer right)
    {
        return left.Value <= right.Value; 
    }

    public static bool operator <(UInt32Integer left, UInt32Integer right)
    {
        return left.Value < right.Value; 
    }

}