using CountdownTimer.EventArgsClasses;
using CountdownTimer.Implementation;
using Moq;
using NUnit.Framework;

namespace CountdownTimer.Tests;

public class TimerMockTests
{
    [TestCase("alarm", 1)]
    [TestCase("alarm", 15)]
    public void EventMethodsCallCheck(string timerName, int ticks)
    {
        var timer = new Timer(timerName, ticks);
        var notifierMock = new Mock<CountdownNotifier>(timer);
        var notifier = notifierMock.Object;

        var onStartMock = new Mock<EventHandler<StartedEventArgs>>();
        var onStopMock = new Mock<EventHandler<StoppedEventArgs>>();
        var onTickMock = new Mock<EventHandler<TickEventArgs>>();

        notifier.Init(onStartMock.Object, onStopMock.Object, onTickMock.Object);
        notifier.Run();

        onStartMock.Verify(n => n(It.IsAny<object>(), It.IsAny<StartedEventArgs>()), Times.Once);
        onStopMock.Verify(n => n(It.IsAny<object>(), It.IsAny<StoppedEventArgs>()), Times.Once);
        onTickMock.Verify(n => n(It.IsAny<object>(), It.IsAny<TickEventArgs>()), Times.Exactly(ticks));
    }
}
