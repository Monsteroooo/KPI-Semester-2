using NUnit.Framework;

namespace OneDimArrays1.Tests
{
    [TestFixture]
    public class UnitTests
    {
        [TestCase(new[] { 3, 7, 16, 12, 5, 22, 100 }, 12, 138)]
        [TestCase(new[] { 8, 20, 35, 1, 0, -5 }, 10, 55)]
        [TestCase(new[] { 8, -12, 7, 22, 9 }, -1, 46)]
        [TestCase(new[] { -5, 0, -2, 3 }, -2, 3)]
        public void Task1ReturnsCorrectValue(int[] array, int a, int expected)
        {
            var actual = SimpleTasks.Task1(array, a);

            Assert.AreEqual(expected, actual, "Task1 returns incorrect value.");
        }

        [TestCase(new[] { -2, 1, -10, 5, 22, 7, 0 }, -12)]
        [TestCase(new[] { -5, 0, -11, -7, 5 }, -23)]
        [TestCase(new[] { 0, -1, 14, -7, 5, -46 }, -54)]
        [TestCase(new[] { -233, -1026, 45, 0 }, -1259)]
        public void Task2ReturnsCorrectValue(int[] array, int expected)
        {
            var actual = SimpleTasks.Task2(array);

            Assert.AreEqual(expected, actual, "Task2 returns incorrect value.");
        }

        [TestCase(new[] { -2, 1, -10, 5, 22, 7, 0 }, 10)]
        [TestCase(new[] { -6, 0, -11, -7, 5 }, -6)]
        [TestCase(new[] { 0, -1, 14, -7, 6, -46 }, 20)]
        [TestCase(new[] { 30, -1026, 46, 0 }, 76)]
        public void Task3ReturnsCorrectValue(int[] array, int expected)
        {
            var actual = SimpleTasks.Task3(array);

            Assert.AreEqual(expected, actual, "Task3 returns incorrect value.");
        }
    }
}
