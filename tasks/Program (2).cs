using System;

//Задана квадратна матриця розмірності н цілочисельна. Елементи матриці зненеровані випадковим чином у діапазоні від 0 до номера 9. Знайти суму елементів її контуру.
//Відобразити матрицю і суму елементів.

namespace Project;

public class Task
{
    public int n;
    public Random random = new Random();
    public int[,] num = new int[0, 0];

    public Task(int n)
    {
        this.n = n;
        num = new int[n, n];
        for (int i = 0; i < num.GetLength(0); i++)
        {
            for (int j = 0; j < num.GetLength(1); j++)
            {
                num[i, j] = random.Next(0, 10);
            }
        }
    }

    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        Task task = new Task(n);
        int result = task.Sum();
        task.PrintMatrix();
        Console.WriteLine($"Сума елементів контуру: {result}");
    }

    public int Sum()
    {
        int sum = 0;
        for (int i = 0; i < num.GetLength(0); i++)
        {
            for (int j = 0; j < num.GetLength(1); j++)
            {
                if (i == 0 || i == num.GetLength(0) - 1 || j == 0 || j == num.GetLength(1) - 1)
                {
                    sum += num[i, j];
                }
            }
        }

        return sum;
    }

    public void PrintMatrix()
    {
        for (int i = 0; i < num.GetLength(0); i++)
        {
            for (int j = 0; j < num.GetLength(1); j++)
            {
                Console.Write($"{num[i, j]} ");
            }

            Console.WriteLine();
        }
    }
}
