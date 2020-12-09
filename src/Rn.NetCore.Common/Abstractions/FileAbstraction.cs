using System.IO;

namespace Rn.NetCore.Common.Abstractions
{
  public interface IFileAbstraction
  {
    bool Exists(string path);
    void Delete(string path);
    string ReadAllText(string path);
    void WriteAllText(string path, string contents);
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
  }
}
