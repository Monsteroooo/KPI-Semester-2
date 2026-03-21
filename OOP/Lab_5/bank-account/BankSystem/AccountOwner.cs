using System.Collections.Generic;
using BankSystem.Helpers;

namespace BankSystem.Accounts;

public sealed class AccountOwner
{
    private readonly List<BankAccount> accounts = new List<BankAccount>();

    public AccountOwner(string firstName, string lastName, string email)
    {
        this.FirstName = VerifyString(firstName, nameof(firstName));
        this.LastName = VerifyString(lastName, nameof(lastName));
        if (!email.IsEmailValid())
        {
            throw new ArgumentException("Не валідний імейл!", nameof(email));
        }
        else
        {
            this.Email = email;
        }
    }

    public string FirstName { get; }

    public string LastName { get; }

    public string Email { get; }

    public void Add(BankAccount account)
    {
       this.accounts.Add(account);
    }

    public IReadOnlyCollection<BankAccount> Accounts()
    {
        return this.accounts.AsReadOnly();
    }

    public override string ToString()
    {
        return $"{this.FirstName} {this.LastName}, {this.Email}.";
    }

    private static string VerifyString(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Рядок не може бути порожнім.", paramName);
        }
        else
        {
            return value;
        }
    }
}
