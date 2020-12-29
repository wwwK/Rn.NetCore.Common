using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Rn.NetCore.Common.Exceptions;
using Rn.NetCore.Common.Logging;

namespace Rn.NetCore.Common.Encryption
{
  public interface IEncryptionService
  {
    string Encrypt(string plainText);
    string Decrypt(string encryptedText);
    bool CanDecrypt(string encryptedText);
  }

  public class EncryptionService : IEncryptionService
  {
    private readonly ILoggerAdapter<EncryptionService> _logger;
    private readonly IEncryptionUtils _utils;
    private readonly EncryptionConfig _config;
    private const string ConfigPath = "RnCore:Encryption";

    private byte[] _keyBytes = new byte[0];
    private byte[] _ivBytes = new byte[0];

    public EncryptionService(
      ILoggerAdapter<EncryptionService> logger,
      IEncryptionUtils utils,
      IConfiguration configuration)
    {
      // TODO: [TESTS] (EncryptionService.EncryptionService) Add tests
      _logger = logger;
      _utils = utils;
      _config = MapConfiguration(configuration);
    }

    // Public methods
    public string Encrypt(string plainText)
    {
      // TODO: [TESTS] (EncryptionService.Encrypt) Add tests
      // TODO: [METRICS] (EncryptionService.Encrypt) Add metrics
      if (!_config.Enabled || string.IsNullOrWhiteSpace(plainText))
        return null;

      try
      {
        using var mStream = new MemoryStream();
        var cStream = _utils.CreateCryptoStream(mStream,
          _utils.CreateEncryptor(_keyBytes, _ivBytes),
          CryptoStreamMode.Write
        );

        var inputBytes = _utils.GetBytes(plainText);
        cStream.Write(inputBytes, 0, inputBytes.Length);
        cStream.FlushFinalBlock();
        var returnBytes = mStream.ToArray();

        cStream.Close();
        mStream.Close();

        return _utils.ToBase64String(returnBytes);
      }
      catch (Exception ex)
      {
        if (_config.LogDecryptInput)
          _logger.LogUnexpectedException(ex);

        return null;
      }
    }

    public string Decrypt(string encryptedText)
    {
      // TODO: [TESTS] (EncryptionService.Decrypt) Add tests
      // TODO: [METRICS] (EncryptionService.Decrypt) Add metrics
      if (!_config.Enabled || string.IsNullOrWhiteSpace(encryptedText))
        return null;

      try
      {
        var encryptedBytes = _utils.FromBase64String(encryptedText);
        using var msDecrypt = new MemoryStream(encryptedBytes);

        var csDecrypt = _utils.CreateCryptoStream(msDecrypt,
          _utils.CreateDecryptor(_keyBytes, _ivBytes),
          CryptoStreamMode.Read
        );

        var fromEncrypt = new byte[encryptedBytes.Length];
        csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

        var bufferString = _utils.GetString(fromEncrypt);
        return bufferString.Replace("\0", "");
      }
      catch (Exception ex)
      {
        if (!_config.LogDecryptInput)
          return null;

        if (_config.LogDecryptInput)
        {
          _logger.Error(ex, "Unable to decrypt: {i}. {s}", encryptedText, ex.HumanStackTrace());
        }
        else
        {
          _logger.LogUnexpectedException(ex);
        }

        return null;
      }
    }

    public bool CanDecrypt(string encryptedText)
    {
      // TODO: [TESTS] (EncryptionService.CanDecrypt) Add tests
      // TODO: [METRICS] (EncryptionService.CanDecrypt) Add metrics
      if (!_config.Enabled || string.IsNullOrWhiteSpace(encryptedText))
        return false;

      return Decrypt(encryptedText) != null;
    }


    // Internal methods
    private EncryptionConfig MapConfiguration(IConfiguration config)
    {
      // TODO: [TESTS] (EncryptionService.MapConfiguration) Add tests
      var encConfig = new EncryptionConfig();
      var configSection = config.GetSection(ConfigPath);

      if (!configSection.Exists())
      {
        _logger.Warning("EncryptionService disabled (config section '{s}' missing)", ConfigPath);
      }
      else
      {
        configSection.Bind(encConfig);
      }

      ProcessConfig(encConfig);
      return encConfig;
    }

    private void ProcessConfig(EncryptionConfig config)
    {
      // TODO: [TESTS] (EncryptionService.ProcessConfig) Add tests
      if (!config.Enabled)
        return;

      if (string.IsNullOrWhiteSpace(config.Key))
        throw new MissingConfigurationException(ConfigPath, "Key");

      if (string.IsNullOrWhiteSpace(config.IV))
        throw new MissingConfigurationException(ConfigPath, "IV");

      _keyBytes = _utils.FromBase64String(config.Key);
      _ivBytes = _utils.FromBase64String(config.IV);

      // Check if we need to warn about potential bad config values
      if (config.LoggingEnabled && config.LogDecryptInput)
      {
        _logger.Error("Logging of Decryption input values has been enabled, " +
                      "this is only for troubleshooting purposes and should " +
                      "be disabled once completed!");
      }
    }
  }
}
