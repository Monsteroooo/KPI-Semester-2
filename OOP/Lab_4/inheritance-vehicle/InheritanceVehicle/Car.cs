namespace InheritanceVehicle;

public class Car : Vehicle
{
    public Car(string name, int maxSpeed)
    : base(name, maxSpeed)
    {
    }

    public void SetName(string name)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
        this.Name = name;
    }

    public string GetNewName()
    {
        return this.Name;
    }
}
