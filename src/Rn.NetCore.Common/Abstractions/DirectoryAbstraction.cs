using System.IO;
using Rn.NetCore.Common.Wrappers;

namespace Rn.NetCore.Common.Abstractions
{
  public interface IDirectoryAbstraction
  {
    bool Exists(string path);
    IDirectoryInfo CreateDirectory(string path);
  }

  public class DirectoryAbstraction : IDirectoryAbstraction
  {
    public bool Exists(string path)
      => Directory.Exists(path);

    public IDirectoryInfo CreateDirectory(string path)
      => new DirectoryInfoWrapper(Directory.CreateDirectory(path));
  }
}
