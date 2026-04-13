namespace GenericSequenceGenerator;

public class IntegerSequenceGenerator : SequenceGenerator<int>
{
    public IntegerSequenceGenerator(int previous, int current)
    : base(previous, current)
    {
    }

    protected override int GetNext()
    {
       return (6 * this.Current) - (8 * this.Previous);
    }
}
