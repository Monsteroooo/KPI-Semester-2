using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Multidimensional1Tasks.Tests
{
    [TestFixture]
    public class TaskTests
    {
        [TestCaseSource(nameof(GetTestingDataForTask1))]
        public void Task1ReturnsCorrectValue(int[,] array, int expected)
        {
             var actual = SimpleTasks.Task1(array);
             Assert.AreEqual(expected, actual, "Task1 returns incorrect value.");
        }

        [TestCaseSource(nameof(GetTestingDataForTask2))]
        public void Task2ReturnsCorrectValue(int[,] array, int expected)
        {
            var actual = SimpleTasks.Task2(array);
            Assert.AreEqual(expected, actual, "Task2 returns incorrect value.");
        }

        [TestCaseSource(nameof(GetTestingDataForTask3))]
        public void Task3ReturnsCorrectValue(int[,] array, int[,] expected)
        {
            SimpleTasks.Task3(array);
            Assert.AreEqual(expected, array, "Task3 returns incorrect value.");
        }

        private static IEnumerable<object[]> GetTestingDataForTask1()
        {
            yield return new object[]
            {
                new int[,] { { 1, 2, -3 }, { 3, 3, -5 } }, 9,
            };
            yield return new object[]
            {
                new int[,] { { -10, 0 }, { 5, 3 } },
                8,
            };
            yield return new object[]
            {
                new int[,] { { 5, 11, -9 }, { 5, 3, -5 } },
                24,
            };
        }

        private static IEnumerable<object[]> GetTestingDataForTask2()
        {
            yield return new object[]
            {
                new int[,] { { 1, 0, 5 }, { 1, 3, 3 }, { 1, 5, 6 } },
                8,
            };
            yield return new object[]
            {
                new int[,] { { -1, 0, 3 }, { 5, 9, 0 }, { 4, 3, 3 } },
                3,
            };
            yield return new object[]
            {
                new int[,] { { 0, 1, 3 }, { 4, 3, 3 }, { 3, 5, 3 } },
                7,
            };
        }

        private static IEnumerable<object[]> GetTestingDataForTask3()
        {
            yield return new object[]
            {
                new int[,] { { 1, 0, 5 }, { 1, 3, 3 }, { 1, 5, 6 } },
                new int[,] { { 1, 0, 5 }, { 0, 2, 2 }, { -1, 3, 4 } },
            };
            yield return new object[]
            {
                new int[,] { { -1, 0, 3 }, { 5, 9, 0 },  { 4, 3, 3 } },
                new int[,] { { -1, 0, 3 }, { 4, 8, -1 }, { 2, 1, 1 } },
            };
            yield return new object[]
            {
                new int[,] { { 0, 1, 3 }, { 4, 3, 3 }, { 3, 5, 3 } },
                new int[,] { { 0, 1, 3 }, { 3, 2, 2 }, { 1, 3, 1 } },
            };
        }
    }
}
