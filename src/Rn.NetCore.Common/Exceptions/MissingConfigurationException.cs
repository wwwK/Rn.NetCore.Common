using System;

namespace Rn.NetCore.Common.Exceptions
{
  public class MissingConfigurationException : Exception
  {
    public string ConfigurationPath { get; set; }
    public string Property { get; set; }

    public MissingConfigurationException(string configPath, string property = null)
      : base($"Configuration section '{configPath}' is missing")
    {
      ConfigurationPath = configPath;
      Property = property;
    }
  }
}
