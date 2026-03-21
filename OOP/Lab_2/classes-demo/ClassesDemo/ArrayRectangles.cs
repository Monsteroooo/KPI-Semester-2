namespace ClassesDemo;

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
