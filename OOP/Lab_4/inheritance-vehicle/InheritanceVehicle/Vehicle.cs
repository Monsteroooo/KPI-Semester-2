namespace InheritanceVehicle;

public class Vehicle
{
    private readonly int maxSpeed;

    protected Vehicle(string name, int maxSpeed)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentOutOfRangeException.ThrowIfNegative(maxSpeed);
        this.maxSpeed = maxSpeed;
        this.Name = name;
    }

    public int MaxSpeed
    {
        get
        {
            return this.maxSpeed;
        }
    }

    protected string Name { get; set; }
}
