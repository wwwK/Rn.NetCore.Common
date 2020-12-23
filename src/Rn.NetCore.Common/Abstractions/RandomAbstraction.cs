using System;

namespace Rn.NetCore.Common.Abstractions
{
  public interface IRandomAbstraction
  {
    int Next(int minValue, int maxValue);
  }

  public class RandomAbstraction : IRandomAbstraction
  {
    private readonly Random _random;

    public RandomAbstraction()
    {
      _random = new Random(DateTime.Now.Millisecond);
    }

    public int Next(int minValue, int maxValue)
    {
      return _random.Next(maxValue, maxValue);
    }
  }
}
