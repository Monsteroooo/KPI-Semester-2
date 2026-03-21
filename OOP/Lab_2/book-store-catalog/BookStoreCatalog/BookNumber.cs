namespace BookStoreCatalog;

/// <summary>
/// Represents an International Standard Book Number (ISBN).
/// </summary>
public class BookNumber
{
    private const string BaseSearchUri = "https://isbnsearch.org/isbn/";
    private readonly string code;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookNumber"/> class with the specified 10-digit ISBN <paramref name="isbnCode"/>.
    /// </summary>
    /// <param name="isbnCode">A 10-digit ISBN code.</param>
    /// <exception cref="ArgumentNullException">a code argument is null.</exception>
    /// <exception cref="ArgumentException">a code argument is invalid or a code has wrong checksum.</exception>
    public BookNumber(string isbnCode)
    {
        ArgumentNullException.ThrowIfNull(isbnCode);

        if (!ValidateCode(isbnCode) || !ValidateChecksum(isbnCode))
        {
            throw new ArgumentException("isbnCode invalid", nameof(isbnCode));
        }

        this.code = isbnCode;
    }

    /// <summary>
    /// Gets a 10-digit ISBN code.
    /// </summary>
    public string Code
    {
        get
        {
            return this.code;
        }
    }

    /// <summary>
    /// Gets an <see cref="Uri"/> to the publication page on the isbnsearch.org website.
    /// </summary>
    /// <returns>an <see cref="Uri"/> to the publication page on the isbnsearch.org website.</returns>
    public Uri GetSearchUri()
    {
        return new Uri(BaseSearchUri + this.code);
    }

    /// <summary>
    /// Returns the string that represents a current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return this.code;
    }

    private static bool ValidateCode(string isbnCode)
    {
        if (isbnCode.Length != 10)
        {
            return false;
        }

        foreach (char c in isbnCode)
        {
            if (!char.IsDigit(c) && c != 'X')
            {
                return false;
            }
        }

        return true;
    }

    private static bool ValidateChecksum(string isbnCode)
    {
        int sum = 0;
        for (int i = 0; i < 10; i++)
        {
            char c = isbnCode[i];
            if (c == 'X')
            {
                sum += 10 * (10 - i);
            }
            else
            {
                sum += (c - '0') * (10 - i);
            }
        }

        return sum % 11 == 0;
    }
}
