using System.Globalization;

namespace BankSystem;

public class AccountCashOperation
{
    private readonly decimal amount;
    private readonly DateTime date;
    private readonly string note;

    public AccountCashOperation(decimal amount, DateTime date, string note)
    {
        ArgumentNullException.ThrowIfNull(note);
        this.amount = amount;
        this.date = date;
        this.note = note;
    }

    public decimal Amount
    {
        get
        {
            return this.amount;
        }
    }

    public DateTime Date
    {
        get
        {
            return this.date;
        }
    }

    public string Note
    {
        get
        {
            return this.note;
        }
    }

    public override string ToString()
    {
        string text = this.amount < 0 ? "Debited from account" : "Credited to account";

        return $"{this.date.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)} {this.note} : {text} {this.amount}.";
    }
}
