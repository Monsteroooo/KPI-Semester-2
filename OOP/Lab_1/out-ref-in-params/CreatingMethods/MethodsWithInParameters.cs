namespace CreatingMethods;

/// <summary>
/// Provides methods that demonstrate using the 'in' parameters.
/// </summary>
public static class MethodsWithInParameters
{
    /// <summary>
    /// Returns the default value without changing the <paramref name="boolValue"/> parameter.
    /// </summary>
    /// <param name="boolValue">The boolean value passed by reference as in parameter.</param>
    /// <returns>The default bool value.</returns>
    public static bool ReturnDefaultValueWithoutChangingParameter(in bool boolValue)
    {
        return default;
    }

    /// <summary>
    /// Returns the default value without changing the <paramref name="charValue"/> parameter.
    /// </summary>
    /// <param name="charValue">The char value passed by reference as in parameter.</param>
    /// <returns>The default char value.</returns>
    public static char ReturnDefaultValueWithoutChangingParameter(in char charValue)
    {
        return default;
    }

    /// <summary>
    /// Returns the default value without changing the <paramref name="floatValue"/> parameter.
    /// </summary>
    /// <param name="floatValue">The float value passed by reference as in parameter.</param>
    /// <returns>The default float value.</returns>
    public static float ReturnDefaultValueWithoutChangingParameter(in float floatValue)
    {
        return default;
    }

    /// <summary>
    /// Returns the default value without changing the <paramref name="intValue"/> parameter.
    /// </summary>
    /// <param name="intValue">The int value passed by reference as in parameter.</param>
    /// <returns>The default int value.</returns>
    public static int ReturnDefaultValueWithoutChangingParameter(in int intValue)
    {
        return default;
    }

    /// <summary>
    /// Returns the default value without changing the <paramref name="longValue"/> parameter.
    /// </summary>
    /// <param name="longValue">The long value passed by reference as in parameter.</param>
    /// <returns>The default long value.</returns>
    public static long ReturnDefaultValueWithoutChangingParameter(in long longValue)
    {
        return default;
    }
}
