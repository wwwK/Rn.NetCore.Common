namespace Rn.NetCore.Common.Metrics.Configuration
{
  public class MetricsConfig
  {
    public bool Enabled { get; set; }

    public MetricsConfig()
    {
      // TODO: [TESTS] (MetricsConfig.MetricsConfig) Add tests
      Enabled = false;
    }
  }
}
