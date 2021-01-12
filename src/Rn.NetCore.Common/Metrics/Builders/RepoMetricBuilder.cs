using System;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics.Builders
{
  public class RepoMetricBuilder
  {
    private readonly MetricBuilder _builder;
    private int _queryCount, _resultsCount;

    private static class Tags
    {
      public const string RepoName = "repo_name";
      public const string RepoMethod = "repo_method";
      public const string CommandType = "command_type";
      public const string Connection = "connection";
      public const string HasParameters = "has_params";
    }

    private static class Fields
    {
      public const string QueryCount = "query_count";
      public const string ResultsCount = "results_count";
      public const string CustomTiming1 = "custom_timing1";
      public const string CustomTiming2 = "custom_timing2";
      public const string CustomTiming3 = "custom_timing3";
      public const string CustomTiming4 = "custom_timing4";
      public const string CustomTiming5 = "custom_timing5";
    }

    // Constructors
    public RepoMetricBuilder(string measurement = null)
    {
      // TODO: [TESTS] (RepoMetricBuilder) Add tests
      _queryCount = 0;
      _resultsCount = 0;

      _builder = new MetricBuilder(MetricSource.RepoCall, measurement)
        // Tags
        .WithTag(Tags.RepoName, string.Empty)
        .WithTag(Tags.RepoMethod, string.Empty)
        .WithTag(Tags.CommandType, string.Empty)
        .WithTag(Tags.Connection, string.Empty)
        .WithTag(Tags.HasParameters, false)
        .WithTag(CoreMetricTag.CustomTag1, string.Empty)
        .WithTag(CoreMetricTag.CustomTag2, string.Empty)
        .WithTag(CoreMetricTag.CustomTag3, string.Empty)
        .WithTag(CoreMetricTag.CustomTag4, string.Empty)
        .WithTag(CoreMetricTag.CustomTag5, string.Empty)
        // Fields
        .WithField(Fields.QueryCount, 0)
        .WithField(Fields.ResultsCount, 0)
        .WithField(Fields.CustomTiming1, (long) 0)
        .WithField(Fields.CustomTiming2, (long) 0)
        .WithField(Fields.CustomTiming3, (long) 0)
        .WithField(Fields.CustomTiming4, (long) 0)
        .WithField(Fields.CustomTiming5, (long) 0);
    }

    public RepoMetricBuilder(string repoName, string repoMethod, string commandType, bool skipToLower = true)
      : this()
    {
      // TODO: [TESTS] (RepoMetricBuilder) Add tests
      ForRepo(repoName, repoMethod, commandType, skipToLower);
    }


    // Builder methods
    public RepoMetricBuilder ForRepo(string repoName, string repoMethod, string commandType, bool skipToLower = true)
    {
      // TODO: [TESTS] (RepoMetricBuilder.ForRepo) Add tests
      _builder
        .WithTag(Tags.RepoName, repoName, skipToLower)
        .WithTag(Tags.RepoMethod, repoMethod, skipToLower)
        .WithTag(Tags.CommandType, commandType, skipToLower);

      return this;
    }

    public RepoMetricBuilder ForConnection(string connection, bool skipToLower = true)
    {
      // TODO: [TESTS] (RepoMetricBuilder.ForConnection) Add tests
      _builder.WithTag(Tags.Connection, connection, skipToLower);
      return this;
    }

    public RepoMetricBuilder WithParameters(object param = null)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithParameters) Add tests
      _builder.WithTag(Tags.HasParameters, param != null);
      return this;
    }

    public RepoMetricBuilder WithQueryCount(int queryCount)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithQueryCount) Add tests
      _queryCount = queryCount;
      return this;
    }

    public RepoMetricBuilder IncrementQueryCount(int amount = 1)
    {
      // TODO: [TESTS] (RepoMetricBuilder.IncrementQueryCount) Add tests
      _queryCount += amount;
      return this;
    }

    public RepoMetricBuilder WithException(Exception ex)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithException) Add tests
      _builder.WithException(ex);
      return this;
    }

    public RepoMetricBuilder WithResultCount(int resultCount)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithResultCount) Add tests
      _resultsCount = resultCount;
      return this;
    }

    public RepoMetricBuilder CountResult(object result = null)
    {
      // TODO: [TESTS] (RepoMetricBuilder.CountResult) Add tests
      if (result != null)
        _resultsCount += 1;
      return this;
    }

    public RepoMetricBuilder IncrementResultCount(int amount = 1)
    {
      // TODO: [TESTS] (RepoMetricBuilder.IncrementResultCount) Add tests
      _resultsCount += amount;
      return this;
    }


    // Timing methods
    public IMetricTimingToken WithTiming()
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithTiming) Add tests
      return _builder.WithTiming(CoreMetricField.Value);
    }

    public IMetricTimingToken WithCustomTiming1()
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomTiming1) Add tests
      return _builder.WithTiming(Fields.CustomTiming1);
    }

    public IMetricTimingToken WithCustomTiming2()
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomTiming2) Add tests
      return _builder.WithTiming(Fields.CustomTiming2);
    }

    public IMetricTimingToken WithCustomTiming3()
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomTiming3) Add tests
      return _builder.WithTiming(Fields.CustomTiming3);
    }

    public IMetricTimingToken WithCustomTiming4()
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomTiming4) Add tests
      return _builder.WithTiming(Fields.CustomTiming4);
    }

    public IMetricTimingToken WithCustomTiming5()
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomTiming5) Add tests
      return _builder.WithTiming(Fields.CustomTiming5);
    }


    // Custom Tags
    public RepoMetricBuilder WithCustomTag1(object value, bool skipToLower = false)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomTag1) Add tests
      _builder.WithCustomTag(1, value, skipToLower);
      return this;
    }

    public RepoMetricBuilder WithCustomTag2(object value, bool skipToLower = false)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomTag2) Add tests
      _builder.WithCustomTag(2, value, skipToLower);
      return this;
    }

    public RepoMetricBuilder WithCustomTag3(object value, bool skipToLower = false)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomTag3) Add tests
      _builder.WithCustomTag(3, value, skipToLower);
      return this;
    }

    public RepoMetricBuilder WithCustomTag4(object value, bool skipToLower = false)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomTag4) Add tests
      _builder.WithCustomTag(4, value, skipToLower);
      return this;
    }

    public RepoMetricBuilder WithCustomTag5(object value, bool skipToLower = false)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomTag5) Add tests
      _builder.WithCustomTag(5, value, skipToLower);
      return this;
    }


    // Build()
    public MetricBuilder Build()
    {
      // TODO: [TESTS] (RepoMetricBuilder.Build) Add tests
      return _builder
        .WithField(Fields.QueryCount, _queryCount)
        .WithField(Fields.ResultsCount, _resultsCount);
    }
  }
}
