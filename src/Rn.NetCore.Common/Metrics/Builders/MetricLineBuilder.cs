using System;
using System.Collections.Generic;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics.Builders
{
  public abstract class BaseMetricBuilder
  {
    public string Measurement { get; }
    public Dictionary<string, string> Tags { get; }
    public Dictionary<string, object> Fields { get; }
    public DateTime? UtcTimestamp { get; set; }

    private readonly List<int> _customInt = new List<int> { 0, 0, 0, 0, 0, 0 };
    private readonly List<long> _customLong = new List<long> { 0, 0, 0, 0, 0 };
    private int _resultsCount, _queryCount;

    // Constructors
    protected BaseMetricBuilder(string measurement, MetricSource source)
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
        {"custom_long1", (long) 0},
        {"custom_long2", (long) 0},
        {"custom_long3", (long) 0},
        {"custom_long4", (long) 0},
        {"custom_long5", (long) 0},
      };
    }


    // Generic builder methods
    public BaseMetricBuilder WithTag(string name, string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithTag) Add tests
      Tags[MetricUtils.CleanTagName(name)] = MetricUtils.CleanTagValue(value, skipToLower);
      return this;
    }

    public BaseMetricBuilder WithTag(string name, int value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithTag) Add tests
      Tags[MetricUtils.CleanTagName(name)] = value.ToString("D");
      return this;
    }

    public BaseMetricBuilder WithTag(string name, long value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithTag) Add tests
      Tags[MetricUtils.CleanTagName(name)] = value.ToString("D");
      return this;
    }

    public BaseMetricBuilder WithTag(string name, bool value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithTag) Add tests
      Tags[MetricUtils.CleanTagName(name)] = value ? "true" : "false";
      return this;
    }

    public BaseMetricBuilder WithField(string name, double value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithField) Add tests
      Fields[MetricUtils.CleanFieldName(name)] = value;
      return this;
    }

    public BaseMetricBuilder WithField(string name, int value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithField) Add tests
      Fields[MetricUtils.CleanFieldName(name)] = value;
      return this;
    }

    public BaseMetricBuilder WithField(string name, long value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithField) Add tests
      Fields[MetricUtils.CleanFieldName(name)] = value;
      return this;
    }

    public BaseMetricBuilder WithField(string name, bool value)
    {
      // TODO: [TESTS] (MetricLineBuilder.WithField) Add tests
      Fields[MetricUtils.CleanFieldName(name)] = value;
      return this;
    }

    public BaseMetricBuilder WithException(Exception ex)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithException) Add tests
      WithTag("succeeded", false);
      WithTag("has_exception", true);
      WithTag("exception_name", ex.GetType().Name, true);

      return this;
    }

    public BaseMetricBuilder IncrementQueryCount(int amount = 1)
    {
      // TODO: [TESTS] (BaseMetricBuilder.IncrementQueryCount) Add tests
      _queryCount += amount;
      return this;
    }

    public BaseMetricBuilder WithQueryCount(int queryCount)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithQueryCount) Add tests
      _queryCount = queryCount;
      return this;
    }


    // Custom Tag1 methods
    public BaseMetricBuilder WithCustomTag1(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTag1) Add tests
      WithTag("custom_tag1", value, skipToLower);
      return this;
    }

    public BaseMetricBuilder WithCustomTag1(bool value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTag1) Add tests
      return WithTag("custom_tag1", value);
    }

    public BaseMetricBuilder WithCustomTag1(int value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTag1) Add tests
      return WithTag("custom_tag1", value);
    }


    // Custom Tag2 methods
    public BaseMetricBuilder WithCustomTag2(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTag2) Add tests
      WithTag("custom_tag2", value, skipToLower);
      return this;
    }

    public BaseMetricBuilder WithCustomTag2(bool value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTag2) Add tests
      return WithTag("custom_tag2", value);
    }

    public BaseMetricBuilder WithCustomTag2(int value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTag2) Add tests
      return WithTag("custom_tag2", value);
    }


    // Custom Tag3 methods
    public BaseMetricBuilder WithCustomTag3(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTag3) Add tests
      WithTag("custom_tag3", value, skipToLower);
      return this;
    }

    public BaseMetricBuilder WithCustomTag3(bool value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTag3) Add tests
      return WithTag("custom_tag3", value);
    }

    public BaseMetricBuilder WithCustomTag3(int value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTag3) Add tests
      return WithTag("custom_tag3", value);
    }


    // Custom Tag4 methods
    public BaseMetricBuilder WithCustomTag4(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTag4) Add tests
      WithTag("custom_tag4", value, skipToLower);
      return this;
    }

    public BaseMetricBuilder WithCustomTag4(bool value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTag4) Add tests
      return WithTag("custom_tag4", value);
    }

    public BaseMetricBuilder WithCustomTag4(int value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTag4) Add tests
      return WithTag("custom_tag4", value);
    }


    // Custom Tag5 methods
    public BaseMetricBuilder WithCustomTag5(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTag5) Add tests
      WithTag("custom_tag5", value, skipToLower);
      return this;
    }

    public BaseMetricBuilder WithCustomTag5(bool value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTag5) Add tests
      return WithTag("custom_tag5", value);
    }

    public BaseMetricBuilder WithCustomTag5(int value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTag5) Add tests
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
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTiming1) Add tests
      return new MetricTimingToken(this, "custom_timing1");
    }

    public IMetricTimingToken WithCustomTiming2()
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTiming2) Add tests
      return new MetricTimingToken(this, "custom_timing2");
    }

    public IMetricTimingToken WithCustomTiming3()
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTiming3) Add tests
      return new MetricTimingToken(this, "custom_timing3");
    }

    public IMetricTimingToken WithCustomTiming4()
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTiming4) Add tests
      return new MetricTimingToken(this, "custom_timing4");
    }

    public IMetricTimingToken WithCustomTiming5()
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTiming5) Add tests
      return new MetricTimingToken(this, "custom_timing5");
    }

    public IMetricTimingToken WithCustomTiming6()
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomTiming6) Add tests
      return new MetricTimingToken(this, "custom_timing6");
    }


    // Custom Int methods
    public BaseMetricBuilder WithCustomInt1(int value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomInt1) Add tests
      _customInt[0] = value;
      return this;
    }

    public BaseMetricBuilder IncrementCustomInt1(int amount = 1)
    {
      // TODO: [TESTS] (BaseMetricBuilder.IncrementCustomInt1) Add tests
      _customInt[0] += amount;
      return this;
    }

    public BaseMetricBuilder WithCustomInt2(int value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomInt2) Add tests
      _customInt[1] = value;
      return this;
    }

    public BaseMetricBuilder IncrementCustomInt2(int amount = 1)
    {
      // TODO: [TESTS] (BaseMetricBuilder.IncrementCustomInt2) Add tests
      _customInt[1] += amount;
      return this;
    }

    public BaseMetricBuilder WithCustomInt3(int value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomInt3) Add tests
      _customInt[2] = value;
      return this;
    }

    public BaseMetricBuilder IncrementCustomInt3(int amount = 1)
    {
      // TODO: [TESTS] (BaseMetricBuilder.IncrementCustomInt3) Add tests
      _customInt[2] += amount;
      return this;
    }

    public BaseMetricBuilder WithCustomInt4(int value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomInt4) Add tests
      _customInt[3] = value;
      return this;
    }

    public BaseMetricBuilder IncrementCustomInt4(int amount = 1)
    {
      // TODO: [TESTS] (BaseMetricBuilder.IncrementCustomInt4) Add tests
      _customInt[3] += amount;
      return this;
    }

    public BaseMetricBuilder WithCustomInt5(int value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomInt5) Add tests
      _customInt[4] = value;
      return this;
    }

    public BaseMetricBuilder IncrementCustomInt5(int amount = 1)
    {
      // TODO: [TESTS] (BaseMetricBuilder.IncrementCustomInt5) Add tests
      _customInt[4] += amount;
      return this;
    }

    public BaseMetricBuilder WithCustomInt6(int value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomInt6) Add tests
      _customInt[5] = value;
      return this;
    }

    public BaseMetricBuilder IncrementCustomInt6(int amount = 1)
    {
      // TODO: [TESTS] (BaseMetricBuilder.IncrementCustomInt6) Add tests
      _customInt[5] += amount;
      return this;
    }


    // Custom Long methods
    public BaseMetricBuilder WithCustomLong1(long value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomLong1) Add tests
      _customLong[0] = value;
      return this;
    }

    public BaseMetricBuilder WithCustomLong2(long value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomLong2) Add tests
      _customLong[1] = value;
      return this;
    }

    public BaseMetricBuilder WithCustomLong3(long value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomLong3) Add tests
      _customLong[2] = value;
      return this;
    }

    public BaseMetricBuilder WithCustomLong4(long value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomLong4) Add tests
      _customLong[3] = value;
      return this;
    }

    public BaseMetricBuilder WithCustomLong5(long value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomLong5) Add tests
      _customLong[4] = value;
      return this;
    }


    // Custom Double methods
    public BaseMetricBuilder WithCustomDouble1(double value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomDouble1) Add tests
      WithField("custom_double1", value);
      return this;
    }

    public BaseMetricBuilder WithCustomDouble2(double value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomDouble2) Add tests
      WithField("custom_double2", value);
      return this;
    }

    public BaseMetricBuilder WithCustomDouble3(double value)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithCustomDouble3) Add tests
      WithField("custom_double3", value);
      return this;
    }


    // Success related methods
    public BaseMetricBuilder MarkSuccess(bool success)
    {
      // TODO: [TESTS] (BaseMetricBuilder.MarkSuccess) Add tests
      return WithTag("succeeded", success);
    }

    public BaseMetricBuilder MarkSuccess(int resultCount = -1)
    {
      // TODO: [TESTS] (BaseMetricBuilder.MarkSuccess) Add tests
      if (resultCount > -1)
        WithResultCount(resultCount);

      return WithTag("succeeded", true);
    }

    public BaseMetricBuilder MarkSuccessIfNotNull(object obj)
    {
      // TODO: [TESTS] (BaseMetricBuilder.MarkSuccessIfNotNull) Add tests
      return WithTag("succeeded", obj != null);
    }

    public BaseMetricBuilder MarkFailed(int resultCount = -1)
    {
      // TODO: [TESTS] (BaseMetricBuilder.MarkFailed) Add tests
      if (resultCount > -1)
        WithResultCount(resultCount);

      return WithTag("succeeded", false);
    }


    // Result counting methods
    public BaseMetricBuilder WithResultCount(int resultCount)
    {
      // TODO: [TESTS] (BaseMetricBuilder.WithResultCount) Add tests
      _resultsCount = resultCount;
      return this;
    }

    public BaseMetricBuilder CountResult(object result = null)
    {
      // TODO: [TESTS] (BaseMetricBuilder.CountResult) Add tests
      if (result != null)
        _resultsCount += 1;

      return this;
    }

    public BaseMetricBuilder IncrementResultCount(int amount = 1)
    {
      // TODO: [TESTS] (BaseMetricBuilder.IncrementResultCount) Add tests
      _resultsCount += amount;
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
      Fields["custom_long1"] = _customLong[0];
      Fields["custom_long2"] = _customLong[1];
      Fields["custom_long3"] = _customLong[2];
      Fields["custom_long4"] = _customLong[3];
      Fields["custom_long5"] = _customLong[4];
      Fields["query_count"] = _queryCount;
      Fields["result_count"] = _resultsCount;

      return new LineProtocolPoint(Measurement, Fields, Tags, UtcTimestamp);
    }
  }
}
