using System;

namespace Rn.NetCore.Common.Metrics.Exceptions
{
  public class MetricConfigException : Exception
  {
    public string Property { get; set; }

    public MetricConfigException(string message)
      : base(message)
    {
      // TODO: [TESTS] (MetricConfigException) Add tests
      Property = string.Empty;
    }

    public MetricConfigException(string property, string message)
     : this(message)
    {
      // TODO: [TESTS] (MetricConfigException) Add tests
      Property = property;
    }
  }
}
