using System;
using BankSystem.Helpers;

namespace BankSystem.Generators;

public class GuidGenerator : IUniqueNumberGenerator
{
    public string Generate()
    {
        return Guid.NewGuid().ToString().GenerateHash();
    }
}
