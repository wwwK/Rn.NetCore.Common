using System;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics.Builders
{
  public class ServiceMetricBuilder
  {
    private readonly MetricLineBuilder _builder;
    private int _resultsCount, _queryCount;
    private int _customInt1, _customInt2, _customInt3, _customInt4, _customInt5, _customInt6;

    // Constructors
    public ServiceMetricBuilder(string measurement = null)
    {
      // TODO: [TESTS] (ServiceMetricBuilder) Add tests
      if (string.IsNullOrWhiteSpace(measurement))
        measurement = $"resolve:{MetricSource.ServiceCall:G}";

      _resultsCount = 0;
      _queryCount = 0;
      _customInt1 = 0;
      _customInt2 = 0;
      _customInt3 = 0;
      _customInt4 = 0;
      _customInt5 = 0;
      _customInt6 = 0;

      _builder = new MetricLineBuilder(measurement)
        .WithTag("service_name", string.Empty)
        .WithTag("service_method", string.Empty)
        .WithTag("category", string.Empty)
        .WithTag("sub_category", string.Empty)
        .WithTag("succeeded", true)
        .WithTag("has_exception", false)
        .WithTag("exception_name", string.Empty)
        .WithTag("custom_tag1", string.Empty)
        .WithTag("custom_tag2", string.Empty)
        .WithTag("custom_tag3", string.Empty)
        .WithTag("custom_tag4", string.Empty)
        .WithTag("custom_tag5", string.Empty)
        .WithField(CoreMetricField.Value, (long)0)
        .WithField(CoreMetricField.CallCount, 1)
        .WithField("query_count", 0)
        .WithField("result_count", 0)
        .WithField("custom_timing1", (long)0)
        .WithField("custom_timing2", (long)0)
        .WithField("custom_timing3", (long)0)
        .WithField("custom_timing4", (long)0)
        .WithField("custom_timing5", (long)0)
        .WithField("custom_timing6", (long)0)
        .WithField("custom_int1", 0)
        .WithField("custom_int2", 0)
        .WithField("custom_int3", 0)
        .WithField("custom_int4", 0)
        .WithField("custom_int5", 0)
        .WithField("custom_int6", 0);
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
        .WithTag("service_name", serviceName, skipToLower)
        .WithTag("service_method", methodName, skipToLower);

      return this;
    }

    public ServiceMetricBuilder WithCategory(string category, string subCategory, bool skipToLower = true)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCategory) Add tests
      _builder
        .WithTag("category", category, skipToLower)
        .WithTag("sub_category", subCategory, skipToLower);

      return this;
    }

    public ServiceMetricBuilder MarkSuccess(int resultCount = -1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.MarkSuccess) Add tests
      if (resultCount != -1)
        WithResultCount(resultCount);

      _builder.WithTag("succeeded", true);
      return this;
    }

    public ServiceMetricBuilder MarkSuccess(object result)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.MarkSuccess) Add tests
      _builder.WithTag("succeeded", result != null);
      return this;
    }

    public ServiceMetricBuilder WithSuccess(bool success)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithSuccess) Add tests
      _builder.WithTag("succeeded", success);
      return this;
    }

    public ServiceMetricBuilder MarkFailed(int resultCount = -1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.MarkFailed) Add tests
      if (resultCount != -1)
        WithResultCount(resultCount);

      _builder.WithTag("succeeded", false);
      return this;
    }

    public ServiceMetricBuilder CountResult(object result = null)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.CountResult) Add tests
      if (result != null)
        _resultsCount += 1;

      return this;
    }

    public ServiceMetricBuilder WithResultCount(int resultCount)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithResultCount) Add tests
      _resultsCount = resultCount;
      return this;
    }

    public ServiceMetricBuilder WithException(Exception ex)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithException) Add tests
      _builder
        .WithTag("succeeded", false)
        .WithTag("has_exception", true)
        .WithTag("exception_name", ex.GetType().Name, true);

      return this;
    }

    public ServiceMetricBuilder WithCustomTag1(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag1) Add tests
      _builder.WithTag("custom_tag1", value, skipToLower);
      return this;
    }

    public ServiceMetricBuilder WithCustomTag2(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag2) Add tests
      _builder.WithTag("custom_tag2", value, skipToLower);
      return this;
    }

    public ServiceMetricBuilder WithCustomTag3(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag3) Add tests
      _builder.WithTag("custom_tag3", value, skipToLower);
      return this;
    }

    public ServiceMetricBuilder WithCustomTag4(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag4) Add tests
      _builder.WithTag("custom_tag4", value, skipToLower);
      return this;
    }

    public ServiceMetricBuilder WithCustomTag5(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag5) Add tests
      _builder.WithTag("custom_tag5", value, skipToLower);
      return this;
    }

    public ServiceMetricBuilder WithCustomTag1(bool value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag1) Add tests
      _builder.WithTag("custom_tag1", value);
      return this;
    }

    public ServiceMetricBuilder WithCustomTag2(bool value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag2) Add tests
      _builder.WithTag("custom_tag2", value);
      return this;
    }

    public ServiceMetricBuilder WithCustomTag3(bool value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag3) Add tests
      _builder.WithTag("custom_tag3", value);
      return this;
    }

    public ServiceMetricBuilder WithCustomTag4(bool value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag4) Add tests
      _builder.WithTag("custom_tag4", value);
      return this;
    }

    public ServiceMetricBuilder WithCustomTag5(bool value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag5) Add tests
      _builder.WithTag("custom_tag5", value);
      return this;
    }

    public ServiceMetricBuilder WithCustomTag1(int value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag1) Add tests
      _builder.WithTag("custom_tag1", value.ToString("D"));
      return this;
    }

    public ServiceMetricBuilder WithCustomTag2(int value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag2) Add tests
      _builder.WithTag("custom_tag2", value.ToString("D"));
      return this;
    }

    public ServiceMetricBuilder WithCustomTag3(int value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag3) Add tests
      _builder.WithTag("custom_tag3", value.ToString("D"));
      return this;
    }

    public ServiceMetricBuilder WithCustomTag4(int value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag4) Add tests
      _builder.WithTag("custom_tag4", value.ToString("D"));
      return this;
    }

    public ServiceMetricBuilder WithCustomTag5(int value)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTag5) Add tests
      _builder.WithTag("custom_tag5", value.ToString("D"));
      return this;
    }

    public ServiceMetricBuilder IncrementCustomInt1(int amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementCustomInt1) Add tests
      _customInt1 += amount;
      return this;
    }

    public ServiceMetricBuilder IncrementCustomInt2(int amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementCustomInt2) Add tests
      _customInt2 += amount;
      return this;
    }

    public ServiceMetricBuilder IncrementCustomInt3(int amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementCustomInt3) Add tests
      _customInt3 += amount;
      return this;
    }

    public ServiceMetricBuilder IncrementCustomInt4(int amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementCustomInt4) Add tests
      _customInt4 += amount;
      return this;
    }

    public ServiceMetricBuilder IncrementCustomInt5(int amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementCustomInt5) Add tests
      _customInt5 += amount;
      return this;
    }

    public ServiceMetricBuilder IncrementCustomInt6(int amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementCustomInt6) Add tests
      _customInt6 += amount;
      return this;
    }

    public ServiceMetricBuilder IncrementQueryCount(int amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementQueryCount) Add tests
      _queryCount += amount;
      return this;
    }

    public IMetricTimingToken WithTiming()
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithTiming) Add tests
      return _builder.WithTiming(CoreMetricField.Value);
    }

    public IMetricTimingToken WithCustomTiming1()
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTiming1) Add tests
      return _builder.WithTiming("custom_timing1");
    }

    public IMetricTimingToken WithCustomTiming2()
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTiming2) Add tests
      return _builder.WithTiming("custom_timing2");
    }

    public IMetricTimingToken WithCustomTiming3()
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTiming3) Add tests
      return _builder.WithTiming("custom_timing3");
    }

    public IMetricTimingToken WithCustomTiming4()
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTiming4) Add tests
      return _builder.WithTiming("custom_timing4");
    }

    public IMetricTimingToken WithCustomTiming5()
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTiming5) Add tests
      return _builder.WithTiming("custom_timing5");
    }

    public IMetricTimingToken WithCustomTiming6()
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCustomTiming6) Add tests
      return _builder.WithTiming("custom_timing6");
    }


    // Build()
    public MetricLineBuilder Build()
    {
      // TODO: [TESTS] (ServiceMetricBuilder.Build) Add tests
      return _builder
        .WithField("query_count", _queryCount)
        .WithField("result_count", _resultsCount)
        .WithField("custom_int1", _customInt1)
        .WithField("custom_int2", _customInt2)
        .WithField("custom_int3", _customInt3)
        .WithField("custom_int4", _customInt4)
        .WithField("custom_int5", _customInt5)
        .WithField("custom_int6", _customInt6);
    }
  }
}
