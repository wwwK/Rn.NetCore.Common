using System;
using System.Linq;

namespace Rn.NetCore.Common.Helpers
{
  public interface IRandomHelper
  {
    string RandomString(int length);
    string RandomString(int length, string seed);
  }

  public class RandomHelper : IRandomHelper
  {
    public const string AlphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    public const string Alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string Numeric = "0123456789";

    private readonly Random _random;

    public RandomHelper()
    {
      // TODO: [TESTS] (RandomHelper.RandomHelper) Add tests
      _random = new Random(DateTime.Now.Millisecond);
    }

    public string RandomString(int length)
      => RandomString(length, AlphaNumeric);

    public string RandomString(int length, string seed)
    {
      // TODO: [TESTS] (RandomHelper.RandomString) Add tests
      return new string(Enumerable
        .Repeat(seed, length)
        .Select(s => s[_random.Next(s.Length)])
        .ToArray()
      );
    }
  }
}
