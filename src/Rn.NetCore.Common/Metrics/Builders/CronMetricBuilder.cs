using System;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics.Builders
{
  public class CronMetricBuilder
  {
    private readonly MetricBuilder _builder;
    private int _queryCount, _resultsCount;

    private static class Tags
    {
      public const string CronClass = "cron_class";
      public const string CronMethod = "cron_method";
      public const string Category = "category";
      public const string SubCategory = "sub_category";
    }

    private static class Fields
    {
      public const string QueryCount = "query_count";
      public const string ResultsCount = "results_count";
    }

    // Constructors
    public CronMetricBuilder(string measurement = null)
    {
      // TODO: [TESTS] (CronMetricBuilder) Add tests
      _builder = new MetricBuilder(MetricSource.CronJob, measurement)
        // Tags
        .WithTag(Tags.CronClass, string.Empty)
        .WithTag(Tags.CronMethod, string.Empty)
        .WithTag(Tags.Category, string.Empty)
        .WithTag(Tags.SubCategory, string.Empty)
        .WithTag(CoreMetricTag.CustomTag1, string.Empty)
        .WithTag(CoreMetricTag.CustomTag2, string.Empty)
        .WithTag(CoreMetricTag.CustomTag3, string.Empty)
        .WithTag(CoreMetricTag.CustomTag4, string.Empty)
        .WithTag(CoreMetricTag.CustomTag5, string.Empty)
        // Fields
        .WithField(Fields.QueryCount, 0)
        .WithField(Fields.ResultsCount, 0)
        .WithField(CoreMetricField.CustomInt1, 0)
        .WithField(CoreMetricField.CustomInt2, 0)
        .WithField(CoreMetricField.CustomInt3, 0)
        .WithField(CoreMetricField.CustomLong1, (long)0)
        .WithField(CoreMetricField.CustomLong2, (long)0)
        .WithField(CoreMetricField.CustomLong3, (long)0)
        .WithField(CoreMetricField.CustomTiming1, (long)0)
        .WithField(CoreMetricField.CustomTiming2, (long)0)
        .WithField(CoreMetricField.CustomTiming3, (long)0)
        .WithField(CoreMetricField.CustomTiming4, (long)0)
        .WithField(CoreMetricField.CustomTiming5, (long)0);
    }

    public CronMetricBuilder(string cronClass, string cronMethod)
      : this()
    {
      // TODO: [TESTS] (CronMetricBuilder) Add tests
      ForCronJob(cronClass, cronMethod);
    }


    // Builder methods
    public CronMetricBuilder ForCronJob(string className, string methodName)
    {
      // TODO: [TESTS] (CronMetricBuilder.ForCronJob) Add tests
      _builder
        .WithTag(Tags.CronClass, className, true)
        .WithTag(Tags.CronMethod, methodName, true);

      return this;
    }

    public CronMetricBuilder WithCategory(string category, string subCategory, bool useGivenCasing = true)
    {
      // TODO: [TESTS] (CronMetricBuilder.WithCategory) Add tests
      _builder
        .WithTag(Tags.Category, category, useGivenCasing)
        .WithTag(Tags.SubCategory, subCategory, useGivenCasing);

      return this;
    }

    public CronMetricBuilder MarkFailed()
    {
      // TODO: [TESTS] (CronMetricBuilder.MarkFailed) Add tests
      _builder.MarkFailed();
      return this;
    }

    public CronMetricBuilder WithException(Exception ex)
    {
      // TODO: [TESTS] (CronMetricBuilder.WithException) Add tests
      _builder.WithException(ex);
      return this;
    }

    public CronMetricBuilder WithQueryCount(int queryCount)
    {
      // TODO: [TESTS] (CronMetricBuilder.WithQueryCount) Add tests
      _queryCount = queryCount;
      return this;
    }

    public CronMetricBuilder IncrementQueryCount(int amount = 1)
    {
      // TODO: [TESTS] (CronMetricBuilder.IncrementQueryCount) Add tests
      _queryCount += amount;
      return this;
    }

    public CronMetricBuilder WithResultsCount(int resultsCount)
    {
      // TODO: [TESTS] (CronMetricBuilder.WithResultsCount) Add tests
      _resultsCount = resultsCount;
      return this;
    }

    public CronMetricBuilder IncrementResultsCount(int amount = 1)
    {
      // TODO: [TESTS] (CronMetricBuilder.IncrementResultsCount) Add tests
      _resultsCount += amount;
      return this;
    }

    public CronMetricBuilder CountResult(object result = null)
    {
      // TODO: [TESTS] (CronMetricBuilder.CountResult) Add tests
      if (result != null)
        _resultsCount += 1;

      return this;
    }

    // Timings
    public IMetricTimingToken WithTiming()
    {
      // TODO: [TESTS] (CronMetricBuilder.WithTiming) Add tests
      return _builder.WithTiming(CoreMetricField.Value);
    }

    public IMetricTimingToken WithCustomTiming1()
    {
      // TODO: [TESTS] (CronMetricBuilder.WithCustomTiming1) Add tests
      return _builder.WithTiming(CoreMetricField.CustomTiming1);
    }

    public IMetricTimingToken WithCustomTiming2()
    {
      // TODO: [TESTS] (CronMetricBuilder.WithCustomTiming2) Add tests
      return _builder.WithTiming(CoreMetricField.CustomTiming2);
    }

    public IMetricTimingToken WithCustomTiming3()
    {
      // TODO: [TESTS] (CronMetricBuilder.WithCustomTiming3) Add tests
      return _builder.WithTiming(CoreMetricField.CustomTiming3);
    }

    public IMetricTimingToken WithCustomTiming4()
    {
      // TODO: [TESTS] (CronMetricBuilder.WithCustomTiming4) Add tests
      return _builder.WithTiming(CoreMetricField.CustomTiming4);
    }

    public IMetricTimingToken WithCustomTiming5()
    {
      // TODO: [TESTS] (CronMetricBuilder.WithCustomTiming5) Add tests
      return _builder.WithTiming(CoreMetricField.CustomTiming5);
    }

    // Custom Tags
    public CronMetricBuilder WithCustomTag1(object value, bool skipToLower = false)
    {
      // TODO: [TESTS] (CronMetricBuilder.WithCustomTag1) Add tests
      _builder.WithCustomTag(1, value, skipToLower);
      return this;
    }

    public CronMetricBuilder WithCustomTag2(object value, bool skipToLower = false)
    {
      // TODO: [TESTS] (CronMetricBuilder.WithCustomTag2) Add tests
      _builder.WithCustomTag(2, value, skipToLower);
      return this;
    }

    public CronMetricBuilder WithCustomTag3(object value, bool skipToLower = false)
    {
      // TODO: [TESTS] (CronMetricBuilder.WithCustomTag3) Add tests
      _builder.WithCustomTag(3, value, skipToLower);
      return this;
    }

    public CronMetricBuilder WithCustomTag4(object value, bool skipToLower = false)
    {
      // TODO: [TESTS] (CronMetricBuilder.WithCustomTag4) Add tests
      _builder.WithCustomTag(4, value, skipToLower);
      return this;
    }

    public CronMetricBuilder WithCustomTag5(object value, bool skipToLower = false)
    {
      // TODO: [TESTS] (CronMetricBuilder.WithCustomTag5) Add tests
      _builder.WithCustomTag(5, value, skipToLower);
      return this;
    }

    // Custom Int
    public CronMetricBuilder WithCustomInt1(int value)
    {
      // TODO: [TESTS] (CronMetricBuilder.WithCustomInt1) Add tests
      _builder.WithCustomInt(1, value);
      return this;
    }

    public CronMetricBuilder WithCustomInt2(int value)
    {
      // TODO: [TESTS] (CronMetricBuilder.WithCustomInt2) Add tests
      _builder.WithCustomInt(2, value);
      return this;
    }

    public CronMetricBuilder WithCustomInt3(int value)
    {
      // TODO: [TESTS] (CronMetricBuilder.WithCustomInt3) Add tests
      _builder.WithCustomInt(3, value);
      return this;
    }

    // Custom Long
    public CronMetricBuilder WithCustomLong1(long value)
    {
      // TODO: [TESTS] (CronMetricBuilder.WithCustomLong1) Add tests
      _builder.WithCustomLong(1, value);
      return this;
    }

    public CronMetricBuilder WithCustomLong2(long value)
    {
      // TODO: [TESTS] (CronMetricBuilder.WithCustomLong2) Add tests
      _builder.WithCustomLong(2, value);
      return this;
    }

    public CronMetricBuilder WithCustomLong3(long value)
    {
      // TODO: [TESTS] (CronMetricBuilder.WithCustomLong3) Add tests
      _builder.WithCustomLong(3, value);
      return this;
    }


    // Build()
    public MetricBuilder Build()
    {
      // TODO: [TESTS] (CronMetricBuilder.Build) Add tests
      return _builder
        .WithField(Fields.QueryCount, _queryCount)
        .WithField(Fields.ResultsCount, _resultsCount);
    }
  }
}
