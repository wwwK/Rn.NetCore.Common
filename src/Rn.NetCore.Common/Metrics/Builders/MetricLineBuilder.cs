using System;
using System.Collections.Generic;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics.Builders
{
  public class MetricLineBuilder
  {
    public string Measurement { get; }
    public Dictionary<string, string> Tags { get; }
    public Dictionary<string, object> Fields { get; }
    public DateTime? UtcTimestamp { get; set; }

    // Constructors
    public MetricLineBuilder(string measurement)
    {
      // TODO: [TESTS] (MetricLineBuilder.MetricLineBuilder) Add tests
      Measurement = measurement;
      Tags = new Dictionary<string, string>();
      Fields = new Dictionary<string, object>();
      UtcTimestamp = null;
    }

    // Build()
    public LineProtocolPoint Build(DateTime? utcTimestamp = null)
    {
      // TODO: [TESTS] (MetricLineBuilder.Build) Add tests
      if (utcTimestamp.HasValue)
        UtcTimestamp = utcTimestamp.Value;

      if (!UtcTimestamp.HasValue)
        throw new Exception("UtcTimestamp is required!");

      return new LineProtocolPoint(Measurement, Fields, Tags, UtcTimestamp);
    }
  }
}
