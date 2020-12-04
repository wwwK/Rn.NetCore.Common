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
    private readonly MetricsConfig _config;

    public MetricService(
      ILoggerAdapter<MetricService> logger,
      IDateTimeAbstraction dateTime,
      IConfiguration configuration)
    {
      _logger = logger;
      _dateTime = dateTime;

      _config = MapConfiguration(configuration);
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
      const string sectionName = "RnCore:Metrics";
      var configSection = configuration.GetSection(sectionName);

      if (!configSection.Exists())
      {
        _logger.Error("Unable to find configuration section {s}, metrics disabled", sectionName);
        return new MetricsConfig();
      }

      var config = new MetricsConfig();
      configSection.Bind(config);
      return config;
    }
  }
}
