using System;
using System.IO;

namespace Rn.NetCore.Common.Wrappers
{
  public interface IFileInfo
  {
    DateTime CreationTime { get; set; }
    DateTime CreationTimeUtc { get; set; }
    DateTime LastAccessTime { get; set; }
    DateTime LastAccessTimeUtc { get; set; }
    DateTime LastWriteTime { get; set; }
    DateTime LastWriteTimeUtc { get; set; }
    IDirectoryInfo Directory { get; }
    FileAttributes Attributes { get; set; }

    long Length { get; }
    string Name { get; }
    bool Exists { get; }
    bool IsReadOnly { get; }
    string FullName { get; }
    string DirectoryName { get; }
    string Extension { get; }
  }

  public class FileInfoWrapper : IFileInfo
  {
    public DateTime CreationTime
    {
      get => _fileInfo.CreationTime;
      set => _fileInfo.CreationTime = value;
    }

    public DateTime CreationTimeUtc
    {
      get => _fileInfo.CreationTimeUtc;
      set => _fileInfo.CreationTimeUtc = value;
    }

    public DateTime LastAccessTime
    {
      get => _fileInfo.LastAccessTime;
      set => _fileInfo.LastAccessTime = value;
    }

    public DateTime LastAccessTimeUtc
    {
      get => _fileInfo.LastAccessTimeUtc;
      set => _fileInfo.LastAccessTimeUtc = value;
    }

    public DateTime LastWriteTime
    {
      get => _fileInfo.LastWriteTime;
      set => _fileInfo.LastWriteTime = value;
    }

    public DateTime LastWriteTimeUtc
    {
      get => _fileInfo.LastWriteTimeUtc;
      set => _fileInfo.LastWriteTimeUtc = value;
    }

    public IDirectoryInfo Directory => new DirectoryInfoWrapper(_fileInfo.Directory);

    public FileAttributes Attributes
    {
      get => _fileInfo.Attributes;
      set => _fileInfo.Attributes = value;
    }

    public long Length => _fileInfo.Length;
    public string Name => _fileInfo.Name;
    public bool Exists => _fileInfo.Exists;
    public bool IsReadOnly => _fileInfo.IsReadOnly;
    public string FullName => _fileInfo.FullName;
    public string DirectoryName => _fileInfo.DirectoryName;
    public string Extension => _fileInfo.Extension;


    private readonly FileInfo _fileInfo;

    public FileInfoWrapper(FileInfo fileInfo)
    {
      _fileInfo = fileInfo;
    }
  }
}
