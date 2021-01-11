using System;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics.Builders
{
  public class RepoMetricBuilder
  {
    private readonly MetricLineBuilder _builder;
    private int _rowCount;

    // Constructors
    public RepoMetricBuilder(string measurement = null)
    {
      // TODO: [TESTS] (RepoMetricBuilder) Add tests
      if (string.IsNullOrWhiteSpace(measurement))
        measurement = $"resolve:{MetricSource.RepoCall:G}";

      _rowCount = 0;
      _builder = new MetricLineBuilder(measurement)
        .WithTag("repo_name", string.Empty)
        .WithTag("repo_method", string.Empty)
        .WithTag("connection", string.Empty)
        .WithTag("has_params", false)
        .WithTag("command_type", string.Empty)
        .WithTag("succeeded", true)
        .WithTag("has_exception", false)
        .WithTag("exception_name", string.Empty)
        .WithTag("custom_tag1", string.Empty)
        .WithTag("custom_tag2", string.Empty)
        .WithTag("custom_tag3", string.Empty)
        .WithField(CoreMetricField.Value, (long)0)
        .WithField(CoreMetricField.CallCount, 1)
        .WithField("row_count", 0)
        .WithField("custom_timing1", (long)0)
        .WithField("custom_timing2", (long)0)
        .WithField("custom_timing3", (long)0)
        .WithField("custom_int1", 0)
        .WithField("custom_int2", 0)
        .WithField("custom_int3", 0)
        .WithField("custom_double1", (double)0)
        .WithField("custom_double2", (double)0)
        .WithField("custom_double3", (double)0);
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
        .WithTag("repo_name", repoName, skipToLower)
        .WithTag("repo_method", repoMethod, skipToLower)
        .WithTag("command_type", commandType, skipToLower);

      return this;
    }

    public RepoMetricBuilder ForConnection(string connection)
    {
      // TODO: [TESTS] (RepoMetricBuilder.ForConnection) Add tests
      _builder.WithTag("connection", connection, true);
      return this;
    }

    public RepoMetricBuilder WithHasParams(object paramObj = null)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithHasParams) Add tests
      _builder.WithTag("has_params", paramObj != null);
      return this;
    }

    public RepoMetricBuilder CountRow(object result = null)
    {
      // TODO: [TESTS] (RepoMetricBuilder.CountRow) Add tests
      if (result != null)
        _rowCount += 1;

      return this;
    }

    public RepoMetricBuilder WithRowCount(int rowCount)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithRowCount) Add tests
      _rowCount = rowCount;
      return this;
    }

    public RepoMetricBuilder WithException(Exception ex)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithException) Add tests
      _builder
        .WithTag("succeeded", false)
        .WithTag("has_exception", true)
        .WithTag("exception_name", ex.GetType().Name, true);

      return this;
    }

    public RepoMetricBuilder WithCustomTag1(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomTag1) Add tests
      _builder.WithTag("custom_tag1", value, skipToLower);
      return this;
    }

    public RepoMetricBuilder WithCustomTag2(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomTag2) Add tests
      _builder.WithTag("custom_tag2", value, skipToLower);
      return this;
    }

    public RepoMetricBuilder WithCustomTag3(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomTag3) Add tests
      _builder.WithTag("custom_tag3", value, skipToLower);
      return this;
    }

    public IMetricTimingToken WithTiming()
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithTiming) Add tests
      return _builder.WithTiming(CoreMetricField.Value);
    }

    public IMetricTimingToken WithCustomTiming1()
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomTiming1) Add tests
      return _builder.WithTiming("custom_timing1");
    }

    public IMetricTimingToken WithCustomTiming2()
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomTiming2) Add tests
      return _builder.WithTiming("custom_timing2");
    }

    public IMetricTimingToken WithCustomTiming3()
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomTiming3) Add tests
      return _builder.WithTiming("custom_timing3");
    }

    public RepoMetricBuilder WithCustomInt1(int value)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomInt1) Add tests
      _builder.WithField("custom_int1", value);
      return this;
    }

    public RepoMetricBuilder WithCustomInt2(int value)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomInt2) Add tests
      _builder.WithField("custom_int2", value);
      return this;
    }

    public RepoMetricBuilder WithCustomInt3(int value)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomInt3) Add tests
      _builder.WithField("custom_int3", value);
      return this;
    }

    public RepoMetricBuilder WithCustomDouble1(double value)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomDouble1) Add tests
      _builder.WithField("custom_double1", value);
      return this;
    }

    public RepoMetricBuilder WithCustomDouble2(double value)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomDouble2) Add tests
      _builder.WithField("custom_double2", value);
      return this;
    }

    public RepoMetricBuilder WithCustomDouble3(double value)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithCustomDouble3) Add tests
      _builder.WithField("custom_double3", value);
      return this;
    }


    // Build() method
    public MetricLineBuilder Build()
    {
      // TODO: [TESTS] (RepoMetricBuilder.Build) Add tests
      return _builder
        .WithField("row_count", _rowCount);
    }
  }
}
