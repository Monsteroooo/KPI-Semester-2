using System.Collections.Generic;
using BankSystem.Generators;

namespace BankSystem.Accounts;

public abstract class BankAccount
{
    private readonly List<AccountCashOperation> operations = new List<AccountCashOperation>();

    protected BankAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
    {
        ArgumentNullException.ThrowIfNull(uniqueNumberGenerator);
        this.CurrencyCode = currencyCode;
        this.Owner = owner;
        this.Number = uniqueNumberGenerator.Generate();
    }

    protected BankAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
    {
        ArgumentNullException.ThrowIfNull(numberGenerator);
        this.Owner = owner;
        this.CurrencyCode = currencyCode;
        this.Number = numberGenerator();
    }

    protected BankAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
    : this(owner, currencyCode, uniqueNumberGenerator)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(initialBalance);
        this.Deposit(initialBalance, DateTime.Now, "Initial deposit");
    }

    protected BankAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
    : this(owner, currencyCode, numberGenerator)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(initialBalance);
        this.Deposit(initialBalance, DateTime.Now, "Initial deposit");
    }

    public string Number { get; }

    public decimal Balance { get; private set; }

    public string CurrencyCode { get; }

    public AccountOwner Owner { get; }

    public int BonusPoints { get; protected set; }

    public abstract decimal Overdraft { get; }

    public IReadOnlyCollection<AccountCashOperation> GetAllOperations()
    {
        return this.operations.AsReadOnly();
    }

    public void Deposit(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        this.Balance += amount;
        this.BonusPoints += this.CalculateDepositRewardPoints(amount);
        AccountCashOperation operation = new AccountCashOperation(amount, date, note);
        this.operations.Add(operation);
    }

    public void Withdraw(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        if (amount > this.Balance + this.Overdraft)
        {
            throw new InvalidOperationException("Not enough funds.");
        }

        this.Balance -= amount;
        this.BonusPoints += this.CalculateWithdrawRewardPoints(amount);
        AccountCashOperation operation = new AccountCashOperation(-amount, date, note);
        this.operations.Add(operation);
    }

    public override string ToString()
    {
        return $"{this.Owner} No:{this.Number}. Balance: {this.Balance}{this.CurrencyCode}.";
    }

    protected abstract int CalculateDepositRewardPoints(decimal amount);

    protected abstract int CalculateWithdrawRewardPoints(decimal amount);
}
