using System;

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
        return input;

      return input + append;
    }

    public static string LowerTrim(this string input)
    {
      // TODO: [TESTS] (StringExtensions.LowerTrim) Add tests

      if (string.IsNullOrWhiteSpace(input))
        return input;

      return input.ToLower().Trim();
    }

    public static string UpperTrim(this string input)
    {
      // TODO: [TESTS] (StringExtensions.UpperTrim) Add tests
      return string.IsNullOrWhiteSpace(input)
        ? string.Empty
        : input.ToUpper().Trim();
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

    public static bool IgnoreCaseEquals(this string value, string compare)
    {
      // TODO: [TESTS] (StringExtensions.IgnoreCaseEquals) Add tests
      return value.Equals(compare, StringComparison.InvariantCultureIgnoreCase);
    }

    public static bool IgnoreCaseContains(this string value, string contains)
    {
      // TODO: [TESTS] (StringExtensions.IgnoreCaseContains) Add tests
      return value.Contains(contains, StringComparison.InvariantCultureIgnoreCase);
    }
  }
}
