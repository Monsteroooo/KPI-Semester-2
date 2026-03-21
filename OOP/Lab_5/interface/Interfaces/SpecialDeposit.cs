using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces;

public class SpecialDeposit
: Deposit, IProlongable
{
    public SpecialDeposit(decimal amount, int period)
    : base(amount, period)
    {
    }

    public override decimal Income()
    {
        decimal currentBalance = this.Amount;
        for (int i = 1; i <= this.Period; i++)
        {
            currentBalance += currentBalance * (i / 100m);
        }

        return currentBalance - this.Amount;
    }

    public bool CanToProlong()
    {
        if (this.Amount > 1000)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
