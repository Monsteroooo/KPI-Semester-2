using NUnit.Framework;
using static CreatingMethods.MethodsWithOutParameters;

namespace RefOutParams.Tests;

[TestFixture]
public class MethodsWithOutParametersTests
{
    [Test]
    public void ReturnValues_ReturnBooleans()
    {
        ReturnValues(out bool trueValue, out var falseValue);

        Assert.That(trueValue, Is.EqualTo(true));
        Assert.That(falseValue, Is.EqualTo(false));
    }

    [Test]
    public void ReturnValues_ReturnLowercaseUppercaseChars()
    {
        ReturnValues(out char lowerCaseA, out char upperCaseA);

        Assert.That(lowerCaseA, Is.EqualTo('a'));
        Assert.That(upperCaseA, Is.EqualTo('A'));
    }

    [Test]
    public void ReturnValues_ReturnMinMaxFloats()
    {
        ReturnValues(out float minFloatValue, out float maxFloatValue);

        Assert.That(minFloatValue, Is.EqualTo(float.MinValue));
        Assert.That(maxFloatValue, Is.EqualTo(float.MaxValue));
    }

    [Test]
    public void ReturnValues_ReturnMinMaxIntegers()
    {
        ReturnValues(out int minIntValue, out int maxIntValue);

        Assert.That(minIntValue, Is.EqualTo(int.MinValue));
        Assert.That(maxIntValue, Is.EqualTo(int.MaxValue));
    }

    [Test]
    public void ReturnValues_ReturnMinMaxLongIntegers()
    {
        ReturnValues(out long minLongValue, out long maxLongValue);

        Assert.That(minLongValue, Is.EqualTo(long.MinValue));
        Assert.That(maxLongValue, Is.EqualTo(long.MaxValue));
    }
}
