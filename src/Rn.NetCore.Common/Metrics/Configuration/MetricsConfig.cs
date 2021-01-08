using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Rn.NetCore.Common.Metrics.Configuration
{
  public class MetricsConfig
  {
    [JsonProperty("Enabled"), JsonPropertyName("Enabled")]
    public bool Enabled { get; set; }

    public MetricsConfig()
    {
      // TODO: [TESTS] (MetricsConfig.MetricsConfig) Add tests
      Enabled = false;
    }
  }
}
