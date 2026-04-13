namespace GenericSequenceGenerator;

public class DelegateSequenceGenerator<T> : SequenceGenerator<T>
{
    private readonly Func<T, T, T> generatorFunction;

    public DelegateSequenceGenerator(T previous, T current, Func<T, T, T> generatorFunction)
        : base(previous, current)
    {
        this.generatorFunction = generatorFunction;
    }

    protected override T GetNext()
    {
        return this.generatorFunction(this.Previous, this.Current);
    }
}
