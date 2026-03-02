using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multidimensional1Tasks
{
    public static class SimpleTasks
    {
        public static int Task1(int[,] array)
        {
            int sum = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] > 0)
                    {
                        sum += array[i, j];
                    }
                }
            }

            return sum;
        }

        public static int Task2(int[,] array)
        {
            int sum = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = i + 1; j < array.GetLength(1); j++)
                {
                    if (array[i, j] > 0)
                    {
                        sum += array[i, j];
                    }
                }
            }

            return sum;
        }

        public static void Task3(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = array[i, j] - i;
                }
            }
        }
    }
}
