using CountdownTimer.EventArgsClasses;
using CountdownTimer.Interfaces;

namespace CountdownTimer.Implementation;

/// <inheritdoc/>
public class CountdownNotifier : ICountdownNotifier
{
    private readonly Timer timer;

    public CountdownNotifier(Timer timer)
    {
        ArgumentNullException.ThrowIfNull(timer, nameof(timer));
        this.timer = timer;
    }

    /// <inheritdoc/>
    public void Init(EventHandler<StartedEventArgs>? startHandler, EventHandler<StoppedEventArgs>? stopHandler, EventHandler<TickEventArgs>? tickHandler)
    {
        if (startHandler is not null)
        {
            this.timer.Started += startHandler;
        }

        if (stopHandler is not null)
        {
            this.timer.Stopped += stopHandler;
        }

        if (tickHandler is not null)
        {
            this.timer.Tick += tickHandler;
        }
    }

    /// <inheritdoc/>
    public void Run()
    {
        this.timer.Run();
    }
}
