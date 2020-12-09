using System;
using Newtonsoft.Json;

namespace Rn.NetCore.Common.Helpers
{
  public interface IJsonHelper
  {
    T DeserializeObject<T>(string value);
    bool TryDeserializeObject<T>(string json, out T parsed);
    string SerializeObject(object value);
    string SerializeObject(object value, bool formatted);
    object? DeserializeObject(string value, Type type);
  }

  public class JsonHelper : IJsonHelper
  {
    public bool TryDeserializeObject<T>(string json, out T parsed)
    {
      parsed = default;

      try
      {
        parsed = JsonConvert.DeserializeObject<T>(json);
        return true;
      }
      catch (Exception)
      {
        // Swallow
        return false;
      }
    }

    public string SerializeObject(object value)
    {
      return SerializeObject(value, false);
    }

    public string SerializeObject(object value, bool formatted)
    {
      return formatted
        ? JsonConvert.SerializeObject(value, Formatting.Indented)
        : JsonConvert.SerializeObject(value);
    }

    public object? DeserializeObject(string value, Type type)
    {
      return JsonConvert.DeserializeObject(value, type);
    }

    public T DeserializeObject<T>(string value)
    {
      return JsonConvert.DeserializeObject<T>(value);
    }
  }
}
