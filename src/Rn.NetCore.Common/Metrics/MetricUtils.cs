using System;
using Rn.NetCore.Common.Extensions;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics
{
  public static class MetricUtils
  {
    public static string CleanTagName(string value)
    {
      // TODO: [TESTS] (MetricUtils.CleanTagName) Add tests
      return value.LowerTrim().Replace(" ", "_");
    }

    public static string CleanFieldName(string value)
    {
      // TODO: [TESTS] (MetricUtils.CleanFieldName) Add tests
      return value.LowerTrim().Replace(" ", "_");
    }

    public static string CleanTagValue(string value, bool skipToLower = false)
    {
      // TODO: [TESTS] (MetricUtils.CleanTagValue) Add tests
      return skipToLower ? value.Trim() : value.LowerTrim();
    }

    public static string GetRequestGuid()
    {
      // TODO: [TESTS] (MetricUtils.GetRequestGuid) Add tests
      return Guid.NewGuid().ToString("N").ToUpper();
    }

    public static MetricSource GetMetricSource(this LineProtocolPoint point)
    {
      // TODO: [TESTS] (MetricUtils.GetMetricSource) Add tests
      if (point == null || !point.Tags.ContainsKey(CoreMetricTag.Source))
        return MetricSource.Unknown;

      if (!Enum.TryParse(typeof(MetricSource), point.Tags[CoreMetricTag.Source], true, out var parsed))
        return MetricSource.Unknown;

      if (parsed != null)
        return (MetricSource) parsed;

      return MetricSource.Unknown;
    }
  }
}
