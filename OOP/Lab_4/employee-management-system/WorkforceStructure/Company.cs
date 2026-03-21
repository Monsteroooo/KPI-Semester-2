namespace WorkforceStructure;

/// <summary>
/// Represents a company with a list of employees.
/// </summary>
public class Company
{
private readonly IList<Employee> employees;

/// <summary>
/// Initializes a new instance of the <see cref="Company"/> class with the specified employees.
/// </summary>
/// <param name="employees">The employees of the company.</param>
/// <exception cref="ArgumentNullException">Thrown when employees is null.</exception>
public Company(IList<Employee> employees)
{
    ArgumentNullException.ThrowIfNull(employees);
    this.employees = employees;
}

/// <summary>
/// Distributes the specified bonus amount to all employees.
/// </summary>
/// <param name="companyBonus">The bonus amount to distribute to each employee.</param>
/// <exception cref="ArgumentOutOfRangeException">Thrown when companyBonus is less than zero.</exception>
public void DistributeBonuses(decimal bonus)
{
    foreach (Employee e in this.employees)
    {
        e.AssignBonus(bonus);
    }
}

/// <summary>
/// Calculates the total amount to be paid to all employees, including bonuses.
/// </summary>
/// <returns>The total amount to be paid to all employees.</returns>
public decimal CalculateTotalPayroll()
{
    decimal sum = 0;
    foreach (Employee e in this.employees)
    {
        sum += e.CalculateTotalPay();
    }

    return sum;
}

/// <summary>
/// Gets the name of the employee with the highest salary.
/// </summary>
/// <returns>The name of the employee with the highest salary.</returns>
/// <exception cref="InvalidOperationException">Thrown when there are no employees in the company.</exception>
public string GetHighestPaidEmployeeName()
{
    if (this.employees.Count == 0)
    {
        throw new InvalidOperationException(nameof(this.employees.Count));
    }

    string emp = string.Empty;
    decimal cur = 0;
    foreach (Employee e in this.employees)
    {
        if (cur < e.CalculateTotalPay())
        {
            cur = e.CalculateTotalPay();
            emp = e.Name;
        }
    }

    return emp;
}
}
