namespace Exceptions.Tests;

[TestFixture]
public class ThrowingArgumentTests
{
    [TestCase(1)]
    [TestCase(3)]
    [TestCase(5)]
    [TestCase(7)]
    [TestCase(9)]
    [TestCase(11)]
    [TestCase(-1)]
    [TestCase(-3)]
    [TestCase(-5)]
    [TestCase(-7)]
    [TestCase(-9)]
    [TestCase(-11)]
    public void CheckParameterAndThrowException1_I_IsOdd_ThrowsArgumentException(int i)
    {
        _ = Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                _ = ThrowingArgument.CheckParameterAndThrowException1(i);
            }
            catch (ArgumentException e)
            {
                Assert.That(e.ParamName, Is.EqualTo(nameof(i)));
                Assert.That(e.Message, Is.EqualTo("i should not be odd. (Parameter 'i')"));
                throw;
            }
        });
    }

    [TestCase(0)]
    [TestCase(2)]
    [TestCase(4)]
    [TestCase(6)]
    [TestCase(8)]
    [TestCase(10)]
    [TestCase(-2)]
    [TestCase(-4)]
    [TestCase(-6)]
    [TestCase(-8)]
    [TestCase(-10)]
    public void CheckParameterAndThrowException1_I_IsEven_ReturnsInteger(int i)
    {
        int actualResult = ThrowingArgument.CheckParameterAndThrowException1(i);

        Assert.That(actualResult, Is.EqualTo(i));
    }

    [Test]
    public void CheckParameterAndThrowException2_I_IsZero_ThrowsArgumentException()
    {
        _ = Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                _ = ThrowingArgument.CheckParameterAndThrowException2(0);
            }
            catch (ArgumentException e)
            {
                Assert.That(e.ParamName, Is.EqualTo("s"));
                Assert.That(e.Message, Is.EqualTo("s should not equals zero. (Parameter 's')"));
                throw;
            }
        });
    }

    [TestCase(1u)]
    [TestCase(2u)]
    [TestCase(3u)]
    [TestCase(4u)]
    [TestCase(5u)]
    [TestCase(6u)]
    [TestCase(7u)]
    [TestCase(8u)]
    [TestCase(9u)]
    [TestCase(10u)]
    public void CheckParameterAndThrowException2_S_IsNotZero_ReturnsInteger(uint s)
    {
        int actualResult = ThrowingArgument.CheckParameterAndThrowException2((ushort)s);

        Assert.That(actualResult, Is.EqualTo(s));
    }

    [TestCase(0L)]
    [TestCase(2L)]
    [TestCase(4L)]
    [TestCase(6L)]
    [TestCase(8L)]
    [TestCase(10L)]
    [TestCase(-2L)]
    [TestCase(-4L)]
    [TestCase(-6L)]
    [TestCase(-8L)]
    [TestCase(-10L)]
    public void CheckParameterAndThrowException3_I_IsEven_ThrowsArgumentException(long l)
    {
        _ = Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                _ = ThrowingArgument.CheckParameterAndThrowException3(l);
            }
            catch (ArgumentException e)
            {
                Assert.That(e.ParamName, Is.EqualTo(nameof(l)));
                Assert.That(e.Message, Is.EqualTo("l should not be even. (Parameter 'l')"));
                throw;
            }
        });
    }

    [TestCase(1L)]
    [TestCase(3L)]
    [TestCase(5L)]
    [TestCase(7L)]
    [TestCase(9L)]
    [TestCase(11L)]
    [TestCase(-1L)]
    [TestCase(-3L)]
    [TestCase(-5L)]
    [TestCase(-9L)]
    [TestCase(-11L)]
    public void CheckParameterAndThrowException3_I_IsOdd_ReturnsInteger(long l)
    {
        long actualResult = ThrowingArgument.CheckParameterAndThrowException3(l);

        Assert.That(actualResult, Is.EqualTo(l));
    }

    [Test]
    public void CheckParameterAndThrowException4_FloatsIsNull_ThrowsArgumentNullException()
    {
        _ = Assert.Throws<ArgumentNullException>(() =>
        {
            try
            {
                _ = ThrowingArgument.CheckParameterAndThrowException4(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.That(e.ParamName, Is.EqualTo("floats"));
                throw;
            }
        });
    }

    [Test]
    public void CheckParameterAndThrowException4_FloatsIsEmpty_ThrowsArgumentNullException()
    {
        _ = Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                _ = ThrowingArgument.CheckParameterAndThrowException4(Array.Empty<float>());
            }
            catch (ArgumentException e)
            {
                Assert.That(e.ParamName, Is.EqualTo("floats"));
                Assert.That(e.Message, Is.EqualTo("floats array has no elements. (Parameter 'floats')"));
                throw;
            }
        });
    }

    [TestCase(new float[] { 1 }, ExpectedResult = 1)]
    [TestCase(new float[] { 1, 2 }, ExpectedResult = 2)]
    [TestCase(new float[] { 1, 2, 3 }, ExpectedResult = 3)]
    public int CheckParameterAndThrowException4_ArrayWithOneElement_ReturnsArrayLength(float[] floats)
    {
        return ThrowingArgument.CheckParameterAndThrowException4(floats);
    }

    [Test]
    public void CheckParameterAndThrowException5_S_IsNull_ThrowsArgumentNullException()
    {
        _ = Assert.Throws<ArgumentNullException>(() =>
        {
            try
            {
                _ = ThrowingArgument.CheckParameterAndThrowException5(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.That(e.ParamName, Is.EqualTo("s"));
                throw;
            }
        });
    }

    [Test]
    public void CheckParameterAndThrowException4_S_IsEmpty_ThrowsArgumentNullException()
    {
        _ = Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                _ = ThrowingArgument.CheckParameterAndThrowException5(string.Empty);
            }
            catch (ArgumentException e)
            {
                Assert.That(e.ParamName, Is.EqualTo("s"));
                Assert.That(e.Message, Is.EqualTo("s string is empty. (Parameter 's')"));
                throw;
            }
        });
    }

    [TestCase("a", ExpectedResult = 1)]
    [TestCase("ab", ExpectedResult = 2)]
    [TestCase("abc", ExpectedResult = 3)]
    public int CheckParameterAndThrowException5_ArrayWithOneElement_ReturnsStringLength(string s)
    {
        return ThrowingArgument.CheckParameterAndThrowException5(s);
    }
}
