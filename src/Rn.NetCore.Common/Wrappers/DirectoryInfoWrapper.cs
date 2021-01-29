using System.IO;
using System.Linq;

namespace Rn.NetCore.Common.Wrappers
{
  public interface IDirectoryInfo
  {
    string FullName { get; }

    IFileInfo[] GetFiles(string searchPattern, SearchOption searchOption);
    IFileInfo[] GetFiles();
    IDirectoryInfo[] GetDirectories();
  }

  public class DirectoryInfoWrapper : IDirectoryInfo
  {
    public string FullName => _directoryInfo.FullName;

    private readonly DirectoryInfo _directoryInfo;

    public DirectoryInfoWrapper(DirectoryInfo directoryInfo)
    {
      _directoryInfo = directoryInfo;
    }

    public IFileInfo[] GetFiles(string searchPattern, SearchOption searchOption)
    {
      return _directoryInfo
        .GetFiles(searchPattern, searchOption)
        .Select(fileInfo => new FileInfoWrapper(fileInfo))
        .Cast<IFileInfo>()
        .ToArray();
    }

    public IFileInfo[] GetFiles()
    {
      return _directoryInfo
        .GetFiles()
        .Select(fileInfo => new FileInfoWrapper(fileInfo))
        .Cast<IFileInfo>()
        .ToArray();
    }

    public IDirectoryInfo[] GetDirectories()
    {
      return _directoryInfo
        .GetDirectories()
        .Select(directoryInfo => new DirectoryInfoWrapper(directoryInfo))
        .Cast<IDirectoryInfo>()
        .ToArray();
    }
  }
}
