﻿using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Rn.NetCore.Common.Encryption
{
  public class EncryptionConfig
  {
    [JsonProperty("Enabled"), JsonPropertyName("Enabled")]
    public bool Enabled { get; set; }

    [JsonProperty("Key"), JsonPropertyName("Key")]
    public string Key { get; set; }

    [JsonProperty("IV"), JsonPropertyName("IV")]
    public string IV { get; set; }

    [JsonProperty("LoggingEnabled"), JsonPropertyName("LoggingEnabled")]
    public bool LoggingEnabled { get; set; }

    [JsonProperty("LogDecryptInput"), JsonPropertyName("LogDecryptInput")]
    public bool LogDecryptInput { get; set; }

    public EncryptionConfig()
    {
      // TODO: [TESTS] (EncryptionConfig) Add tests
      Enabled = false;
      Key = string.Empty;
      IV = string.Empty;
      LogDecryptInput = false;
      LoggingEnabled = false;
    }
  }
}
