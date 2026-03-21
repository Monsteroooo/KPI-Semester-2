using System.Globalization;
using System.Text.RegularExpressions;

namespace BankSystem.Helpers;

public static class ValidatorService
{
    public static bool IsCurrencyValid(this string? currencyCode)
    {
        if (string.IsNullOrWhiteSpace(currencyCode))
        {
            return false;
        }

        foreach (var culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
        {
            var region = new RegionInfo(culture.Name);
            if (region.ISOCurrencySymbol == currencyCode)
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsEmailValid(this string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
