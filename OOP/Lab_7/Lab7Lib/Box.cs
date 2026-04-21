namespace Lab7Lib
{
    public class Box
    {
        public int Value { get; set; }
        public Box Next { get; set; }

        public Box(int value)
        {
            Value = value;
            Next = null;
        }
    }
}
