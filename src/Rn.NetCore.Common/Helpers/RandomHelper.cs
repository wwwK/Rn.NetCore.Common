using System;
using System.Linq;

namespace Rn.NetCore.Common.Helpers
{
  public interface IRandomHelper
  {
    string RandomString(int length);
    string RandomString(int length, string seed);
    int RandomInt(int min, int max);
    int RandomInt(int max);
    float RandomFloat(float min, float max);
    double RandomDouble(double min, double max);
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

    public int RandomInt(int min, int max)
    {
      // TODO: [TESTS] (RandomHelper.RandomInt) Add tests
      return _random.Next(min, max);
    }

    public int RandomInt(int max)
    {
      // TODO: [TESTS] (RandomHelper.RandomInt) Add tests
      return _random.Next(max);
    }

    public float RandomFloat(float min, float max)
    {
      // TODO: [TESTS] (RandomHelper.RandomFloat) Add tests
      var val = (_random.NextDouble() * (max - min) + min);
      return (float)val;
    }

    public double RandomDouble(double min, double max)
    {
      // TODO: [TESTS] (RandomHelper.RandomDouble) Add tests
      return (_random.NextDouble() * (max - min) + min);
    }
  }
}
