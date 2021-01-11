using System;
using System.Collections.Generic;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics.Builders
{
  public class MetricLineBuilder
  {
    public string Measurement { get; }
    public Dictionary<string, string> Tags { get; }
    public Dictionary<string, object> Fields { get; }
    public DateTime? UtcTimestamp { get; set; }

    // Constructors
    public MetricLineBuilder(string measurement)
    {
      // TODO: [TESTS] (MetricLineBuilder.MetricLineBuilder) Add tests
      Measurement = measurement;
      Tags = new Dictionary<string, string>();
      Fields = new Dictionary<string, object>();
      UtcTimestamp = null;
    }


    // Builder methods
    public MetricLineBuilder WithTag(string name, string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithTag) Add tests
      Tags[MetricUtils.CleanTagName(name)] = MetricUtils.CleanTagValue(value, skipToLower);
      return this;
    }

    public MetricLineBuilder WithTag(string name, int value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithTag) Add tests
      Tags[MetricUtils.CleanTagName(name)] = value.ToString("D");
      return this;
    }

    public MetricLineBuilder WithTag(string name, long value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithTag) Add tests
      Tags[MetricUtils.CleanTagName(name)] = value.ToString("D");
      return this;
    }

    public MetricLineBuilder WithTag(string name, bool value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithTag) Add tests
      Tags[MetricUtils.CleanTagName(name)] = value ? "true" : "false";
      return this;
    }

    public MetricLineBuilder WithField(string name, double value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithField) Add tests
      Fields[MetricUtils.CleanFieldName(name)] = value;
      return this;
    }

    public MetricLineBuilder WithField(string name, int value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithField) Add tests
      Fields[MetricUtils.CleanFieldName(name)] = value;
      return this;
    }

    public MetricLineBuilder WithField(string name, long value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithField) Add tests
      Fields[MetricUtils.CleanFieldName(name)] = value;
      return this;
    }

    public MetricLineBuilder WithField(string name, bool value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithField) Add tests
      Fields[MetricUtils.CleanFieldName(name)] = value;
      return this;
    }

    public IMetricTimingToken WithTiming(string name = null)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithTiming) Add tests
      if (string.IsNullOrWhiteSpace(name))
        name = "value";

      return new MetricTimingToken(this, name);
    }


    // Build()
    public LineProtocolPoint Build(DateTime? utcTimestamp = null)
    {
      // TODO: [TESTS] (MetricLineBuilder.Build) Add tests
      if (utcTimestamp.HasValue)
        UtcTimestamp = utcTimestamp.Value;

      if (!UtcTimestamp.HasValue)
        throw new Exception("UtcTimestamp is required!");

      return new LineProtocolPoint(Measurement, Fields, Tags, UtcTimestamp);
    }
  }
}
