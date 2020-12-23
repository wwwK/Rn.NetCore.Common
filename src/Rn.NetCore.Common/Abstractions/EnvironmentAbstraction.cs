using System;

namespace Rn.NetCore.Common.Abstractions
{
  public interface IEnvironmentAbstraction
  {
    string MachineName { get; }
    string NewLine { get; }
    string CurrentDirectory { get; }
    string AppDomainBaseDirectory { get; }
  }

  public class EnvironmentAbstraction : IEnvironmentAbstraction
  {
    public string MachineName => Environment.MachineName;
    public string NewLine => Environment.NewLine;
    public string CurrentDirectory => Environment.CurrentDirectory;
    public string AppDomainBaseDirectory => AppDomain.CurrentDomain.BaseDirectory;
  }
}
