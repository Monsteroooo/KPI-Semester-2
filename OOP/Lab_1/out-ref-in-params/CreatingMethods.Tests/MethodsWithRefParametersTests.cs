using NUnit.Framework;
using static CreatingMethods.MethodsWithRefParameters;

namespace RefOutParams.Tests;

[TestFixture]
public class MethodsWithRefParametersTests
{
    [TestCase(true, ExpectedResult = true)]
    [TestCase(false, ExpectedResult = false)]
    public bool ReturnParameterValueAndSetParameterToDefaultValue_ReturnsBoolean(bool boolValue)
    {
        bool result = ReturnParameterValueAndSetParameterToDefaultValue(boolValue: ref boolValue);

        Assert.That(boolValue, Is.EqualTo(default(bool)));

        return result;
    }

    [TestCase('a', ExpectedResult = 'a')]
    [TestCase('A', ExpectedResult = 'A')]
    [TestCase('z', ExpectedResult = 'z')]
    [TestCase('Z', ExpectedResult = 'Z')]
    public char ReturnParameterValueAndSetParameterToDefaultValue_ReturnsChar(char charValue)
    {
        char result = ReturnParameterValueAndSetParameterToDefaultValue(charValue: ref charValue);

        Assert.That(charValue, Is.EqualTo(default(char)));

        return result;
    }

    [TestCase(0, ExpectedResult = 0f)]
    [TestCase(0.01f, ExpectedResult = 0.01f)]
    [TestCase(-0.01f, ExpectedResult = -0.01f)]
    [TestCase(float.MinValue, ExpectedResult = float.MinValue)]
    [TestCase(float.MaxValue, ExpectedResult = float.MaxValue)]
    public float ReturnParameterValueAndSetParameterToDefaultValue_ReturnsFloat(float floatValue)
    {
        float result = ReturnParameterValueAndSetParameterToDefaultValue(floatValue: ref floatValue);

        Assert.That(floatValue, Is.EqualTo(default(float)));

        return result;
    }

    [TestCase(0, ExpectedResult = 0)]
    [TestCase(1, ExpectedResult = 1)]
    [TestCase(-1, ExpectedResult = -1)]
    [TestCase(int.MinValue, ExpectedResult = int.MinValue)]
    [TestCase(int.MaxValue, ExpectedResult = int.MaxValue)]
    public int ReturnParameterValueAndSetParameterToDefaultValue_ReturnsInt(int intValue)
    {
        int result = ReturnParameterValueAndSetParameterToDefaultValue(intValue: ref intValue);

        Assert.That(intValue, Is.EqualTo(default(int)));

        return result;
    }

    [TestCase(0L, ExpectedResult = 0L)]
    [TestCase(1L, ExpectedResult = 1L)]
    [TestCase(-1L, ExpectedResult = -1L)]
    [TestCase(long.MinValue, ExpectedResult = long.MinValue)]
    [TestCase(long.MaxValue, ExpectedResult = long.MaxValue)]
    public long ReturnParameterValueAndSetParameterToDefaultValue_ReturnsLong(long longValue)
    {
        long result = ReturnParameterValueAndSetParameterToDefaultValue(longValue: ref longValue);

        Assert.That(longValue, Is.EqualTo(default(long)));

        return result;
    }
}
