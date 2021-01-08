using System.Collections.Generic;
using System.Threading.Tasks;
using Rn.NetCore.Common.Metrics.Interfaces;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Metrics.Rabbit
{
  public class RabbitMetricOutput : IMetricOutput
  {
    public bool Enabled { get; }
    public string Name { get; }

    public RabbitMetricOutput()
    {
      // TODO: [TESTS] (RabbitMetricOutput) Add tests
      Enabled = false;
      Name = nameof(RabbitMetricOutput);
    }

    public async Task SubmitPoint(LineProtocolPoint point)
    {
      // TODO: [TESTS] (RabbitMetricOutput.SubmitPoint) Add tests
    }

    public async Task SubmitPoints(List<LineProtocolPoint> points)
    {
      // TODO: [TESTS] (RabbitMetricOutput.SubmitPoints) Add tests
    }
  }
}
