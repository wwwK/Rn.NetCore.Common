using System.IO;

namespace Rn.NetCore.Common.Wrappers
{
  public interface IDirectoryInfo
  {
  }

  public class DirectoryInfoWrapper : IDirectoryInfo
  {
    private readonly DirectoryInfo _directoryInfo;

    public DirectoryInfoWrapper(DirectoryInfo directoryInfo)
    {
      _directoryInfo = directoryInfo;
    }
  }
}
