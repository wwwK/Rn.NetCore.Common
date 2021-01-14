using System.IO;

namespace Rn.NetCore.Common.Wrappers
{
  public interface IFileInfo
  {
    long Length { get; }
  }

  public class FileInfoWrapper : IFileInfo
  {
    public long Length => _fileInfo.Length;

    private readonly FileInfo _fileInfo;

    public FileInfoWrapper(FileInfo fileInfo)
    {
      _fileInfo = fileInfo;
    }
  }
}
