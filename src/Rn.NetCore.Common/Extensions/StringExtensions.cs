namespace Rn.NetCore.Common.Extensions
{
  public static class StringExtensions
  {
    public static string AppendIfMissing(this string input, string append)
    {
      // TODO: [TESTS] (StringExtensions.AppendIfMissing) Add tests

      if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(append))
        return input;

      if (input.EndsWith(append))
        return append;

      return input + append;
    }

    public static string LowerTrim(this string input)
    {
      // TODO: [TESTS] (StringExtensions.LowerTrim) Add tests

      if (string.IsNullOrWhiteSpace(input))
        return input;

      return input.ToLower().Trim();
    }

    public static bool AsBool(this string input, bool fallback = false)
    {
      // TODO: [TESTS] (StringExtensions.AsBool) Add tests

      if (string.IsNullOrWhiteSpace(input))
        return fallback;

      switch (input.LowerTrim())
      {
        case "true":
        case "1":
        case "yes":
        case "on":
        case "enabled":
          return true;

        case "false":
        case "0":
        case "no":
        case "off":
        case "disabled":
          return false;

        default:
          return fallback;
      }
    }

    public static int AsInt(this string input, int fallback = 0)
    {
      // TODO: [TESTS] (StringExtensions.AsInt) Add tests

      if (string.IsNullOrWhiteSpace(input))
        return fallback;

      if (int.TryParse(input, out var parsed))
      {
        return parsed;
      }

      return fallback;
    }
  }
}
