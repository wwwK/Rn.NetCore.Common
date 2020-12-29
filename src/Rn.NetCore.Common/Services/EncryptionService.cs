using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rn.NetCore.Common.Metrics;

namespace Rn.NetCore.Common.Services
{
  public interface IEncryptionService
  {
    string Encrypt(string plainText, string password);
    string Encrypt(string plainText);
    string Decrypt(string encryptedText, string password);
    string Decrypt(string encryptedText);
    bool CanDecrypt(string encryptedText);
    bool CanDecrypt(string encryptedText, string password);
    bool UsingDefaultConfig();
  }

  public class EncryptionService : BaseService<EncryptionService>, IEncryptionService
  {
    private readonly EncryptionServiceConfig _config = new EncryptionServiceConfig();
    public const string ConfigKey = "Rn:Encryption";

    public EncryptionService(
      ILogger<EncryptionService> logger,
      IMetricService metricService,
      IConfiguration config)
    : base(logger, metricService, nameof(EncryptionService))
    {
      // TODO: [TESTS] (EncryptionService.EncryptionService) Add tests
      // TODO: [LOGGING] (EncryptionService.EncryptionService) Add logging
      // TODO: [COMPLETE] (EncryptionService) Complete this

      var configSection = config.GetSection(ConfigKey);

      if (!configSection.Exists())
      {
        throw new Exception($"Missing config section '{ConfigKey}'");
      }

      configSection.Bind(_config);
    }


    // Public methods
    public string Encrypt(string plainText, string password)
    {
      // TODO: [TESTS] (EncryptionService.Encrypt) Add tests
      // TODO: [LOGGING] (EncryptionService.Encrypt) Add logging

      // TODO: [METRICS] (EncryptionService.Encrypt) Revise metrics
      //return TimeServiceCall("Encrypt", () =>
      //{
        
      //});

      if (plainText == null) return null;
      if (password == null) password = string.Empty;

      // Get the bytes of the string
      var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
      var passwordBytes = Encoding.UTF8.GetBytes(password);

      // Hash the password with SHA256
      passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

      try
      {
        var bytesEncrypted = Encrypt(bytesToBeEncrypted, passwordBytes);
        return Convert.ToBase64String(bytesEncrypted);
      }
      catch (Exception ex)
      {
        // TODO: [LOGGING] (EncryptionService) Add logging

        if (_config.SuppressErrors)
        {
          return null;
        }

        throw;
      }
    }

    public string Encrypt(string plainText)
    {
      // TODO: [TESTS] (EncryptionService.Encrypt) Add tests
      // TODO: [LOGGING] (EncryptionService.Encrypt) Add logging

      return Encrypt(plainText, _config.DefaultPassword);
    }

    public string Decrypt(string encryptedText, string password)
    {
      // TODO: [TESTS] (EncryptionService.Decrypt) Add tests
      // TODO: [LOGGING] (EncryptionService.Decrypt) Add logging

      // TODO: [METRICS] (EncryptionService.Decrypt) Revise metrics
      //return TimeServiceCall("Decrypt", () =>
      //{
        
      //});

      if (encryptedText == null) return null;
      if (password == null) password = string.Empty;

      // Get the bytes of the string
      var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
      var passwordBytes = Encoding.UTF8.GetBytes(password);

      passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

      try
      {
        var bytesDecrypted = Decrypt(bytesToBeDecrypted, passwordBytes);
        return Encoding.UTF8.GetString(bytesDecrypted);
      }
      catch (Exception ex)
      {
        // TODO: [LOGGING] (EncryptionService) Add logging

        if (_config.SuppressErrors)
        {
          return null;
        }

        throw;
      }
    }

    public string Decrypt(string encryptedText)
    {
      // TODO: [TESTS] (EncryptionService.Decrypt) Add tests
      // TODO: [LOGGING] (EncryptionService.Decrypt) Add logging

      return Decrypt(encryptedText, _config.DefaultPassword);
    }

    public bool CanDecrypt(string encryptedText)
    {
      // TODO: [TESTS] (EncryptionService.CanDecrypt) Add tests
      // TODO: [LOGGING] (EncryptionService.CanDecrypt) Add logging

      return CanDecrypt(encryptedText, _config.DefaultPassword);
    }

    public bool CanDecrypt(string encryptedText, string password)
    {
      // TODO: [TESTS] (EncryptionService.CanDecrypt) Add tests
      // TODO: [TESTS] (EncryptionService.CanDecrypt) Add logging

      if (string.IsNullOrWhiteSpace(encryptedText))
        return false;

      if (string.IsNullOrWhiteSpace(password))
        return false;

      try
      {
        return !string.IsNullOrWhiteSpace(Decrypt(encryptedText, password));
      }
      catch
      {
        return false;
      }
    }

    public bool UsingDefaultConfig()
    {
      // Ensure that the default password has been changed
      if (_config.DefaultPassword == "Th15Is@p@55w0rd!")
        return true;

      // Ensure that the SALT has been changed
      if (_config.SaltBytes.Length == 8)
      {
        var usingDefault = true;
        var counter = 1;

        // Default value is [1,2,3,4,5,6,7,8]
        foreach (var b in _config.SaltBytes)
        {
          if (b != counter) usingDefault = false;
          counter++;
        }

        return usingDefault;
      }

      // Ensure that there is a password set
      if (string.IsNullOrWhiteSpace(_config.DefaultPassword))
        return true;

      // It looks like we are not using the default configuration
      return false;
    }


    // Internal methods - https://www.selamigungor.com/post/7/encrypt-decrypt-a-string-in-csharp
    private byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
    {
      byte[] encryptedBytes = null;

      using (var ms = new MemoryStream())
      {
        using (var AES = new RijndaelManaged())
        {
          var key = new Rfc2898DeriveBytes(passwordBytes, _config.SaltBytes, 1000);

          AES.KeySize = 256;
          AES.BlockSize = 128;
          AES.Key = key.GetBytes(AES.KeySize / 8);
          AES.IV = key.GetBytes(AES.BlockSize / 8);

          AES.Mode = CipherMode.CBC;

          using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
          {
            cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
            cs.Close();
          }

          encryptedBytes = ms.ToArray();
        }
      }

      return encryptedBytes;
    }

    private byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
    {
      byte[] decryptedBytes = null;

      using (var ms = new MemoryStream())
      {
        using (var AES = new RijndaelManaged())
        {
          var key = new Rfc2898DeriveBytes(passwordBytes, _config.SaltBytes, 1000);

          AES.KeySize = 256;
          AES.BlockSize = 128;
          AES.Key = key.GetBytes(AES.KeySize / 8);
          AES.IV = key.GetBytes(AES.BlockSize / 8);
          AES.Mode = CipherMode.CBC;

          using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
          {
            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
            cs.Close();
          }

          decryptedBytes = ms.ToArray();
        }
      }

      return decryptedBytes;
    }
  }
}
