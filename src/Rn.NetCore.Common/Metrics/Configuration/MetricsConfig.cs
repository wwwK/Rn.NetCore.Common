using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Rn.NetCore.Common.Metrics.Configuration
{
  public class MetricsConfig
  {
    [JsonProperty("Enabled"), JsonPropertyName("Enabled")]
    public bool Enabled { get; set; }

    [JsonProperty("DevelopmentMode"), JsonPropertyName("DevelopmentMode")]
    public bool DevelopmentMode { get; set; }

    [JsonProperty("ApplicationName"), JsonPropertyName("ApplicationName")]
    public string ApplicationName { get; set; }

    [JsonProperty("MeasurementTemplate"), JsonPropertyName("MeasurementTemplate")]
    public string MeasurementTemplate { get; set; }

    [JsonProperty("DevelopmentModeValue"), JsonPropertyName("DevelopmentModeValue")]
    public string DevelopmentModeValue { get; set; }

    [JsonProperty("ProductionModeValue"), JsonPropertyName("ProductionModeValue")]
    public string ProductionModeValue { get; set; }

    public Dictionary<string, string> Measurements { get; set; }

    public MetricsConfig()
    {
      // TODO: [TESTS] (MetricsConfig.MetricsConfig) Add tests
      Enabled = false;
      DevelopmentMode = false;
      ApplicationName = string.Empty;
      MeasurementTemplate = string.Empty;
      DevelopmentModeValue = "dev";
      ProductionModeValue = "production";
      Measurements = new Dictionary<string, string>();
    }
  }
}
