using System;
using System.Diagnostics;
using Rn.NetCore.Common.Metrics.Builders;
using Rn.NetCore.Common.Metrics.Enums;

namespace Rn.NetCore.Common.Metrics.Models
{
  public interface IMetricTimingToken : IDisposable
  {
    string FieldName { get; }
  }

  public class MetricTimingToken : IMetricTimingToken
  {
    public string FieldName { get; private set; }

    private readonly MetricBuilder _builder;
    private readonly Stopwatch _stopwatch;

    public MetricTimingToken(MetricBuilder builder, string fieldName)
    {
      _builder = builder;
      FieldName = fieldName;

      // If there was no field name provided, fall back to "value"
      if (string.IsNullOrWhiteSpace(FieldName))
        FieldName = CoreMetricField.Value;

      _stopwatch = Stopwatch.StartNew();
    }

    public void Dispose()
    {
      _builder.WithField(FieldName, _stopwatch.ElapsedMilliseconds);
    }
  }
}
