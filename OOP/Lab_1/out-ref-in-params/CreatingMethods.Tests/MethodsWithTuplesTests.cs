using NUnit.Framework;
using static CreatingMethods.MethodsWithTuples;

namespace RefOutParams.Tests;

[TestFixture]
public class MethodsWithTuplesTests
{
    [Test]
    public void TestReturnBoolValues()
    {
        var result = ReturnBoolValues();
        Assert.That(result == (true, false));
    }

    [Test]
    public void TestReturnCharValues()
    {
        var result = ReturnCharValues();
        Assert.That(result == ('a', 'A'));
    }

    [Test]
    public void TestReturnFloatValues()
    {
        var result = ReturnFloatValues();
        Assert.That(result == (float.MinValue, float.MaxValue));
    }

    [Test]
    public void TestReturnIntValues()
    {
        var result = ReturnIntValues();
        Assert.That(result == (int.MinValue, int.MaxValue));
    }

    [Test]
    public void TestReturnLongValues()
    {
        var result = ReturnLongValues();
        Assert.That(result == (long.MinValue, long.MaxValue));
    }

    [Test]
    public void TestReturnStringAndLength()
    {
        var result = ReturnStringAndLength("Hello");
        Assert.That("Hello", Is.EqualTo(result.stringValue));
        Assert.That(5, Is.EqualTo(result.length));
    }

    [Test]
    public void TestReturnDateAndDay()
    {
        var date = new DateTime(2022, 1, 1);
        var result = ReturnDateAndDay(date);
        Assert.That(date, Is.EqualTo(result.date));
        Assert.That(DayOfWeek.Saturday, Is.EqualTo(result.dayOfWeek));
    }
}
