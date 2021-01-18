using System;
using System.Collections.Generic;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics.Builders
{
  public class ServiceMetricBuilder
  {
    private readonly MetricBuilder _builder;
    private readonly List<int> _customInt = new List<int> { 0, 0, 0, 0, 0 };
    private readonly List<long> _customLong = new List<long> { 0, 0, 0 };
    private int _queryCount;
    private int _resultsCount;

    private static class Tags
    {
      public const string ServiceName = "service_name";
      public const string ServiceMethod = "service_method";
      public const string Category = "category";
      public const string SubCategory = "sub_category";
    }

    private static class Fields
    {
      public const string QueryCount = "query_count";
      public const string ResultsCount = "results_count";
    }

    // Constructors
    public ServiceMetricBuilder(string measurement = null)
    {
      // TODO: [TESTS] (ServiceMetricBuilder) Add tests
      _builder = new MetricBuilder(MetricSource.ServiceCall, measurement)
        // Tags
        .WithTag(Tags.ServiceName, string.Empty)
        .WithTag(Tags.ServiceMethod, string.Empty)
        .WithTag(Tags.Category, string.Empty)
        .WithTag(Tags.SubCategory, string.Empty)
        .WithTag(CoreMetricTag.CustomTag1, string.Empty)
        .WithTag(CoreMetricTag.CustomTag2, string.Empty)
        .WithTag(CoreMetricTag.CustomTag3, string.Empty)
        .WithTag(CoreMetricTag.CustomTag4, string.Empty)
        .WithTag(CoreMetricTag.CustomTag5, string.Empty)
        // Fields
        .WithField(CoreMetricField.CustomTiming1, (long)0)
        .WithField(CoreMetricField.CustomTiming2, (long)0)
        .WithField(CoreMetricField.CustomTiming3, (long)0)
        .WithField(CoreMetricField.CustomTiming4, (long)0)
        .WithField(CoreMetricField.CustomTiming5, (long)0)
        .WithField(CoreMetricField.CustomInt1, 0)
        .WithField(CoreMetricField.CustomInt2, 0)
        .WithField(CoreMetricField.CustomInt3, 0)
        .WithField(CoreMetricField.CustomInt4, 0)
        .WithField(CoreMetricField.CustomInt5, 0)
        .WithField(CoreMetricField.CustomLong1, (long)0)
        .WithField(CoreMetricField.CustomLong2, (long)0)
        .WithField(CoreMetricField.CustomLong3, (long)0)
        .WithField(Fields.QueryCount, 0)
        .WithField(Fields.ResultsCount, 0);
    }

    public ServiceMetricBuilder(string serviceName, string methodName)
      : this()
    {
      ForService(serviceName, methodName);
    }


    // Builders
    public ServiceMetricBuilder ForService(string serviceName, string methodName, bool skipToLower = true)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.ForService) Add tests
      _builder
        .WithTag(Tags.ServiceName, serviceName, skipToLower)
        .WithTag(Tags.ServiceMethod, methodName, skipToLower);

      return this;
    }

    public ServiceMetricBuilder WithCategory(string category, string subCategory, bool skipToLower = true)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCategory) Add tests
      _builder
        .WithTag(Tags.Category, category, skipToLower)
        .WithTag(Tags.SubCategory, subCategory, skipToLower);

      return this;
    }

    public ServiceMetricBuilder WithQueryCount(int queryCount)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithQueryCount) Add tests
      _queryCount = queryCount;
      return this;
    }

    public ServiceMetricBuilder IncrementQueryCount(int amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementQueryCount) Add tests
      _queryCount += amount;
      return this;
    }

    public ServiceMetricBuilder WithResultsCount(int resultsCount)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithResultsCount) Add tests
      _resultsCount = resultsCount;
      return this;
    }

    public ServiceMetricBuilder IncrementResultsCount(int amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementResultsCount) Add tests
      _resultsCount += amount;
      return this;
    }

    public ServiceMetricBuilder CountResult(object result = null)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.CountResult) Add tests
      if (result != null)
        _resultsCount += 1;

      return this;
    }

    public ServiceMetricBuilder WithException(Exception ex)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithException) Add tests
      _builder.WithException(ex);
      return this;
    }

    public ServiceMetricBuilder MarkFailed()
    {
      // TODO: [TESTS] (ServiceMetricBuilder.MarkFailed) Add tests
      _builder.MarkFailed();
      return this;
    }

    public ServiceMetricBuilder WithUserId(int userId)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithUserId) Add tests
      _builder.WithUserId(userId);
      return this;
    }

    public ServiceMetricBuilder WithSuccess(bool success)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithSuccess) Add tests
      _builder.WithSuccess(success);
      return this;
    }


    // Custom Int
    public ServiceMetricBuilder WithCustomInt1(int value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomInt1) Add tests
      _customInt[0] = value;
      return this;
    }

    public ServiceMetricBuilder IncrementCustomInt1(int amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementCustomInt1) Add tests
      _customInt[0] += amount;
      return this;
    }

    public ServiceMetricBuilder WithCustomInt2(int value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomInt2) Add tests
      _customInt[1] = value;
      return this;
    }

    public ServiceMetricBuilder IncrementCustomInt2(int amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementCustomInt2) Add tests
      _customInt[1] += amount;
      return this;
    }

    public ServiceMetricBuilder WithCustomInt3(int value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomInt3) Add tests
      _customInt[2] = value;
      return this;
    }

    public ServiceMetricBuilder IncrementCustomInt3(int amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementCustomInt3) Add tests
      _customInt[2] += amount;
      return this;
    }

    public ServiceMetricBuilder WithCustomInt4(int value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomInt4) Add tests
      _customInt[3] = value;
      return this;
    }

    public ServiceMetricBuilder IncrementCustomInt4(int amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementCustomInt4) Add tests
      _customInt[3] += amount;
      return this;
    }

    public ServiceMetricBuilder WithCustomInt5(int value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomInt5) Add tests
      _customInt[4] = value;
      return this;
    }

    public ServiceMetricBuilder IncrementCustomInt5(int amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementCustomInt5) Add tests
      _customInt[4] += amount;
      return this;
    }


    // Custom Long
    public ServiceMetricBuilder WithCustomLong1(long value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomLong1) Add tests
      _customLong[0] = value;
      return this;
    }

    public ServiceMetricBuilder IncrementCustomLong1(long amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementCustomLong1) Add tests
      _customLong[0] += amount;
      return this;
    }

    public ServiceMetricBuilder WithCustomLong2(long value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomLong2) Add tests
      _customLong[1] = value;
      return this;
    }

    public ServiceMetricBuilder IncrementCustomLong2(long amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementCustomLong2) Add tests
      _customLong[1] += amount;
      return this;
    }

    public ServiceMetricBuilder WithCustomLong3(long value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomLong3) Add tests
      _customLong[2] = value;
      return this;
    }

    public ServiceMetricBuilder IncrementCustomLong3(long amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementCustomLong3) Add tests
      _customLong[2] += amount;
      return this;
    }


    // Custom Tags
    public ServiceMetricBuilder WithCustomTag1(object value, bool skipToLower = false)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag1) Add tests
      _builder.WithCustomTag(1, value, skipToLower);
      return this;
    }

    public ServiceMetricBuilder WithCustomTag2(object value, bool skipToLower = false)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag2) Add tests
      _builder.WithCustomTag(2, value, skipToLower);
      return this;
    }

    public ServiceMetricBuilder WithCustomTag3(object value, bool skipToLower = false)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag3) Add tests
      _builder.WithCustomTag(3, value, skipToLower);
      return this;
    }

    public ServiceMetricBuilder WithCustomTag4(object value, bool skipToLower = false)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag4) Add tests
      _builder.WithCustomTag(4, value, skipToLower);
      return this;
    }

    public ServiceMetricBuilder WithCustomTag5(object value, bool skipToLower = false)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag5) Add tests
      _builder.WithCustomTag(5, value, skipToLower);
      return this;
    }


    // Timings
    public IMetricTimingToken WithTiming()
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithTiming) Add tests
      return _builder.WithTiming(CoreMetricField.Value);
    }

    public IMetricTimingToken WithCustomTiming1()
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTiming1) Add tests
      return _builder.WithTiming(CoreMetricField.CustomTiming1);
    }

    public IMetricTimingToken WithCustomTiming2()
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTiming2) Add tests
      return _builder.WithTiming(CoreMetricField.CustomTiming2);
    }

    public IMetricTimingToken WithCustomTiming3()
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTiming3) Add tests
      return _builder.WithTiming(CoreMetricField.CustomTiming3);
    }

    public IMetricTimingToken WithCustomTiming4()
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTiming4) Add tests
      return _builder.WithTiming(CoreMetricField.CustomTiming4);
    }

    public IMetricTimingToken WithCustomTiming5()
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTiming5) Add tests
      return _builder.WithTiming(CoreMetricField.CustomTiming5);
    }


    // Build()
    public MetricBuilder Build()
    {
      // TODO: [TESTS] (ServiceMetricBuilder.Build) Add tests
      return _builder
        .WithField(CoreMetricField.CustomInt1, _customInt[0])
        .WithField(CoreMetricField.CustomInt2, _customInt[1])
        .WithField(CoreMetricField.CustomInt3, _customInt[2])
        .WithField(CoreMetricField.CustomInt4, _customInt[3])
        .WithField(CoreMetricField.CustomInt5, _customInt[4])
        .WithField(CoreMetricField.CustomLong1, _customLong[0])
        .WithField(CoreMetricField.CustomLong2, _customLong[1])
        .WithField(CoreMetricField.CustomLong3, _customLong[2])
        .WithField(Fields.QueryCount, _queryCount)
        .WithField(Fields.ResultsCount, _resultsCount);
    }
  }
}
