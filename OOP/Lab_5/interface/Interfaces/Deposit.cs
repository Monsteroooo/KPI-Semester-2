using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces;

public abstract class Deposit
: IComparable<Deposit>
{
    private readonly decimal amount;
    private readonly int period;

    protected Deposit(decimal depositAmount, int depositPeriod)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(depositAmount);
        ArgumentOutOfRangeException.ThrowIfNegative(depositPeriod);
        this.amount = depositAmount;
        this.period = depositPeriod;
    }

    public decimal Amount
    {
        get
        {
            return this.amount;
        }
    }

    public int Period
    {
        get
        {
            return this.period;
        }
    }

    public static bool operator >(Deposit deposit1, Deposit deposit2)
    {
        if (deposit1 is null)
        {
            return false;
        }

        return deposit1.CompareTo(deposit2) > 0;
    }

    public static bool operator ==(Deposit deposit1, Deposit deposit2)
    {
        if (deposit1 is null)
        {
            return deposit2 is null;
        }

        return deposit1.CompareTo(deposit2) == 0;
    }

    public static bool operator <(Deposit deposit1, Deposit deposit2)
    {
        if (deposit1 is null)
        {
            return deposit2 is not null;
        }

        return deposit1.CompareTo(deposit2) < 0;
    }

    public static bool operator !=(Deposit deposit1, Deposit deposit2)
    {
        return !(deposit1 == deposit2);
    }

    public static bool operator <=(Deposit deposit1, Deposit deposit2)
    {
        if (deposit1 is null)
        {
            return deposit2 is not null;
        }

        return deposit1.CompareTo(deposit2) <= 0;
    }

    public static bool operator >=(Deposit deposit1, Deposit deposit2)
    {
        if (deposit1 is null)
        {
            return false;
        }

        return deposit1.CompareTo(deposit2) >= 0;
    }

    public override bool Equals(object obj)
    {
        if (obj is Deposit otherDeposit)
        {
            return this.CompareTo(otherDeposit) == 0;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Amount, this.Period);
    }

    public abstract decimal Income();

    public int CompareTo(Deposit other)
    {
        if (other is null)
        {
            return 1;
        }

        decimal sum1 = this.Amount + this.Income();
        decimal sum2 = other.Amount + other.Income();
        if (sum1 > sum2)
        {
            return 1;
        }
        else if (sum2 > sum1)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
