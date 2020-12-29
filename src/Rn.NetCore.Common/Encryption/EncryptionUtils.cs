using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Rn.NetCore.Common.Encryption
{
  public interface IEncryptionUtils
  {
    ICryptoTransform CreateEncryptor(byte[] key, byte[] iv);
    ICryptoTransform CreateDecryptor(byte[] key, byte[] iv);
    byte[] FromBase64String(string s);
    string ToBase64String(byte[] inArray);
    byte[] GetBytes(string s);
    string GetString(byte[] bytes);
    CryptoStream CreateCryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode);
    byte[] GetRandomBytes(int length);
  }

  public class EncryptionUtils : IEncryptionUtils
  {
    private readonly Random _random = new Random(DateTime.Now.Millisecond);

    public ICryptoTransform CreateEncryptor(byte[] key, byte[] iv)
    {
      return new DESCryptoServiceProvider().CreateEncryptor(key, iv);
    }

    public ICryptoTransform CreateDecryptor(byte[] key, byte[] iv)
    {
      return new DESCryptoServiceProvider().CreateDecryptor(key, iv);
    }

    public byte[] FromBase64String(string s)
    {
      return Convert.FromBase64String(s);
    }

    public string ToBase64String(byte[] inArray)
    {
      return Convert.ToBase64String(inArray);
    }

    public byte[] GetBytes(string s)
    {
      return new ASCIIEncoding().GetBytes(s);
    }

    public string GetString(byte[] bytes)
    {
      return new ASCIIEncoding().GetString(bytes);
    }

    public CryptoStream CreateCryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode)
    {
      return new CryptoStream(stream, transform, mode);
    }

    public byte[] GetRandomBytes(int length)
    {
      var output = new List<byte>();

      for (var i = 0; i < length; i++)
      {
        output.Add((byte)_random.Next(0, 255));
      }

      return output.ToArray();
    }
  }
}
