using System;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics.Builders
{
  public class ServiceMetricBuilder
  {
    private readonly MetricBuilder _builder;
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
        .WithField(CoreMetricField.CustomTiming1, (long) 0)
        .WithField(CoreMetricField.CustomTiming2, (long) 0)
        .WithField(CoreMetricField.CustomTiming3, (long) 0)
        .WithField(CoreMetricField.CustomTiming4, (long) 0)
        .WithField(CoreMetricField.CustomTiming5, (long) 0)
        .WithField(CoreMetricField.CustomInt1, 0)
        .WithField(CoreMetricField.CustomInt2, 0)
        .WithField(CoreMetricField.CustomInt3, 0)
        .WithField(CoreMetricField.CustomInt4, 0)
        .WithField(CoreMetricField.CustomInt5, 0)
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

    // Custom Int
    public ServiceMetricBuilder WithCustomInt1(int value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomInt1) Add tests
      _builder.WithCustomInt(1, value);
      return this;
    }

    public ServiceMetricBuilder WithCustomInt2(int value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomInt2) Add tests
      _builder.WithCustomInt(2, value);
      return this;
    }

    public ServiceMetricBuilder WithCustomInt3(int value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomInt3) Add tests
      _builder.WithCustomInt(3, value);
      return this;
    }

    public ServiceMetricBuilder WithCustomInt4(int value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomInt4) Add tests
      _builder.WithCustomInt(4, value);
      return this;
    }

    public ServiceMetricBuilder WithCustomInt5(int value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomInt5) Add tests
      _builder.WithCustomInt(5, value);
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
        .WithField(Fields.QueryCount, _queryCount)
        .WithField(Fields.ResultsCount, _resultsCount);
    }
  }
}
