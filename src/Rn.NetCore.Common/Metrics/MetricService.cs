using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.Common.Metrics.Builders;
using Rn.NetCore.Common.Metrics.Configuration;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics
{
  public interface IMetricService
  {
    void SubmitPoint(MetricLineBuilder builder);
    Task SubmitPointAsync(MetricLineBuilder builder);
    void SubmitPoint(LineProtocolPoint point);
    Task SubmitPointAsync(LineProtocolPoint point);
  }

  public class MetricService : IMetricService
  {
    private readonly ILoggerAdapter<MetricService> _logger;
    private readonly IDateTimeAbstraction _dateTime;

    public MetricService(
      ILoggerAdapter<MetricService> logger,
      IDateTimeAbstraction dateTime,
      IConfiguration configuration)
    {
      _logger = logger;
      _dateTime = dateTime;
    }


    // Interface methods
    public void SubmitPoint(MetricLineBuilder builder)
    {
      // TODO: [TESTS] (MetricService.SubmitPoint) Add tests
      SubmitPointAsync(builder).ConfigureAwait(false).GetAwaiter().GetResult();
    }

    public async Task SubmitPointAsync(MetricLineBuilder builder)
    {
      // TODO: [TESTS] (MetricService.SubmitPointAsync) Add tests
      await SubmitPointAsync(builder.Build(_dateTime.UtcNow));
    }

    public void SubmitPoint(LineProtocolPoint point)
    {
      // TODO: [TESTS] (MetricService.SubmitPoint) Add tests
      SubmitPointAsync(point).ConfigureAwait(false).GetAwaiter().GetResult();
    }

    public async Task SubmitPointAsync(LineProtocolPoint point)
    {
      // TODO: [TESTS] (MetricService.SubmitPointAsync) Add tests
      await Task.CompletedTask;
    }


    // Internal methods
    private MetricsConfig MapConfiguration(IConfiguration configuration)
    {
      // TODO: [TESTS] (MetricService.MapConfiguration) Add tests
      const string SectionName = "Rn:Metrics";
      var configSection = configuration.GetSection("Rn:Metrics");

      if (!configSection.Exists())
      {
        _logger.Error("Unable to find configuration section {s}, metrics disabled", SectionName);
        return new MetricsConfig();
      }

      var config = new MetricsConfig();
      configSection.Bind(config);
      return config;
    }
  }
}
