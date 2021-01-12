using Rn.NetCore.Common.Metrics.Enums;

namespace Rn.NetCore.Common.Metrics.Builders
{
  // public class CronMetricBuilder : BaseMetricBuilder
  // {
  //   // Constructors
  //   public CronMetricBuilder(string measurement = null)
  //     : base(measurement, MetricSource.CronJob)
  //   {
  //     // TODO: [TESTS] (CronMetricBuilder) Add tests
  //     WithTag("cron_class", string.Empty);
  //     WithTag("cron_method", string.Empty);
  //     WithTag("category", string.Empty);
  //     WithTag("sub_category", string.Empty);
  //   }
  //
  //   public CronMetricBuilder(string cronClass, string cronMethod)
  //     : this()
  //   {
  //     ForCronJob(cronClass, cronMethod);
  //   }
  //
  //
  //   // Builder methods
  //   public CronMetricBuilder ForCronJob(string className, string methodName)
  //   {
  //     // TODO: [TESTS] (CronMetricBuilder.ForCronJob) Add tests
  //     WithTag("cron_class", className, true);
  //     WithTag("cron_method", methodName, true);
  //     return this;
  //   }
  //
  //   public CronMetricBuilder WithClassification(string category, string subCategory, bool useGivenCasing = true)
  //   {
  //     // TODO: [TESTS] (CronMetricBuilder.WithClassification) Add tests
  //     WithTag("category", category, useGivenCasing);
  //     WithTag("sub_category", subCategory, useGivenCasing);
  //     return this;
  //   }
  // }
}
