using System;

namespace AlastairLundy.DotExtensions.Numbers.Helpers.Integers;

internal class UInt64Integer : Integer<ulong>, IEquatable<UInt64Integer>
{
    
    internal UInt64Integer(ulong number) : base(number, 0)
    {
        
    }


    internal override Integer<ulong> Add(Integer<ulong> number)
    {
        return new UInt64Integer(Value + number.Value);
    }

    internal override Integer<ulong> Subtract(Integer<ulong> number)
    {
        return new UInt64Integer(Value - number.Value);
    }

    internal override Integer<ulong> Multiply(Integer<ulong> number)
    {
        return new UInt64Integer(Value * number.Value);
    }

    internal override Integer<ulong> Divide(Integer<ulong> number)
    {
       return new UInt64Integer(Value / number.Value);
    }

    internal override Integer<ulong> Modulo(Integer<ulong> number)
    {
       return new UInt64Integer(Value % number.Value);
    }

    internal override int ToInt32()
    {
        return Convert.ToInt32(Value);
    }

    internal override long ToInt64()
    {
        return Convert.ToInt64(Value);
    }
    
    public static bool operator ==(UInt64Integer left, UInt64Integer right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(UInt64Integer left, UInt64Integer right)
    {
        return left.Equals(right) == false;
    }

    public static bool operator >=(UInt64Integer left, UInt64Integer right)
    {
        return left.Value >= right.Value;
    }

    public static bool operator >(UInt64Integer left, UInt64Integer right)
    {
        return left.Value > right.Value;
    }

    public static bool operator <=(UInt64Integer left, UInt64Integer right)
    {
        return left.Value <= right.Value; 
    }

    public static bool operator <(UInt64Integer left, UInt64Integer right)
    {
        return left.Value < right.Value; 
    }

    public bool Equals(UInt64Integer? other)
    {
        if(other is null)
            return false;
        
        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        
        if (obj is UInt64Integer integer)
            return Equals(integer);

        return false;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}