using System.IO;

namespace Rn.NetCore.Common.Wrappers
{
  public interface IFileInfo
  {
    long Length { get; }
    string Name { get; }
    bool Exists { get; }
    string FullName { get; }
  }

  public class FileInfoWrapper : IFileInfo
  {
    public long Length => _fileInfo.Length;
    public string Name => _fileInfo.Name;
    public bool Exists => _fileInfo.Exists;
    public string FullName => _fileInfo.FullName;

    private readonly FileInfo _fileInfo;

    public FileInfoWrapper(FileInfo fileInfo)
    {
      _fileInfo = fileInfo;
    }
  }
}
