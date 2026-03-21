using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassDemo;

public class Rectangle
{
    private double sideA;
    private double sideB;

    public Rectangle(double a, double b)
    {
        this.sideA = a;
        this.sideB = b;
    }

    public Rectangle(double a)
    : this(a, 5)
    {
    }

    public Rectangle()
    : this(4, 3)
    {
    }

    public double GetSideA()
    {
        return this.sideA;
    }

    public double GetSideB()
    {
        return this.sideB;
    }

    public double Area()
    {
        return this.sideA * this.sideB;
    }

    public double Perimeter()
    {
        return 2 * (this.sideA + this.sideB);
    }

    public bool IsSquare()
    {
        if (this.sideA == this.sideB)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ReplaceSides()
    {
        double temp = this.sideA;
        this.sideA = this.sideB;
        this.sideB = temp;
    }
}

public class ArrayRectangles
{
    private readonly Rectangle[] rectangleArray;

    public ArrayRectangles(int n)
    {
        this.rectangleArray = new Rectangle[n];
    }

    public ArrayRectangles(Rectangle[] rectangles)
    {
        this.rectangleArray = rectangles;
    }

    public bool AddRectangle(Rectangle rectangle)
    {
        for (int i = 0; i < this.rectangleArray.Length; i++)
        {
            if (this.rectangleArray[i] == null)
            {
                this.rectangleArray[i] = rectangle;
                return true;
            }
        }

        return false;
    }

    public int NumberMaxArea()
    {
        double max = 0;
        double temp = 0;
        int res = 0;
        for (int i = 0; i < this.rectangleArray.Length; i++)
        {
            if (this.rectangleArray[i] != null)
            {
                temp = this.rectangleArray[i].Area();
            }

            if (temp > max)
            {
                max = temp;
            }
        }

        for (int i = 0; i < this.rectangleArray.Length; i++)
        {
            if (this.rectangleArray[i].Area() == max)
            {
                res += 1;
            }
        }

        return res;
    }

    public int NumberMinPerimeter()
    {
        double min = double.MaxValue;
        double temp = 0;
        int res = 0;
        for (int i = 0; i < this.rectangleArray.Length; i++)
        {
            if (this.rectangleArray[i] != null)
            {
                temp = this.rectangleArray[i].Perimeter();
            }

            if (temp < min)
            {
                min = temp;
                res = i;
            }
        }

        return res;
    }

    public int NumberSquare()
    {
        int res = 0;
        for (int i = 0; i < this.rectangleArray.Length; i++)
        {
            if (this.rectangleArray[i] != null && this.rectangleArray[i].IsSquare())
            {
                res += 1;
            }
        }

        return res;
    }
}
