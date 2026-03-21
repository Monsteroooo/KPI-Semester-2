using NUnit.Framework;
using static CreatingMethods.MethodsWithInParameters;

namespace CreatingMethods.Tests;

[TestFixture]
public class MethodsWithInParametersTests
{
    [TestCase(true, ExpectedResult = true)]
    [TestCase(false, ExpectedResult = false)]
    public bool ReturnParameterValueAndSetParameterToDefaultValue_ReturnsBoolean(bool boolValue)
    {
        bool result = ReturnDefaultValueWithoutChangingParameter(boolValue: boolValue);

        Assert.That(result, Is.EqualTo(default(bool)));

        return boolValue;
    }

    [TestCase('a', ExpectedResult = 'a')]
    [TestCase('A', ExpectedResult = 'A')]
    [TestCase('z', ExpectedResult = 'z')]
    [TestCase('Z', ExpectedResult = 'Z')]
    public char ReturnParameterValueAndSetParameterToDefaultValue_ReturnsChar(char charValue)
    {
        char result = ReturnDefaultValueWithoutChangingParameter(charValue: in charValue);

        Assert.That(result, Is.EqualTo(default(char)));

        return charValue;
    }

    [TestCase(0, ExpectedResult = 0f)]
    [TestCase(0.01f, ExpectedResult = 0.01f)]
    [TestCase(-0.01f, ExpectedResult = -0.01f)]
    [TestCase(float.MinValue, ExpectedResult = float.MinValue)]
    [TestCase(float.MaxValue, ExpectedResult = float.MaxValue)]
    public float ReturnParameterValueAndSetParameterToDefaultValue_ReturnsFloat(float floatValue)
    {
        float result = ReturnDefaultValueWithoutChangingParameter(floatValue: in floatValue);

        Assert.That(result, Is.EqualTo(default(float)));

        return floatValue;
    }

    [TestCase(0, ExpectedResult = 0)]
    [TestCase(1, ExpectedResult = 1)]
    [TestCase(-1, ExpectedResult = -1)]
    [TestCase(int.MinValue, ExpectedResult = int.MinValue)]
    [TestCase(int.MaxValue, ExpectedResult = int.MaxValue)]
    public int ReturnParameterValueAndSetParameterToDefaultValue_ReturnsInt(int intValue)
    {
        int result = ReturnDefaultValueWithoutChangingParameter(intValue: in intValue);

        Assert.That(result, Is.EqualTo(default(int)));

        return intValue;
    }

    [TestCase(0L, ExpectedResult = 0L)]
    [TestCase(1L, ExpectedResult = 1L)]
    [TestCase(-1L, ExpectedResult = -1L)]
    [TestCase(long.MinValue, ExpectedResult = long.MinValue)]
    [TestCase(long.MaxValue, ExpectedResult = long.MaxValue)]
    public long ReturnParameterValueAndSetParameterToDefaultValue_ReturnsLong(long longValue)
    {
        long result = ReturnDefaultValueWithoutChangingParameter(longValue: in longValue);

        Assert.That(result, Is.EqualTo(default(long)));

        return longValue;
    }
}