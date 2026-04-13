using CountdownTimer.Implementation;
using CountdownTimer.Interfaces;

namespace CountdownTimer.Factories;

/// <summary>
/// Implements the factory method pattern
/// <see>
///     <cref>https://en.wikipedia.org/wiki/Factory_method_pattern</cref>
/// </see>
/// for creating an object of the class that implements the <see cref="ICountdownNotifier"/> interface.
/// </summary>
public class CountdownNotifierFactory
{
    /// <summary>
    /// Create an object of the class that implements the <see cref="ICountdownNotifier"/> interface.
    /// </summary>
    /// <param name="timer">A reference to a class CustomTimer.</param>
    /// <returns>A reference to an object of the class that implements the <see cref="ICountdownNotifier"/> interface.</returns>
    /// <exception cref="ArgumentNullException">When timer is null.</exception>
    public ICountdownNotifier CreateNotifierForTimer(Timer? timer)
    {
        ArgumentNullException.ThrowIfNull(timer, nameof(timer));
        return new CountdownNotifier(timer);
    }
}
