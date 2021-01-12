using Rn.NetCore.Common.Metrics.Enums;

namespace Rn.NetCore.Common.Metrics.Builders
{
  public class RepoMetricBuilder : BaseMetricBuilder
  {
    // Constructors
    public RepoMetricBuilder(string measurement = null)
      : base(measurement, MetricSource.RepoCall)
    {
      // TODO: [TESTS] (RepoMetricBuilder) Add tests
      WithTag("repo_name", string.Empty);
      WithTag("repo_method", string.Empty);
      WithTag("connection", string.Empty);
      WithTag("has_params", false);
      WithTag("command_type", string.Empty);
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
      WithTag("repo_name", repoName, skipToLower);
      WithTag("repo_method", repoMethod, skipToLower);
      WithTag("command_type", commandType, skipToLower);

      return this;
    }

    public RepoMetricBuilder ForConnection(string connection)
    {
      // TODO: [TESTS] (RepoMetricBuilder.ForConnection) Add tests
      WithTag("connection", connection, true);
      return this;
    }

    public RepoMetricBuilder WithHasParams(object paramObj = null)
    {
      // TODO: [TESTS] (RepoMetricBuilder.WithHasParams) Add tests
      WithTag("has_params", paramObj != null);
      return this;
    }
  }
}
