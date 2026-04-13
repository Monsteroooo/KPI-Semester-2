namespace GenericSequenceGenerator;

public abstract class SequenceGenerator<T> : ISequenceGenerator<T>
{
    private T previous;
    private T current;

    protected SequenceGenerator(T previous, T current)
    {
        this.previous = previous;
        this.current = current;
        this.Count = 2;
    }

    public T Previous
        {
            get
            {
                return this.previous;
            }
        }

    public T Current
    {
        get
        {
            return this.current;
        }
    }

    public int Count { get; private set; }

    public T Next
    {
        get
        {
            T nextValue = this.GetNext();
            this.previous = this.Current;
            this.current = nextValue;
            this.Count++;
            return nextValue;
        }
    }

    protected abstract T GetNext();
}
