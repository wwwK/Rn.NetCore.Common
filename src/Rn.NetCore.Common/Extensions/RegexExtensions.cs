using System.Text.RegularExpressions;

namespace Rn.NetCore.Common.Extensions
{
  public static class RegexExtensions
  {
    public static bool MatchesRegex(this string input, string pattern)
    {
      // TODO: [TESTS] (RegexExtensions.MatchesRegex) Add tests

      if (string.IsNullOrWhiteSpace(input))
        return false;

      return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
    }

    public static Match GetRegexMatch(this string input, string pattern)
    {
      // TODO: [TESTS] (RegexExtensions.GetRegexMatch) Add tests

      return Regex.Match(input, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
    }
  }
}
