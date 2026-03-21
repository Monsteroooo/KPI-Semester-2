using System.Globalization;
using BankSystem.Helpers;

namespace BankSystem.Generators;

public sealed class SimpleGenerator : IUniqueNumberGenerator
{
    private int lastNumber = 1234567890;

    static SimpleGenerator()
    {
        Instance = new SimpleGenerator();
    }

    private SimpleGenerator()
    {
    }

    public static SimpleGenerator Instance { get; }

    public string Generate()
    {
        this.lastNumber++;
        return this.lastNumber.ToString(CultureInfo.InvariantCulture).GenerateHash();
    }
}