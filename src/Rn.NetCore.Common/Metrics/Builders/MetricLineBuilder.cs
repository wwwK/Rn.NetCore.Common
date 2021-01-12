using System;
using System.Collections.Generic;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics.Builders
{
  // public abstract class BaseMetricBuilder
  // {
  //   public BaseMetricBuilder WithTag(string name, int value)
  //   {
  //     // TODO: [TESTS] (MetricLineBuilder.WithTag) Add tests
  //     Tags[MetricUtils.CleanTagName(name)] = value.ToString("D");
  //     return this;
  //   }
  //
  //   public BaseMetricBuilder WithTag(string name, long value)
  //   {
  //     // TODO: [TESTS] (MetricLineBuilder.WithTag) Add tests
  //     Tags[MetricUtils.CleanTagName(name)] = value.ToString("D");
  //     return this;
  //   }
  //
  //   public BaseMetricBuilder WithField(string name, double value)
  //   {
  //     // TODO: [TESTS] (MetricLineBuilder.WithField) Add tests
  //     Fields[MetricUtils.CleanFieldName(name)] = value;
  //     return this;
  //   }
  //
  //   public BaseMetricBuilder WithField(string name, int value)
  //   {
  //     // TODO: [TESTS] (MetricLineBuilder.WithField) Add tests
  //     Fields[MetricUtils.CleanFieldName(name)] = value;
  //     return this;
  //   }
  //
  //   public BaseMetricBuilder WithField(string name, long value)
  //   {
  //     // TODO: [TESTS] (MetricLineBuilder.WithField) Add tests
  //     Fields[MetricUtils.CleanFieldName(name)] = value;
  //     return this;
  //   }
  //
  //   public BaseMetricBuilder WithField(string name, bool value)
  //   {
  //     // TODO: [TESTS] (MetricLineBuilder.WithField) Add tests
  //     Fields[MetricUtils.CleanFieldName(name)] = value;
  //     return this;
  //   }
  //
  //   public BaseMetricBuilder IncrementQueryCount(int amount = 1)
  //   {
  //     // TODO: [TESTS] (BaseMetricBuilder.IncrementQueryCount) Add tests
  //     _queryCount += amount;
  //     return this;
  //   }
  //
  //   // Custom Int methods
  //   public BaseMetricBuilder WithCustomInt1(int value)
  //   {
  //     // TODO: [TESTS] (BaseMetricBuilder.WithCustomInt1) Add tests
  //     _customInt[0] = value;
  //     return this;
  //   }
  //
  //   public BaseMetricBuilder IncrementCustomInt1(int amount = 1)
  //   {
  //     // TODO: [TESTS] (BaseMetricBuilder.IncrementCustomInt1) Add tests
  //     _customInt[0] += amount;
  //     return this;
  //   }
  //
  //   public BaseMetricBuilder WithCustomInt2(int value)
  //   {
  //     // TODO: [TESTS] (BaseMetricBuilder.WithCustomInt2) Add tests
  //     _customInt[1] = value;
  //     return this;
  //   }
  //
  //   public BaseMetricBuilder IncrementCustomInt2(int amount = 1)
  //   {
  //     // TODO: [TESTS] (BaseMetricBuilder.IncrementCustomInt2) Add tests
  //     _customInt[1] += amount;
  //     return this;
  //   }
  //
  //   public BaseMetricBuilder WithCustomInt3(int value)
  //   {
  //     // TODO: [TESTS] (BaseMetricBuilder.WithCustomInt3) Add tests
  //     _customInt[2] = value;
  //     return this;
  //   }
  //
  //   public BaseMetricBuilder IncrementCustomInt3(int amount = 1)
  //   {
  //     // TODO: [TESTS] (BaseMetricBuilder.IncrementCustomInt3) Add tests
  //     _customInt[2] += amount;
  //     return this;
  //   }
  //
  //
  //   // Custom Long methods
  //   public BaseMetricBuilder WithCustomLong1(long value)
  //   {
  //     // TODO: [TESTS] (BaseMetricBuilder.WithCustomLong1) Add tests
  //     _customLong[0] = value;
  //     return this;
  //   }
  //
  //   public BaseMetricBuilder WithCustomLong2(long value)
  //   {
  //     // TODO: [TESTS] (BaseMetricBuilder.WithCustomLong2) Add tests
  //     _customLong[1] = value;
  //     return this;
  //   }
  //
  //   public BaseMetricBuilder WithCustomLong3(long value)
  //   {
  //     // TODO: [TESTS] (BaseMetricBuilder.WithCustomLong3) Add tests
  //     _customLong[2] = value;
  //     return this;
  //   }
  //
  //
  //   // Custom Double methods
  //   public BaseMetricBuilder WithCustomDouble1(double value)
  //   {
  //     // TODO: [TESTS] (BaseMetricBuilder.WithCustomDouble1) Add tests
  //     WithField("custom_double1", value);
  //     return this;
  //   }
  //
  //   public BaseMetricBuilder WithCustomDouble2(double value)
  //   {
  //     // TODO: [TESTS] (BaseMetricBuilder.WithCustomDouble2) Add tests
  //     WithField("custom_double2", value);
  //     return this;
  //   }
  //
  //   public BaseMetricBuilder WithCustomDouble3(double value)
  //   {
  //     // TODO: [TESTS] (BaseMetricBuilder.WithCustomDouble3) Add tests
  //     WithField("custom_double3", value);
  //     return this;
  //   }
  //
  //
  //   // Success related methods
  //   public BaseMetricBuilder MarkSuccess(bool success)
  //   {
  //     // TODO: [TESTS] (BaseMetricBuilder.MarkSuccess) Add tests
  //     return WithTag("succeeded", success);
  //   }
  //
  //   public BaseMetricBuilder MarkSuccess(int resultCount = -1)
  //   {
  //     // TODO: [TESTS] (BaseMetricBuilder.MarkSuccess) Add tests
  //     if (resultCount > -1)
  //       WithResultCount(resultCount);
  //
  //     return WithTag("succeeded", true);
  //   }
  //
  //   public BaseMetricBuilder MarkSuccessIfNotNull(object obj)
  //   {
  //     // TODO: [TESTS] (BaseMetricBuilder.MarkSuccessIfNotNull) Add tests
  //     return WithTag("succeeded", obj != null);
  //   }
  //
  //   public BaseMetricBuilder MarkFailed(int resultCount = -1)
  //   {
  //     // TODO: [TESTS] (BaseMetricBuilder.MarkFailed) Add tests
  //     if (resultCount > -1)
  //       WithResultCount(resultCount);
  //
  //     return WithTag("succeeded", false);
  //   }
  //
  //
  //   // Build()
  //   public LineProtocolPoint Build(DateTime? utcTimestamp = null)
  //   {
  //     // TODO: [TESTS] (MetricLineBuilder.Build) Add tests
  //     if (utcTimestamp.HasValue)
  //       UtcTimestamp = utcTimestamp.Value;
  //
  //     if (!UtcTimestamp.HasValue)
  //       throw new Exception("UtcTimestamp is required!");
  //
  //     Fields["custom_int1"] = _customInt[0];
  //     Fields["custom_int2"] = _customInt[1];
  //     Fields["custom_int3"] = _customInt[2];
  //     Fields["custom_long1"] = _customLong[0];
  //     Fields["custom_long2"] = _customLong[1];
  //     Fields["custom_long3"] = _customLong[2];
  //     Fields["query_count"] = _queryCount;
  //     Fields["result_count"] = _resultsCount;
  //
  //     return new LineProtocolPoint(Measurement, Fields, Tags, UtcTimestamp);
  //   }
  // }
}
