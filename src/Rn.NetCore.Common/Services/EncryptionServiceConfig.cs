namespace Rn.NetCore.Common.Services
{
  public class EncryptionServiceConfig
  {
    public byte[] SaltBytes { get; set; }
    public bool SuppressErrors { get; set; }
    public string DefaultPassword { get; set; }
  }
}
