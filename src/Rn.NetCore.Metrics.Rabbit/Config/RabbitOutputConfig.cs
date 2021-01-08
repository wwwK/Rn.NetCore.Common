using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Rn.NetCore.Metrics.Rabbit.Config
{
  public class RabbitOutputConfig
  {
    [JsonProperty("Enabled"), JsonPropertyName("Enabled")]
    public bool Enabled { get; set; }

    public RabbitOutputConfig()
    {
      // TODO: [TESTS] (RabbitOutputConfig) Add tests
      Enabled = false;
    }
  }
}
