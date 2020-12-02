using System;

namespace Rn.NetCore.Common.Abstractions
{
  public interface IEnvironmentAbstraction
  {
    string MachineName { get; }
    string NewLine { get; }
    string TempFileName(string extension = "tmp");
  }

  public class EnvironmentAbstraction : IEnvironmentAbstraction
  {
    public string MachineName => Environment.MachineName;
    public string NewLine => Environment.NewLine;

    [Obsolete("NO NOT USE THIS")]
    public string TempFileName(string extension = "tmp")
    {
      // TODO: [TESTS] (EnvironmentAbstraction.TempFileName) Add tests
      return $"{DateTime.Now.Ticks}.{extension}";
    }
  }
}
