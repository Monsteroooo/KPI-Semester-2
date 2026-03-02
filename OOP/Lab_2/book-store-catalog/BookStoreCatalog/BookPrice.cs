namespace BookStoreCatalog;

/// <summary>
/// Represents a book price.
/// </summary>
public class BookPrice
{
    private decimal amount;
    private string currency;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookPrice"/> class.
    /// </summary>
    public BookPrice()
    : this(0m, "USD")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BookPrice"/> class with specified <paramref name="amount"/> and <paramref name="currency"/>.
    /// </summary>
    /// <param name="amount">An amount of money of a book.</param>
    /// <param name="currency">A price currency.</param>
    public BookPrice(decimal amount, string currency)
    {
        this.Amount = amount;
        this.Currency = currency;
    }

    /// <summary>
    /// Gets or sets an amount of money that a book costs.
    /// </summary>i
    public decimal Amount
    {
        get
        {
            return this.amount;
        }

        set
        {
            ThrowExceptionIfAmountIsNotValid(value);
            this.amount = value;
        }
    }

    /// <summary>
    /// Gets or sets a book price currency.
    /// </summary>
    public string Currency
    {
        get
        {
            return this.currency;
        }

        set
        {
            ThrowExceptionIfCurrencyIsNotValid(value);
            this.currency = value;
        }
    }

    /// <summary>
    /// Returns the string that represents a current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return this.amount.ToString("N", System.Globalization.CultureInfo.InvariantCulture) + " " + this.currency;
    }

    private static void ThrowExceptionIfAmountIsNotValid(decimal value)
    {
        if (value < 0m)
        {
            throw new ArgumentException("amount invalid", nameof(value));
        }
    }

    private static void ThrowExceptionIfCurrencyIsNotValid(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value.Length != 3)
        {
            throw new ArgumentException("currency invalid", nameof(value));
        }
    }
}
