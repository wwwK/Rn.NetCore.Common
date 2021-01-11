using System;
using System.Collections.Generic;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics.Builders
{
  public abstract class BaseMetricLineBuilder
  {
    public string Measurement { get; }
    public Dictionary<string, string> Tags { get; }
    public Dictionary<string, object> Fields { get; }
    public DateTime? UtcTimestamp { get; set; }

    private readonly List<int> _customInt = new List<int> { 0, 0, 0, 0, 0, 0 };
    private int _resultsCount, _queryCount;

    // Constructors
    protected BaseMetricLineBuilder(string measurement, MetricSource source)
    {
      // TODO: [TESTS] (MetricLineBuilder.MetricLineBuilder) Add tests
      if (string.IsNullOrWhiteSpace(measurement))
        measurement = $"resolve:{source:G}";

      Measurement = measurement;
      UtcTimestamp = null;

      Tags = new Dictionary<string, string>
      {
        {"custom_tag1", string.Empty},
        {"custom_tag2", string.Empty},
        {"custom_tag3", string.Empty},
        {"custom_tag4", string.Empty},
        {"custom_tag5", string.Empty},
        {"succeeded", "true"},
        {"has_exception", "false"},
        {"exception_name", string.Empty}
      };

      Fields = new Dictionary<string, object>
      {
        {CoreMetricField.Value, (long) 0},
        {CoreMetricField.CallCount, 1},
        {"result_count", 0},
        {"query_count", 0},
        {"custom_timing1", (long) 0},
        {"custom_timing2", (long) 0},
        {"custom_timing3", (long) 0},
        {"custom_timing4", (long) 0},
        {"custom_timing5", (long) 0},
        {"custom_timing6", (long) 0},
        {"custom_int1", _customInt[0]},
        {"custom_int2", _customInt[1]},
        {"custom_int3", _customInt[2]},
        {"custom_int4", _customInt[3]},
        {"custom_int5", _customInt[4]},
        {"custom_int6", _customInt[5]},
        {"custom_double1", (double) 0},
        {"custom_double2", (double) 0},
        {"custom_double3", (double) 0},
      };
    }


    // Generic builder methods
    public BaseMetricLineBuilder WithTag(string name, string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithTag) Add tests
      Tags[MetricUtils.CleanTagName(name)] = MetricUtils.CleanTagValue(value, skipToLower);
      return this;
    }

    public BaseMetricLineBuilder WithTag(string name, int value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithTag) Add tests
      Tags[MetricUtils.CleanTagName(name)] = value.ToString("D");
      return this;
    }

    public BaseMetricLineBuilder WithTag(string name, long value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithTag) Add tests
      Tags[MetricUtils.CleanTagName(name)] = value.ToString("D");
      return this;
    }

    public BaseMetricLineBuilder WithTag(string name, bool value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithTag) Add tests
      Tags[MetricUtils.CleanTagName(name)] = value ? "true" : "false";
      return this;
    }

    public BaseMetricLineBuilder WithField(string name, double value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithField) Add tests
      Fields[MetricUtils.CleanFieldName(name)] = value;
      return this;
    }

    public BaseMetricLineBuilder WithField(string name, int value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithField) Add tests
      Fields[MetricUtils.CleanFieldName(name)] = value;
      return this;
    }

    public BaseMetricLineBuilder WithField(string name, long value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithField) Add tests
      Fields[MetricUtils.CleanFieldName(name)] = value;
      return this;
    }

    public BaseMetricLineBuilder WithField(string name, bool value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithField) Add tests
      Fields[MetricUtils.CleanFieldName(name)] = value;
      return this;
    }

    public BaseMetricLineBuilder WithException(Exception ex)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithException) Add tests
      WithTag("succeeded", false);
      WithTag("has_exception", true);
      WithTag("exception_name", ex.GetType().Name, true);

      return this;
    }

    public BaseMetricLineBuilder IncrementQueryCount(int amount = 1)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.IncrementQueryCount) Add tests
      _queryCount += amount;
      return this;
    }

    public BaseMetricLineBuilder WithQueryCount(int queryCount)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithQueryCount) Add tests
      _queryCount = queryCount;
      return this;
    }


    // Custom Tag1 methods
    public BaseMetricLineBuilder WithCustomTag1(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTag1) Add tests
      WithTag("custom_tag1", value, skipToLower);
      return this;
    }

    public BaseMetricLineBuilder WithCustomTag1(bool value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTag1) Add tests
      return WithTag("custom_tag1", value);
    }

    public BaseMetricLineBuilder WithCustomTag1(int value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTag1) Add tests
      return WithTag("custom_tag1", value);
    }


    // Custom Tag2 methods
    public BaseMetricLineBuilder WithCustomTag2(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTag2) Add tests
      WithTag("custom_tag2", value, skipToLower);
      return this;
    }

    public BaseMetricLineBuilder WithCustomTag2(bool value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTag2) Add tests
      return WithTag("custom_tag2", value);
    }

    public BaseMetricLineBuilder WithCustomTag2(int value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTag2) Add tests
      return WithTag("custom_tag2", value);
    }


    // Custom Tag3 methods
    public BaseMetricLineBuilder WithCustomTag3(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTag3) Add tests
      WithTag("custom_tag3", value, skipToLower);
      return this;
    }

    public BaseMetricLineBuilder WithCustomTag3(bool value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTag3) Add tests
      return WithTag("custom_tag3", value);
    }

    public BaseMetricLineBuilder WithCustomTag3(int value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTag3) Add tests
      return WithTag("custom_tag3", value);
    }


    // Custom Tag4 methods
    public BaseMetricLineBuilder WithCustomTag4(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTag4) Add tests
      WithTag("custom_tag4", value, skipToLower);
      return this;
    }

    public BaseMetricLineBuilder WithCustomTag4(bool value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTag4) Add tests
      return WithTag("custom_tag4", value);
    }

    public BaseMetricLineBuilder WithCustomTag4(int value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTag4) Add tests
      return WithTag("custom_tag4", value);
    }


    // Custom Tag5 methods
    public BaseMetricLineBuilder WithCustomTag5(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTag5) Add tests
      WithTag("custom_tag5", value, skipToLower);
      return this;
    }

    public BaseMetricLineBuilder WithCustomTag5(bool value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTag5) Add tests
      return WithTag("custom_tag5", value);
    }

    public BaseMetricLineBuilder WithCustomTag5(int value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTag5) Add tests
      return WithTag("custom_tag5", value);
    }


    // Custom Timing methods
    public IMetricTimingToken WithTiming(string name = null)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithTiming) Add tests
      if (string.IsNullOrWhiteSpace(name))
        name = CoreMetricField.Value;

      return new MetricTimingToken(this, name);
    }

    public IMetricTimingToken WithCustomTiming1()
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTiming1) Add tests
      return new MetricTimingToken(this, "custom_timing1");
    }

    public IMetricTimingToken WithCustomTiming2()
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTiming2) Add tests
      return new MetricTimingToken(this, "custom_timing2");
    }

    public IMetricTimingToken WithCustomTiming3()
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTiming3) Add tests
      return new MetricTimingToken(this, "custom_timing3");
    }

    public IMetricTimingToken WithCustomTiming4()
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTiming4) Add tests
      return new MetricTimingToken(this, "custom_timing4");
    }

    public IMetricTimingToken WithCustomTiming5()
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTiming5) Add tests
      return new MetricTimingToken(this, "custom_timing5");
    }

    public IMetricTimingToken WithCustomTiming6()
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomTiming6) Add tests
      return new MetricTimingToken(this, "custom_timing6");
    }


    // Custom Int methods
    public BaseMetricLineBuilder WithCustomInt1(int value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomInt1) Add tests
      _customInt[0] = value;
      return this;
    }

    public BaseMetricLineBuilder IncrementCustomInt1(int amount = 1)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.IncrementCustomInt1) Add tests
      _customInt[0] += amount;
      return this;
    }

    public BaseMetricLineBuilder WithCustomInt2(int value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomInt2) Add tests
      _customInt[1] = value;
      return this;
    }

    public BaseMetricLineBuilder IncrementCustomInt2(int amount = 1)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.IncrementCustomInt2) Add tests
      _customInt[1] += amount;
      return this;
    }

    public BaseMetricLineBuilder WithCustomInt3(int value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomInt3) Add tests
      _customInt[2] = value;
      return this;
    }

    public BaseMetricLineBuilder IncrementCustomInt3(int amount = 1)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.IncrementCustomInt3) Add tests
      _customInt[2] += amount;
      return this;
    }

    public BaseMetricLineBuilder WithCustomInt4(int value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomInt4) Add tests
      _customInt[3] = value;
      return this;
    }

    public BaseMetricLineBuilder IncrementCustomInt4(int amount = 1)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.IncrementCustomInt4) Add tests
      _customInt[3] += amount;
      return this;
    }

    public BaseMetricLineBuilder WithCustomInt5(int value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomInt5) Add tests
      _customInt[4] = value;
      return this;
    }

    public BaseMetricLineBuilder IncrementCustomInt5(int amount = 1)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.IncrementCustomInt5) Add tests
      _customInt[4] += amount;
      return this;
    }

    public BaseMetricLineBuilder WithCustomInt6(int value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomInt6) Add tests
      _customInt[5] = value;
      return this;
    }

    public BaseMetricLineBuilder IncrementCustomInt6(int amount = 1)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.IncrementCustomInt6) Add tests
      _customInt[5] += amount;
      return this;
    }


    // Custom Double methods
    public BaseMetricLineBuilder WithCustomDouble1(double value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomDouble1) Add tests
      WithField("custom_double1", value);
      return this;
    }

    public BaseMetricLineBuilder WithCustomDouble2(double value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomDouble2) Add tests
      WithField("custom_double2", value);
      return this;
    }

    public BaseMetricLineBuilder WithCustomDouble3(double value)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithCustomDouble3) Add tests
      WithField("custom_double3", value);
      return this;
    }


    // Success related methods
    public BaseMetricLineBuilder MarkSuccess(bool success)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.MarkSuccess) Add tests
      return WithTag("succeeded", success);
    }

    public BaseMetricLineBuilder MarkSuccess(int resultCount = -1)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.MarkSuccess) Add tests
      if (resultCount > -1)
        WithResultCount(resultCount);

      return WithTag("succeeded", true);
    }

    public BaseMetricLineBuilder MarkSuccessIfNotNull(object obj)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.MarkSuccessIfNotNull) Add tests
      return WithTag("succeeded", obj != null);
    }

    public BaseMetricLineBuilder MarkFailed(int resultCount = -1)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.MarkFailed) Add tests
      if (resultCount > -1)
        WithResultCount(resultCount);

      return WithTag("succeeded", false);
    }


    // Result counting methods
    public BaseMetricLineBuilder WithResultCount(int resultCount)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.WithResultCount) Add tests
      _resultsCount = resultCount;
      return this;
    }

    public BaseMetricLineBuilder CountResult(object result = null)
    {
      // TODO: [TESTS] (BaseMetricLineBuilder.CountResult) Add tests
      if (result != null)
        _resultsCount += 1;

      return this;
    }


    // Build()
    public LineProtocolPoint Build(DateTime? utcTimestamp = null)
    {
      // TODO: [TESTS] (MetricLineBuilder.Build) Add tests
      if (utcTimestamp.HasValue)
        UtcTimestamp = utcTimestamp.Value;

      if (!UtcTimestamp.HasValue)
        throw new Exception("UtcTimestamp is required!");

      Fields["custom_int1"] = _customInt[0];
      Fields["custom_int2"] = _customInt[1];
      Fields["custom_int3"] = _customInt[2];
      Fields["custom_int4"] = _customInt[3];
      Fields["custom_int5"] = _customInt[4];
      Fields["custom_int6"] = _customInt[5];
      Fields["query_count"] = _queryCount;
      Fields["result_count"] = _resultsCount;

      return new LineProtocolPoint(Measurement, Fields, Tags, UtcTimestamp);
    }
  }
}
