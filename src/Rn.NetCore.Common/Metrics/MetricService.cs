using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.Common.Metrics.Builders;
using Rn.NetCore.Common.Metrics.Configuration;
using Rn.NetCore.Common.Metrics.Interfaces;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics
{
  public interface IMetricService
  {
    bool Enabled { get; }

    void SubmitPoint(MetricLineBuilder builder);
    Task SubmitPointAsync(MetricLineBuilder builder);
    void SubmitPoint(LineProtocolPoint point);
    Task SubmitPointAsync(LineProtocolPoint point);
  }

  public class MetricService : IMetricService
  {
    public bool Enabled { get; }

    private readonly ILoggerAdapter<MetricService> _logger;
    private readonly IDateTimeAbstraction _dateTime;
    private readonly List<IMetricOutput> _outputs;

    public const string ConfigKey = "RnCore:Metrics";

    public MetricService(
      ILoggerAdapter<MetricService> logger,
      IDateTimeAbstraction dateTime,
      IConfiguration configuration,
      IEnumerable<IMetricOutput> outputs)
    {
      // TODO: [TESTS] (MetricService) Add tests
      _logger = logger;
      _dateTime = dateTime;

      // Check to see if metrics are enabled
      var config = MapConfiguration(configuration);
      Enabled = config.Enabled;
      if (!Enabled)
        return;

      // Check to see if there are any enabled outputs
      _outputs = outputs.Where(x => x.Enabled).ToList();
      if (_outputs.Count > 0)
        return;

      // No enabled outputs, disabled metrics service
      _logger.Warning("There are no enabled metric outputs, disabling metrics service");
      Enabled = false;
    }


    // Interface methods
    public void SubmitPoint(MetricLineBuilder builder)
    {
      // TODO: [TESTS] (MetricService.SubmitPoint) Add tests
      if (!Enabled) { return; }

      SubmitPointAsync(builder)
        .ConfigureAwait(false)
        .GetAwaiter()
        .GetResult();
    }

    public async Task SubmitPointAsync(MetricLineBuilder builder)
    {
      // TODO: [TESTS] (MetricService.SubmitPointAsync) Add tests
      if (!Enabled) { return; }

      await SubmitPointAsync(builder.Build(_dateTime.UtcNow));
    }

    public void SubmitPoint(LineProtocolPoint point)
    {
      // TODO: [TESTS] (MetricService.SubmitPoint) Add tests
      if (!Enabled) { return; }

      SubmitPointAsync(point)
        .ConfigureAwait(false)
        .GetAwaiter()
        .GetResult();
    }

    public async Task SubmitPointAsync(LineProtocolPoint point)
    {
      // TODO: [TESTS] (MetricService.SubmitPointAsync) Add tests
      if (!Enabled) { return; }

      foreach (var output in _outputs)
      {
        await output.SubmitPoint(point);
      }
    }


    // Internal methods
    private MetricsConfig MapConfiguration(IConfiguration configuration)
    {
      // TODO: [TESTS] (MetricService.MapConfiguration) Add tests
      var metricsConfig = new MetricsConfig();
      var configSection = configuration.GetSection(ConfigKey);

      if (configSection.Exists())
      {
        configSection.Bind(metricsConfig);
        return metricsConfig;
      }

      _logger.Warning(
        "Unable to find configuration section {s}, metrics disabled",
        ConfigKey
      );

      return metricsConfig;
    }
  }
}
