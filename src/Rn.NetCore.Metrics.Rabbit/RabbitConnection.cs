using Rn.NetCore.Common.Logging;

namespace Rn.NetCore.Metrics.Rabbit
{
  public interface IRabbitConnection
  {
  }

  public class RabbitConnection : IRabbitConnection
  {
    private readonly ILoggerAdapter<RabbitConnection> _logger;

    public RabbitConnection(ILoggerAdapter<RabbitConnection> logger)
    {
      _logger = logger;
    }
  }
}
