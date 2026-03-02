namespace ClassesDemo;

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
