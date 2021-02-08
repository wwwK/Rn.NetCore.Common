using System.Diagnostics;

namespace Rn.NetCore.Common.Wrappers
{
  public interface IStopwatch
  {
    long ElapsedMilliseconds { get; }
    void Start();
    void Stop();
  }

  public class StopwatchWrapper : IStopwatch
  {
    public long ElapsedMilliseconds => _stopwatch.ElapsedMilliseconds;

    private readonly Stopwatch _stopwatch;

    public StopwatchWrapper()
    {
      _stopwatch = new Stopwatch();
    }

    public void Start()
    {
      _stopwatch?.Start();
    }

    public void Stop()
    {
      _stopwatch?.Stop();
    }
  }
}
