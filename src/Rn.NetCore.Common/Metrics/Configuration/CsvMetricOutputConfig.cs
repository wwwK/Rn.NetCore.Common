using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Rn.NetCore.Common.Metrics.Configuration
{
  public class CsvMetricOutputConfig
  {
    [JsonProperty("Enabled"), JsonPropertyName("Enabled")]
    public bool Enabled { get; set; }

    [JsonProperty("OutputDir"), JsonPropertyName("OutputDir")]
    public string OutputDir { get; set; }

    [JsonProperty("UseHourlyFolders"), JsonPropertyName("UseHourlyFolders")]
    public bool UseHourlyFolders { get; set; }

    public CsvMetricOutputConfig()
    {
      // TODO: [TESTS] (CsvMetricOutputConfig) Add tests
      Enabled = false;
      OutputDir = "./csv-output";
      UseHourlyFolders = false;
    }
  }
}
