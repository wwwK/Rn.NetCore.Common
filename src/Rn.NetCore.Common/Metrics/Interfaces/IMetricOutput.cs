using System.Collections.Generic;
using System.Threading.Tasks;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics.Interfaces
{
  public interface IMetricOutput
  {
    bool Enabled { get; }
    string Name { get; }

    Task SubmitPoint(LineProtocolPoint point);
    Task SubmitPoints(List<LineProtocolPoint> points);
  }
}
