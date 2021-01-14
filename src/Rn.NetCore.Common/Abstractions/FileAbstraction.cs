using System.IO;
using Rn.NetCore.Common.Wrappers;

namespace Rn.NetCore.Common.Abstractions
{
  public interface IFileAbstraction
  {
    bool Exists(string path);
    void Delete(string path);
    string ReadAllText(string path);
    void WriteAllText(string path, string contents);
    void Copy(string sourceFileName, string destFileName);
    void Move(string sourceFileName, string destFileName);
    IFileInfo GetFileInfo(string fileName);
  }

  public class FileAbstraction : IFileAbstraction
  {
    public bool Exists(string path)
      => File.Exists(path);

    public void Delete(string path)
      => File.Delete(path);

    public string ReadAllText(string path)
      => File.ReadAllText(path);

    public void WriteAllText(string path, string contents)
      => File.WriteAllText(path, contents);

    public void Copy(string sourceFileName, string destFileName)
      => File.Copy(sourceFileName, destFileName);

    public void Move(string sourceFileName, string destFileName)
      => File.Move(sourceFileName, destFileName);

    public IFileInfo GetFileInfo(string fileName)
      => new FileInfoWrapper(new FileInfo(fileName));
  }
}
