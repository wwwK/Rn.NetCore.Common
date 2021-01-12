using Rn.NetCore.Common.Metrics.Enums;

namespace Rn.NetCore.Common.Metrics.Builders
{
  // public class ServiceMetricBuilder : BaseMetricBuilder
  // {
  //   // Constructors
  //   public ServiceMetricBuilder(string measurement = null)
  //     : base(measurement, MetricSource.ServiceCall)
  //   {
  //     // TODO: [TESTS] (ServiceMetricBuilder) Add tests
  //     WithTag("service_name", string.Empty);
  //     WithTag("service_method", string.Empty);
  //     WithTag("category", string.Empty);
  //     WithTag("sub_category", string.Empty);
  //   }
  //
  //   public ServiceMetricBuilder(string serviceName, string methodName)
  //     : this()
  //   {
  //     ForService(serviceName, methodName);
  //   }
  //
  //
  //   // Builders
  //   public ServiceMetricBuilder ForService(string serviceName, string methodName, bool skipToLower = true)
  //   {
  //     // TODO: [TESTS] (ServiceMetricBuilder.ForService) Add tests
  //     WithTag("service_name", serviceName, skipToLower);
  //     WithTag("service_method", methodName, skipToLower);
  //     return this;
  //   }
  //
  //   public ServiceMetricBuilder WithCategory(string category, string subCategory, bool skipToLower = true)
  //   {
  //     // TODO: [TESTS] (ServiceMetricBuilder.WithCategory) Add tests
  //     WithTag("category", category, skipToLower);
  //     WithTag("sub_category", subCategory, skipToLower);
  //     return this;
  //   }
  // }
}
