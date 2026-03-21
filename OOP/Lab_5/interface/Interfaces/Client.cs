using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces;

public class Client
: IEnumerable<Deposit>
{
    private readonly Deposit[] deposits;

    public Client()
    {
        this.deposits = new Deposit[10];
    }

    public bool AddDeposit(Deposit deposit)
    {
        for (int i = 0; i < 10; i++)
        {
            if (this.deposits[i] is null)
            {
                this.deposits[i] = deposit;
                return true;
            }
        }

        return false;
    }

    public decimal TotalIncome()
    {
        decimal sum = 0;
        for (int i = 0; i < 10; i++)
        {
            if (this.deposits[i] is not null)
            {
                sum += this.deposits[i].Income();
            }
        }

        return sum;
    }

    public decimal MaxIncome()
    {
        decimal current = 0;
        for (int i = 0; i < 10; i++)
        {
            if (this.deposits[i] is not null && this.deposits[i].Income() > current)
            {
                current = this.deposits[i].Income();
            }
        }

        return current;
    }

    public decimal GetIncomeByNumber(int number)
    {
        int numberReal = number - 1;
        if (this.deposits[numberReal] is null)
        {
            return 0;
        }

        return this.deposits[numberReal].Income();
    }

    public int CountPossibleToProlongDeposit()
    {
        int i = 0;
        for (int j = 0; j < 10; j++)
        {
            if (this.deposits[j] is IProlongable p && p.CanToProlong())
            {
                i++;
            }
        }

        return i;
    }

    public void SortDeposits()
    {
        int n = this.deposits.Length;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (this.deposits[j] == null && this.deposits[j + 1] != null)
                {
                    this.deposits[j] = this.deposits[j + 1];
                    this.deposits[j + 1] = null;
                }
                else if (this.deposits[j] != null && this.deposits[j + 1] != null && this.deposits[j] < this.deposits[j + 1])
                {
                    Deposit temp = this.deposits[j];
                    this.deposits[j] = this.deposits[j + 1];
                    this.deposits[j + 1] = temp;
                }
            }
        }
    }

    public IEnumerator<Deposit> GetEnumerator()
    {
        for (int i = 0; i < this.deposits.Length; i++)
        {
            if (this.deposits[i] is not null)
            {
                yield return this.deposits[i];
            }
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
