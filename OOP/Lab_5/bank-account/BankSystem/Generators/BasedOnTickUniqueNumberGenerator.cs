using System.Globalization;
using BankSystem.Helpers;

namespace BankSystem.Generators;

public class BasedOnTickUniqueNumberGenerator : IUniqueNumberGenerator
{
    public BasedOnTickUniqueNumberGenerator(DateTime startingPoint)
    {
        this.StartingPoint = startingPoint;
    }

    public DateTime StartingPoint { get; }

    public string Generate()
    {
        long ticks = (DateTime.Now - this.StartingPoint).Ticks;
        return ticks.ToString(CultureInfo.InvariantCulture).GenerateHash();
    }
}