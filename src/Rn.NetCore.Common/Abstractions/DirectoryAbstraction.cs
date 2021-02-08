using System.IO;
using Rn.NetCore.Common.Wrappers;

namespace Rn.NetCore.Common.Abstractions
{
  public interface IDirectoryAbstraction
  {
    bool Exists(string path);
    void Delete(string path);
    IDirectoryInfo CreateDirectory(string path);
    IDirectoryInfo NewDirectoryInfo(string path);
  }

  public class DirectoryAbstraction : IDirectoryAbstraction
  {
    public bool Exists(string path)
      => Directory.Exists(path);

    public void Delete(string path)
      => Directory.Delete(path);

    public IDirectoryInfo CreateDirectory(string path)
      => new DirectoryInfoWrapper(Directory.CreateDirectory(path));

    public IDirectoryInfo NewDirectoryInfo(string path)
      => new DirectoryInfoWrapper(new DirectoryInfo(path));
  }
}
