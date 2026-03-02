namespace CreatingMethods;

/// <summary>
/// Provides methods that return multiple values using tuples.
/// </summary>
public static class MethodsWithTuples
{
    /// <summary>
    /// Returns true and false boolean values.
    /// </summary>
    /// <returns>A tuple containins boolean values.</returns>
    public static (bool trueValue, bool falseValue) ReturnBoolValues()
    {
        return (true, false);
    }

    /// <summary>
    /// Returns 'a' and 'A' char values.
    /// </summary>
    /// <returns>A tuple containing 'a' and 'A' values.</returns>
    public static (char lowerCaseA, char upperCaseA) ReturnCharValues()
    {
        return ('a', 'A');
    }

    /// <summary>
    /// Returns the minimum and maximum float values.
    /// </summary>
    /// <returns>A tuple containing the minimum and maximum float values.</returns>
    public static (float minFloatValue, float maxFloatValue) ReturnFloatValues()
    {
        return (float.MinValue, float.MaxValue);
    }

    /// <summary>
    /// Returns the minimum and maximum int values.
    /// </summary>
    /// <returns>A tuple containing the minimum and maximum int values.</returns>
    public static (int minIntValue, int maxIntValue) ReturnIntValues()
    {
        return (int.MinValue, int.MaxValue);
    }

    /// <summary>
    /// Returns the minimum and maximum long values.
    /// </summary>
    /// <returns>A tuple containing the minimum and maximum long values.</returns>
    public static (long minLongValue, long maxLongValue) ReturnLongValues()
    {
        return (long.MinValue, long.MaxValue);
    }

    /// <summary>
    /// Returns a string and its length.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>A tuple containing the input string and its length.</returns>
    public static (string stringValue, int length) ReturnStringAndLength(string input)
    {
        return (input, input.Length);
    }

    /// <summary>
    /// Returns a DateTime and its corresponding DayOfWeek.
    /// </summary>
    /// <param name="date">The input DateTime.</param>
    /// <returns>A tuple containing the DateTime and its DayOfWeek.</returns>
    public static (DateTime date, DayOfWeek dayOfWeek) ReturnDateAndDay(DateTime date)
    {
        return (date, date.DayOfWeek);
    }
}
