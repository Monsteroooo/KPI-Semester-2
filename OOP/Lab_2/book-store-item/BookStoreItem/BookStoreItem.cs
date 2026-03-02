using System;
using System.Globalization;

namespace BookStoreItem;

/// <summary>
/// Represents an item in a book store.
/// </summary>
public class BookStoreItem
{
    private readonly string authorName;
    private readonly string? isni;
    private readonly bool hasIsni;
    private decimal price;
    private string currency = "USD";
    private int amount;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookStoreItem"/> class with the specified <paramref name="authorName"/>, <paramref name="title"/>, <paramref name="publisher"/> and <paramref name="isbn"/>.
    /// </summary>
    /// <param name="authorName">A book author's name.</param>
    /// <param name="title">A book title.</param>
    /// <param name="publisher">A book publisher.</param>
    /// <param name="isbn">A book ISBN.</param>
    public BookStoreItem(string authorName, string title, string publisher, string isbn)
    {
        if (string.IsNullOrWhiteSpace(authorName))
        {
            throw new ArgumentException("Author name cannot be null or empty.", nameof(authorName));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be null or empty.", nameof(title));
        }

        if (string.IsNullOrWhiteSpace(publisher))
        {
            throw new ArgumentException("Publisher cannot be null or empty.", nameof(publisher));
        }

        if (!ValidateIsbnFormat(isbn) || !ValidateIsbnChecksum(isbn))
        {
            throw new ArgumentException("Invalid ISBN.", nameof(isbn));
        }

        this.authorName = authorName;
        this.Title = title;
        this.Publisher = publisher;
        this.Isbn = isbn;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BookStoreItem"/> class with the specified <paramref name="authorName"/>, <paramref name="isni"/>, <paramref name="title"/>, <paramref name="publisher"/> and <paramref name="isbn"/>.
    /// </summary>
    /// <param name="authorName">A book author's name.</param>
    /// <param name="isni">A book author's ISNI.</param>
    /// <param name="title">A book title.</param>
    /// <param name="publisher">A book publisher.</param>
    /// <param name="isbn">A book ISBN.</param>
    public BookStoreItem(string authorName, string isni, string title, string publisher, string isbn)
        : this(authorName, title, publisher, isbn)
    {
        if (!ValidateIsni(isni))
        {
            throw new ArgumentException("Invalid ISNI.", nameof(isni));
        }

        this.isni = isni;
        this.hasIsni = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BookStoreItem"/> class with the specified <paramref name="authorName"/>, <paramref name="title"/>, <paramref name="publisher"/> and <paramref name="isbn"/>, <paramref name="published"/>, <paramref name="bookBinding"/>, <paramref name="price"/>, <paramref name="currency"/> and <paramref name="amount"/>.
    /// </summary>
    /// <param name="authorName">A book author's name.</param>
    /// <param name="title">A book title.</param>
    /// <param name="publisher">A book publisher.</param>
    /// <param name="isbn">A book ISBN.</param>
    /// <param name="published">A book publishing date.</param>
    /// <param name="bookBinding">A book binding type.</param>
    /// <param name="price">An amount of money that a book costs.</param>
    /// <param name="currency">A price currency.</param>
    /// <param name="amount">An amount of books in the store's stock.</param>
    public BookStoreItem(string authorName, string title, string publisher, string isbn, DateTime? published, string bookBinding = "", decimal price = 0m, string currency = "USD", int amount = 0)
        : this(authorName, title, publisher, isbn)
    {
        this.Published = published;
        this.BookBinding = bookBinding;
        this.Price = price;
        this.Currency = currency;
        this.Amount = amount;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BookStoreItem"/> class with the specified <paramref name="authorName"/>, <paramref name="isni"/>, <paramref name="title"/>, <paramref name="publisher"/> and <paramref name="isbn"/>, <paramref name="published"/>, <paramref name="bookBinding"/>, <paramref name="price"/>, <paramref name="currency"/> and <paramref name="amount"/>.
    /// </summary>
    /// <param name="authorName">A book author's name.</param>
    /// <param name="isni">A book author's ISNI.</param>
    /// <param name="title">A book title.</param>
    /// <param name="publisher">A book publisher.</param>
    /// <param name="isbn">A book ISBN.</param>
    /// <param name="published">A book publishing date.</param>
    /// <param name="bookBinding">A book binding type.</param>
    /// <param name="price">An amount of money that a book costs.</param>
    /// <param name="currency">A price currency.</param>
    /// <param name="amount">An amount of books in the store's stock.</param>
    public BookStoreItem(string authorName, string isni, string title, string publisher, string isbn, DateTime? published, string bookBinding = "", decimal price = 0m, string currency = "USD", int amount = 0)
        : this(authorName, isni, title, publisher, isbn)
    {
        this.Published = published;
        this.BookBinding = bookBinding;
        this.Price = price;
        this.Currency = currency;
        this.Amount = amount;
    }

    /// <summary>
    /// Gets a book author's name.
    /// </summary>
    public string AuthorName => this.authorName;

    /// <summary>
    /// Gets an International Standard Name Identifier (ISNI) that uniquely identifies a book author.
    /// </summary>
    public string? Isni => this.isni;

    /// <summary>
    /// Gets a value indicating whether an author has an International Standard Name Identifier (ISNI).
    /// </summary>
    public bool HasIsni => this.hasIsni;

    /// <summary>
    /// Gets a book title.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// Gets a book publisher.
    /// </summary>
    public string Publisher { get; private set; }

    /// <summary>
    /// Gets a book International Standard Book Number (ISBN).
    /// </summary>
    public string Isbn { get; private set; }

    /// <summary>
    /// Gets or sets a book publishing date.
    /// </summary>
    public DateTime? Published { get; set; }

    /// <summary>
    /// Gets or sets a book binding type.
    /// </summary>
    public string BookBinding { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets an amount of money that a book costs.
    /// </summary>
    public decimal Price
    {
        get
        {
            return this.price;
        }

        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Price cannot be less than zero.");
            }

            this.price = value;
        }
    }

    /// <summary>
    /// Gets or sets a price currency.
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
    /// Gets or sets an amount of books in the store's stock.
    /// </summary>
    public int Amount
    {
        get
        {
            return this.amount;
        }

        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Amount cannot be less than zero.");
            }

            this.amount = value;
        }
    }

    /// <summary>
    /// Gets a <see cref="Uri"/> to the contributor's page at the isni.org website.
    /// </summary>
    /// <returns>A <see cref="Uri"/> to the contributor's page at the isni.org website.</returns>
    public Uri GetIsniUri()
    {
        if (!this.hasIsni)
        {
            throw new InvalidOperationException("ISNI is not set.");
        }

        return new Uri($"https://isni.org/isni/{this.isni}");
    }

    /// <summary>
    /// Gets an <see cref="Uri"/> to the publication page on the isbnsearch.org website.
    /// </summary>
    /// <returns>an <see cref="Uri"/> to the publication page on the isbnsearch.org website.</returns>
    public Uri GetIsbnSearchUri()
    {
        return new Uri($"https://isbnsearch.org/isbn/{this.Isbn}");
    }

    /// <summary>
    /// Returns the string that represents a current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        string isniString = this.hasIsni ? this.isni! : "ISNI IS NOT SET";

        // "N2" делает красиво: 123456789 -> 123,456,789.00
        string formattedPrice = this.Price.ToString("N2", CultureInfo.InvariantCulture);

        // Склеиваем цену и валюту
        string priceAndCurrency = $"{formattedPrice} {this.Currency}";

        // Если в получившейся строке есть запятая, берем ВСЁ в кавычки
        if (priceAndCurrency.Contains(','))
        {
            priceAndCurrency = $"\"{priceAndCurrency}\"";
        }

        return $"{this.Title}, {this.authorName}, {isniString}, {priceAndCurrency}, {this.Amount}";
    }

    private static bool ValidateIsni(string? isni)
    {
        if (string.IsNullOrWhiteSpace(isni) || isni.Length != 16)
        {
            return false;
        }

        foreach (char c in isni)
        {
            if (!char.IsDigit(c) && c != 'X')
            {
                return false;
            }
        }

        return true;
    }

    private static bool ValidateIsbnFormat(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn) || isbn.Length != 10)
        {
            return false;
        }

        foreach (char c in isbn)
        {
            if (!char.IsDigit(c) && c != 'X')
            {
                return false;
            }
        }

        return true;
    }

    private static bool ValidateIsbnChecksum(string isbn)
    {
        if (!ValidateIsbnFormat(isbn))
        {
            return false;
        }

        int sum = 0;
        for (int i = 0; i < 10; i++)
        {
            char c = isbn[i];
            int value = (c == 'X') ? 10 : (c - '0');
            sum += value * (10 - i);
        }

        return sum % 11 == 0;
    }

    private static void ThrowExceptionIfCurrencyIsNotValid(string currency)
    {
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
        {
            throw new ArgumentException("Currency must have exactly 3 characters.", nameof(currency));
        }

        foreach (char c in currency)
        {
            if (!char.IsLetter(c))
            {
                throw new ArgumentException("Currency must contain only letters.", nameof(currency));
            }
        }
    }
}
