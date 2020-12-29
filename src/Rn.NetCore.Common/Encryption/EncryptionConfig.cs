namespace Rn.NetCore.Common.Encryption
{
  public class EncryptionConfig
  {
    public bool Enabled { get; set; }
    public string Key { get; set; }
    public string IV { get; set; }
    public bool LoggingEnabled { get; set; }
    public bool LogDecryptInput { get; set; }

    public EncryptionConfig()
    {
      // TODO: [TESTS] (EncryptionConfig) Add tests
      Enabled = false;
      Key = string.Empty;
      IV = string.Empty;
      LogDecryptInput = false;
      LoggingEnabled = false;
    }
  }
}
