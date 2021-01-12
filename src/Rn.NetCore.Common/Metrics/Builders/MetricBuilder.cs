using System;
using System.Collections.Generic;
using System.Globalization;
using Rn.NetCore.Common.Extensions;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics.Builders
{
  public class MetricBuilder
  {
    public string Measurement { get; }
    public Dictionary<string, string> Tags { get; }
    public Dictionary<string, object> Fields { get; }
    public DateTime? UtcTimestamp { get; set; }

    // Constructor
    public MetricBuilder(MetricSource source, string measurement = null)
    {
      // TODO: [TESTS] (MetricBuilder.MetricBuilder) Add tests
      if (string.IsNullOrWhiteSpace(measurement))
        measurement = $"resolve:{source:G}";

      Measurement = measurement;
      UtcTimestamp = null;

      Tags = new Dictionary<string, string>
      {
        { CoreMetricTag.Source, source.ToString("G").LowerTrim() },
        { CoreMetricTag.Success, "true" },
        { CoreMetricTag.HasException, "false" },
        { CoreMetricTag.ExceptionName, string.Empty }
      };

      Fields = new Dictionary<string, object>
      {
        { CoreMetricField.Value, (long) 0 },
        { CoreMetricField.CallCount, 1 }
      };
    }


    // Builder methods
    public MetricBuilder WithTag(string tag, string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (MetricBuilder.WithTag) Add tests
      Tags[MetricUtils.CleanTagName(tag)] = MetricUtils.CleanTagValue(value, skipToLower);
      return this;
    }

    public MetricBuilder WithTag(string tag, bool value)
    {
      // TODO: [TESTS] (MetricBuilder.WithTag) Add tests
      Tags[MetricUtils.CleanTagName(tag)] = value ? "true" : "false";
      return this;
    }

    public MetricBuilder WithField(string field, int value)
    {
      // TODO: [TESTS] (MetricBuilder.WithField) Add tests
      Fields[MetricUtils.CleanFieldName(field)] = value;
      return this;
    }

    public MetricBuilder WithField(string field, long value)
    {
      // TODO: [TESTS] (MetricBuilder.WithField) Add tests
      Fields[MetricUtils.CleanFieldName(field)] = value;
      return this;
    }

    public IMetricTimingToken WithTiming(string name = null)
    {
      // TODO: [TESTS] (MetricBuilder.WithTiming) Add tests
      if (string.IsNullOrWhiteSpace(name))
        name = CoreMetricField.Value;
    
      return new MetricTimingToken(this, name);
    }


    // Helper methods
    public void WithCustomTag(int number, object value, bool skipToLower = false)
    {
      // TODO: [TESTS] (MetricBuilder.WithCustomTag) Add tests
      WithTag($"custom_tag{number}", CastTagValue(value), skipToLower);
    }

    public void WithCustomInt(int number, int value)
    {
      // TODO: [TESTS] (MetricBuilder.WithCustomInt) Add tests
      WithField($"custom_int{number}", value);
    }

    public void WithCustomLong(int number, long value)
    {
      // TODO: [TESTS] (MetricBuilder.WithCustomLong) Add tests
      WithField($"custom_long{number}", value);
    }

    public void WithException(Exception ex)
    {
      // TODO: [TESTS] (MetricBuilder.WithException) Add tests
      WithTag(CoreMetricTag.Success, false);
      WithTag(CoreMetricTag.HasException, true);
      WithTag(CoreMetricTag.ExceptionName, ex.GetType().Name, true);
    }


    // Build()
    public LineProtocolPoint Build(DateTime? overrideDate = null)
    {
      // TODO: [TESTS] (CustomMetricBuilder.Build) Add tests
      if (overrideDate.HasValue)
        UtcTimestamp = overrideDate;

      if (!UtcTimestamp.HasValue)
        throw new Exception($"No date provided for measurement: {Measurement}");

      return new LineProtocolPoint(Measurement, Fields, Tags, UtcTimestamp);
    }


    // Internal methods
    private static string CastTagValue(object value)
    {
      // TODO: [TESTS] (MetricBuilder.CastTagValue) Add tests
      return value switch
      {
        string sValue => sValue,
        bool bValue => bValue ? "true" : "false",
        int iValue => iValue.ToString("D"),
        long lValue => lValue.ToString("D"),
        double dValue => dValue.ToString(CultureInfo.InvariantCulture),
        DateTime dateValue => dateValue.ToString("u"),
        _ => null
      };
    }
  }
}
