using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Rn.NetCore.Metrics.Rabbit.Config
{
  public class RabbitOutputConfig
  {
    [JsonProperty("Enabled"), JsonPropertyName("Enabled")]
    public bool Enabled { get; set; }

    [JsonProperty("Username"), JsonPropertyName("Username")]
    public string Username { get; set; }

    [JsonProperty("Password"), JsonPropertyName("Password")]
    public string Password { get; set; }

    [JsonProperty("VirtualHost"), JsonPropertyName("VirtualHost")]
    public string VirtualHost { get; set; }

    [JsonProperty("Host"), JsonPropertyName("Host")]
    public string Host { get; set; }

    [JsonProperty("Port"), JsonPropertyName("Port")]
    public int Port { get; set; }

    [JsonProperty("Exchange"), JsonPropertyName("Exchange")]
    public string Exchange { get; set; }

    [JsonProperty("RoutingKey"), JsonPropertyName("RoutingKey")]
    public string RoutingKey { get; set; }

    public RabbitOutputConfig()
    {
      // TODO: [TESTS] (RabbitOutputConfig) Add tests
      Enabled = false;
      Username = "guest";
      Password = "guest";
      VirtualHost = "/";
      Host = "127.0.0.1";
      Port = 5672;
      Exchange = "amq.topic";
      RoutingKey = "rn_core.metrics";
    }
  }
}
