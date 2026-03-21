using BankSystem.Generators;
using NUnit.Framework;

namespace BankSystem.Tests.GeneratorTests;

[TestFixture]
public sealed class SimpleGeneratorTests
{
    private SimpleGenerator generator = null!;

    [SetUp]
    public void SetUp()
    {
        this.generator = SimpleGenerator.Instance;
    }

    [Test]
    public void Instance_AlwaysReturnSameInstance() => Assert.That(this.generator == SimpleGenerator.Instance);
}
