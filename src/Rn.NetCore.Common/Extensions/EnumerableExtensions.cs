using System;
using System.Collections.Generic;
using System.Linq;

namespace Rn.NetCore.Common.Extensions
{
  public static class EnumerableExtensions
  {
    public static T PickRandom<T>(this IEnumerable<T> source)
    {
      // TODO: [TESTS] (EnumerableExtensions.PickRandom) Add tests

      return source.PickRandom(1).Single();
    }

    public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
    {
      // TODO: [TESTS] (EnumerableExtensions.PickRandom) Add tests

      return source.Shuffle().Take(count);
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
      // TODO: [TESTS] (EnumerableExtensions.Shuffle) Add tests

      return source.OrderBy(x => Guid.NewGuid());
    }

    public static List<string> TrimAndLowerAll(this IEnumerable<string> source)
    {
      // TODO: [TESTS] (EnumerableExtensions.TrimAndLowerAll) Add tests

      return source.Select(entry => entry.LowerTrim()).ToList();
    }
  }
}
