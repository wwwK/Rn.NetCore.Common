using System.IO;

namespace Rn.NetCore.Common.Abstractions
{
  public interface IFileAbstraction
  {
    bool Exists(string path);
  }

  public class FileAbstraction : IFileAbstraction
  {
    public bool Exists(string path)
      => File.Exists(path);
  }
}
