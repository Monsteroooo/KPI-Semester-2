using System;

namespace Functions;

public static class DemoFunction
{
    public static bool IsSorted(int[] array, SortOrder order)
    {
        if (array == null || array.Length <= 1)
        {
            return true;
        }

        for (int i = 0; i < array.Length - 1; i++)
        {
            if (order == SortOrder.Ascending && array[i] > array[i + 1])
            {
                return false;
            }

            if (order == SortOrder.Descending && array[i] < array[i + 1])
            {
                return false;
            }
        }

        return true;
    }

    public static void Transform(int[] array, SortOrder order)
    {
        if (array == null)
        {
            return;
        }

        if (IsSorted(array, order))
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i] + i;
            }
        }
    }

    public static double MultArithmeticElements(double a, double t, int n)
    {
        if (n <= 0)
        {
            return 0;
        }

        double result = 1;
        double currentElement = a;

        for (int i = 0; i < n; i++)
        {
            result *= currentElement;
            currentElement += t;
        }

        return result;
    }

    public static double SumGeometricElements(double a, double t, double alim)
    {
        double sum = 0;
        double currentElement = a;

        while (currentElement > alim)
        {
            sum += currentElement;
            currentElement *= t;
        }

        return sum;
    }
}