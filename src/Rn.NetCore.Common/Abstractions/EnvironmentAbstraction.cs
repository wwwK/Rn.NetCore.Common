using System;
using System.Collections;

namespace Rn.NetCore.Common.Abstractions
{
  public interface IEnvironmentAbstraction
  {
    string MachineName { get; }
    string NewLine { get; }
    string CurrentDirectory { get; }
    string AppDomainBaseDirectory { get; }
    string CommandLine { get; }

    IDictionary GetEnvironmentVariables();
    IDictionary GetEnvironmentVariables(EnvironmentVariableTarget target);
    string GetEnvironmentVariable(string variable);
    string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target);
  }

  public class EnvironmentAbstraction : IEnvironmentAbstraction
  {
    public string MachineName 
      => Environment.MachineName;
    
    public string NewLine 
      => Environment.NewLine;
    
    public string CurrentDirectory 
      => Environment.CurrentDirectory;
    
    public string AppDomainBaseDirectory
      => AppDomain.CurrentDomain.BaseDirectory;

    public string CommandLine
      => Environment.CommandLine;

    public IDictionary GetEnvironmentVariables()
      => Environment.GetEnvironmentVariables();

    public IDictionary GetEnvironmentVariables(EnvironmentVariableTarget target)
      => Environment.GetEnvironmentVariables(target);

    public string GetEnvironmentVariable(string variable)
      => Environment.GetEnvironmentVariable(variable);

    public string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
      => Environment.GetEnvironmentVariable(variable, target);
  }
}
