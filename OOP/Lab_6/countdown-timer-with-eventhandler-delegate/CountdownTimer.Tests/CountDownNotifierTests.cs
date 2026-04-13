using CountdownTimer.EventArgsClasses;
using CountdownTimer.Factories;
using NUnit.Framework;

namespace CountdownTimer.Tests;

public class CountDownNotifierTests
{
    private CountdownNotifierFactory countDownNotifierFactory = null!;
    private TimerFactory timerFactory = null!;

    [SetUp]
    public void Setup()
    {
        this.countDownNotifierFactory = new CountdownNotifierFactory();
        this.timerFactory = new TimerFactory();
    }

    [TestCase("pie", 10)]
    [TestCase("cookies", 5)]
    [TestCase("pizza", 1)]
    public void Run_ValidTimer_AllEventsWorkAsExpected(string name, int totalTicks)
    {
        var timer = this.timerFactory.CreateTimer(name, totalTicks);
        var notifier = this.countDownNotifierFactory.CreateNotifierForTimer(timer);

        void TimerStarted(object? sender, StartedEventArgs e)
        {
            Assert.That(e!.Name, Is.EqualTo(name));
            Assert.That(e.Ticks, Is.EqualTo(totalTicks));
            Console.WriteLine($"Start timer '{e.Name}', total {e.Ticks} ticks");
        }

        void TimerStopped(object? sender, StoppedEventArgs e)
        {
            Assert.That(e!.Name, Is.EqualTo(name));
            Console.WriteLine($"Stop timer '{e.Name}'");
        }

        var remainsTicks = totalTicks;

        void TimerTick(object? sender, TickEventArgs e)
        {
            remainsTicks -= 1;
            Assert.That(e!.Name, Is.EqualTo(name));
            Assert.That(e.Ticks, Is.EqualTo(remainsTicks));
            Console.WriteLine($"Timer '{e.Name}', remains {e.Ticks} ticks");
        }

        notifier.Init(TimerStarted, TimerStopped, TimerTick);
        notifier.Run();

        Assert.That(remainsTicks, Is.EqualTo(0));
    }

    [TestCase("pie", 10)]
    [TestCase("cookies", 5)]
    [TestCase("pizza", 1)]
    public void Run_NullDelegates_TimerIsWorking(string name, int totalTicks)
    {
        var timer = this.timerFactory.CreateTimer(name, totalTicks);
        var notifier = this.countDownNotifierFactory.CreateNotifierForTimer(timer);

        Assert.DoesNotThrow(() =>
        {
            notifier.Init(null, null, null);
            notifier.Run();
        });
    }

    [Test]
    public void Ctor_TimerIsNull_ThrowsArgumentNullException()
    {
        _ = Assert.Throws<ArgumentNullException>(() => this.countDownNotifierFactory.CreateNotifierForTimer(null));
    }
}
