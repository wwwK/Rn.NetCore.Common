using System.IO;

namespace Rn.NetCore.Common.Abstractions
{
  public interface IFileAbstraction
  {
    bool Exists(string path);
    string ReadAllText(string path);
  }

  public class FileAbstraction : IFileAbstraction
  {
    public bool Exists(string path)
      => File.Exists(path);

    public string ReadAllText(string path)
      => File.ReadAllText(path);
  }
}
