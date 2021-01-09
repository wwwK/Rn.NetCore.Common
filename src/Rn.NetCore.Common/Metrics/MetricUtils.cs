using System;
using Rn.NetCore.Common.Extensions;

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
  }
}
