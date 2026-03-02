namespace CreatingMethods;

/// <summary>
/// Provides methods that demonstrate the using the 'out' parameters.
/// </summary>
public static class MethodsWithOutParameters
{
    /// <summary>
    /// Returns true and false boolean values.
    /// </summary>
    /// <param name="trueValue">Output parameter that returns true.</param>
    /// <param name="falseValue">Output parameter that returns false.</param>
    public static void ReturnValues(out bool trueValue, out bool falseValue)
    {
        trueValue = true;
        falseValue = false;
    }

    /// <summary>
    /// Returns 'a' and 'A' char values.
    /// </summary>
    /// <param name="lowerCaseA">Output parameter that returns 'a'.</param>
    /// <param name="upperCaseA">Output parameter that returns 'A'.</param>
    public static void ReturnValues(out char lowerCaseA, out char upperCaseA)
    {
        lowerCaseA = 'a';
        upperCaseA = 'A';
    }

    /// <summary>
    /// Returns the minimum and maximum float values.
    /// </summary>
    /// <param name="minFloatValue">Output parameter that returns the minimum float value.</param>
    /// <param name="maxFloatValue">Output parameter that returns the maximum float value.</param>
    public static void ReturnValues(out float minFloatValue, out float maxFloatValue)
    {
        minFloatValue = float.MinValue;
        maxFloatValue = float.MaxValue;
    }

    /// <summary>
    /// Returns the minimum and maximum int values.
    /// </summary>
    /// <param name="minIntValue">Output parameter that returns the minimum int value.</param>
    /// <param name="maxIntValue">Output parameter that returns the maximum int value.</param>
    public static void ReturnValues(out int minIntValue, out int maxIntValue)
    {
        minIntValue = int.MinValue;
        maxIntValue = int.MaxValue;
    }

    /// <summary>
    /// Returns the minimum and maximum long values.
    /// </summary>
    /// <param name="minLongValue">Output parameter that returns the minimum long value.</param>
    /// <param name="maxLongValue">Output parameter that returns the maximum long value.</param>
    public static void ReturnValues(out long minLongValue, out long maxLongValue)
    {
        minLongValue = long.MinValue;
        maxLongValue = long.MaxValue;
    }
}
