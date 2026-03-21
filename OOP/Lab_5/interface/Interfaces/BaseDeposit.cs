using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces;

public class BaseDeposit
: Deposit
{
    public BaseDeposit(decimal amount, int period)
    : base(amount, period)
    {
    }

    public override decimal Income()
    {
        decimal currentBalance = this.Amount;
        for (int i = 0; i < this.Period; i++)
        {
            currentBalance += currentBalance * 0.05m;
        }

        return currentBalance - this.Amount;
    }
}
