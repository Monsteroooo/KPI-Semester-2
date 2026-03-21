using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces;

public class LongDeposit
: Deposit, IProlongable
{
    public LongDeposit(decimal amount, int period)
    : base(amount, period)
    {
    }

    public override decimal Income()
    {
        decimal currentBalance = this.Amount;
        for (int i = 7; i <= this.Period; i++)
        {
            currentBalance += currentBalance * 0.15m;
        }

        return currentBalance - this.Amount;
    }

    public bool CanToProlong()
    {
        if (this.Period <= 36)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
