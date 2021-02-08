using Rn.NetCore.Common.Wrappers;

namespace Rn.NetCore.Common.Factories
{
  public interface ITimerFactory
  {
    IStopwatch NewStopwatch();
  }

  public class TimerFactory : ITimerFactory
  {
    public IStopwatch NewStopwatch()
    {
      // TODO: [TESTS] (TimerFactory.NewStopwatch) Add tests
      return new StopwatchWrapper();
    }
  }
}
