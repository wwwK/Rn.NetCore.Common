using Rn.NetCore.Common.Metrics.Enums;

namespace Rn.NetCore.Common.Metrics.Builders
{
  public class ServiceMetricBuilder : BaseMetricLineBuilder
  {
    private int _queryCount;

    // Constructors
    public ServiceMetricBuilder(string measurement = null)
      : base(measurement, MetricSource.ServiceCall)
    {
      // TODO: [TESTS] (ServiceMetricBuilder) Add tests
      _queryCount = 0;

      WithTag("service_name", string.Empty);
      WithTag("service_method", string.Empty);
      WithTag("category", string.Empty);
      WithTag("sub_category", string.Empty);
      WithField("query_count", 0);
      WithField("result_count", 0);
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
      WithTag("service_name", serviceName, skipToLower);
      WithTag("service_method", methodName, skipToLower);
      return this;
    }

    public ServiceMetricBuilder WithCategory(string category, string subCategory, bool skipToLower = true)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.WithCategory) Add tests
      WithTag("category", category, skipToLower);
      WithTag("sub_category", subCategory, skipToLower);
      return this;
    }

    public ServiceMetricBuilder IncrementQueryCount(int amount = 1)
    {
      // TODO: [TESTS] (ServiceMetricBuilder.IncrementQueryCount) Add tests
      _queryCount += amount;
      return this;
    }


    // Build()
    public override void FinalizeBuilder()
    {
      // TODO: [TESTS] (ServiceMetricBuilder.FinalizeBuilder) Add tests
      WithField("query_count", _queryCount);
    }
  }
}
