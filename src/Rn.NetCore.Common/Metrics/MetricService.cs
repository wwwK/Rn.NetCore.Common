using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Extensions;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.Common.Metrics.Builders;
using Rn.NetCore.Common.Metrics.Configuration;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.Common.Metrics.Exceptions;
using Rn.NetCore.Common.Metrics.Interfaces;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics
{
  public interface IMetricService
  {
    bool Enabled { get; }

    void SubmitPoint(MetricBuilder builder);
    Task SubmitPointAsync(MetricBuilder builder);
    void SubmitPoint(LineProtocolPoint point);
    Task SubmitPointAsync(LineProtocolPoint point);
  }

  public class MetricService : IMetricService
  {
    public bool Enabled { get; }

    private readonly ILoggerAdapter<MetricService> _logger;
    private readonly IDateTimeAbstraction _dateTime;
    private readonly List<IMetricOutput> _outputs;
    private readonly MetricsConfig _config;
    private readonly string _devPlaceholderValue;

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
      _config = MapConfiguration(configuration);
      Enabled = _config.Enabled;
      if (!Enabled) return;

      // Set the "_devPlaceholderValue" value
      _devPlaceholderValue = _config.DevelopmentMode
        ? _config.DevelopmentModeValue
        : _config.ProductionModeValue;

      // Check to see if there are any enabled outputs
      _outputs = outputs.Where(x => x.Enabled).ToList();
      if (_outputs.Count > 0)
        return;

      // No enabled outputs, disabled metrics service
      _logger.Warning("There are no enabled metric outputs, disabling metrics service");
      Enabled = false;
    }


    // Interface methods
    public void SubmitPoint(MetricBuilder builder)
    {
      // TODO: [TESTS] (MetricService.SubmitPoint) Add tests
      if (!Enabled) { return; }

      SubmitPointAsync(builder)
        .ConfigureAwait(false)
        .GetAwaiter()
        .GetResult();
    }

    public async Task SubmitPointAsync(MetricBuilder builder)
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

      // Ensure that we are working with a valid metric point
      FinalizePoint(point);
      if (!point.IsValidPoint)
        return;

      // Dispatch the point to all enabled outputs
      foreach (var output in _outputs)
      {
        await output.SubmitPoint(point);
      }
    }


    // Point processing related methods
    private string WorkMetricBuilderType(LineProtocolPoint point)
    {
      // TODO: [TESTS] (MetricService.WorkMetricBuilderType) Add tests
      if (!point.Tags.ContainsKey(CoreMetricTag.Source))
        return "unknown";

      var safeSource = point.Tags[CoreMetricTag.Source].LowerTrim();

      if (safeSource.IgnoreCaseEquals(MetricSource.RepoCall.ToString("G")))
        return "repo_call";

      if (safeSource.IgnoreCaseEquals(MetricSource.ServiceCall.ToString("G")))
        return "service_call";

      if (safeSource.IgnoreCaseEquals(MetricSource.CronJob.ToString("G")))
        return "cron_job";

      if (safeSource.IgnoreCaseEquals(MetricSource.ApiCall.ToString("G")))
        return "api_call";

      if (safeSource.IgnoreCaseEquals(MetricSource.Client.ToString("G")))
        return "client";

      if (safeSource.IgnoreCaseEquals(MetricSource.Custom.ToString("G")))
        return "custom";

      // Unknown / Unhandled metric source
      _logger.Warning("Unable to resolve '{type}' to known measurement - using {final}",
        point.Tags[CoreMetricTag.Source],
        safeSource
      );

      return safeSource;
    }

    private string ResolveMeasurement(LineProtocolPoint point)
    {
      // TODO: [TESTS] (MetricService.ResolveMeasurement) Add tests
      var resolved = _config.MeasurementTemplate;
      var key = point.Measurement.Split(':')[1];

      if (string.IsNullOrWhiteSpace(key))
        return resolved;

      // Have we cached the key before?
      if (_config.Measurements.ContainsKey(key))
        return _config.Measurements[key];

      // Generate a NEW measurement for the requested key and log
      resolved = resolved.Replace("{type}", WorkMetricBuilderType(point));
      _logger.Error("Unable to resolve template '{name}' (using: {fallback})", key, resolved);
      _config.Measurements[key] = resolved;

      return resolved;
    }

    private void FinalizePoint(LineProtocolPoint point)
    {
      // TODO: [TESTS] (MetricService.FinalizePoint) Add tests
      var finalMeasurement = point.Measurement;

      if (point.Measurement.Contains("resolve:"))
        finalMeasurement = ResolveMeasurement(point);

      if (point.GetMetricSource() == MetricSource.Custom && finalMeasurement.Contains("resolve:"))
      {
        _logger.Warning(
          "Unable to resolve custom metric point '{measurement}' - dropping",
          point.Measurement
        );

        return;
      }

      finalMeasurement = finalMeasurement
        .Replace("{app}", _config.ApplicationName)
        .Replace("{mode}", _devPlaceholderValue);

      point.ReplaceMeasurement(finalMeasurement, true);
    }


    // Configuration related methods
    private MetricsConfig MapConfiguration(IConfiguration configuration)
    {
      // TODO: [TESTS] (MetricService.MapConfiguration) Add tests
      var boundConfiguration = new MetricsConfig();
      var configurationSection = configuration.GetSection(ConfigKey);

      if (configurationSection.Exists())
      {
        configurationSection.Bind(boundConfiguration);
      }
      else
      {
        _logger.Warning("Unable to find configuration section {s}, metrics disabled", ConfigKey);
      }

      ProcessConfiguration(boundConfiguration);
      return boundConfiguration;
    }

    private static void ProcessConfiguration(MetricsConfig config)
    {
      // TODO: [TESTS] (MetricService.ProcessConfiguration) Add tests
      if (!config.Enabled)
        return;

      // Ensure that we have an ApplicationName to work with
      if (string.IsNullOrWhiteSpace(config.ApplicationName))
        throw new MetricConfigException("ApplicationName is required");
      config.ApplicationName = config.ApplicationName.LowerTrim();

      // Ensure that we have a measurement template defined
      if (string.IsNullOrWhiteSpace(config.MeasurementTemplate))
        config.MeasurementTemplate = "{app}/{mode}/{type}";

      // Ensure that "DevelopmentModeValue" and "ProductionModeValue" are set
      if (string.IsNullOrWhiteSpace(config.DevelopmentModeValue))
        config.DevelopmentModeValue = "dev";

      if (string.IsNullOrWhiteSpace(config.ProductionModeValue))
        config.ProductionModeValue = "production";

      config.DevelopmentModeValue = config.DevelopmentModeValue.LowerTrim();
      config.ProductionModeValue = config.ProductionModeValue.LowerTrim();

      // Ensure that all "MetricSource" values have an associated template
      SetMeasurement(config, MetricSource.RepoCall, "repo_call");
      SetMeasurement(config, MetricSource.ServiceCall, "service_call");
      SetMeasurement(config, MetricSource.CronJob, "cron_job");
      SetMeasurement(config, MetricSource.ApiCall, "api_call");
      SetMeasurement(config, MetricSource.Client, "client");
      SetMeasurement(config, MetricSource.Custom, "custom");
    }

    private static void SetMeasurement(MetricsConfig config, MetricSource source, string builderType)
    {
      // TODO: [TESTS] (MetricService.SetMeasurement) Add tests
      var key = source.ToString("G");

      if (config.Measurements.ContainsKey(key))
        return;

      config.Measurements[key] = config.MeasurementTemplate
        .Replace("{type}", builderType);
    }
  }
}
